using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    public float life;
	// Use this for initialization
	void Start () {
		life = 9;
	}
	

    /// <summary>
    /// Callback from Animation Event 
    /// </summary>
    public void OnAttackFinished()
    {
        //    isAttacking = false;
        //    //Debug.Log("ApagatePerraAireosa");
        this.GetComponent<Animator>().SetBool("Attacking", false);
        this.GetComponent<Animator>().SetInteger("AnimState", 0);
        //    ToggleScripts(true);
    }
}
