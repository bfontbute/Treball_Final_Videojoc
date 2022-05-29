using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player3 : MonoBehaviour
{
    public AudioSource Coin_SFX;

    [Header("Animation")]
    private const string IsFallingBool = "IsFalling";
    private const string IsGroundedBool = "IsGrounded";
    private const string IsJumpingBool = "IsJumping";
    private const string IsDobleJumpingBool = "IsDobleJump";
    private const string IsMovingBool = "IsMoving";
    public Animator anim;

    [Header("Player")]
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    private bool DobleJump;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;


    [SerializeField]
    public static Vector3 lastCeckPointPos = new Vector3(55, 5, 16);



    [SerializeField] private float jumpButtonGracePeriod;

    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private float? dobleJumpButtonPressedTime;


    private bool isJumping;
    private bool isGrounded;

    RaycastHit hit;

    [Header("PLayerStats")]
    int GearPoints = 0;
    [SerializeField]
    private Text TextGearPoints;

    int Health = 3;
    [SerializeField]
    private Text TextHealth;


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

            Jumping();

        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            DobleJump = true;
            anim.SetBool(IsGroundedBool, true);
            isGrounded = true;
            anim.SetBool(IsJumpingBool, false);
            isJumping = false;
            anim.SetBool(IsJumpingBool, false);
            anim.SetBool(IsDobleJumpingBool, false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                anim.SetBool(IsJumpingBool, true);
                anim.SetBool(IsGroundedBool, false);
                isJumping = true;
                DobleJump = true;
                lastGroundedTime = null;
                jumpButtonPressedTime = null;

            }

            Falling();
        }
        else
        {
            characterController.stepOffset = 0;
            anim.SetBool(IsGroundedBool, false);
            isGrounded = false;
            Debug.Log(isGrounded);

            if (Input.GetButtonDown("Jump") && DobleJump == true)
            {
                ySpeed = jumpSpeed;
                anim.SetBool(IsJumpingBool, false);
                anim.SetBool(IsDobleJumpingBool, true);
                anim.SetBool(IsJumpingBool, false);
                anim.SetBool(IsGroundedBool, false);
                isJumping = true;
                DobleJump = false;
                lastGroundedTime = null;
                jumpButtonPressedTime = null;

            }

            Falling();
        }

        void Jumping()
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpButtonPressedTime = Time.time;
                DobleJump = true;
            }
        }

        void Falling()
        {

            if ((isJumping && ySpeed < 0 && !anim.GetBool(IsDobleJumpingBool)) || ySpeed < -2 && !anim.GetBool(IsDobleJumpingBool))
            {
                anim.SetBool(IsJumpingBool, true);
            }

        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            anim.SetBool(IsMovingBool, true);

            anim.SetFloat("Moviment2", magnitude);


            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool(IsMovingBool, false);
        }
    }

    //-------------------------------------------------------------------------------------INTERACTION-------TRIGGER--------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            Gears();
            PlayCoin();
        }

        if (other.gameObject.layer == 7)
        {
            HealthLoose();
        }

        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            HealthPlus();
        }
    }

    public void PlayCoin()
    {
        Coin_SFX.Play ();
    }



    //OnTriggerEnter Layer 9
    public void Gears()
    {
        GearPoints += 1;
        TextGearPoints.text = GearPoints.ToString();

    }


    private void Walk2()
    {
        anim.SetFloat("Moviment2", 0.5f);

    }
    private void Run2()
    {
        anim.SetFloat("Moviment2", 1f);


    }

    //-------------------------------------------Vides------------------------------------------------------//

    //Layer 7
    public void HealthLoose()
    {

        if (Health == 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Health -= 1;
            TextHealth.text = Health.ToString();
            GameObject.FindGameObjectWithTag("Player").transform.position = lastCeckPointPos;

        }
    }
    public void HealthPlus()
    {
        Health += 1;
        TextHealth.text = Health.ToString();

    }


}