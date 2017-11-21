using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public GunScript gun;

    public Vector3 speed;

    public float timeTillDeletion;
    private float survivalTime;

    private float camWidth;
    private float camHeight;

    // Use this for initialization
    void Start () {
        survivalTime = 0;

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
	
	// Update is called once per frame
	void Update () {

        survivalTime += Time.deltaTime;

		//update time till deletion and check if it has to be destroyed
        if(survivalTime >= timeTillDeletion)
        {
            DeleteBullet();
        }

        transform.Translate(speed);

        CheckWrap();
	}

    //check if the bullets need to wrap
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

    //remove the bullet from lists and destroy it
    public void DeleteBullet()
    {
        try
        {
            gun.bullets.Remove(gameObject);

            gun.bulletSprites.Remove(gameObject.GetComponent<SpriteRenderer>());
        }
        catch
        {

        }

        Destroy(gameObject);
    }
}
