using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject panel;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    public Transform playerTransform;

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.activeInHierarchy == false)
                openMenu();          
            else
                CloseMenu();
        }
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void openMenu()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }
}
