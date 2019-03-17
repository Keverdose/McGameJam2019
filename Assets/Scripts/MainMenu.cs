using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject theButton;

    public GameObject TitleScreen;
    public GameObject NextScreen;
    public GameObject StoryScreen;
    public GameObject TutorialScreen;


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
        index=1;
    }


    private void Update()
    {
        Debug.Log(index);
        if(index != 0)
        {
            if (index == 1) // story
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log(index + " Escape Down");
                    TutorialScreen.SetActive(true);
                    StoryScreen.SetActive(false);
                }
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    Debug.Log(index + " Escape Up");
                    index = 2;
                }
            }
            if (index == 2) // tutorial
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log(index + "Loading game...");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
               
            }
        }
    }
}
