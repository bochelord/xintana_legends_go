using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    public float life = 9;
	// Use this for initialization
	//void Start () {
	//	life = 9;
	//}
	

    /// <summary>
    /// Callback from Animation Event 
    /// </summary>
    public void OnAttackFinished()
    {
        AnimationToIdle();
    }

    /// <summary>
    /// Callback for Animation Event Air Attack
    /// </summary>
    public void OnAirAttackFinished()
    {
        this.GetComponent<Animator>().SetBool("Attacking", false);
        this.GetComponent<Animator>().SetInteger("AnimState", 20);
    }

    /// <summary>
    /// CAllback for Animation Event Wing Jump
    /// </summary>
    public void OnAirWingJumpFinished()
    {
        AnimationToIdle();
    }

    /// <summary>
    /// Sets the animation parameter to Idle Animation
    /// </summary>
    private void AnimationToIdle()
    {
        this.GetComponent<Animator>().SetBool("Attacking", false);
        this.GetComponent<Animator>().SetInteger("AnimState", 0);
    }


    public void ReceiveDamage(float damage)
    {
        life -= damage;
    }
}
