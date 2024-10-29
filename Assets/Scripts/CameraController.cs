using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CameraController : MonoBehaviour
{
    public PlayerController playerController; // temporario
    private float speed; // 7?
    [SerializeField]
    private float startSpeed = 7f;
    [SerializeField]
    private float yOffset = 4f;
    [SerializeField]
    private float xOffset = 1f;

    private void Start()
    {   
        speed=startSpeed;
        transform.position = new Vector3(playerController.transform.position.x + xOffset, playerController.transform.position.y + yOffset, transform.position.z); //prototipo
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, playerController.transform.position.y + yOffset, transform.position.z);

        if (transform.position.x - playerController.transform.position.x <= 1)
        { speed = 8f; }
        else 
        { speed = startSpeed; }

        isPlayerOffScreen();
    }

    private void isPlayerOffScreen()
    {
        Camera camera = this.GetComponent<Camera>();
        Vector2 screenPosition = camera.WorldToScreenPoint(playerController.transform.position);
        float playerMaxLimit = -50f;

        if (screenPosition.x < playerMaxLimit)
        { Debug.Log("Player morreu"); }
    }
}

/*
 * Camera se movimente no eixo X numa velocidade fixa (5f)
 * Camera deve seguir o movimento do personagem em y
 * 
 * 
*/