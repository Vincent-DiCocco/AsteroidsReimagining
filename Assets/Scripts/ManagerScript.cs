using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {

    public CollisionScript collideScript;
    public AsteroidCreationScript asteroidScript;
    public BossCreationScript bossCreationScript;
    public GameObject ship;
    public SpriteRenderer shipSprite;
    public GunScript gun;
    public GUIScript gui;

    public GameObject shipPrefab;

    public int startingLives;
    public int currentLives;

    public float maxImmunityTime;
    private float currentImmunityTime;
    public bool isImmune;

    public bool gameOver;

    public int score;

    //the current level
    public int level;

	// Use this for initialization
	void Start () {
        currentLives = startingLives;
        score = 0;

        //start immune
        isImmune = true;
        currentImmunityTime = 0;

        gameOver = false;

        level = 0;

        NextLevel();
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameOver)
        {
            //adjust immunity
            currentImmunityTime += Time.deltaTime;

            if (isImmune && currentImmunityTime >= maxImmunityTime)
            {
                isImmune = false;
            }

            if (level % 3 != 0)
            {
                //check if there are no asteroids, if none go to next level
                if (asteroidScript.asteroids.Count == 0)
                {
                    NextLevel();
                }

                for (int i = 0; i < asteroidScript.asteroids.Count; i++)
                {

                    if (!isImmune && collideScript.CircleCollision(shipSprite, asteroidScript.asteroidSprites[i]))
                    {
                        currentLives--;

                        isImmune = true;
                        currentImmunityTime = 0;

                        asteroidScript.asteroidSprites[i].GetComponent<AsteroidDestructionScript>().DestroyAsteroid();
                        i--;
                    }

                    for (int j = 0; j < gun.bullets.Count; j++)
                    {
                        //possible bandage for bug
                        if (gun.bulletSprites[j] == null)
                        {
                            gun.bullets.RemoveAt(j);
                            gun.bulletSprites.RemoveAt(j);
                        }

                        if (i >= 0 && j >= 0 && collideScript.CircleCollision(gun.bulletSprites[j], asteroidScript.asteroidSprites[i]))
                        {
                            //increase score here
                            if (asteroidScript.asteroids[i].GetComponent<AsteroidInfoScript>().stage == 1)
                            {
                                score += 20;
                            }
                            else
                            {
                                score += 50;
                            }

                            gun.bullets[j].GetComponent<BulletScript>().DeleteBullet();
                            j--;

                            asteroidScript.asteroidSprites[i].GetComponent<AsteroidDestructionScript>().DestroyAsteroid();
                            i--;
                        }
                    }
                }
            }
            else
            {
                //can we proceed?
                if(bossCreationScript.bosses.Count <= 0)
                {
                    NextLevel();
                }

                //boss stuff here
                //check for bosses colliding with bullets
                for (int i = 0; i < bossCreationScript.bosses.Count; i++)
                {
                    //bosses and bullets
                    for (int j = 0; j < gun.bullets.Count; j++)
                    {
                        if(collideScript.CircleCollision(bossCreationScript.bossSprites[i], gun.bulletSprites[j]))
                        {
                            //remove a life from boss
                            bossCreationScript.bossScripts[i].currentLife--;
                            
                            //check if dead
                            if(bossCreationScript.bossScripts[i].currentLife <= 0)
                            {
                                score += 500;
                            }

                            //destroy bullet
                            gun.bullets[j].GetComponent<BulletScript>().DeleteBullet();
                            j--;
                        }
                    }

                    if (!isImmune)
                    {
                        //ship and bubbles
                        for (int j = 0; j < bossCreationScript.bossGuns[i].bubbles.Count; j++)
                        {
                            if (collideScript.CircleCollision(bossCreationScript.bossGuns[i].bubbleSprites[j], shipSprite))
                            {
                                bossCreationScript.bossGuns[i].bubbleSprites[j].GetComponent<BubbleScript>().DestroyBubble();
                                j--;
                                currentLives--;

                                isImmune = true;
                                currentImmunityTime = 0;

                            }
                        }

                        //boss and ship
                        if (collideScript.CircleCollision(bossCreationScript.bossSprites[i], shipSprite))
                        {
                            currentLives--;

                            isImmune = true;
                            currentImmunityTime = 0;
                        }
                    }
                }


            }

            //is ship dead
            if (currentLives <= 0)
            {
                gameOver = true;
                Destroy(ship);
            }
        }
        else
        {
            //reset if game over and r is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }
        }

	}

    //reset the game
    private void Reset()
    {
        //destroy all asteroids and bosses
        int count = asteroidScript.asteroids.Count;
        for (int i = 0; i < count; i++)
        {
            asteroidScript.asteroids[0].GetComponent<AsteroidDestructionScript>().DestroyAsteroid();
        }

        count = asteroidScript.asteroids.Count;
        for (int i = 0; i < count; i++)
        {
            asteroidScript.asteroids[0].GetComponent<AsteroidDestructionScript>().DestroyAsteroid();
        }

        for (int i = 0; i < bossCreationScript.bosses.Count; i++)
        {
            bossCreationScript.bossScripts[0].DestroyBoss();
        }

        score = 0;
        level = 0;
        gameOver = false;
        currentLives = startingLives;


        //create new ship and put him at 0,0,0
        ship = Instantiate(shipPrefab);
        ship.transform.position = Vector3.zero;
        ship.transform.eulerAngles = Vector3.zero;

        //set the scripts and other stuff
        shipSprite = ship.GetComponent<SpriteRenderer>();
        bossCreationScript.ship = ship;
        gui.ship = ship.GetComponent<Renderer>();
        gun = ship.GetComponentInChildren<GunScript>();
        //give immunity
        isImmune = true;
        currentImmunityTime = 0;
    }

    //goes to the next level
    public void NextLevel()
    {
        //increment level
        level++;

        //immunity granted
        isImmune = true;
        currentImmunityTime = 0;

        //spawn asteroids if 1 or 2
        if(level % 3  != 0)
        {
            asteroidScript.SpawnAsteroids(1 + level);
        }
        else
        {
            bossCreationScript.SpawnBosses(level / 3);
        }
       
    }
}
