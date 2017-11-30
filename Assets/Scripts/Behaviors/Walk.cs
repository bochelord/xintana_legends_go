using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {

	public float speed = 50f;
	public float runMultiplier = 2f;
	public bool running;
    public bool onSlope=false;
    public float slopeFriction = 0.95f;
    public GameObject dustFXPrefabIzq;
    public GameObject dustFXPrefabDer;

    protected bool dustFXOn;
    protected GameObject clone;

    private PlayerManager playermanager;

	// Use this for initialization
	void Start () {
        //clone = Instantiate(dustFXPrefab);
        //clone.transform.SetParent(this.transform, false);
        //clone.transform.position = new Vector3(0f,1.87f,0f);
        playermanager = this.GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        if (playermanager.xintanaisdead == false) { 

		    running = false;

		    var right = inputState.GetButtonValue (inputButtons [0]);
		    var left = inputState.GetButtonValue (inputButtons [1]);
		    var run = inputState.GetButtonValue (inputButtons [2]);


            //NormalizeSlope();

            //if (onSlope)
            //{

            //    collisionState.onSlope = true;
            //}
            //else
            //{
            //    collisionState.onSlope = false;
            //}


		    if (right || left ){

			    var tmpSpeed = speed;

			    if(run && runMultiplier > 0 && !collisionState.onWall && collisionState.standing){
				    tmpSpeed *= runMultiplier;
				    running = true;
			    }

                if (running && !dustFXOn)
                {
                
                
                    if (right)
                    {
                        //Quaternion qrot = new Quaternion(0, 0, 90, 0);

                        //clone.transform.rotation = qrot;
                        //clone.transform.rotation.SetLookRotation (new Vector3(0, 0, 90));
                        //Vector3 theScale = clone.transform.localScale;
                        //theScale.x *= -1;
                        //clone.transform.localScale = theScale;
                        dustFXPrefabDer.SetActive(true);

                    }
                    else if (left)
                    {
                        //clone.transform.rotation.SetLookRotation(new Vector3(0,180,90));
                        //clone.transform.rotation.SetEulerAngles(new Vector3(0, 180, 90));
                        //Vector3 theScale = clone.transform.localScale;
                        //theScale.x *= -1;
                        //clone.transform.localScale = theScale;
                        dustFXPrefabIzq.SetActive(true);
                    }

                    //if (!clone.activeSelf)
                    //{
                    //    //clone.SetActive(true);
                    
                    //}


                    dustFXOn = true;
                }

                if (!running && dustFXOn)
                {
                    //clone.SetActive(false);
                    dustFXPrefabDer.SetActive(false);
                    dustFXPrefabIzq.SetActive(false);
                    dustFXOn = false;
                }

			    var velX = tmpSpeed * (float)inputState.direction;
                //Debug.Log("input" + (float)inputState.direction);
                //if (!collisionState.onWall)
                //{
                    //Debug.Log("!collisionstate.onWall <<<<<<<<<<<<<<<<<<<<<<<<");
                    body2d.velocity = new Vector2(velX, body2d.velocity.y);
                //}
			    

		    }
            else
            {
            //    body2d.velocity = new Vector2(0.1f * (float)inputState.direction,body2d.velocity.y *0.1f);
                if (collisionState.standing && body2d.velocity.x !=0) {
                    //if (!run)
                    //{
                        body2d.velocity = new Vector2(0, body2d.velocity.y);
                        //clone.SetActive(false);
                        dustFXPrefabDer.SetActive(false);
                        dustFXPrefabIzq.SetActive(false);
                        dustFXOn = false;
                    //}
                    //else if (run)
                    //{
                    //    body2d.velocity = new Vector2(-(float)inputState.direction * 0.3f, body2d.velocity.y);
                    //}
                   //x z Debug.Log("body2dvel" + body2d.velocity);
                }
            
            }





        }
	}




    //void NormalizeSlope()
    //{
    //    //Instead of this technique of normalizing the slope , we use the built in effector from unity editor. Easier...


    //    // Attempt vertical normalization
    //    if (collisionState.standing)
    //    {
    //        LayerMask ground = LayerMask.GetMask("Ground");
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up+transform.position, 1f, ground);
            
    //        //Debug.DrawLine(transform.position, -Vector3.up + transform.position, Color.cyan, 0.2f);
    //        if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
    //        {
    //            //Rigidbody2D body = GetComponent<Rigidbody2D>();
    //            //Debug.Log("OnSlope");
    //            //Debug.DrawLine(transform.position, hit.point, Color.red, 0.2f);
    //            onSlope = true;
    //            // Apply the opposite force against the slope force 
    //            // You will need to provide your own slopeFriction to stabalize movement
    //            //body2d.velocity = new Vector2(body2d.velocity.x - (hit.normal.x * slopeFriction), body2d.velocity.y);

    //            //Move Player up or down to compensate for the slope below them
    //            //Vector3 pos = transform.position;
    //            //pos.y += -hit.normal.x * Mathf.Abs(body2d.velocity.x) * Time.fixedDeltaTime * (body2d.velocity.x - hit.normal.x > 0 ? 1 : -1);
    //            //body2d.gameObject.transform.position = pos;
    //        }
    //        else
    //        {
    //            onSlope = false;
    //        }
    //    }
    //}
}
