using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    // loading the start view
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    // loading the game board view 
    public void LoadBoardScene(string mode)
    {
        GameSpecification.GameMode = mode;
        SceneManager.LoadScene(1);
    }

    // loading the result view
    public void LoadResultScene(string result)
    {
        GameSpecification.Result = result;
        SceneManager.LoadScene(2);

    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
