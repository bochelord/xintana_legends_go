using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pauser : MonoBehaviour {

    public GameObject controls_panel;
	private bool paused = false;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("Start"))
		{
			paused = !paused;
		}



        if (paused)
        {
            Time.timeScale = 0;

            controls_panel.SetActive(true);


        }
        else
        {
            controls_panel.SetActive(false);
            Time.timeScale = 1;
        }
	}
}
