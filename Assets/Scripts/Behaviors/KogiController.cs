using UnityEngine;
using System.Collections;
using DG.Tweening;

public class KogiController : MonoBehaviour {

    public float movement_duration;
    public Vector2 escape_position;

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        //changeAnimState(0); // Idle animation.
	}

    public void changeAnimState(int value)
    {
        animator.SetInteger("AnimState", value);
    }


    public void kickStart()
    {
        this.transform.DOMove(escape_position, movement_duration, false);
    }



}
