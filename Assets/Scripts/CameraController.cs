using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class CameraController : MonoBehaviour
{
    public PlayerController playerController; // temporario
    private float speed = 8f;
    [SerializeField]
    private float startSpeed = 6f;
    [SerializeField]
    private float yOffset = 4f;
    [SerializeField]
    private float xOffset = 1f;

    private void Start()
    {
        speed = startSpeed;
        transform.position = new Vector3(playerController.transform.position.x + xOffset, playerController.transform.position.y + yOffset, transform.position.z); //prototipo
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, playerController.transform.position.y + yOffset, transform.position.z);
        isPlayerOffScreen();
    }

    private void isPlayerOffScreen()
    {
        Camera camera = GetComponent<Camera>();
        Vector2 screenPosition = camera.WorldToScreenPoint(playerController.transform.position);
        float leftLimit = -50f;

        if (screenPosition.x < leftLimit)
        {
            GameEvents.onGameover.Invoke();
        }
        if (transform.position.x - playerController.transform.position.x > 1) // c = 5 p = 6 = -1
        {
            speed = startSpeed;
        }
        else if (transform.position.x - playerController.transform.position.x < 1) // c = 5 p = 4   
        {
            speed = 15f;
        }
        else
        {
            speed = 8f;
        }
    }
}