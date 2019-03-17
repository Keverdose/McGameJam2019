using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Harvest{Milk, Egg, Cheese};

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public Player[] players;
    public Harvest[] harvest;
    public GameObject basket;
    public float score;
    public float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        harvest = new Harvest[3];
        harvest[0] = Harvest.Milk;
        harvest[1] = Harvest.Egg;
        harvest[2] = Harvest.Cheese;
        score = 0;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        if(score < 100)
        {
            //TODO: Change to losing scene.
            print("U lost");
        }else if(score > 100)
        {
            //TODO: Change to losing scene.
            print("WINNER WINNER CHICKEN DINNER oh wait MY CHIKCEN");
        }
    }
}