using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XintanaReferencePosition : MonoBehaviour {

    public Transform xintana;


    void Awake()
    {
        //xintana.localPosition = Camera.main.WorldToScreenPoint
        //    (new Vector3(this.transform.position.x, this.transform.position.y, 0));
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        xintana.position = Camera.main.ScreenToWorldPoint
            (new Vector3(this.transform.position.x, this.transform.position.y, 0));
        Debug.Log("Transform is moving");
    }
}
