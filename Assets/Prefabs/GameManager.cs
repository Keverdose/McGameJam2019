using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum Harvest{Milk, Egg, Cheese};

public class GameManager : MonoBehaviour
{
    public static float score;
    public static float timeLeft = 45;
    public int pointsToWin = 30;
    public static int hearts;

    [SerializeField]
    public Text scoreText;
    private int m_Score;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore();
        hearts = 0;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GameOver();
        }
        UpdateScore();
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

    public void UpdateScore() => scoreText.text = score.ToString();
}