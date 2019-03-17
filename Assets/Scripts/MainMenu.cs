using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject theButton;

    public GameObject TitleString;
    public GameObject TitleScreen;
    public GameObject NextScreen;
    public GameObject StoryScreen;
    public GameObject TutorialScreen;
    public GameObject EnemyScreen;

    public AudioMixer audioMixer;

    private bool isReady = true;


    // H4cky way to keep track of which screen we're at
    // 0 : title, 1: story, 2: tutorial
    private int index = 0;

    public void Start()
    {
        index = 0;
        eventSystem.firstSelectedGameObject = theButton;
    }

    public void PlayGame()
    {
        NextScreen.SetActive(true);
        TitleScreen.SetActive(false);
        index = 1;
    }


    private void Update()
    {
        Debug.Log(index);
        if (index != 0)
        {
            if (index == 1) // story
            {
                StartCoroutine(LoadTutorialRoutine());
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("AButton"))
                {
                    TutorialScreen.SetActive(true);
                    StoryScreen.SetActive(false);
                }
                if (isReady && Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("AButton"))
                {
                    Debug.Log("Out of story" + index);
                    isReady = false;
                    index = 2;
                }
            }
            if (index == 2) // tutorial
            {
                //StartCoroutine(LoadEnemiesRoutine());
                if (Input.GetKeyDown(KeyCode.Escape) )//|| Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton"))
                {
                    Debug.Log("Showing enemy screen" + index);
                    EnemyScreen.SetActive(true);
                    TutorialScreen.SetActive(false);
                    TitleString.SetActive(false);
                    isReady = true;
                }
                if(isReady && Input.GetKeyUp(KeyCode.Escape) )//|| Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("AButton"))
                {
                    Debug.Log("Out of Tutorial" + index);
                    isReady = false;
                    index = 3;
                }
            }
            if(index == 3) // Enemies
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isReady = true;
                }
                if (isReady && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton"))
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    IEnumerator LoadTutorialRoutine()
    {
        yield return new WaitForSeconds(50);
        if (index == 1)
        {
            TutorialScreen.SetActive(true);
            StoryScreen.SetActive(false);
            index = 2;
            isReady = true;
        }

    }

    IEnumerator LoadEnemiesRoutine()
    {
        yield return new WaitForSeconds(10);
        if(index == 2)
        {
            StoryScreen.SetActive(true);
            index = 3;
            isReady = true;
        }
    }

    public void DoQuit()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }
}
