using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Harvest{Milk, Egg, Cheese};

public class GameManager : MonoBehaviour
{
    public static float score;
    public static float timeLeft = 45;
    public int pointsToWin = 30;
    public static int hearts;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        hearts = 0;
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
        if(score < pointsToWin)
        {
            //TODO: Change to losing scene.
            print("U lost, score: " + score);
        }else if(score > pointsToWin)
        {
            //TODO: Change to losing scene.
            print("WINNER WINNER CHICKEN DINNER oh wait MY CHIKCEN");
        }
    }
}