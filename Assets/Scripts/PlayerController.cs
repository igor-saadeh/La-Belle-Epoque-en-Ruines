using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private bool storagedSlide = false;

    [SerializeField]
    private bool storagedJump = false;

    [SerializeField]
    private Vector3 boxSize = new Vector3(1, 1, 1);

    [SerializeField]
    private float castDistance = 1;

    [SerializeField]
    private Vector3 _velocity;

    public Vector3 velocity
    { 
        get { return _velocity; }
    }

    [SerializeField]
    private Vector3 move;

    [SerializeField]
    private float gravity = -40f;

    [SerializeField]
    private float jumpHeight = 1.5f;

    [SerializeField]
    private float speed = 8f;

    
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
        if (isPlayerGrounded())
        {
            animator.SetBool("isGrounded", true);
            if (_velocity.y < 0f)
            {
                _velocity.y = 0f;
            }
            if (Input.GetKeyDown("space") || Input.GetKeyDown("up"))
            {
                _velocity.y += Mathf.Sqrt(jumpHeight * -4.0f * gravity); // verificar
                animator.SetBool("isGrounded", false);
            }
            else if (Input.GetKey("space") && storagedJump || Input.GetKey("up") && storagedJump)
            {
                storagedJump = false;
                _velocity.y += Mathf.Sqrt(jumpHeight * -4.0f * gravity); // verificar
                animator.SetBool("isGrounded", false);
            }
            if (Input.GetKeyUp("space") || Input.GetKeyUp("up"))
            {
                // reduz altura do pulo
                // velocity /= 2;
            }
        }
        else
        {
            if (Input.GetKeyDown("space") && !storagedJump || Input.GetKeyDown("up") && !storagedJump)
            { 
                storagedJump = true;
                StartCoroutine(StoragedJumpTimer());
            }
        }
        _velocity.y += gravity * Time.deltaTime;
        characterController.Move(_velocity * Time.deltaTime);
        animator.SetFloat("ySpeed", _velocity.y);
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
        if (isPlayerGrounded())
        {
            if (Input.GetKeyDown("down"))
            {
                animator.SetBool("isSliding", true);
                speed += 4f;
                characterController.height /= 2f;
                StartCoroutine("OnSliding");
            }
            else if (Input.GetKey("down") && storagedSlide)
            {
                storagedSlide = false;
                animator.SetBool("isSliding", true);
                speed += 4f;
                characterController.height /= 2f;
                StartCoroutine("OnSliding");
            }
        }
        else
        {
            if (Input.GetKeyDown("down"))
            {
                storagedSlide = true;
                StartCoroutine(StoragedSlideTimer());
            }
        }
    }
    //Controla se o player pode sair do "slide"
    IEnumerator OnSliding()
    {
        yield return new WaitForSeconds(0.25f);
        
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 1f))
        {
            characterController.height *= 2f;
            speed -= 4f;
            animator.SetBool("isSliding", false);
            yield break;
        }
        StartCoroutine("OnSliding");
    }

    IEnumerator StoragedJumpTimer()
    {
        yield return new WaitForSeconds(0.25f);
        storagedJump = false;
    }

    IEnumerator StoragedSlideTimer()
    {
        yield return new WaitForSeconds(0.25f);
        storagedSlide = false;
    }
}
