using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;
    private const float MovementSpeed = 10f;
    private const float RotationSpeed = 0.75f;

    public float jumpSpeed;
    private float ySpeed;

    private Vector3 _movementInput;
    private Vector3 _rotationInput;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ReadInputs();
        Move();


        

            
        if (_controller.isGrounded)
        {
            ySpeed = 0f;
            if (Input.GetButtonDown("Jump"))


            {
                ySpeed += Physics.gravity.y * Time.deltaTime;
                ySpeed = jumpSpeed;
            }
        }
        Vector3 velocity = _movementInput * MovementSpeed;
        velocity.y = ySpeed;

        _controller.Move(velocity * Time.deltaTime);
    }

    void ReadInputs()
    {
        float xAxisMovement = Input.GetAxisRaw("Horizontal") * MovementSpeed;
        float zAxisMovement = Input.GetAxisRaw("Vertical") * MovementSpeed;
        _movementInput.Set(xAxisMovement, _movementInput.y, zAxisMovement);
       //KartCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2;
        
        Vector3 desiredForward = new Vector3(xAxisMovement, 0, zAxisMovement);
        _rotationInput = Vector3.RotateTowards(transform.forward, desiredForward,
            RotationSpeed, 0f);
    }

    void Move()
    {
        if (_movementInput != Vector3.zero)
        {
            _controller.Move(transform.TransformDirection(Vector3.forward) * (MovementSpeed * Time.deltaTime));
            transform.rotation = Quaternion.LookRotation(_rotationInput * Time.deltaTime);
        }
    }

    void Jump()
    {
        
    }

}
