using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;

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
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection =  transform.position - worldPosition; //player will move away from your finger, if we do it other way around, it'll move toward our fingers. suppose transform positon is 0,0,0 and world is 2,2,0, we'll get -2,-2,0 diagonally bottom left

            movementDirection.z = 0f;
            movementDirection.Normalize(); //WD - diagonally middle moment
            

        }
        else
        {
            movementDirection = Vector3.zero;
        }

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

        rb.velocity =  Vector3.ClampMagnitude(rb.velocity, maxVelocity);

    }
}
