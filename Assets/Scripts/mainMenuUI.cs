using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject setting;
    public GameObject highScore;
    public GameObject howToPlay;
    public TextMeshProUGUI highscoreText;
    private float highscore;
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public AudioClip buttonSound;
    public AudioSource audioSource;

    void Start()
    {
        mainMenu.SetActive(true);
        setting.SetActive(false);
        highScore.SetActive(false);
        howToPlay.SetActive(false);
        highscore = PlayerPrefs.GetFloat("Highscore");
        highscoreText.text = highscore.ToString();
        loadingScreen.SetActive(false); // Initially disable loading screen
    }

    public void PlayButton()
    {
        StartCoroutine(DelayAndLoadGame());
    }

    public IEnumerator DelayAndLoadGame()
    {
        loadingScreen.SetActive(true); // Enable loading screen
        yield return new WaitForSeconds(1); // Wait for a short delay (if needed)

        // Load the game scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);

        // Wait until the game scene is fully loaded
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }

    public void SettingButton()
    {
        setting.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void HighScoreButton()
    {
        highScore.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void HideToPlayButton()
    {
        howToPlay.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        setting.SetActive(false);
        highScore.SetActive(false);
        howToPlay.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void PlaySound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
