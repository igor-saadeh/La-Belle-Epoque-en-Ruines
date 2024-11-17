using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    private int score = 0;
    void Awake()
    {
        GameEvents.onCollect.AddListener(RaiseScore);
    }

    void RaiseScore()
    {
        score += 10;
        Debug.Log(score);
    }
}
