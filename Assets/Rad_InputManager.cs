using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rad_InputManager : MonoBehaviour {

    public GameObject UITouchPrefab;
	
	
	// Update is called once per frame
	void Update () {
		

        if (Input.GetButtonUp("Fire1"))
        {
            Instantiate(UITouchPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f,0f,10f),Quaternion.identity);
        }

	}
}
