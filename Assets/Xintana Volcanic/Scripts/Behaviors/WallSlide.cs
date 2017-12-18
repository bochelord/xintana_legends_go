using UnityEngine;
using System.Collections;

public class WallSlide : SticktoWall {

    public float slideVelocity = -5;
    public float slideMultiplier = 5f;

    override protected void Update()
    {
        base.Update();

        if (onWallDetected)
        {
            var velY = slideVelocity;

            if (inputState.GetButtonValue(inputButtons[0]))
            {
                velY *= slideMultiplier;
            }

            body2d.velocity = new Vector2(body2d.velocity.x, velY);
            //body2d.velocity = new Vector2(0, velY);
        }
    }

    override protected void OnStick()
    {
        body2d.velocity = Vector2.zero;
    }

    protected override void OffWall()
    {
        //This does nothing on purpose.
    }
	
}
