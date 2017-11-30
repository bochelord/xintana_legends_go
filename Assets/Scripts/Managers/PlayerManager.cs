using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour {

	private InputState inputState;
	private Walk walkBehavior;
    private Duck crouchBehavior;
    private Attack attackBehavior;
    private WallJump walljumpBehavior;
    private Jump jumpBehavior;
    private LookUp lookBehavior;
    private LoadLevelData loadLevelData;
    [HideInInspector]
	public Animator animator;
	private CollisionState collisionState;  
    public bool _crounchingNow = false;
    public bool _attackingNow = false;
    private GameObject platform;
    public Scrollbar healthbar;
    public float xintanaLife;


    private bool Beingdamaged = false;
    private float timepassed = 0;
    private float footstepSpeed = 0.35f;
    private int lastfootstepplayed = 2;
    //private bool crouchplayed = false;
    public bool xintanaisdead = false;
    public SmoothCamera smoothCamera;
    public GameCamera gameCamera;
    private bool hitable;
    private GameObject damagedBy_obj;


	void Awake(){
		inputState = GetComponent<InputState> ();
		walkBehavior = GetComponent<Walk> ();
		animator = GetComponent<Animator> ();
		collisionState = GetComponent<CollisionState> ();
        crouchBehavior = GetComponent<Duck>();
        attackBehavior = GetComponent<Attack>();
        walljumpBehavior = GetComponent<WallJump>();
        jumpBehavior = GetComponent<Jump>();
        lookBehavior = GetComponent<LookUp>();

        //loadLevelData = GameObject.Find("PermanentData").GetComponent<LoadLevelData>();
        loadLevelData = LoadLevelData.instance;
	}

	// Use this for initialization
	void Start () {
        hitable = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if (!Beingdamaged && !xintanaisdead){ //if we are on the damage animation we wait till is over...



            ///Idle Animation
            /////////////////
            if ((collisionState.standing || collisionState.onPlatform || collisionState.onSlope ) && (inputState.absVelX <0.25f && animator.GetInteger("AnimState") != 0 && !crouchBehavior.ducking)) {
			    ChangeAnimationState(0); // IDLE Animation

                if (jumpBehavior) { //We check it just to be sure it can be set
                    jumpBehavior.onDoubleJump = false;
                }
            
                AudioFXManager.Instance.PlayIdle();
                //Debug.Log("Disparado Idle sound");
		    }


            ///Walk/Run Animation
            /////////////////
            if (inputState.absVelX > 0.25f && (collisionState.standing || collisionState.onPlatform || collisionState.onSlope))
            {
			    ChangeAnimationState(1);
                timepassed += Time.fixedDeltaTime;
                //Debug.Log(timepassed);

                if (walkBehavior.running)
                {
                    animator.speed = walkBehavior.runMultiplier;
                    footstepSpeed = 0.17f;
                }
                else
                {
                    animator.speed = 1;
                    footstepSpeed = 0.35f;
                }

                if (timepassed > footstepSpeed)
                {
                    if (lastfootstepplayed == 2)
                    {
                        AudioFXManager.Instance.PlayVoiceFootsteps1();
                        lastfootstepplayed = 1;
                        //Debug.Log("footstep1");
                        timepassed = 0;
                    }
                    else
                    {
                        AudioFXManager.Instance.PlayVoiceFootsteps2();
                        lastfootstepplayed = 2;
                        //Debug.Log("footstep2");
                        timepassed = 0;
                    }
                    
                }
            }
            else
            {
                timepassed = 0;
            }


            //Jump Animation
            ////////////////
            if (inputState.vSpeed > 0.85f && !collisionState.standing && !collisionState.onPlatform && !collisionState.onSlope && !attackBehavior.isAirAttacking)
            {
                if (animator.GetInteger("EquippedItem") == 0 )
                {
                    ChangeAnimationState(6); //simple jump animation without sword
                }
                else if (jumpBehavior.onDoubleJump)
                {
                    ChangeAnimationState(8); //double jump animation (looping with sword and wings)
                }
                else
                {
                    ChangeAnimationState(7); //simple jump animation (with sword)
                }

            }


            //Look Up Behavior
            ////////////////
            if (lookBehavior.canLook)
            {
                if (smoothCamera.isActiveAndEnabled)
                {
                    smoothCamera.offset.y = 1.25f;
                }
                if (gameCamera)
                {
                    if (gameCamera.isActiveAndEnabled)
                    {
                        gameCamera.offset.y = 1.45f;
                    }
                }
            }
            else
            {
                smoothCamera.offset.y = 0.75f;
                if (gameCamera)
                {
                    gameCamera.offset.y = 0.95f;
                }
            }

            //Crouch Animation
            ////////////////
            if (crouchBehavior.ducking)
            {
                if (animator.GetInteger("AnimState") != 2 && !_crounchingNow && !animator.GetBool("CrouchFinishing"))
                {
                    AudioFXManager.Instance.PlayVoiceCrouch();
                    //crouchplayed = true;
                    //Debug.Log("diparadocrouch");
                }
                ChangeAnimationState(2);

                if (smoothCamera.isActiveAndEnabled)
                {
                    smoothCamera.offset.y = 0.10f;
                }
                if (gameCamera)
                {
                    if (gameCamera.isActiveAndEnabled)
                    {
                        gameCamera.offset.y = 0.10f;
                    }
                }
                
                
                walkBehavior.dustFXPrefabDer.SetActive(false);//We set off the dust from running while crouching!
                walkBehavior.dustFXPrefabIzq.SetActive(false);
                
                _crounchingNow = true;
            }

            if (!crouchBehavior.ducking && _crounchingNow)
            {
                //we just finished crouching so we fire the crouchingfinishing animation
                CrouchFinishing(1);
                _crounchingNow = false;
                smoothCamera.offset.y = 0.75f;
                if (gameCamera)
                {
                    gameCamera.offset.y = 0.95f;
                }
                
            }




            //Attack Animation
            ////////////////
            //if (attackBehavior.isAttacking)
            //{
            //    animator.SetBool("Attacking", true);
                
                
            //}
            //else
            //{
            //    animator.SetBool("Attacking", false);
            //}


            ////AirAttack Animation
            //////////////////
            //if (attackBehavior.isAirAttacking)
            //{
            //    animator.SetBool("AirAttacking", true);
            //}
            //else
            //{
            //    animator.SetBool("AirAttacking", false);
            //}

            //WallSlide or Walljump Animation
            ////////////////
            if (collisionState && walljumpBehavior)
            {
                if (collisionState.onWall && !walljumpBehavior.jumpingOffWall && !collisionState.standing && !collisionState.onSlope)
                {
                    ChangeAnimationState(9);//Xintana_Wall_Sliding
                }
                else if (!collisionState.onWall && walljumpBehavior.jumpingOffWall && !collisionState.standing)
                {
                    ChangeAnimationState(7);//JumpWithSword
                }
            }


            //if (attackBehavior.isAttacking && !_attackingNow)
            //{
            //    ChangeAnimationState(5);

            //    _attackingNow = true;

            //    Debug.Log("Anim5 called");
            //}

            //if (!attackBehavior.isAttacking && _attackingNow)
            //{
            //    //we just finished the attack
            //}


            if (xintanaLife <= 0 && !xintanaisdead)
            {
                AudioFXManager.Instance.PlayDead();
                Time.timeScale = 0.35f;
                ChangeAnimationState(4);

                //We create a custom Analytics Event to see where people has died
                // We collect the following info
                // Position on level (x,y)
                // Level (nhumber)
                // LastApplyDamageByEnemy (enemyName)
                // EnemyPosition (enemyPosition)
                AnalyticsTriggerDead();


                //this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                StartCoroutine(FunctionLibrary.CallWithDelay(XintanaDead,1f));
                xintanaisdead = true;
                
                //XintanaDead();
            }

        }

        //if (collisionState.onPlatform && inputState.absVelX < 0.25f)
        //{
        //    ChangeAnimationState(0);
        //}

		//animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;
	}

    //void FixedUpdate()
    //{
    //    if (collisionState.onPlatform)
    //    {
    //        //AdjustPlayerVelocityOnPlatform();

    //        //if (collisionState.GetCurrentPlatform().tag == "FallingPlatform")
    //        //{

    //        //    Invoke("ThrowPlatform", 1);
    //        //}
    //        //else
    //        //{
    //        FatherPlatform(true);
    //        //}
    //    }
    //}


    private void AnalyticsTriggerDead()
    {
        //We create a custom Analytics Event to see where people has died
        // We collect the following info
        // Position on level (x,y)
        // Level (string)
        // LastApplyDamageByEnemy (enemyName)
        // EnemyPosition (enemyPosition)

        Vector2 position_dead = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        string level = loadLevelData.GetSelectedLevelName();
        string enemy;
        Vector2 enemy_position;

        if (damagedBy_obj)
        {
            enemy = damagedBy_obj.name;
            enemy_position = damagedBy_obj.transform.position;
        }
        else
        {
            enemy = "null cause object was destroyed!!";
            enemy_position = new Vector3(0,0,0);
        }
        
        

        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
              {
                { "position_dead", position_dead },
                { "level", level },
                { "enemy", enemy },
                {"enemy_position", enemy_position}
              });

        Debug.Log(position_dead);
        Debug.Log(level);
        Debug.Log(enemy);
        Debug.Log(enemy_position);
    }




	public void ChangeAnimationState(int value){
		animator.SetInteger ("AnimState", value);
	}

    public void XintanaDead()
    {
        
        Time.timeScale = 1f;



        if (SaveManager.level == 5)
        {
            SaveManager.finalBossStarted = 1;
            SaveManager.SaveData();
        }
        
        SaveManager.LoadData();
        this.transform.position = new Vector2(SaveManager.checkpointXPosition, SaveManager.checkpointYPosition);
        xintanaLife = SaveManager.xintanaLife;
        healthbar.size = xintanaLife;
        xintanaisdead = false;
        Beingdamaged = false;
        ChangeAnimationState(0);
        if (SaveManager.level == 5)
        {
            Application.LoadLevel(5);
        }
        //this.gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }


    public void CrouchFinishing(int value)
    {
        if (value > 0) {
        animator.SetBool("CrouchFinishing",true);
        }

        if (value == 0)
        {
            animator.SetBool("CrouchFinishing", false);
        }

        //crouchplayed = false;
    }


    //public bool CrouchingFinishingEnds
    //{
    //    get { return _crounchingNow;}

    //    set {
    //        //_crounchingNow = false;
    //        CrouchFinishing(false);
    //        animator.SetBool("CrouchFinishing", false);
    //    }
    //}


    //void ThrowPlatform()
    //{
    //    platform = collisionState.GetCurrentPlatform();
    //    Debug.Log("We are in a falling platform: " + platform.name);
    //    //yield return new WaitForSeconds(1f);
    //    Rigidbody2D rb2d = platform.GetComponent<Rigidbody2D>();
    //    rb2d.isKinematic = false;
    //    //rb2d.constraints = RigidbodyConstraints2D.None;
    //    //rb2d.gravityScale = 3f;
    //}

    //void AdjustPlayerVelocityOnPlatform()
    //{
    //    platform = collisionState.GetCurrentPlatform();
    //    Rigidbody2D platformBody = platform.GetComponent<Rigidbody2D>();
    //    Rigidbody2D body2d = this.GetComponent<Rigidbody2D>();
    //    Debug.Log("HOLA???");

    //    if (platform != null)
    //    {
    //        body2d.velocity += new Vector2(body2d.velocity.x, platformBody.velocity.y);
    //        Debug.Log("velocity.y: " + body2d.velocity.y);
    //    }
    //}

    //void FatherPlatform(bool onPlatform)
    //{
    //    platform = collisionState.GetCurrentPlatform();
    //    Rigidbody2D platformBody = platform.GetComponent<Rigidbody2D>();
    //    Rigidbody2D body2d = this.GetComponent<Rigidbody2D>();

    //    Transform transformPlayer = this.GetComponent<Transform>();
    //    float move = Input.GetAxis("Horizontal");

    //    if (onPlatform)
    //    {
    //        //transformPlayer.parent = platform.transform;
    //        body2d.velocity += new Vector2(0, platformBody.velocity.y);
    //        //body2d.MovePosition(platform.transform.position + platform.transform.forward * Time.deltaTime );
    //    }
    //    else
    //    {
    //        transformPlayer.parent = null;
    //    }

    //}

    public void ApplyDamage(float damage, GameObject damageBy)
    {

        if (xintanaisdead == false) { 
            xintanaLife -= damage;
            StartCoroutine(HitColorEffect());
            healthbar.size = xintanaLife / 100f;

            //damagedBy_obj = damageBy;
            Debug.Log("Gameobject " + damageBy.name + " dealt " + damage + " damage points.");
            int num = Random.Range(1, 3);
            //Debug.Log("hurt" + num);
            if (num == 1)
            {
                AudioFXManager.Instance.PlayHurt1();
            }
            else if (num == 2)
            {
                AudioFXManager.Instance.PlayHurt2();
            }

            ChangeAnimationState(3);

        }

    }

    public IEnumerator HitColorEffect()
    {
        if (hitable) {
            hitable = false;
            Beingdamaged = true;
            Color orig_color = this.GetComponent<Renderer>().material.color;

            this.GetComponent<Renderer>().material.color = new Color(1f, 0.21f, 0.21f, 1f);
            yield return new WaitForSeconds(0.2f);

            this.GetComponent<Renderer>().material.color = orig_color;
            Beingdamaged = false;
            hitable = true;
        }
    }
}
