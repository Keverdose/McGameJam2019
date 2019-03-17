using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public AudioSource punchSound;

    public bool foxHunting;
    public Vector2 foxMapSize = new Vector2(2.0f, 1.0f);
    public Vector3 foxTargetLoc;
    public float foxSpeed = 0.5f;
    public float foxHuntingTime = 10.0f;
    public float foxHuntingTimer;
    public GameObject foxTarget;
    public AudioSource foxSound;
    public bool foxSoundHasPlayed;
    public bool chickenSaved;

    public Vector2 chupacabraMapSize = new Vector2(2.0f, 1.0f);
    public float chupacabraSpeed = 2.0f;
    public GameObject chupacabraTarget;
    public bool chupacabraHunting;
    public float chupacabraHuntingTime = 15.0f;
    public float chupacabraHuntingTimer;
    public Vector3 chupacabraOriginalLocation;
    public bool chupacabraHasRespawned;
    public AudioSource chupacabraSound;
    public bool chupacabraSoundHasPlayed;
    public bool chupacabraHuntingFailed = false;

    public bool ufoHunting;
    public GameObject ufoTarget;
    public Vector3 ufoTargetLocation;
    public Vector3 ufoOriginalTargetLocation;
    public float ufoSpeed = 0.75f;
    public float ufoHeight = 0.4f;
    public Vector3 ufoOriginalLocation;
    public float ufoTime = 15.0f;
    public float ufoTimer;
    public AudioSource ufoSound;
    public bool ufoSoundHasPlayed;
    public GameObject ufoZone;
    public bool playerColliding;
    // Start is called before the first frame update
    void Start() {
        if (gameObject.CompareTag("fox")) {
            foxSoundHasPlayed = false;
            foxHuntingTimer = 0.0f;
            foxHunting = false;
            chickenSaved = false;
            transform.position = new Vector3(foxMapSize.x, foxMapSize.y, 0.0f);
            foxTargetLoc = new Vector2(foxMapSize.x, -foxMapSize.y);
        }

        else if (gameObject.CompareTag("chupacabra")) {
            this.GetComponent<SpriteRenderer>().enabled = false;
            chupacabraSoundHasPlayed = false;
            chupacabraHasRespawned = true;
            chupacabraHunting = false;
            chupacabraHuntingTimer = 0.0f;
            chupacabraOriginalLocation = transform.position;
            chupacabraRespawn();
        }

        else if (gameObject.CompareTag("ufo")) {
            ufoSoundHasPlayed = false;
            ufoHunting = false;
            ufoTimer = 0.0f;
            ufoOriginalLocation = transform.position;
            ufoOriginalTargetLocation = ufoTarget.transform.position;
            ufoTargetLocation = ufoTarget.transform.position;
            ufoTargetLocation.y += ufoHeight;
        }

    }

    // Update is called once per frame
    void Update() {
        switch (tag) {
            case "ufo":
                ufoDo();
                break;
            case "fox":
                foxDo();
                break;
            case "chupacabra":
                chupacabraDo();
                break;
            default:
                break;
        }



    }

    public void ufoDo() {
        if (!(ufoTarget.GetComponent<Animal>().state == Animal.AnimalStates.respawning) && !ufoHunting) {
            ufoTimer += Time.deltaTime;
        }

        if (ufoTimer >= ufoTime && !(ufoTarget.GetComponent<Animal>().state == Animal.AnimalStates.respawning)) {
            ufoHunting = true;
        }
        if (ufoHunting) {
            if (!ufoSoundHasPlayed) {
                ufoSound.Play();
                ufoSoundHasPlayed = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, ufoTargetLocation, ufoSpeed * Time.deltaTime);
            if (transform.position == ufoTargetLocation) {


                if ((Input.GetKey("space") || Input.GetButton("AButton") || Input.GetButton("AButton2")) && ufoZone.GetComponent<ufoZoneScript>().canSave) {
                    ufoTarget.transform.position = Vector2.MoveTowards(ufoTarget.transform.position, ufoOriginalTargetLocation, 0.1f * Time.deltaTime);
                }
                else {
                    ufoTarget.transform.position = Vector2.MoveTowards(ufoTarget.transform.position, transform.position, 0.1f * Time.deltaTime);
                    if (!(ufoTarget.GetComponent<Animal>().state == Animal.AnimalStates.needHelp)) {
                        ufoTarget.GetComponent<Animal>().attacked();
                    }

                }
                if (ufoTarget.transform.position == ufoOriginalTargetLocation) {
                    ufoTimer = 0.0f;
                    ufoHunting = false;
                    ufoSoundHasPlayed = false;
                    ufoTarget.GetComponent<Animal>().animalSaved();

                }
                if (ufoTarget.transform.position == transform.position) {
                    ufoTarget.GetComponent<Animal>().originalPosition.position = ufoOriginalTargetLocation;
                    ufoTarget.GetComponent<Animal>().animalDied();
                    ufoTimer = 0.0f;
                    ufoHunting = false;
                    ufoSoundHasPlayed = false;
                }
            }
        }
        else {

            transform.position = Vector2.MoveTowards(transform.position, ufoOriginalLocation, ufoSpeed * Time.deltaTime);
        }
    }



    void chupacabraDo() {
        if (!(chupacabraTarget.GetComponent<Animal>().state == Animal.AnimalStates.respawning)) {
            chupacabraHuntingTimer += Time.deltaTime;
        }

        if (chupacabraHuntingTimer >= chupacabraHuntingTime && !(chupacabraTarget.GetComponent<Animal>().state == Animal.AnimalStates.respawning)) {
            chupacabraHunting = true;
            if (!chupacabraSoundHasPlayed) {
                chupacabraSound.Play();
                chupacabraSoundHasPlayed = true;
            }
        }
        if (chupacabraHuntingTimer >= (chupacabraHuntingTime - 1.0f))
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
        /*else
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }*/
        if (chupacabraHuntingTimer >= (chupacabraHuntingTime - 5.0f)) {

            if (!(chupacabraTarget.GetComponent<Animal>().state == Animal.AnimalStates.needHelp)) {
                chupacabraTarget.GetComponent<Animal>().attacked();
            };
        }

        if (chupacabraHunting) {
            transform.position = Vector2.MoveTowards(transform.position, chupacabraTarget.transform.position, chupacabraSpeed * Time.deltaTime);

            // Disable the BoxCollider
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (transform.position == chupacabraTarget.transform.position) {
                chupacabraHuntingTimer = 0.0f;
                chupacabraHunting = false;
                chupacabraHasRespawned = false;
                chupacabraSoundHasPlayed = false;
                Debug.LogWarning("O NOO GOAT GOT ATTACKED!!!!!");


                chupacabraTarget.GetComponent<Animal>().animalDied();
                //BEHAVOUR WHEN GOAT ATTACKED HERE

            }

            else {
                // SAVED GOAT 
                chupacabraTarget.GetComponent<Animal>().animalSaved();

                // Enable the BoxCollider
                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, chupacabraOriginalLocation, chupacabraSpeed * Time.deltaTime);
            if (!chupacabraHasRespawned && chupacabraOriginalLocation == transform.position) {
                this.GetComponent<SpriteRenderer>().enabled = false;
                chupacabraRespawn();
            }
        }
    }
    public void chupacabraRespawn() {
        int pos = Mathf.RoundToInt(Random.Range(1.0f, 4.0f));
        print(pos);
        Vector3 newLoc = new Vector3(chupacabraMapSize.x, chupacabraMapSize.y, 0.0f);
        if (pos == 1) {
            newLoc = new Vector3(chupacabraMapSize.x, Random.Range(-chupacabraMapSize.y, chupacabraMapSize.y), 0.0f);
        }
        else if (pos == 2) {
            newLoc = new Vector3(Random.Range(-chupacabraMapSize.x, chupacabraMapSize.x), chupacabraMapSize.y, 0.0f);
        }
        else if (pos == 3) {
            newLoc = new Vector3(-chupacabraMapSize.x, Random.Range(-chupacabraMapSize.y, chupacabraMapSize.y), 0.0f);
        }
        else if (pos == 4) {
            newLoc = new Vector3(Random.Range(-chupacabraMapSize.x, chupacabraMapSize.x), -chupacabraMapSize.y, 0.0f);
        }
        transform.position = newLoc;
        chupacabraOriginalLocation = transform.position;

        chupacabraHasRespawned = true;
    }

    void foxDo() {
        // Increase Time Until Hunt
        if (!(foxTarget.GetComponent<Animal>().state == Animal.AnimalStates.respawning)) {
            foxHuntingTimer += Time.deltaTime;
        }

        if (chickenSaved) {

            foxHuntingTimer = 0.0f;

            // CHICKENHAS BEEN SAVED
            foxTarget.GetComponent<Animal>().animalSaved();

            // Disable the BoxCollider
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            chickenSaved = false;
            foxHunting = false;


        }

        // Switch to Hunting Mode when timer is up
        if (foxHuntingTimer >= foxHuntingTime && !foxHunting && (foxTarget.GetComponent<Animal>().state != Animal.AnimalStates.respawning) && (foxTarget.GetComponent<Animal>().state != Animal.AnimalStates.needHelp)) {
            foxHunting = true;

            print("HERE");
            foxTarget.GetComponent<Animal>().attacked();

            if (!foxSoundHasPlayed) {
                foxSound.Play();
                foxSoundHasPlayed = true;
            }

        }
        /*if (foxHuntingTimer >= (foxHuntingTime - 5.0f))
        {

            if (!(foxTarget.GetComponent<Animal>().state == Animal.AnimalStates.needHelp))
            {
                foxTarget.GetComponent<Animal>().attacked();
            };
        }*/


        // IF it is not hunting, ROAM
        if (!foxHunting) {
            transform.position = Vector2.MoveTowards(transform.position, foxTargetLoc, foxSpeed * Time.deltaTime);
            if (transform.position == foxTargetLoc) {
                if (foxTargetLoc == new Vector3(foxMapSize.x, -foxMapSize.y)) {
                    foxTargetLoc = new Vector3(-foxMapSize.x, -foxMapSize.y);
                }
                else if (foxTargetLoc == new Vector3(-foxMapSize.x, -foxMapSize.y)) {
                    foxTargetLoc = new Vector3(-foxMapSize.x, foxMapSize.y);
                }
                else if (foxTargetLoc == new Vector3(-foxMapSize.x, foxMapSize.y)) {
                    foxTargetLoc = new Vector3(foxMapSize.x, foxMapSize.y);
                }
                else if (foxTargetLoc == new Vector3(foxMapSize.x, foxMapSize.y)) {
                    foxTargetLoc = new Vector3(foxMapSize.x, -foxMapSize.y);
                }

            }

        }

        // HUNTING MODE
        else {
            // Enable the BoxCollider
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;

            transform.position = Vector2.MoveTowards(transform.position, foxTarget.transform.position, foxSpeed * Time.deltaTime);
            if (transform.position == foxTarget.transform.position) {

                foxHuntingTimer = 0.0f;
                foxHunting = false;
                foxSoundHasPlayed = false;

                //soundHasPlayed = false;
                Debug.LogWarning("O NOO CHICKEN GOT ATTACKED!!!!!");
                //BEHAVOUR WHEN GOAT ATTACKED HERE
                foxTarget.GetComponent<Animal>().animalDied();

                // Disable the BoxCollider
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (this.gameObject.CompareTag("chupacabra")) {
            if (collision.gameObject.CompareTag("Player")) {
                Debug.LogWarning("UwU YUU SAVED GOAT!!");
                chupacabraHuntingTimer = 0.0f;
                chupacabraHunting = false;
                chupacabraHasRespawned = false;
                chupacabraSoundHasPlayed = false;
                punchSound.Play();
                // chupacabraTarget.GetComponent<Animal>().animalSaved();
            }
        }
        if (this.gameObject.CompareTag("fox")) {
            if (collision.gameObject.CompareTag("Player")) {
                Debug.LogWarning("UwU YUU SAVED CHICKEN!!");
                foxHuntingTimer = 0.0f;
                foxHunting = false;
                chickenSaved = true;
                foxSoundHasPlayed = false;
                punchSound.Play();

            }
        }

    }
}


