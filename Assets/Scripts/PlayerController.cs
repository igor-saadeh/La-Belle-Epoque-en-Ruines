using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    // Alterar para o player se movere em uma velocidade fixa

    private CharacterController characterController;
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private Vector3 move;

    [SerializeField]
    private float gravity = -40f;
    [SerializeField]
    private float jumpHeight = 1.5f;
    [SerializeField]
    private float speed = 7f;
    [SerializeField]
    private bool isGrounded;

    private float _playerPosition;

    public float playerPosition => _playerPosition;

    private void Awake()
    {
        GameEvents.onPlayerCentered.AddListener(ChangeSpeed);
    }
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _playerPosition = transform.position.x;

        // Criar função IsGrounded
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        // Criar função Move
        if (Input.GetKey("space") || Input.GetKey("up") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity); // verificar
        }

        //move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        move = new Vector3(1, 0, 0);
        characterController.Move(move * Time.deltaTime * speed);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void ChangeSpeed()
    {
        if (speed == 8f)
            speed = 7f;
        else
            speed = 7f;
    }
}