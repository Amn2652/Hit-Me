using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    public TextMeshProUGUI tapCountText;
    public GameManager gameManager;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI CountDownText;
    public TextMeshProUGUI winOrLostText;
    public GameObject pauseMenu;
    public GameObject gamePanal;
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI levelText;
   
    
    

    void Update()
    {
        if (gameManager.timerEnded)
        {
            highscoreText.text = "Highscore: " + GameManager.highscore;
        }
        levelText.text = "Level " + gameManager.LevelNumber;
        timerText.text = gameManager.timerSecond.ToString();
        CountDownText.text = gameManager.countdownSecond.ToString();
        targetText.text = gameManager.target.ToString();

    }
    
    public void UpdateTextCount()
    {
        tapCountText.text = gameManager.tapCount.ToString();
        
    }

    public void UpdateWinOrLostText()
    {
        if (gameManager.hasWon)
        {
            winOrLostText.text = "You Win!";
        }
        else
        {
            winOrLostText.text = "You Lost!";
        }
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gamePanal.SetActive(true);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        gamePanal.SetActive(false);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gamePanal.SetActive(true);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void nextLevel()
    {
        if(gameManager.hasWon)
        {
            gameManager.IncreaseLevelNumber();
            SceneManager.LoadScene(1);
        }
        
    }
}