using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {
    //script for ship to make bullets and such

    public GameObject bulletPrefab;

    public List<GameObject> bullets;
    public List<SpriteRenderer> bulletSprites;

    public float shotDelay;
    private float timeFromLast;


	// Use this for initialization
	void Start () {
        timeFromLast = 100;
	}
	
	// Update is called once per frame
	void Update () {
        //add time since last frame to timefromLast
        timeFromLast += Time.deltaTime;

        //check if they are shooting
        if(Input.GetKeyDown(KeyCode.Space) && timeFromLast > shotDelay)
        {
            GameObject newBullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));

            bulletPrefab.GetComponent<BulletScript>().gun = this;

            bullets.Add(newBullet);
            bulletSprites.Add(newBullet.GetComponent<SpriteRenderer>());

            timeFromLast = 0;
        }
	}
}
