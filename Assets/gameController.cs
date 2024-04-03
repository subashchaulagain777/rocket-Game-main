using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public void ResetGame()
    {
        StartCoroutine(LoadThisLevel());
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadThisLevel()
    {
        yield return new WaitForSeconds(2f);
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }


    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2f);
        int nextlevl = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nextlevl+1);
        
    }
}
