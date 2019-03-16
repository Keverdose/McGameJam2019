using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

    // Enum of possible Animal State
    public enum AnimalStates { neutral, hungry, thirsty, readyToHarvest, needHelp, respawning };

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



    // Start Function
    void Start() {
        hungryTimer  = 0;
        thirstyTimer = 0;
        harvestTimer = 0;
        respawnTimer = 0;
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
            // Enable Hungry Sprite/Animation
        }

        if (state == AnimalStates.neutral && thirstyTimer >= maxThirstyTimer) {
            changeState(AnimalStates.thirsty);
            // Enable Thirsty Sprite/Animation
        }

        if (state == AnimalStates.neutral && harvestTimer >= maxHarvestTimer) {
            changeState(AnimalStates.readyToHarvest);
            // Enable Ready to Harest Sprite/Animation
        }

        if (state == AnimalStates.respawning && respawnTimer >= maxRespawnTimer) {
            changeState(AnimalStates.neutral);
            // Enable GameObject and Sprite/Animation
        }

    }


    // Changes Current State to new defined State
    private void changeState(AnimalStates newState) {
        if(state != newState) {
            state = newState;
        }
    }


    // Feeds Animal Funtion
    public void feedAnimal() {
        hungryTimer = 0.0f;
        changeState(AnimalStates.neutral); 
        // Disable Animal Hungry Animation/Sprite
    }

    // Feed Animal Water 
    public void feedWater() {
        thirstyTimer = 0.0f;
        changeState(AnimalStates.neutral);
        // Disable Animal Thirsty Animation/Sprite
    }


    // Harvest Produce from Animal
    public void harvestAnimal() {
        harvestTimer = 0.0f;
        changeState(AnimalStates.neutral);
        // Disable Animal Harvest Animation/Sprite
    }


    // Animal being Attacked Fucntion (To be called Externally when events happen)
    public void attacked() {
        state = AnimalStates.needHelp;
        pastAnimalState = state;
        // Enable Animal Being attacked Sprites/Animations
    }

    public void respawnAnimal() {
        changeState(AnimalStates.respawning);
        // Disable Animal Sprite Entirely 
        // Disable Animal GameObject Until Respawned
    }

}
