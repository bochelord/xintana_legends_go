using UnityEngine;
using System.Collections;

public class CollisionState : MonoBehaviour {

	public LayerMask collisionLayer;
    public LayerMask collisionPlatform;

	public bool standing;
    public bool onWall;
    public bool onPlatform = false;
    public bool onSlope = false;
    public bool onSlope_right = false;
    public bool onSlope_left = false;

	public Vector2 bottomPosition = Vector2.zero;
    public Vector2 leftPosition = Vector2.zero;
    public Vector2 rightPosition = Vector2.zero;
    public Vector2 right_bottomPosition = Vector2.zero;
    public Vector2 left_bottomPosition = Vector2.zero;
	public float collisionRadius = 10f;
	public Color debugCollisionColor = Color.red;
    public Color debugSlopesCollisionColor = Color.magenta;

    private InputState inputState;

    private PlayerManager playermanager;

    private GameObject platform;
    void Awake()
    {
        inputState = GetComponent<InputState>();
        playermanager = GetComponent<PlayerManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
        if (!playermanager.xintanaisdead) { 
		
            var pos = bottomPosition;
		    pos.x += transform.position.x;
		    pos.y += transform.position.y;

		    standing = Physics2D.OverlapCircle (pos, collisionRadius, collisionLayer);



            pos = inputState.direction == Directions.Right ? rightPosition : leftPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onWall = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);


            pos = bottomPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onPlatform = Physics2D.OverlapCircle(pos, collisionRadius, collisionPlatform);


            pos = right_bottomPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onSlope_right = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);
            //onSlope = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

            pos = left_bottomPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onSlope_left = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);
            //onSlope = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);

            if (onSlope_left || onSlope_right)
            {
                onSlope = true;
            }
            else if (!onSlope_left&&!onSlope_right)
            {
                onSlope = false;
            }

        }

	}

	void OnDrawGizmos(){
		Gizmos.color = debugCollisionColor;

        var positions = new Vector2[] { rightPosition, bottomPosition, leftPosition, right_bottomPosition, left_bottomPosition };


        foreach (var position in positions)
        {
            var pos = position;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            if (position == right_bottomPosition || position == left_bottomPosition)
            {
                Gizmos.color = debugSlopesCollisionColor;
            }

            Gizmos.DrawWireSphere(pos, collisionRadius);
        }
	}

    //void OnCollisionStay2D(Collision2D other)
    //{
    //    if (other.transform.tag == "Platform" || other.transform.tag == "FallingPlatform")
    //    {
            
    //        onPlatform = true;
    //        platform = other.gameObject;
    //    }
    //    else
    //    {
    //        onPlatform = false;
    //    }
        
    //}

    //public GameObject GetCurrentPlatform() { return platform; }

}
