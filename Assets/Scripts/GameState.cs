using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private GameObject player;
    private Lives lives;

    private GameObject scoreObject;
    private Score score;

    private GameObject playerMovementObject;
    private PMovement pMovement;

    private GameObject followPlayerObject;
    private FollowPlayer followPlayer;

    private GameObject cloudsManagerObject;
    private CloudsManager cloudsManager;

    private GameObject uiObject;
    private UI ui;


    private void Start()
    {
        FindObjects();
        GetScripts();

        StopGame();
    }

    private void FindObjects()
    {
        uiObject = GameObject.FindGameObjectWithTag("UI");

        player = GameObject.FindGameObjectWithTag("Player");
        scoreObject = GameObject.FindGameObjectWithTag("Score");
        playerMovementObject = GameObject.FindGameObjectWithTag("PlayerMovement");
        followPlayerObject = GameObject.FindGameObjectWithTag("FollowPlayer");
        cloudsManagerObject = GameObject.FindGameObjectWithTag("CloudsManager");
    }

    private void GetScripts()
    {
        ui = uiObject.GetComponent<UI>();

        lives = player.GetComponent<Lives>();
        score = scoreObject.GetComponent<Score>();
        pMovement = playerMovementObject.GetComponent<PMovement>();
        followPlayer = followPlayerObject.GetComponent<FollowPlayer>();
        cloudsManager = cloudsManagerObject.GetComponent<CloudsManager>();
    }

    public void StartGame()
    {
        lives.ResetLives();
        score.BeginUpdateScore();

        pMovement.ResetMovement();
        followPlayer.StartFollow();

        pMovement.StartMovement();
        cloudsManager.StartSpawnObstacles();

        ui.CloseGameOverUI();
    }

    public void StopGame()
    {
        PauseGame();

        pMovement.ResetMovement();

        followPlayer.StopFollow();
        followPlayer.ResetFollow();

        lives.ResetLives();
        score.ResetScore();

        cloudsManager.StopSpawnObstacles();
        cloudsManager.DestroyClouds();

        ContinueGame();
    }   

    public void GameOver()
    {
        ui.OpenGameOverUI();
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
    }
}
