using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void quitApplication()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene("Essential", LoadSceneMode.Single);
        SceneManager.LoadScene("Main Gameplay", LoadSceneMode.Additive);
        
    }
}
