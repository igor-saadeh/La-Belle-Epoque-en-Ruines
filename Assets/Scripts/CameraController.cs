using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering;
//using UnityEditorInternal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController playerController; // temporario
    private float speed = 7f;
    private float _cameraPosition;

    public float cameraPosition => _cameraPosition;


    private void Update()
    {
        _cameraPosition = transform.position.x;
        //transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0); //rever
        float xPos = playerController.transform.position.x + 1; //prototipo
        
        transform.position = new Vector3(xPos, playerController.transform.position.y, transform.position.z); //prototipo
    }
}
