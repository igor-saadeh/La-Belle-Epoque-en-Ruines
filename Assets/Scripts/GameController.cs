using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    private PauseMenu pauseMenu;

    private int score = 0;
    void Awake()
    {
        GameEvents.onCollect.AddListener(RaiseScore);
        GameObject[] root = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject go in root)
        {
            if (go.name == "Canvas")
            {
                pauseMenu = go.GetComponent<PauseMenu>();
                break;
            }
        }
    }

    void RaiseScore()
    {
        score += 10;
        //Debug.Log(score);
        pauseMenu.UpdateScore(score);
    }
}
