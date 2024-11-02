using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMove : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private float speed = 0.2f;

    private void Start()
    {
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go.name == "Player")
            {
                player = go.GetComponent<PlayerController>();
            }
        }
    }

    private void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * player.velocity.x, 0f);
    }
}
