﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject theButton;

    public GameObject TitleScreen;
    public GameObject NextScreen;
    public GameObject StoryScreen;
    public GameObject TutorialScreen;

    public AudioMixer audioMixer;


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
                if (Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("AButton"))
                {
                    Debug.Log("ButtonUp works");
                    index = 2;
                }
            }
            if (index == 2) // tutorial
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton"))
                {
                    Debug.Log(index + "Loading game...");
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
