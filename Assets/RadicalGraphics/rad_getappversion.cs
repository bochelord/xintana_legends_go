using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Read version and print text version on screen
/// </summary>
public class rad_getappversion : MonoBehaviour {

	// Use this for initialization
	void Start () {    
            this.GetComponent<Text>().text = "version "+Application.version;
        }
}