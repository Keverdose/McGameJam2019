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
    // Start is called before the first frame update
    void Start()
    {
        hunting = true;
        originalTargetLocation = target.transform.position;
        targetLocation = target.transform.position;
        targetLocation.y += ufoHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if(hunting)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, ufoSpeed * Time.deltaTime);
            if(transform.position == targetLocation)
            {
                if(Input.GetKeyDown("space"))
                {
                    print("GOOD STUFF");
                }
                else
                {
                    target.transform.position = Vector2.MoveTowards(target.transform.position, transform.position, 0.1f * Time.deltaTime);
                }
                
            }
        }
    }
}
