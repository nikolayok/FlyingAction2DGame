using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    private GameObject livesObject;
    private Text livesText;

    private int maximumLives = 5;
    private int lives = 5;
    private const float timeToGetDamage = 1f;

    private bool isShockedAfterHit = false;

    private bool isColliding = false;

    private GameObject gameStateObject;
    private GameState gameState;


    private void Start()
    {

        gameStateObject = GameObject.FindGameObjectWithTag("GameState");
        gameState = gameStateObject.GetComponent<GameState>();

        livesObject = GameObject.FindGameObjectWithTag("LivesText");
        livesText = livesObject.GetComponent<Text>();

        lives = maximumLives;
    }

    public void ResetLives()
    {
        lives = maximumLives;
        isShockedAfterHit = false;

        StopGettingDamage();
        CancelInvoke();
        UpdateLivesUI();
    }

    private void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    private void DecreaseLife()
    {
        --lives;
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameState.StopGame();
        gameState.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            StartGettingDamage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            StopGettingDamage();
        }
    }

    private void StartGettingDamage()
    {
        isColliding = true;

        if (!isShockedAfterHit)
        {
            DecreaseLife();
            isShockedAfterHit = true;

            Invoke("StopShockAfterHit", timeToGetDamage);
        }
    }

    private void StopGettingDamage()
    {
        isColliding = false;
    }

    private void StopShockAfterHit()
    {
        isShockedAfterHit = false;

        if (isColliding)
        {
            StartGettingDamage();
        }
    }    
}
