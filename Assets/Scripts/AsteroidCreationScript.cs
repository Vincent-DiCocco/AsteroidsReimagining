using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreationScript : MonoBehaviour {

    public GameObject[] smallAsteroidPrefabs;
    public GameObject[] asteroidPrefabs;
    public List<GameObject> asteroids;
    public List<SpriteRenderer> asteroidSprites;

    private float camWidth;
    private float camHeight;
    // Use this for initialization
    void Start () {
        asteroids = new List<GameObject>();
        asteroidSprites = new List<SpriteRenderer>();

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }


    // Update is called once per frame
    void Update () {

        

    }

    //spawns "count" asteroids
    public void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            //create asteroid
            int prefabNumber = UnityEngine.Random.Range(0, asteroidPrefabs.Length);

            //find spot off screen
            //x
            int side = UnityEngine.Random.Range(0, 4);

            float xCoord = 0;
            float yCoord = 0;
            float xForce = 0;
            float yForce = 0;

            if (side == 0)
            {
                xCoord = -1 * camWidth - .49f;
                yCoord = UnityEngine.Random.Range(-1f * camHeight - .49f, camHeight + .49f);
            }
            else if (side == 1)
            {
                xCoord = UnityEngine.Random.Range(-1f * camWidth - .49f, camWidth + .49f);
                yCoord = camHeight + .49f;
            }
            else if (side == 2)
            {
                xCoord = camWidth + .49f;
                yCoord = UnityEngine.Random.Range(-1f * camHeight - .49f, camHeight + .49f);
            }
            else if (side == 3)
            {
                xCoord = UnityEngine.Random.Range(-1f * camWidth - .49f, camWidth + .49f);
                yCoord = -1f * camHeight - .49f;
            }

            xForce = UnityEngine.Random.Range(-.05f, .05f);
            yForce = UnityEngine.Random.Range(-.05f, .05f);
            Vector3 forceVector = new Vector3(xForce, yForce, 0);

            GameObject newAsteroid = Instantiate(asteroidPrefabs[prefabNumber]);

            newAsteroid.transform.position = new Vector3(xCoord, yCoord, 0);
            newAsteroid.GetComponent<AsteroidMovementScript>().velocity = forceVector;

            newAsteroid.GetComponent<AsteroidInfoScript>().stage = 1;

            newAsteroid.GetComponent<AsteroidDestructionScript>().asteroidScript = this;

            asteroids.Add(newAsteroid);
            asteroidSprites.Add(newAsteroid.GetComponent<SpriteRenderer>());
        }
    }

    //spawns smaller asteroids given a larger asteroid
    public void SpawnSmaller(GameObject asteroidGameObject)
    {
        AsteroidMovementScript moveScipt = asteroidGameObject.GetComponent<AsteroidMovementScript>();

        //first asteroid
        int prefabNumber = UnityEngine.Random.Range(0, smallAsteroidPrefabs.Length);
        GameObject newAsteroid = Instantiate(smallAsteroidPrefabs[prefabNumber]);

        newAsteroid.transform.position = new Vector3(asteroidGameObject.transform.position.x, asteroidGameObject.transform.position.y, 0);

        float xForce = UnityEngine.Random.Range(moveScipt.velocity.x - .01f, moveScipt.velocity.x + .01f);
        float yForce = UnityEngine.Random.Range(moveScipt.velocity.y  - .01f, moveScipt.velocity.y + .01f);
        Vector3 forceVector = new Vector3(xForce, yForce, 0);

        newAsteroid.GetComponent<AsteroidMovementScript>().velocity = forceVector;
        newAsteroid.GetComponent<AsteroidInfoScript>().stage = 2;

        newAsteroid.GetComponent<AsteroidDestructionScript>().asteroidScript = this;

        asteroids.Add(newAsteroid);
        asteroidSprites.Add(newAsteroid.GetComponent<SpriteRenderer>());

        //second asteroid
        prefabNumber = UnityEngine.Random.Range(0, smallAsteroidPrefabs.Length);
        newAsteroid = Instantiate(smallAsteroidPrefabs[prefabNumber]);

        newAsteroid.transform.position = new Vector3(asteroidGameObject.transform.position.x, asteroidGameObject.transform.position.y, 0);

        xForce = UnityEngine.Random.Range(moveScipt.velocity.x - .01f, moveScipt.velocity.x + .01f);
        yForce = UnityEngine.Random.Range(moveScipt.velocity.y  - .01f, moveScipt.velocity.y + .01f);
        forceVector = new Vector3(xForce, yForce, 0);

        newAsteroid.GetComponent<AsteroidMovementScript>().velocity = forceVector;
        newAsteroid.GetComponent<AsteroidInfoScript>().stage = 2;

        newAsteroid.GetComponent<AsteroidDestructionScript>().asteroidScript = this;

        asteroids.Add(newAsteroid);
        asteroidSprites.Add(newAsteroid.GetComponent<SpriteRenderer>());
    }

}
