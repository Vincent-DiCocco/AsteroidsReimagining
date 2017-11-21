using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovementScript : MonoBehaviour {

    public Vector3 velocity;

    private float camWidth;
    private float camHeight;
    // Use this for initialization
    void Start () {

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(velocity);

        CheckWrap();
	}

    //check if the asteroids need to wrap
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
}
