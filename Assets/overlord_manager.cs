using UnityEngine;
using System.Collections;
using System;

public class overlord_manager : MonoBehaviour {

    public Transform explosion_big;
    
    // Use this for initialization
	void Start () {
        StartCoroutine(Kickme());
	}
	
	// Update is called once per frame
	void Update () {

        
	}


    IEnumerator Kickme()
    {

        for (int i = 0; i < 10; i++) {


            float x_aleatoria = UnityEngine.Random.Range(-1f, 1f);
            float y_aleatoria = UnityEngine.Random.Range(-1f, 1f);

            Transform tempobj = Instantiate(explosion_big, new Vector3(x_aleatoria, y_aleatoria, 0), Quaternion.identity) as Transform;

            yield return new WaitForSeconds(0.2f);

            tempobj.gameObject.SetActive(false);
        }

    }
}
