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

    [SerializeField] private bool storagedSlide = false;
    [SerializeField] private bool storagedJump = false;

    [SerializeField] private Vector3 boxSize = new Vector3(0.4f, 0.05f, 0.7f);
    [SerializeField] private float castDistance = 0.82f;

    [SerializeField] private Vector3 _velocity;
    public Vector3 velocity
    { 
        get { return _velocity; }
    }

    [SerializeField] private Vector3 move;
    [SerializeField] private float gravity = -40f;
    [SerializeField] private float jumpHeight = 6f;
    [SerializeField] private float speed = 8f;
    [SerializeField] private bool doubleJump = true;
    [SerializeField] private float doubleJumpHeight = 3f;


    private Vector3 lastPosition;

    [SerializeField] private float dashSpeed = 12f;
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private float dashTime = 0.2f;
    
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
        AirDash();

        if (transform.position.x == lastPosition.x)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }

        lastPosition.x = transform.position.x;
    }

    private void Move()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        //move = new Vector3(1, 0, 0);
        characterController.Move(move * Time.deltaTime * speed);
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }

        return velocity;
    }

    private void Jump()
    {
        if (isPlayerGrounded())
        {
            doubleJump = true;
            animator.SetBool("isGrounded", true);
            if (_velocity.y < 0f)
            {
                _velocity.y = 0f;
            }
            if (Input.GetKeyDown("space") || Input.GetKeyDown("up"))
            {
                _velocity.y += Mathf.Sqrt(-jumpHeight * gravity); // verificar
                animator.SetBool("isGrounded", false);
            }
            else if (Input.GetKey("space") && storagedJump || Input.GetKey("up") && storagedJump)
            {
                storagedJump = false;
                _velocity.y += Mathf.Sqrt(-jumpHeight * gravity); // verificar
                animator.SetBool("isGrounded", false);
            }
        }
        else
        {
            // se nao estiver no chao e a distancia percorrida pelo raycast até o chão for maior que x, executar pulo duplo??
            if(Input.GetKeyDown("space") && doubleJump || Input.GetKeyDown("up") && doubleJump)
            {
                _velocity.y = 0f;
                _velocity.y += Mathf.Sqrt(-doubleJumpHeight * gravity);
                doubleJump = false;
            }
            else if (Input.GetKeyDown("space") && !storagedJump || Input.GetKeyDown("up") && !storagedJump)
            { 
                storagedJump = true;
                StartCoroutine(StoragedJumpTimer());
            }
            if (Input.GetKeyUp("space") || Input.GetKeyUp("up"))
            {
                // reduz altura do pulo
                _velocity.y /= 2f;
            }
        }
        _velocity = AdjustVelocityToSlope(_velocity);
        _velocity.y += gravity * Time.deltaTime; // verificar 

        characterController.Move(_velocity * Time.deltaTime);
        animator.SetFloat("ySpeed", _velocity.y);
    }
    private bool isPlayerGrounded()
    {
        if (Physics.BoxCast(transform.position, boxSize, transform.TransformDirection(Vector3.down), Quaternion.identity, castDistance))
        {
            return true;
        }
        return false;
    }

    // metodo raycast?

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + transform.TransformDirection(Vector3.down) * castDistance, boxSize);
    }

    private void AirDash()
    {
        if (Input.GetKeyDown("left shift") && canDash && !isPlayerGrounded())
        {
            animator.SetBool("isDashing", true);
            isDashing = true;
            canDash = false;
        }
        if (isDashing)
        {
            characterController.Move(move * Time.deltaTime * dashSpeed);
            StartCoroutine(StopDashing());
        }
    }
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

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        animator.SetBool("isDashing", false);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
