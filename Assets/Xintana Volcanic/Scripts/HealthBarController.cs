using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {



    public PlayerManager playermanager;

    private Slider bar;
	// Use this for initialization
	void Start () {
        bar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

        if (playermanager != null)
        {
            bar.value = playermanager.life  / playermanager.GetMaxLife();
        }
        

	}
}
