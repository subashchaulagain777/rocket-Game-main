using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public void ResetGame()
    {
        StartCoroutine(Check());
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("level2");
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
