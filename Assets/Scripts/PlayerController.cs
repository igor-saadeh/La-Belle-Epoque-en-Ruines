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
    private Animator animator;

    [SerializeField]
    private Vector3 boxSize = new Vector3(1, 1, 1);

    [SerializeField]
    private float castDistance = 1;

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
        animator = GetComponent<Animator>();
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
        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //move = new Vector3(1, 0, 0);
        characterController.Move(move * Time.deltaTime * speed);
    }

    //Responsável pelo pulo do jogador
    private void Jump()
    {
        //if (characterController.isGrounded)
        if (isPlayerGrounded())
        {
            animator.SetBool("isGrounded", true);
            if (velocity.y < 0f)
            {
                //animator.SetFloat("ySpeed", velocity.y);
                velocity.y = 0f;
            }
            if (Input.GetKeyDown("space") || Input.GetKeyDown("up"))
            {
                velocity.y += Mathf.Sqrt(jumpHeight * -4.0f * gravity); // verificar
                animator.SetBool("isGrounded", false);
            }
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        animator.SetFloat("ySpeed", velocity.y);
    }
    //Retorna true se o player está tocando o chão
    private bool isPlayerGrounded()
    {
        if (Physics.BoxCast(transform.position, boxSize, transform.TransformDirection(Vector3.down), Quaternion.identity, castDistance))
        {
            return true;
        }
        return false;
    }
    //Desenhar o boxCast no editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + transform.TransformDirection(Vector3.down) * castDistance, boxSize);
    }
    //Responsável pelo movimento de deslizar do jogador
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
    //Controla se o player pode sair do "slide"
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Quando colidir com o obstáculo, chama a função da câmera
            Camera.main.GetComponent<CameraFollower>().PlayerHitObstacle();
        }
    }



}
