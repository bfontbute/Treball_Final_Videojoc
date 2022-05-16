using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3 : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;

    private bool DobleJump;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;

    public Animator anim;

    [SerializeField] private float jumpButtonGracePeriod;

    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float? dobleJumpButtonPressedTime;


    private bool isJumping;
    private bool isGrounded;

    RaycastHit hit;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;

        anim = GetComponentInChildren<Animator>();



    }

    void Update()
    {


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime * 1.8f;


        if (characterController.isGrounded)
        {

            lastGroundedTime = Time.time;


            if (Input.GetButtonDown("Jump"))
            {
                jumpButtonPressedTime = Time.time;
                DobleJump = true;
            }

        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            DobleJump = true;
            anim.SetBool("IsGrounded", true);
            isGrounded = true;
            anim.SetBool("IsJumping", false);
            isJumping = false;
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsDobleJump", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                anim.SetBool("IsJumping", true);
                anim.SetBool("IsGrounded", false);
                isJumping = true;
                DobleJump = true;
                lastGroundedTime = null;
                jumpButtonPressedTime = null;

            }
        }
        else
        {
            characterController.stepOffset = 0;
            anim.SetBool("IsGrounded", false);
            isGrounded = false;
            Debug.Log(isGrounded);

            if (Input.GetButtonDown("Jump") && DobleJump == true)
            {
                ySpeed = jumpSpeed;
                anim.SetBool("IsJumping", false);
                anim.SetBool("IsDobleJump", true);
                anim.SetBool("IsFalling", false);
                anim.SetBool("IsGounded", false);
                isJumping = true;
                DobleJump = false;
                lastGroundedTime = null;
                jumpButtonPressedTime = null;

            }

            if ((isJumping && ySpeed < 0 && !anim.GetBool("IsDobleJump")) || ySpeed < -2 && !anim.GetBool("IsDobleJump"))
            {
                anim.SetBool("IsFalling", true);
                //anim.SetBool("IsGounded", false);
            }
        }




        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);
        Debug.Log(magnitude);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            anim.SetBool("IsMoving", true);

            anim.SetFloat("Moviment2", magnitude);


            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    /*
    private void Idle2()
    {
        anim.SetFloat("Moviment2", 0f);

    }
    */
    private void Walk2()
    {
        anim.SetFloat("Moviment2", 0.5f);

    }
    private void Run2()
    {
        anim.SetFloat("Moviment2", 1f);


    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8)
        {
            Debug.Log("Grouding-Collider");
            isGrounded = true;
        }

    }
}