using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    private GameObject gameoverMenu;
    private GameObject scoreText;

    private void Awake()
    {
        GameEvents.onGameover.AddListener(ShowGameover);
    }

    private void Start()
    {
        pauseMenu = transform.Find("PauseMenu").gameObject;
        gameoverMenu = transform.Find("GameoverMenu").gameObject;
        scoreText = transform.Find("Score").gameObject.transform.Find("ScoreText").gameObject;
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.GetComponent<TMP_Text>().text = "SCORE: " + score.ToString();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);      
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowGameover()
    {
            Time.timeScale = 0f;
            gameoverMenu.SetActive(true);
    }
}
