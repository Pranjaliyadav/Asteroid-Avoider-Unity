using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotateSpeed;

    private Camera mainCamera;
    private Rigidbody rb;
    private Vector3 movementDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main; //this access the main camera
    }

    private void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();

    }
    private void FixedUpdate()
    {
        //update is called every frame
        //FIxedUpdate is called everytime the physics system updates
        //update's call frequency changes based on how well the game is running
        //FIxedUpdate call frequency stays consistent regardless of the game's performance

        if(movementDirection == Vector3.zero)
        {
            return;
        }
        rb.AddForce(movementDirection * forceMagnitude * Time.deltaTime, ForceMode.Force);

        rb.velocity =  Vector3.ClampMagnitude(rb.velocity, maxVelocity); //wont let rb velocity go more than maxVelocity.
        //rigid body's velocity also tells the direction we are moving in. so can be used for rotations also 
    }
    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPosition; //player will move away from your finger, if we do it other way around, it'll move toward our fingers. suppose transform positon is 0,0,0 and world is 2,2,0, we'll get -2,-2,0 diagonally bottom left

            movementDirection.z = 0f;
            movementDirection.Normalize(); //WD - diagonally middle moment


        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }
    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition =  mainCamera.WorldToViewportPoint(transform.position);

        if(viewportPosition.x > 1)
        {
            //if gone on right side of screen
            newPosition.x = -newPosition.x + 0.1f;
        }

        else if (viewportPosition.x < 0)
        {
            //if gone on left side of screen
            newPosition.x = -newPosition.x - 0.1f;
        }
        if (viewportPosition.y > 1)
        {
            //if gone on down side of screen
            newPosition.y = -newPosition.y + 0.1f;
        }

        else if (viewportPosition.y < 0)
        {
            //if gone on up side of screen
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
    }

    private void RotateToFaceVelocity()
    {   
        if(rb.velocity == Vector3.zero) { return; }

        Quaternion targetRotation =  Quaternion.LookRotation(rb.velocity,Vector3.back);//vector3.back is the green transform position direction
        
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        //when we are farther from our rotation location we rotate faster, when we are close we rotate slower
    }
}
