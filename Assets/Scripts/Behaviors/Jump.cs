using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {

	public float jumpSpeed = 200f;
	public float jumpDelay = .1f;
	public int jumpCount = 2;
    public int jumpsRemaining = 0;
    public bool onDoubleJump = false;
	public GameObject doubleJumpEffectPrefab;

    //public bool isJumping;

	protected float lastJumpTime = 0;
	//protected int jumpsRemaining = 0;
    private PlayerManager playermanager;
    private float holdTime;
    private bool canJump;

	// Use this for initialization
	void Start () {
        playermanager = this.GetComponent<PlayerManager>();
        onDoubleJump = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {

        if (playermanager.xintanaisdead == false) { 
	    
            canJump = inputState.GetButtonValue (inputButtons [0]);
		    holdTime = inputState.GetButtonHoldTime (inputButtons [0]);

            

		    if (collisionState.standing || collisionState.onPlatform || collisionState.onSlope || collisionState.onWall) {
			    if (canJump && holdTime < .1f) {
				    jumpsRemaining = jumpCount - 1;
                    //isJumping = true;
                    //Debug.Log("holdtime:" + holdTime);
				    OnJump ();
			    }
		    } else {
			    if(canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay){

				    if(jumpsRemaining > 0){
                        onDoubleJump = true;
                        OnJump ();
                        //isJumping = true;
                        
                        
					    jumpsRemaining --;
					    var clone = Instantiate(doubleJumpEffectPrefab);
					    clone.transform.position = transform.position;
                        Destroy(clone, 2f);
				    }
			    }
		    }

            //if () {

            //}

        }
        //if (collisionState.standing && isJumping)
        //{
        //    isJumping = false;
        //}

	}

	protected virtual void OnJump(){
		var vel = body2d.velocity;
		lastJumpTime = Time.time;
        AudioFXManager.Instance.PlayVoiceJump();

        if (onDoubleJump)
        {
            playermanager.ChangeAnimationState(8);
            //onDoubleJump = false;
        }
        
        //if (playermanager.animator.GetInteger("EquippedItem") == 0) { 
        //    playermanager.ChangeAnimationState(6);
        //}
        //else
        //{
        //    playermanager.ChangeAnimationState(7);
        //}


        //Debug.Log("llamadadoblejump");
		body2d.velocity = new Vector2 (vel.x, jumpSpeed);

	}
}
