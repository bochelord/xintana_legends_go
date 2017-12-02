using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadLauncher : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {

        yield return new WaitForSeconds(12);
        SceneManager.LoadScene(1);
	}
	
	
}
