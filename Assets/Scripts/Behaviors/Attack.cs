using UnityEngine;
using System.Collections;

public class Attack : AbstractBehavior {



    //public GameObject FlyingHitPrefab;
    public GameObject HitPrefabRight;
    public GameObject HitPrefabLeft;
    //public GameObject HitDamageEffect;
    public bool isAirAttacking;
    public bool isAttacking;
    public float AirAttackingDelay = 0.1f;
    public float AttackDelay = 0.1f;
    protected float lastAirAttackingTime = 0;
    protected float lastAttackTime = 0;

    private Animator animator;
    private PlayerManager playermanager;
    //private InputState inputState; <<<< This is taken from base.Awake! Redundant, pops an error!

    public float damage;

    //private float timelapse=0;

    public GameObject rightSwordCollider;
    //public GameObject leftSwordCollider;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        playermanager = GetComponent<PlayerManager>();
        //inputState = GetComponent<InputState>(); <<<< This is taken from base.Awake! Redundant, pops an error!
    }
	
	// Update is called once per frame
	void Update () {

        ///timelapse = Time.deltaTime;

        if (animator.GetInteger("EquippedItem") == 1 && !playermanager.xintanaisdead) { 

            var canAttack = inputState.GetButtonValue(inputButtons[0]);
            var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

            //we see if we are jumping or in the air at least...
            if (canAttack && holdTime < .1f && !collisionState.onWall && !collisionState.standing && !collisionState.onPlatform && !collisionState.onSlope && !isAirAttacking && Time.time - lastAirAttackingTime > AirAttackingDelay)
            {
                OnAirAttack();
                isAirAttacking = true;
                animator.SetBool("AirAttacking", true);
                playermanager.ChangeAnimationState(11);
                //timelapse = 0;

            }else  if (canAttack && holdTime < .1f && !collisionState.onWall && !isAttacking && !isAirAttacking && Time.time - lastAttackTime > AttackDelay)
            {
                if (collisionState.standing || collisionState.onPlatform || collisionState.onSlope)
                {
                    OnAttack();
                    isAttacking = true;
                    animator.SetBool("Attacking", true);
                    playermanager.ChangeAnimationState(10);
                    ToggleScripts(false);
                    //timelapse = 0;
                }
                
                
            }

            if (isAttacking && Time.time - lastAttackTime > 0.5f)
            {
                isAttacking = false;
                OnAttackFinished();
            }

            if (isAirAttacking && Time.time - lastAirAttackingTime > 0.5f)
            {
                isAirAttacking = false;

                OnAirAttackFinished();
            }
        }

        
	}


    public void OnAttackFinished()
    {
        isAttacking = false;
        //Debug.Log("ApagatePerraAireosa");
        animator.SetBool("Attacking", false);
        ToggleScripts(true);
    }

    public void OnAirAttackFinished()
    {
        isAirAttacking = false;
        animator.SetBool("AirAttacking", false);
    }

    protected virtual void OnAirAttack()
    {
        //var clone_prefab = Instantiate(FlyingHitPrefab);
        GameObject clone_prefab;
        if (inputState.direction == Directions.Right)
        {
            clone_prefab = Instantiate(HitPrefabRight);
        }
        else
        {
            clone_prefab = Instantiate(HitPrefabLeft);
        }

        AudioFXManager.Instance.PlayVoiceAttackingAir();
        lastAirAttackingTime = Time.time;
        clone_prefab.transform.position = transform.position;
        StartCoroutine(activateRightSwordCollider());
        
    }


    protected virtual void OnAttack()
    {
        GameObject clone_prefab;
        if (inputState.direction == Directions.Right)
        {
            clone_prefab = Instantiate(HitPrefabRight);
        }
        else
        {
            clone_prefab = Instantiate(HitPrefabLeft);
        }
        
        AudioFXManager.Instance.PlayVoiceAttacking();
        lastAttackTime = Time.time;
        clone_prefab.transform.position = transform.position;
        //playermanager.ChangeAnimationState(5);
        //if (inputState.direction == Directions.Right || inputState.direction == Directions.Left)
        //{
        StartCoroutine(activateRightSwordCollider());
        //animator.SetBool("Attacking", false);
        
        //}
        //else
        //{
        //    StartCoroutine(activateLeftSwordCollider());
        //}
    }

    IEnumerator activateRightSwordCollider()
    {
        damage = (int)Random.Range(5, 13); // We set the damage done randomly here...
        rightSwordCollider.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        rightSwordCollider.SetActive(false);
    }

    //IEnumerator activateLeftSwordCollider()
    //{
    //    leftSwordCollider.SetActive(true);
    //    yield return new WaitForSeconds(0.1f);
    //    leftSwordCollider.SetActive(false);
    //}
}
