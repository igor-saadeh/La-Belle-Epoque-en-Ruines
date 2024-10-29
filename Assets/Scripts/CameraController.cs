using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController playerController; // temporario
    //private Camera cameraPlayer;
    private float speed = 1f; // 7?

    private void Start()
    {
        //Camera[] temp = GetComponentsInChildren<Camera>();

        //foreach (Camera cam in temp)
        //{
        //    if (cam.gameObject.name == "Camera")
        //    {
        //        cameraPlayer = cam;
        //        break;
        //    }
        //}    
        //transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0); //rever
        
        float xPos = playerController.transform.position.x + 1; //prototipo
        transform.position = new Vector3(xPos, playerController.transform.position.y, transform.position.z); //prototipo

        //cameraPlayer.transform.position = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, playerController.transform.position.y, transform.position.z);
    }
}

/*
 * Camera se movimente no eixo X numa velocidade fixa (5f)
 * Camera deve seguir o movimento do personagem em y
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
*/