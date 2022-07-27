using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public Text scoreText, highScoreText;
    public GameObject titleScreen;

    private int highScore;
    public int curScore;

    public bool hasStarted = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !hasStarted)
        {
            hasStarted = true;
            titleScreen.SetActive(false);
            AudioManager.instance.PlaySFX(2);
        }

        scoreText.text = curScore.ToString();
        highScoreText.text = highScore.ToString();
    }

    public void ScoreUpdate()
    {
        curScore++;

        AudioManager.instance.PlaySFX(0);

        if(curScore >= highScore)
        {
            highScore = curScore;
        }
    }
}
