using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if(gameSession)
        gameSession.Destroy();
        SceneManager.LoadScene("Level");
    }

    public void LoadGameOver()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadStartMenu()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.Destroy();
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
