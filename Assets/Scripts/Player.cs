using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // List of Animal State GameObject Sprites
    public List<GameObject> pickedUpItemList;

    // Held Item Number(Index)
    public int itemIndex;
    private int NONE_ITEM = 5;

    // Start is called before the first frame update
    void Start() {
        itemIndex = NONE_ITEM;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("u")) {
            deliverHarvest();
        }
    }

    // Player Collision Function
    public void OnTriggerStay2D(Collider2D other) {
     
        // Player Action Key is Pressed        
        if(Input.GetKeyDown("space") || Input.GetButtonDown("AButton")) {

            // Animal Interaction (Cow, Goat, Chicken)
            if (other.gameObject.tag == "Cow" || other.gameObject.tag == "Goat" || other.gameObject.tag == "Chicken") {

                if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.hungry && itemIndex == 3) {
                    other.gameObject.GetComponent<Animal>().feedFood();
                    disableItemSprite();
                    itemIndex = NONE_ITEM;
                }

                else if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.thirsty && itemIndex == 4) {
                   other.gameObject.GetComponent<Animal>().feedWater();
                    disableItemSprite();
                    itemIndex = NONE_ITEM;
                }

                else if (other.gameObject.GetComponent<Animal>().state == Animal.AnimalStates.readyToHarvest && itemIndex == NONE_ITEM) {
                    other.gameObject.GetComponent<Animal>().harvestAnimal();
                    itemIndex = other.gameObject.GetComponent<Animal>().getHarvestedProduct();
                    enableItemSprite(itemIndex);
                    
                }
            }

            // Food/Water Interaction
            else if((other.gameObject.tag == "Food" || other.gameObject.tag == "Water") && itemIndex == NONE_ITEM) {
                itemIndex = other.gameObject.GetComponent<FeedingItem>().getItemNumber();

                print("FEEDING INDEX: " + itemIndex);

                enableItemSprite(itemIndex);
            }

            // Put held itemm into the trash can
            else if(other.gameObject.tag == "TrashCan") {
                deliverHarvest();
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
        pickedUpItemList[item].SetActive(true);
        
    }

    // Function to Disable Item Sprite on Player
    private void disableItemSprite() {
        for (int i = 0; i < pickedUpItemList.Count; i++) {
            pickedUpItemList[i].SetActive(false);
        }
    }
}
