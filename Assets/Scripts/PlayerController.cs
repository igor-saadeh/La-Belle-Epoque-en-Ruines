using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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
    private float speed = 8f;
    [SerializeField]


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Jump();
        Slide();

        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1f, Color.yellow);
    }

    //Responsável pelo movimento horizontal do jogador
    private void Move()
    {
        //move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        move = new Vector3(1, 0, 0);
        characterController.Move(move * Time.deltaTime * speed);
    }

    //Responsável pelo pulo do jogador
    private void Jump()
    {
        if (Input.GetKeyDown("space") && characterController.isGrounded || Input.GetKeyDown("up") && characterController.isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity); // verificar
        }

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Slide()
    {
        if (Input.GetKeyDown("down") && characterController.isGrounded)
        {
            speed += 2f;
            characterController.height /= 2f;
            StartCoroutine("OnSliding");
            speed -= 2f;
        }
        
    }

    IEnumerator OnSliding()
    {
        yield return new WaitForSeconds(0.5f);
        
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 1f))
        {
            characterController.height *= 2f;

            yield break;
        }
        StartCoroutine("OnSliding");
    }
}