using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 3f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadFirstGame()
    {
        SceneManager.LoadScene("Game"); // for the name of the Scene in unity.
        FindObjectOfType<GameSession>().ResetGame(); //calling ResetGame by findoftype game session.
    }
    public void LoadGameOver()
    {
        StartCoroutine(pDeath());
        SceneManager.LoadScene("Game Over");
    }

    IEnumerator pDeath()
    { 
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
