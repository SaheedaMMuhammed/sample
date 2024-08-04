using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMovement : MonoBehaviour
{


    public float mouseSensitivity = 500f;


    float xRotation = 0f;
    float yRotation = 0f;   



    // variables to clamp(we dont want to look down more than 90 degrees)
    public float topclamp = -90f;
    public float bottomclamp = 90f;

    // Start is called before the first frame update
    void Start()
    {

        //make the cursor locked and invisible;
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //get the mouse input
        float mouseX=Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float mouseY=Input.GetAxis("Mouse Y")*mouseSensitivity* Time.deltaTime;


        //looking up and down

        xRotation-=mouseY;


        //clamping the up and down 
        xRotation=Mathf.Clamp(xRotation, topclamp, bottomclamp);


        //looking sideways

        yRotation+=mouseX;

        //apply the rotation to out player
        transform.localRotation=Quaternion.Euler(xRotation,yRotation,0f);
    }
}
