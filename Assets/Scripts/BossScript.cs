using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStage
{
    TURNTOMOVE,
    SHOOT,
    TURNTOSHOOT,
    MOVE,
}

public class BossScript : MonoBehaviour {
    //public stuff
    public float moveSpeed;
    public float turnSpeed;
    public float shotDelay;
    public int shotNumber;
    public GameObject ship;

    public BubbleGunScript gun;

    //life stuff
    public int startingLife;
    public int currentLife;

    //put creation script here
    public BossCreationScript creationScript;

    //what is it doing
    private BossStage currentStage;

    private int currentShots;
    private float timeFromShot;

    private Vector3 targetPosition;
    private Vector3 targetRotation;

    private float camWidth;
    private float camHeight;

    private float currentRotation;

    private Vector3 direction;

    // Use this for initialization
    void Start () {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

        currentStage = BossStage.TURNTOMOVE;
        NextPosition();

        //set the target rotation
        FindTargetRotation(targetPosition);

        currentLife = startingLife;
    }
	
	// Update is called once per frame
	void Update () {
        //check if dead
        if(currentLife == 0)
        {
            DestroyBoss();
        }

        //do stuff
        switch (currentStage)
        {

            case BossStage.TURNTOMOVE:
                //turn the fish to the desired rotation
                if (currentRotation % 360> targetRotation.z + turnSpeed)
                {
                    transform.eulerAngles = new Vector3 (0, 0, transform.rotation.eulerAngles.z - turnSpeed);
                    currentRotation -= turnSpeed;
                }
                else if(currentRotation % 360 < targetRotation.z - turnSpeed)
                {
                    transform.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z + turnSpeed);
                    currentRotation += turnSpeed;
                }
                else
                {
                    currentStage = BossStage.MOVE;
                }
                break;
                
            case BossStage.SHOOT:
                //update time from shot
                timeFromShot += Time.deltaTime;
                //does it shoot
                if(timeFromShot >= shotDelay)
                {
                    gun.Shoot();

                    currentShots++;
                    timeFromShot = 0;
                }

                if(currentShots >= shotNumber)
                {
                    //next position time
                    NextPosition();
                    FindTargetRotation(targetPosition);
                    currentStage = BossStage.TURNTOMOVE;
                }
                break;
            case BossStage.TURNTOSHOOT:

                //turn the fish to the desired rotation
                if (currentRotation % 360 > targetRotation.z + turnSpeed)
                {
                    transform.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z - turnSpeed);
                    currentRotation -= turnSpeed;

                }
                else if (currentRotation % 360 < targetRotation.z - turnSpeed)
                {
                    transform.eulerAngles = new Vector3(0, 0, transform.rotation.eulerAngles.z + turnSpeed);
                    currentRotation += turnSpeed;
                }
                else
                {
                    currentStage = BossStage.SHOOT;
                    timeFromShot = shotDelay;
                    currentShots = 0;
                }
                break;
            case BossStage.MOVE:
                //is it close enough
                if ((targetPosition - transform.position).magnitude < moveSpeed)
                {

                    if (ship != null)
                    {
                        //time to turn to shoot
                        FindTargetRotation(ship.transform.position);
                    }

                    currentStage = BossStage.TURNTOSHOOT;

                }
                else
                {
                    //move the fish
                    direction = (targetPosition - transform.position).normalized;

                    transform.position += direction * moveSpeed;
                }
                break;
            default:
                break;
        }
    }
   
    //finds where the fish is moving to
    private void NextPosition()
    {
        //random position close to the fish
        targetPosition = new Vector3(Random.Range(transform.position.x - 4, transform.position.x + 4), Random.Range(transform.position.y - 4, transform.position.y + 4), transform.position.z);

        //wrap it if it is off the screen
        Vector3 newCoord = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);

        if (targetPosition.x < -1 * camWidth - .5f)
        {
            newCoord.x = camWidth + targetPosition.x;
        }
        else if (targetPosition.x > camWidth + .5f)
        {
            newCoord.x = -1 * camWidth + targetPosition.x;
        }

        if (targetPosition.y < -1 * camHeight - .5f)
        {
            newCoord.y = camHeight + targetPosition.y;
        }
        else if (targetPosition.y > camHeight + .5f)
        {
            newCoord.y = -1 * camHeight + targetPosition.y;
        }

        targetPosition = newCoord;
    }

    //finds the rotation the boss is aiming for
    private void FindTargetRotation(Vector3 target)
    {
        if (target.x >= transform.position.x)
        {
            targetRotation = new Vector3(0, 0, (Mathf.Atan((target.y - transform.position.y) / (target.x - transform.position.x)) * Mathf.Rad2Deg) % 360);
        }
        else if (target.y >= transform.position.y) 
        {
            targetRotation = new Vector3(0, 0,180 + Mathf.Atan((target.y - transform.position.y) / (target.x - transform.position.x)) * Mathf.Rad2Deg);
        }
        else{
            targetRotation = new Vector3(0, 0, 180 + Mathf.Atan((target.y - transform.position.y) / (target.x - transform.position.x)) * Mathf.Rad2Deg);
        }
    }

    //destroys a boss
    public void DestroyBoss()
    {
        //remove from lists
        creationScript.bosses.Remove(gameObject);
        creationScript.bossGuns.Remove(gameObject.GetComponentInChildren<BubbleGunScript>());
        creationScript.bossScripts.Remove(this);
        creationScript.bossSprites.Remove(gameObject.GetComponent<SpriteRenderer>());

        Destroy(gameObject);
    }
}
