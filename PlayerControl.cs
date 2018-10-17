using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    CharacterController characterController;

    public Text winText;
    public float speed;
    public float jumpSpeed;
    public float gravity = 10f;
    AudioSource audioSource;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        //winText.text = "Still Playing";
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection); // turns this into a world space position
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                audioSource.Play();
            }
        }
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection); // turns this into a world space position
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }

        transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X") * speed / 2.0f, 0f));
        
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Target")
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            //winText.text = "You win!";
        }   
    }
}