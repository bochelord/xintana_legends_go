using UnityEngine;
using System.Collections;

public class FaceDirection : AbstractBehavior {

    private PlayerManager playermanager;

	// Use this for initialization
	void Start () {
        playermanager = this.GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (playermanager.xintanaisdead == false) { 
        
            var right = inputState.GetButtonValue (inputButtons [0]);
		    var left = inputState.GetButtonValue (inputButtons [1]);

		    if (right) {
			    inputState.direction = Directions.Right;
		    } else if (left) {
			    inputState.direction = Directions.Left;
		    }

		    transform.localScale = new Vector3 ((float)inputState.direction, 1, 1);

        }
	}
}
