using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Collider2D))]

public class Animal : MonoBehaviour
{

    // Enum of possible Animal State
    public enum AnimalStates { readyToHarvest, hungry, thirsty, needHelp, respawning, neutral };
    public enum HarvestProduct { Milk, Egg, Cheese };

    // List of Animal State GameObject Sprites
    public List<GameObject> animalStateSprites;

    // Current Animal State
    public AnimalStates state;
    public AnimalStates pastAnimalState;

    // Max Animal State Timers
    public float maxHungryTimer;
    public float maxThirstyTimer;
    public float maxHarvestTimer;
    public float maxRespawnTimer;

    // Animal State Timer 
    public float hungryTimer;
    public float thirstyTimer;
    public float harvestTimer;
    public float respawnTimer;

    // BoxCollider2D collider;


    // Start Function
    void Start() {
        hungryTimer  = 0;
        thirstyTimer = 0;
        harvestTimer = 0;
        respawnTimer = 0;

        state = AnimalStates.neutral;
        pastAnimalState = AnimalStates.neutral;
    }

    // Update Function
    void Update() {

        // ================== Timer change based on State ================== //
        if (state == AnimalStates.neutral) {
            hungryTimer  += Time.deltaTime;
            thirstyTimer += Time.deltaTime;
            harvestTimer += Time.deltaTime;
        }

        else if(state == AnimalStates.respawning) {
            respawnTimer -= Time.deltaTime;
        }

        // ============ Change of State based on Timer ============ // 

        if (state == AnimalStates.neutral && hungryTimer >= maxHungryTimer) {
            changeState(AnimalStates.hungry);
        }

        if (state == AnimalStates.neutral && thirstyTimer >= maxThirstyTimer) {
            changeState(AnimalStates.thirsty);
        }

        if (state == AnimalStates.neutral && harvestTimer >= maxHarvestTimer) {
            changeState(AnimalStates.readyToHarvest);
        }

        if (state == AnimalStates.respawning && respawnTimer >= maxRespawnTimer) {
            changeState(AnimalStates.neutral);
        }

    }


    // Returns an Integer of the harvested product
    public int getHarvestedProduct() {
        if (this.gameObject.tag == "Cow")
            return 0;
        if (this.gameObject.tag == "Chicken")
            return 1;
        if (this.gameObject.tag == "Goat")
            return 2;

        // Returns NONE_ITEM
        print("NOTHTING RETURNED FROM REQUEST");

        return 3;
    }


    // Changes Current State to new defined State
    private void changeState(AnimalStates newState) {
        if(state != newState) {
            state = newState;

            // Only change the Sprite if it is ReadyToHarvest, Hungry, Thirsty, or NeedHelp
            if ((int)state >= 0 && (int)state < animalStateSprites.Count) {
                animalStateSprites[(int)state].SetActive(true);
            }
        }
    }

    // Disables all State Sprites
    private void disableStateSprite() {
        for(int i = 0; i < animalStateSprites.Count; i++) {
            animalStateSprites[i].SetActive(false);
        }
    }


    // Feeds Animal Funtion
    public void feedFood() {
        hungryTimer = 0.0f;
        changeState(AnimalStates.neutral);
        disableStateSprite(); 
    }

    // Feed Animal Water 
    public void feedWater() {
        thirstyTimer = 0.0f;
        changeState(AnimalStates.neutral);
        disableStateSprite();
    }


    // Harvest Produce from Animal
    public void harvestAnimal() {
        harvestTimer = 0.0f;
        changeState(AnimalStates.neutral);
        disableStateSprite();
    }


    // Animal being Attacked Fucntion (To be called Externally when events happen)
    public void attacked() {
        state = AnimalStates.needHelp;
        pastAnimalState = state;
        animalStateSprites[(int)state].SetActive(true);
    }

    public void respawnAnimal() {
        changeState(AnimalStates.respawning);
        // Disable Animal Sprite Entirely 
        // Disable Animal GameObject Until Respawned
    }

}
