using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneController : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(ReloadGameRoutine());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    IEnumerator ReloadGameRoutine()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadSceneAsync(0);
    }

}
