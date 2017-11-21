using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


	}

    //calculates AABBCollisions from two SpriteRenderers
    public bool AABBCollision(SpriteRenderer first, SpriteRenderer second)
    {

        if (first.bounds.min.x < second.bounds.max.x)
        {
            if (first.bounds.max.x > second.bounds.min.x)
            {
                if (first.bounds.min.y < second.bounds.max.y)
                {
                    if (first.bounds.max.y > second.bounds.min.y)
                    {
                        return true;
                    }
                }
            }
        }

        return false;

    }
    
    //checks for circle collisions from two SpriteRenderers
    public bool CircleCollision(SpriteRenderer first, SpriteRenderer second)
    {

        //set the radius
        float firstSRRadius = Mathf.Max(first.bounds.max.x - first.bounds.center.x, first.bounds.max.y - first.bounds.center.y);
        float planetRadius = Mathf.Max(second.bounds.max.x - second.bounds.center.x, second.bounds.max.y - second.bounds.center.y);

        //get distance
        float distance = (first.transform.position.x - second.transform.position.x) * (first.transform.position.x - second.transform.position.x)
            + (first.transform.position.y - second.transform.position.y) * (first.transform.position.y - second.transform.position.y);

        //check for intersection
        if (distance < (firstSRRadius + planetRadius) * (firstSRRadius + planetRadius))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
