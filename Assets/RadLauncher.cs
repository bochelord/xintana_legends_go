using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadLauncher : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {

        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("LoadingScreen");
	}
	
	
}
