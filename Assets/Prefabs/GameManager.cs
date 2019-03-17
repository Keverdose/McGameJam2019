using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Harvest{Milk, Egg, Cheese};

public class GameManager : MonoBehaviour
{
    public static float score;
    public static float timeLeft = 10;
    public int pointsToWin = 30;
    public static int hearts;

    [SerializeField]
    public Text scoreText;
    public GameObject losingScreen;
    public GameObject winningScreen;
    public GameObject secretScreen;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore();
        pointsToWin = 30;
        hearts = 0;
        timeLeft = 180; // Changes to three minutes
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
        scoreText.gameObject.SetActive(false);
        if(score < pointsToWin)
        {
            StartCoroutine(LosingRoutine());
        }
        else if(score > pointsToWin)
        {
            StartCoroutine(WinningRoutine());
        }
    }

    IEnumerator LosingRoutine()
    {
        losingScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(0);
    }

    IEnumerator WinningRoutine()
    {
        winningScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(0);
    }


    public void UpdateScore() => scoreText.text = "Score: " + score.ToString();

}