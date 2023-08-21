using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    [SerializeField] private CharacterController controller;
    [SerializeField] private float _speed = 13f;
    [SerializeField] float normalSpeed = 13f;
    [SerializeField] float sprintSpeed = 18f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = /*transform.right * x*/ (transform.right * x) + (transform.forward * z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = sprintSpeed;
        }
        else if (_speed != normalSpeed)
        {
            _speed = normalSpeed;
        }

        controller.Move(move * _speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    // void FixedUpdate()
    // {
    //     if (transform.position.z != 0)
    //     {
    //         transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    //     }
    // }
}
