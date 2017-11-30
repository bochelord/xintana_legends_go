using UnityEngine;
using System.Collections;

public class Duck : AbstractBehavior {

    public float scaleCollisionCollider = .5f;
    public bool ducking;
    public float centerOffsetY = 0f;

    private CircleCollider2D circleCollider;
    private Vector2 originalCenter;
    private bool canDuck;

    protected override void Awake()
    {
        base.Awake();

        circleCollider = GetComponent<CircleCollider2D>();
        originalCenter = circleCollider.offset;
    }

    protected virtual void OnDuck(bool value)
    {
        ducking = value;

        ToggleScripts(!ducking);

        //Debug.Log("Toggled when ducking!!!TO:"+!ducking);

        var size = circleCollider.radius;

        float newOffsetY;
        float sizeReciprocal;

        if (ducking)
        {
            sizeReciprocal = scaleCollisionCollider;
            newOffsetY = circleCollider.offset.y - size / 2 + centerOffsetY;

        }
        else
        {
            sizeReciprocal = 1 / scaleCollisionCollider;
            newOffsetY = originalCenter.y;
        }

        size = size * sizeReciprocal;
        circleCollider.radius = size;
        circleCollider.offset = new Vector2(circleCollider.offset.x, newOffsetY);

    }

	
	// Update is called once per frame
	void Update () {

        canDuck = inputState.GetButtonValue(inputButtons[0]);

        if (canDuck && (collisionState.standing || collisionState.onPlatform || collisionState.onSlope) && !ducking)
        {
            OnDuck(true);
        }
        else if (ducking && !canDuck)
        {
            OnDuck(false);
        }

        if (ducking)
        {
            ToggleScripts(false);
        }

	}
}
