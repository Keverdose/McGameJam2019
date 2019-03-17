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


    public Transform originalPosition;
    private int activeSpriteIdex;


    // Start Function
    void Start() {
        hungryTimer  = 0;
        thirstyTimer = 0;
        harvestTimer = 0;
        respawnTimer = 0;

        activeSpriteIdex = 9;

        state = AnimalStates.neutral;
        pastAnimalState = AnimalStates.neutral;

        originalPosition = this.gameObject.transform;
    }

    // Update Function
    void Update() {

        if(Input.GetKeyDown("o")) {
            animalDied();
        }

        // ================== Timer change based on State ================== //
        if (state == AnimalStates.neutral) {
            hungryTimer  += Time.deltaTime;
            thirstyTimer += Time.deltaTime;
            harvestTimer += Time.deltaTime;
        }

        if(state == AnimalStates.respawning && respawnTimer <= maxRespawnTimer) {
            respawnTimer += Time.deltaTime;
        }

        if (state == AnimalStates.respawning && respawnTimer >= maxRespawnTimer) {
            animalRespawn();

            print("IN HERE");

            state = pastAnimalState;
            pastAnimalState = AnimalStates.neutral;
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


    // Animal has died and set into a respawning state
    public void animalDied() {
        pastAnimalState = state;
        changeState(AnimalStates.respawning);
        // Disable Animal Sprite Entirely 
        // Disable Animal GameObject Until Respawned

        this.gameObject.transform.position = originalPosition.position;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        disableChildObjects();

    }

    // Respawns the Animal
    public void animalRespawn() {
        respawnTimer = 0.0f;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        enableChildObjects();
    }

    private void disableChildObjects() {
        for(int i = 0; i < animalStateSprites.Count; i++) {
            if(animalStateSprites[i].activeInHierarchy == true) {
                activeSpriteIdex = i;
                animalStateSprites[i].SetActive(false);   
            }
        }
    }

    private void enableChildObjects() {

        if (activeSpriteIdex != 9) {
            animalStateSprites[activeSpriteIdex].SetActive(true);
            activeSpriteIdex = 9;
        }
    }
}
