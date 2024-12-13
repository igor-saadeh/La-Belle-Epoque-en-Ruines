using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        //FindObjectOfType<AudioManager>().Play("Button");
        AudioManager.instance.Play("Button");
        AudioManager.instance.Stop("MenuTheme");
        AudioManager.instance.Play("Scene1Theme");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SettingsScreen()
    {
        //FindObjectOfType<AudioManager>().Play("Button");
        AudioManager.instance.Play("Button");
    }

    public void CreditScreen()
    {
        //FindObjectOfType<AudioManager>().Play("Button");
        AudioManager.instance.Play("Button");
    }

    public void QuitGame()
    {
        //FindObjectOfType<AudioManager>().Play("Button");
        AudioManager.instance.Play("Button");
        Application.Quit();
    }
}
