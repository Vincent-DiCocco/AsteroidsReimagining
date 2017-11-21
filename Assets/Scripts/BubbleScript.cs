using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour {

    public BubbleGunScript gun;

    public Vector3 speed;


    private float camWidth;
    private float camHeight;

    // Use this for initialization
    void Start () {

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
	
	// Update is called once per frame
	void Update () {


        //if outside bounds of camera, destroy bubble
        if (transform.position.x < -1 * camWidth - .5f || transform.position.x > camWidth + .5f || transform.position.y < -1 * camHeight - .5f || transform.position.y > camHeight + .5f)
        {
            DestroyBubble();
        }


        transform.Translate(speed);
    }

    //destroys the bullet
    public void DestroyBubble()
    {
        //remove from list
        gun.bubbles.Remove(gameObject);
        gun.bubbleSprites.Remove(gameObject.GetComponent<SpriteRenderer>());

        Destroy(gameObject);
    }
}
