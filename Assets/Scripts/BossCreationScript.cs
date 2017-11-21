using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreationScript : MonoBehaviour {

    public List<GameObject> bosses;
    public List<SpriteRenderer> bossSprites;
    public List<BubbleGunScript> bossGuns;
    public List<BossScript> bossScripts;

    public GameObject bossPrefab;

    public GameObject ship;

    private float camWidth;
    private float camHeight;
    // Use this for initialization
    void Start () {
        bosses = new List<GameObject>();
        bossSprites = new List<SpriteRenderer>();
        bossGuns = new List<BubbleGunScript>();
        bossScripts = new List<BossScript>();

        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //create count bosses
    public void SpawnBosses(int count)
    {
        for (int i = 0; i < count; i++)
        {
            //find spot off screen
            //x
            int side = UnityEngine.Random.Range(0, 4);

            float xCoord = 0;
            float yCoord = 0;

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

            GameObject newBoss = Instantiate(bossPrefab);

            newBoss.transform.position = new Vector3(xCoord, yCoord, 0);

            //add to lists
            bosses.Add(newBoss);

            //give references to boss
            BossScript newScript = newBoss.GetComponent<BossScript>();

            newScript.ship = ship;
            newScript.creationScript = this;
            bossScripts.Add(newScript);

            bossGuns.Add(newBoss.GetComponentInChildren<BubbleGunScript>());

            bossSprites.Add(newBoss.GetComponent<SpriteRenderer>());
        }
    }
}
