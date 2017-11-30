using UnityEngine;
using System.Collections;

public class LookUp : AbstractBehavior {

    public float lookupHoldTimedelay = .35f;
    public bool canLook = false;
	private bool lookingUp = false;
    private float holdtime = 0;
	// Update is called once per frame
	void Update () {

        lookingUp = inputState.GetButtonValue(inputButtons[0]);
        holdtime = inputState.GetButtonHoldTime(inputButtons[0]);

        if (lookingUp && (collisionState.standing || collisionState.onPlatform || collisionState.onWall) && holdtime > lookupHoldTimedelay)
        {
            canLook = true;
        }
        else
        {
            canLook = false;
        }


	}
}
