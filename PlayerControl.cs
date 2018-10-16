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
        winText.text = "Still Playing";
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
            }
        }
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection); // turns this into a world space position
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void OnCollisionEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            audioSource.Play();
            winText.text = "You win!";
        }   
        else 
        {
            winText.text = "You lose!";
        }
    }
}