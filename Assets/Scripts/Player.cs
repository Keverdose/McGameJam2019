using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // List of Animal State GameObject Sprites
    public List<GameObject> pickedUpItemList;

    // Max Action Time
    public float maxFoodFeedingTime;
    public float maxWaterFeedingTime;
    public float maxHarvestTime;

    // Action Timer 
    public float timer;

    // Held Item Number(Index)
    public int itemIndex;
    private int NONE_ITEM = 3;

    // Start is called before the first frame update
    void Start() {
        itemIndex = NONE_ITEM;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("u")) {
            deliverHarvest();
        }
    }

    // Player Collision Function
    public void OnTriggerStay2D(Collider2D other) {
     
        // Player Action Key is Pressed        
        if(Input.GetKey("space")) {

            if (other.gameObject.tag == "Cow" || other.gameObject.tag == "Goat" || other.gameObject.tag == "Chicken") {

                if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.hungry) {
                    timer += Time.deltaTime;

                    if (timer >= maxFoodFeedingTime) {
                        other.gameObject.GetComponent<Animal>().feedFood();
                        timer = 0.0f;
                    }
                }

                else if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.thirsty) {
                    timer += Time.deltaTime;

                    if (timer >= maxWaterFeedingTime) {
                        other.gameObject.GetComponent<Animal>().feedWater();
                        timer = 0.0f;
                    }
                }

                else if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.readyToHarvest && itemIndex == NONE_ITEM) {
                    timer += Time.deltaTime;

                    if (timer >= maxHarvestTime) {
                        other.gameObject.GetComponent<Animal>().harvestAnimal();

                        itemIndex = other.gameObject.GetComponent<Animal>().getHarvestedProduct();

                        print("ANIMAL INDEX: " + itemIndex);

                        enableItemSprite(itemIndex);
                        timer = 0.0f;
                    }
                }
            }

        }
    }


    // Delivers the item and removes it from the player
    public void deliverHarvest() {
        disableItemSprite();
        itemIndex = NONE_ITEM;
    }

    // Function to Enable Item Sprite on Player
    private void enableItemSprite(int item) {
        if (itemIndex != 3) {
            pickedUpItemList[item].SetActive(true);
        }
    }

    // Function to Disable Item Sprite on Player
    private void disableItemSprite() {
        for (int i = 0; i < pickedUpItemList.Count; i++) {
            pickedUpItemList[i].SetActive(false);
        }
    }
}
