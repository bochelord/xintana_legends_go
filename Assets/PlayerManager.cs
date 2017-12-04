using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAttackFinished()
    {
        //    isAttacking = false;
        //    //Debug.Log("ApagatePerraAireosa");
        this.GetComponent<Animator>().SetBool("Attacking", false);
        this.GetComponent<Animator>().SetInteger("AnimState", 0);
        //    ToggleScripts(true);
    }
}
