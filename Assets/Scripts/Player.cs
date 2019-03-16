using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxFoodFeedingTime;
    public float maxWaterFeedingTime;
    public float maxHarvestTime;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Player Collision Function
    public void OnTriggerStay2D(Collider2D other) {
        
        
        // Player Action Key is Pressed        
        if(Input.GetKey("space")) {

            if (other.gameObject.tag == "Animal") {

                if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.hungry) {
                    timer += Time.deltaTime;

                    if (timer >= maxFoodFeedingTime) {
                        timer = 0.0f;
                        other.gameObject.GetComponent<Animal>().feedFood();
                    }
                }

                else if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.thirsty) {
                    timer += Time.deltaTime;

                    if (timer >= maxWaterFeedingTime) {
                        timer = 0.0f;
                        other.gameObject.GetComponent<Animal>().feedWater();
                    }
                }

                else if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.readyToHarvest) {
                    timer += Time.deltaTime;

                    if (timer >= maxHarvestTime) {
                        timer = 0.0f;
                        other.gameObject.GetComponent<Animal>().harvestAnimal();
                    }
                }
            }

        }
    }
}
