using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool foxHunting;
    public Vector2 foxMapSize = new Vector2(2.0f, 1.0f);
    public Vector3 foxTargetLoc;
    public float foxSpeed = 0.5f;
    public float foxHuntingTime = 10.0f;
    public float foxHuntingTimer;
    public GameObject foxTarget;
    public AudioSource foxSound;
    public bool foxSoundHasPlayed;

    public float chupacabraMapSize = 2.0f;
    public float chupacabraSpeed = 2.0f;
    public GameObject chupacabraTarget;
    public bool chupacabraHunting;
    public float chupacabraHuntingTime = 15.0f;
    public float chupacabraHuntingTimer;
    public Vector3 chupacabraOriginalLocation;
    public bool chupacabraHasRespawned;
    public AudioSource chupacabraSound;
    public bool chupacabraSoundHasPlayed;

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
    // Start is called before the first frame update
    void Start()
    {
        foxSoundHasPlayed = false;
        foxHuntingTimer = 0.0f;
        foxHunting = false;
        transform.position = new Vector3(foxMapSize.x, foxMapSize.y, 0.0f);
        foxTargetLoc = new Vector2(foxMapSize.x, -foxMapSize.y);

        chupacabraSoundHasPlayed = false;
        chupacabraHasRespawned = true;
        chupacabraHunting = false;
        chupacabraHuntingTimer = 0.0f;
        chupacabraOriginalLocation = transform.position;
        chupacabraRespawn();

        ufoSoundHasPlayed = false;
        ufoHunting = true;
        ufoTimer = 0.0f;
        ufoOriginalLocation = transform.position;
        ufoOriginalTargetLocation = ufoTarget.transform.position;
        ufoTargetLocation = ufoTarget.transform.position;
        ufoTargetLocation.y += ufoHeight;
    }

    // Update is called once per frame
    void Update()
    {
        ufoDo();
        foxDo();
        chupacabraDo();
    }

    public void ufoDo()
    {
        ufoTimer += Time.deltaTime;
        if (ufoTimer >= ufoTime)
        {
            ufoHunting = true;
        }
        if (ufoHunting)
        {
            if (!ufoSoundHasPlayed)
            {
                ufoSound.Play();
                ufoSoundHasPlayed = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, ufoTargetLocation, ufoSpeed * Time.deltaTime);
            if (transform.position == ufoTargetLocation)
            {

                if (Input.GetKey("space"))
                {
                    ufoTarget.transform.position = Vector2.MoveTowards(ufoTarget.transform.position, ufoOriginalTargetLocation, 0.1f * Time.deltaTime);
                }
                else
                {
                    ufoTarget.transform.position = Vector2.MoveTowards(ufoTarget.transform.position, transform.position, 0.1f * Time.deltaTime);
                }
                if (ufoTarget.transform.position == ufoOriginalTargetLocation)
                {
                    ufoTimer = 0.0f;
                    ufoHunting = false;
                    ufoSoundHasPlayed = false;
                }
            }
        }
        else
        {
            print("DONE");

            transform.position = Vector2.MoveTowards(transform.position, ufoOriginalLocation, ufoSpeed * Time.deltaTime);
        }
    }

    void chupacabraDo()
    {
        chupacabraHuntingTimer += Time.deltaTime;
        if (chupacabraHuntingTimer >= chupacabraHuntingTime)
        {
            chupacabraHunting = true;
            if (!chupacabraSoundHasPlayed)
            {
                chupacabraSound.Play();
                chupacabraSoundHasPlayed = true;
            }
        }

        if (chupacabraHunting)
        {
            transform.position = Vector2.MoveTowards(transform.position, chupacabraTarget.transform.position, chupacabraSpeed * Time.deltaTime);
            if (transform.position == chupacabraTarget.transform.position)
            {
                chupacabraHuntingTimer = 0.0f;
                chupacabraHunting = false;
                chupacabraHasRespawned = false;
                chupacabraSoundHasPlayed = false;
                Debug.LogWarning("O NOO GOAT GOT ATTACKED!!!!!");
                //BEHAVOUR WHEN GOAT ATTACKED HERE

            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, chupacabraOriginalLocation, chupacabraSpeed * Time.deltaTime);
            if (!chupacabraHasRespawned && chupacabraOriginalLocation == transform.position)
            {
                chupacabraRespawn();
            }
        }
    }
    public void chupacabraRespawn()
    {
        int pos = Mathf.RoundToInt(Random.Range(1.0f, 4.0f));
        print(pos);
        Vector3 newLoc = new Vector3(chupacabraMapSize, chupacabraMapSize, 0.0f);
        if (pos == 1)
        {
            newLoc = new Vector3(chupacabraMapSize, Random.Range(-chupacabraMapSize, chupacabraMapSize), 0.0f);
        }
        else if (pos == 2)
        {
            newLoc = new Vector3(Random.Range(-chupacabraMapSize, chupacabraMapSize), chupacabraMapSize, 0.0f);
        }
        else if (pos == 3)
        {
            newLoc = new Vector3(-chupacabraMapSize, Random.Range(-chupacabraMapSize, chupacabraMapSize), 0.0f);
        }
        else if (pos == 4)
        {
            newLoc = new Vector3(Random.Range(-chupacabraMapSize, chupacabraMapSize), -chupacabraMapSize, 0.0f);
        }
        transform.position = newLoc;
        chupacabraOriginalLocation = transform.position;

        chupacabraHasRespawned = true;
    }

    void foxDo()
    {
        foxHuntingTimer += Time.deltaTime;
        if (foxHuntingTimer >= foxHuntingTime)
        {
            foxHunting = true;
            if (!foxSoundHasPlayed)
            {
                foxSound.Play();
                foxSoundHasPlayed = true;
            }

        }

        if (!foxHunting)
        {
            transform.position = Vector2.MoveTowards(transform.position, foxTargetLoc, foxSpeed * Time.deltaTime);
            if (transform.position == foxTargetLoc)
            {
                if (foxTargetLoc == new Vector3(foxMapSize.x, -foxMapSize.y))
                {
                    foxTargetLoc = new Vector3(-foxMapSize.x, -foxMapSize.y);
                }
                else if (foxTargetLoc == new Vector3(-foxMapSize.x, -foxMapSize.y))
                {
                    foxTargetLoc = new Vector3(-foxMapSize.x, foxMapSize.y);
                }
                else if (foxTargetLoc == new Vector3(-foxMapSize.x, foxMapSize.y))
                {
                    foxTargetLoc = new Vector3(foxMapSize.x, foxMapSize.y);
                }
                else if (foxTargetLoc == new Vector3(foxMapSize.x, foxMapSize.y))
                {
                    foxTargetLoc = new Vector3(foxMapSize.x, -foxMapSize.y);
                }

            }

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, foxTarget.transform.position, foxSpeed * Time.deltaTime);
            if (transform.position == foxTarget.transform.position)
            {
                foxHuntingTimer = 0.0f;
                foxHunting = false;
                foxSoundHasPlayed = false;

                //soundHasPlayed = false;
                Debug.LogWarning("O NOO CHICKEN GOT ATTACKED!!!!!");
                //BEHAVOUR WHEN GOAT ATTACKED HERE
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.gameObject.CompareTag("Chupacabra"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.LogWarning("UwU YUU SAVED GOAT!!");
                chupacabraHuntingTimer = 0.0f;
                chupacabraHunting = false;
                chupacabraHasRespawned = false;
            }
        }
        if(this.gameObject.CompareTag("Fox"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.LogWarning("UwU YUU SAVED CHICKEN!!");
                foxHuntingTimer = 0.0f;
                foxHunting = false;

            }
        }
        
    }
}


