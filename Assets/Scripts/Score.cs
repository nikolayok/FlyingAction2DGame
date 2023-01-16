using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameObject scoreTextObject;
    private Text scoreText;
    private GameObject player;
    private Transform playerTransform;

    private bool isUpdatingScore = false;

    private int score = 0;

    private void Start()
    {
        GetPlayer();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        CancelInvoke();

        isUpdatingScore = false;
    }

    public void BeginUpdateScore()
    {
        if (!isUpdatingScore)
        {
            isUpdatingScore = true;
            InvokeRepeating("SetScore", 0f, 0.1f);
        }
    }

    public void GetScoreUI()
    {
        scoreTextObject = GameObject.FindGameObjectWithTag("ScoreText");
        scoreText = scoreTextObject.GetComponent<Text>();
    }

    private void GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    private void SetScore()
    {
        score = (int) playerTransform.position.x;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString() + "/20000";
    }

}
