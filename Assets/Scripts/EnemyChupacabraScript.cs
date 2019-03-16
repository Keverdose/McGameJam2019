using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChupacabraScript : MonoBehaviour
{
    public float mapSize = 2.0f;
    public float speed = 2.0f;
    public GameObject target;
    public bool hunting;
    public float huntingTime = 15.0f;
    public float huntingTimer;
    public Vector3 originalLocation;
    public bool hasRespawned;
    public AudioSource sound;
    public bool soundHasPlayed;
    // Start is called before the first frame update
    void Start()
    {
        soundHasPlayed = false;
        hasRespawned = true;
        hunting = false;
        huntingTimer = 0.0f;
        originalLocation = transform.position;
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        huntingTimer += Time.deltaTime;
        if(huntingTimer >= huntingTime)
        {
            hunting = true;
            if(!soundHasPlayed)
            {
                sound.Play();
                soundHasPlayed = true;
            }
        }

        if(hunting)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if(transform.position == target.transform.position)
            {
                huntingTimer = 0.0f;
                hunting = false;
                hasRespawned = false;
                soundHasPlayed = false;
                Debug.LogWarning("O NOO GOAT GOT ATTACKED!!!!!");
                //BEHAVOUR WHEN GOAT ATTACKED HERE

            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, originalLocation, speed * Time.deltaTime);
            if(!hasRespawned && originalLocation == transform.position)
            {
                Respawn();
            }   
        }
    }

    public void Respawn()
    {
        int pos = Mathf.RoundToInt(Random.Range(1.0f, 4.0f));
        print(pos);
        Vector3 newLoc = new Vector3(mapSize, mapSize, 0.0f);
        if (pos == 1)
        {
            newLoc = new Vector3(mapSize, Random.Range(-mapSize, mapSize), 0.0f);
        }
        else if (pos == 2)
        {
            newLoc = new Vector3(Random.Range(-mapSize, mapSize), mapSize, 0.0f);
        }
        else if(pos == 3)
        {
            newLoc = new Vector3(-mapSize, Random.Range(-mapSize, mapSize), 0.0f);
        }
        else if (pos == 4)
        {
            newLoc = new Vector3(Random.Range(-mapSize, mapSize), -mapSize, 0.0f);
        }
        transform.position = newLoc;
        originalLocation = transform.position;

        hasRespawned = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("UwU YUU SAVED GOAT!!");
            huntingTimer = 0.0f;
            hunting = false;
            hasRespawned = false;
        }
    }
}
