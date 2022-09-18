using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//태어날때 PauseUI를 비활성화하고싶다.
//PauseButton을 누를 때 PauseUI를 활성화하고싶다.
public class GameManager : MonoBehaviour
{
    public GameObject PauseUI = null;
    public GameObject Info;
    public GameObject Help;
    public GameObject GameOverUI;
    public GameObject GameOverCamera;
    public GameObject GameClearUI;
    public GameObject GameClearCamera;
    public GameObject MainUI;   
    public GameObject MainCamera;
    public GameObject StartGame;
    public GameObject GameMode;
    public GameObject PlusScore;

    public static GameManager instance;
    public int secondsLeft = 300;

    private void Awake()
    {
        instance = this;

    }
    public void GameStart()
    {
        StartGame.SetActive(true);
        MainUI.SetActive(false);
        if (GameOverUI.activeSelf)
        {
            GameOverUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (PauseUI.activeSelf)
            {
                PauseUI.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PauseUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void OnClickInfoMenu()
    {
        Info.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnClickPauseMenu()
    {
        Info.SetActive(false);
        PauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnClickHelpMenu()
    {
        Info.SetActive(true);
        PauseUI.SetActive(false);
    }
    public void OnClickContinueButton()
    {
        Info.SetActive(false);
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClickQuitButton()
    {
        Application.Quit();
        Time.timeScale = 0;
    }
    public void OnClickStartButton()
    {
        StartGame.SetActive(false);
        MainUI.SetActive(true);
        Info.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnClickRestart()
    {
        GameStart();
    }


    

}
