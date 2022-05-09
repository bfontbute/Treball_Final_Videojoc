using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;

    private bool DobleJump;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;

    public Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;

        anim = GetComponentInChildren<Animator>();

        

    }

    void Update()
    {

        Debug.Log(ySpeed);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        RaycastHit hit;
        Ray floorLandingRay = new Ray(transform.position, Vector3.down * 1.5f);
        Ray floorMovimentRay = new Ray(transform.position, Vector3.down * 0.5f);
     

        Debug.DrawRay(transform.position, Vector3.down * 1.5f , Color.yellow);
        Debug.DrawRay(transform.position, Vector3.down * 0.5f, Color.green);


        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime * 1.8f;

        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            anim.SetBool("Jump", false);
            //anim.Play("Moviment");
            anim.SetBool("DobleJump", false);
            anim.SetBool("Grounded", true);
            anim.ResetTrigger("IsLanding");

            if (Physics.Raycast(floorMovimentRay, out hit, 0.5f))
            {
                
                //anim.Play("Moviment");

            }

            if (Input.GetButtonDown("Jump"))
            { 
                ySpeed = jumpSpeed;
                Jump();
                anim.SetBool("Jump", true);
                DobleJump = true;

            }

        }

        else
        {
            anim.SetBool("Grounded", false);
            if (Input.GetKeyDown(KeyCode.Space) && DobleJump == true)
            {
                ySpeed = jumpSpeed;
                DobleJump2();
                anim.SetBool("Jump", true);
                DobleJump = false;
            }

            characterController.stepOffset = 0;
        }


        if (!anim.GetBool("Grounded") && ySpeed <=2 && Physics.Raycast(floorLandingRay, out hit))
        {
            
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                //anim.Blend("Jump_Final_1");
                anim.SetTrigger("IsLanding");
            }

        }



        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            Run();

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Idle();
        }
    }

    private void Idle()
    {
        anim.SetFloat("Moviment", 0f);

    }
    private void Walk()
    {
        anim.SetFloat("Moviment", 0.5f);

    }
    private void Run()
    {
        anim.SetFloat("Moviment", 1f);

    }

    private void Jump()
    {
        anim.Play("Jump2");

    }
    private void DobleJump2()
    {
        anim.Play("DobleJump");

    }
}