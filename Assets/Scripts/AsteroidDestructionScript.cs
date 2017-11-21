using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestructionScript : MonoBehaviour {

    public AsteroidCreationScript asteroidScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //remove the asteroid from lists and destroy it
    public void DestroyAsteroid()
    {
        asteroidScript.asteroids.Remove(gameObject);
        asteroidScript.asteroidSprites.Remove(gameObject.GetComponent<SpriteRenderer>());

        //spawns the smaller ones
        if(gameObject.GetComponent<AsteroidInfoScript>().stage == 1)
        {
            asteroidScript.SpawnSmaller(gameObject);
        }
        
        Destroy(gameObject);

    }
}
