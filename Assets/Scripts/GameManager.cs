using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int tapCount;
    public GamePlayUI gamePlayUI;
    public float timeLeft;
    public float timer;
    public bool timerEnded = false;
    public bool hasWon = false;
    public float target;
    public static float highscore;
    public float countdownTimer = 3.0f;
    public bool countdownEnded = false;
    public GameObject counttimerObj;
    public int countdownSecond;
    public int timerSecond;
    public AudioSource audioSource;
    public GameObject gameoverMenu;
    public GameObject gamepanal;
    public int LevelNumber;
    public int targetMultiplier = 5;
    public AudioClip duck_sound;
    public AudioClip win_sound;
    public AudioClip lose_sound;
    public AudioClip buttonSound;
    public Transform duckPosition;
    public GameObject duck;
    public GameObject gamePanel;

    // Start is called before the first frame update
    void Start()
    {
        LoadHighscore();
        audioSource.Play();
        timer = timeLeft;
        LevelNumber = PlayerPrefs.GetInt("LevelNumber", 1);
        SetTarget(LevelNumber);
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer -= Time.deltaTime;
        countdownSecond = Mathf.FloorToInt(countdownTimer) + 1;
        if (countdownTimer <= 0)
        {
            countdownEnded = true;
        }

        if (countdownEnded == true && timerEnded == false)
        {
            counttimerObj.SetActive(false);
            timer -= Time.deltaTime;
            timerSecond = Mathf.FloorToInt(timer) + 1;

            if (timer <= 0)
            {
                timer = 0;
                timerEnded = true;
                if (tapCount >= target)
                {
                    hasWon = true;
                    audioSource.PlayOneShot(win_sound);
                }
                else
                {
                    hasWon = false;
                    audioSource.PlayOneShot(lose_sound);
                }
                if (tapCount > highscore)
                {
                    highscore = tapCount;
                }
                SaveHighscore();
                gamePlayUI.UpdateWinOrLostText();
                OpenGameOverMenu();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (duck != null && duckPosition != null && gamePanel != null)
                {
                    // Define random positions within the specified range
                    float randomX = Random.Range(-500f, 500f);
                    float randomY = Random.Range(10f, 400f);

                    // Instantiate the duck at the random position
                    GameObject newDuck = Instantiate(duck, new Vector3(randomX, randomY, 0f), Quaternion.identity);
                    Rigidbody2D newDuckRigidbody = newDuck.GetComponent<Rigidbody2D>();

                    // Change gravity scale between 5 to 10 on both x and y axes
                    float gravityScaleX = Random.Range(-100f, 100f);
                    float gravityScaleY = Random.Range(-5f, -10f);

                    // Set the gravity scale for the x and y axes separately
                    newDuckRigidbody.gravityScale = gravityScaleX;
                    newDuckRigidbody.gravityScale = gravityScaleY;

                    // Set the gamePanel as the parent of the newDuck GameObject
                    newDuck.transform.SetParent(gamePanel.transform, false);
                }
                audioSource.PlayOneShot(duck_sound);
                tapCount++;
                gamePlayUI.UpdateTextCount();
            }
        }
    }

    public void OpenGameOverMenu()
    {
        gameoverMenu.SetActive(true);
        gamepanal.SetActive(false);
    }

    public void SaveHighscore()
    {
        PlayerPrefs.SetFloat("Highscore", highscore);
    }

    public void LoadHighscore()
    {
        highscore = PlayerPrefs.GetFloat("Highscore");
    }

    public void IncreaseLevelNumber()
    {
        LevelNumber++;
        PlayerPrefs.SetInt("LevelNumber", LevelNumber);
    }

    public void SetTarget(int levelNumber)
    {
        target = levelNumber * targetMultiplier;
    }

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
