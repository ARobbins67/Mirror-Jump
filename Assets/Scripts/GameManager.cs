using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject LeftPlayer;
    [SerializeField] GameObject RightPlayer;
    [SerializeField] MenuController Menu;

    private PlayerController leftController;
    private PlayerController rightController;
    private StarManager starMan;
    static int sceneCount;
    static int currentSceneIndex = 0;
    private bool bIsPaused = false;

    private void Awake()
    {
        leftController = LeftPlayer.GetComponent<PlayerController>();
        rightController = RightPlayer.GetComponent<PlayerController>();
        starMan = FindObjectOfType<StarManager>();
    }

    private void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene: " + currentSceneIndex);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetPauseState();
        }
    }

    public void SetPauseState()
    {
        bIsPaused = !bIsPaused;
        if (bIsPaused) { Time.timeScale = 0; }
        else { Time.timeScale = 1;}
        Menu.SetEscapeMenu(bIsPaused);
    }

    public void EndLevel()
    {
        Time.timeScale = 0;
        Debug.Log("Checking end level...");
        // Game won
        if (currentSceneIndex == sceneCount - 1)
        {
            Menu.SetWinGame(true);
            Debug.Log("Game Won!");
            return;
        }
        // Level won
        else if (currentSceneIndex < sceneCount-1)
        {
            Debug.Log("Level Won!");
            Menu.SetWinLevel(true, "Level " + currentSceneIndex + " Done!");
            return;
        }
        throw new NullReferenceException("Neither game nor level was done.");
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        if(currentSceneIndex < sceneCount-1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
            Debug.Log("Loaded scene " + (currentSceneIndex + 1).ToString());
            Time.timeScale = 1;
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // reset starlist
            starMan.ResetStarList();
        }
        else if (currentSceneIndex == sceneCount-1)
        {
            Menu.gameObject.SetActive(true);
        }
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}