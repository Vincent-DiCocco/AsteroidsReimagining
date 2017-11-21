using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementScript : MonoBehaviour {

    public Vector3 direction;
    public float accelerationRate;
    public float decelerationPercent;
    public float turnSpeed;
    public float maxSpeed = .2f;
    public float maxReverseSpeed = -.1f;

    private Vector3 velocity;

    private float camWidth;
    private float camHeight;
    // Use this for initialization
    void Start () {
        velocity = new Vector3(0, 0, 0);

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
	
	// Update is called once per frame
	void Update () {

        TurnShip();

        VelocityCalculation();

        transform.position += velocity;

        CheckWrap();


    }

    //does the ship need to wrap
    private void CheckWrap()
    {

        Vector3 newCoord = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (transform.position.x < -1 * camWidth - .5f)
        {
            newCoord.x = camWidth - .5f;
        }
        else if (transform.position.x > camWidth + .5f)
        {
            newCoord.x = -1 * camWidth - .5f;
        }

        if (transform.position.y < -1 * camHeight - .5f)
        {
            newCoord.y = camHeight + .5f;
        }
        else if (transform.position.y > camHeight + .5f)
        {
            newCoord.y = -1 * camHeight - .5f;
        }

        transform.position = newCoord;
    }

    //calculates the velocity of the ship looking for input
    private void VelocityCalculation()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            velocity += accelerationRate * direction;
        }
        //adjust velocity more to center
        else if (velocity.x < 0)
        {
            velocity *= decelerationPercent;
        }
        else if (velocity.x > 0)
        {
            velocity *= decelerationPercent;
        }

        velocity = Vector3.ClampMagnitude(velocity,maxSpeed);
    }

    //checks for input to turn the ship
    private void TurnShip()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + turnSpeed);
            direction = Quaternion.Euler(0,0,turnSpeed) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - turnSpeed);
            direction = Quaternion.Euler(0, 0, -turnSpeed) * direction;
        }
    }
}
