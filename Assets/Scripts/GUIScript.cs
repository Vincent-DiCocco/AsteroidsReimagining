using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScript : MonoBehaviour {

    public float immuneFlashInterval;
    private float timeFromImmuneFlashing;

    public Renderer ship;
    public ManagerScript manager;


    // Use this for initialization
    void Start () {
        timeFromImmuneFlashing = 0;

    }
	
	// Update is called once per frame
	void Update () {
        if (!manager.gameOver)
        {
            //check if player is immune
            if (manager.isImmune == true)
            {
                //show if player is immune
                timeFromImmuneFlashing += Time.deltaTime;

                if (timeFromImmuneFlashing > immuneFlashInterval)
                {
                    timeFromImmuneFlashing = 0;

                    ship.enabled = !ship.enabled;
                }
            }
            else
            {
                //make ship visible
                if (ship.enabled == false)
                {
                    ship.enabled = true;
                }
            }
        }
	}

    //makes the gui
    private void OnGUI()
    {
        if (!manager.gameOver)
        {
            GUI.Box(new Rect(10, 10, 100, 85), "Lives: " + manager.currentLives + " \n\nScore: " + manager.score + "\n\nLevel: " + manager.level);
        }
        else
        {
            GUI.Box(new Rect( Camera.main.pixelWidth / 2 - 75, Camera.main.pixelHeight / 2 - 60, 150, 120), "Game Over\n\nFinal Score: " + manager.score + "\nFinal Level: " + manager.level + "\n\n Press R to Restart.");
        }
    }
}
