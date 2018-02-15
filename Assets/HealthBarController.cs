using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {



    public PlayerManager playermanager;

    private Scrollbar bar;
	// Use this for initialization
	void Start () {
        bar = GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () {

        if (playermanager != null)
        {
            bar.size = playermanager.life  / playermanager.GetMaxLife();
        }
        

	}
}
