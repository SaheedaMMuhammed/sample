using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    //character conroller of player
    private CharacterController characterController;

    //values for jump and move
    public float speed = 12f;
    public float gravity = -9.8f * 2;
    public float jumpheight = 3f;


    //values for if condition checks,groundcheck
    public Transform groundCheck;
    public float groundDistance=0.4f;
    public LayerMask groundMask;


    //vector for jumping height and jumping execution
    Vector3 velocity;


    bool isGrounded;
    bool isMoving;


    Vector3 lastposition= new Vector3(0f,0f,0f);


    // Start is called before the first frame update
    void Start()
    {
       characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //checking whether the player is grounded
        isGrounded=Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        if (isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }


        //getting the input for movement of the player(buttons a d w s,or arrow keys)
        float x = Input.GetAxis("Horizontal");               //right red axis sideway move
        float z = Input.GetAxis("Vertical");                 //forward movement   blue color axis 

        //moving vector for forward,back,sideways
        Vector3 move=transform.right*x+transform.forward*z;
        

        //the above one is to get the value of the player move
        // next line is to actually move the player according to the  above value in a plane
        characterController.Move(move*speed*Time.deltaTime);


        //if condition here is to check the player can move or not.
        //because if the player is not grounded, we not need to further go up
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);

        }

        velocity.y += gravity * Time.deltaTime;

        //get the jumping value and height, if we want to actually move we need to move the controller;
        characterController.Move(velocity*Time.deltaTime);




        if (lastposition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
            //for future use
        }



        else
        {
            isMoving= false;
            //for future use
        }


        lastposition = gameObject.transform.position;   
    }
}
