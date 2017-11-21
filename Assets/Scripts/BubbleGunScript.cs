using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGunScript : MonoBehaviour {

    public GameObject bubblePrefab;
    public float bulletSpeed;

    public List<GameObject> bubbles;
    public List<SpriteRenderer> bubbleSprites;

	// Use this for initialization
	void Start () {
        bubbles = new List<GameObject>();
        bubbleSprites = new List<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //creates the bubble bullets
    public void Shoot()
    {
        //create 3 bubbles at 45 degree angles
        //first bubble
        GameObject newBubble = Instantiate(bubblePrefab);

        //set transform
        newBubble.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        newBubble.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        //set the bubble script stuff
        BubbleScript script = newBubble.GetComponent<BubbleScript>();

        script.speed = new Vector3(bulletSpeed, 0, 0);
        script.gun = this;

        //add to the lists
        bubbles.Add(newBubble);
        bubbleSprites.Add(newBubble.GetComponent<SpriteRenderer>());


        //second bubble
        newBubble = Instantiate(bubblePrefab);

        //set transform
        newBubble.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        newBubble.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 45);

        //set the bubble script stuff
        script = newBubble.GetComponent<BubbleScript>();

        script.speed = new Vector3(bulletSpeed, 0, 0);
        script.gun = this;

        //add to the lists
        bubbles.Add(newBubble);
        bubbleSprites.Add(newBubble.GetComponent<SpriteRenderer>());


        //third bubble
        newBubble = Instantiate(bubblePrefab);

        //set transform
        newBubble.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        newBubble.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 45);

        //set the bubble script stuff
        script = newBubble.GetComponent<BubbleScript>();

        script.speed = new Vector3(bulletSpeed, 0, 0);
        script.gun = this;

        //add to the lists
        bubbles.Add(newBubble);
        bubbleSprites.Add(newBubble.GetComponent<SpriteRenderer>());
    }
}
