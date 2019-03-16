using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUFOScript : MonoBehaviour
{

    public bool hunting;
    public GameObject target;
    public Vector3 targetLocation;
    public Vector3 originalTargetLocation;
    public float ufoSpeed = 0.75f;
    public float ufoHeight = 0.4f;
    public Vector3 ufoOriginalLocation;
    public float ufoTime = 15.0f;
    public float ufoTimer;
    // Start is called before the first frame update
    void Start()
    {
        hunting = true;
        ufoTimer = 0.0f;
        ufoOriginalLocation = transform.position;
        originalTargetLocation = target.transform.position;
        targetLocation = target.transform.position;
        targetLocation.y += ufoHeight;
    }

    // Update is called once per frame
    void Update()
    {
        ufoTimer += Time.deltaTime;
        if(ufoTimer >= ufoTime)
        {
            hunting = true;
        }
        if(hunting)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, ufoSpeed * Time.deltaTime);
            if(transform.position == targetLocation)
            {
                if (Input.GetKey("space"))
                {
                    target.transform.position = Vector2.MoveTowards(target.transform.position, originalTargetLocation, 0.1f * Time.deltaTime);
                }
                else 
                {
                    target.transform.position = Vector2.MoveTowards(target.transform.position, transform.position, 0.1f * Time.deltaTime);
                }
                if (target.transform.position == originalTargetLocation)
                {
                    ufoTimer = 0.0f;
                    hunting = false;
                }
            }
        }
        else
        {
            print("DONE");
            
            transform.position = Vector2.MoveTowards(transform.position, ufoOriginalLocation, ufoSpeed * Time.deltaTime);
        }
    }
}
