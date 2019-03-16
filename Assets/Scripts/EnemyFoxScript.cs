using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hunting;
    public Vector2 mapSize = new Vector2(2.0f, 1.0f);
    public Vector3 targetLoc;
    public float speed = 0.5f;
    public float huntingTime = 10.0f;
    public float huntingTimer;
    public GameObject target;
    public AudioSource sound;
    public bool soundHasPlayed;

    void Start()
    {
        soundHasPlayed = false;
        huntingTimer = 0.0f;
        hunting = false;
        transform.position = new Vector3(mapSize.x, mapSize.y, 0.0f);
        targetLoc = new Vector2(mapSize.x, -mapSize.y);
    }

    // Update is called once per frame
    void Update()
    {
        huntingTimer += Time.deltaTime;
        if (huntingTimer >= huntingTime)
        {
            hunting = true;
            if(!soundHasPlayed)
            {
                sound.Play();
                soundHasPlayed = true;
            }
            
        }

        if (!hunting)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetLoc, speed * Time.deltaTime);
            if (transform.position == targetLoc)
            {
                if (targetLoc == new Vector3(mapSize.x, -mapSize.y))
                {
                    targetLoc = new Vector3(-mapSize.x, -mapSize.y);
                }
                else if (targetLoc == new Vector3(-mapSize.x, -mapSize.y))
                {
                    targetLoc = new Vector3(-mapSize.x, mapSize.y);
                }
                else if (targetLoc == new Vector3(-mapSize.x, mapSize.y))
                {
                    targetLoc = new Vector3(mapSize.x, mapSize.y);
                }
                else if (targetLoc == new Vector3(mapSize.x, mapSize.y))
                {
                    targetLoc = new Vector3(mapSize.x, -mapSize.y);
                }

            }

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (transform.position == target.transform.position)
            {
                huntingTimer = 0.0f;
                hunting = false;
                soundHasPlayed = false;

                //soundHasPlayed = false;
                Debug.LogWarning("O NOO CHICKEN GOT ATTACKED!!!!!");
                //BEHAVOUR WHEN GOAT ATTACKED HERE
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("UwU YUU SAVED CHICKEN!!");
            huntingTimer = 0.0f;
            hunting = false;

        }
    }

}
