using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarControllerBoss : MonoBehaviour
{



    public LevelManager levelManager;

    private Scrollbar bar;
    private float initialValue;
	// Use this for initialization
	void Start () {
        bar = GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () {

        if (levelManager.enemyController != null)
        {
            bar.size = (levelManager.enemyController.GetLife()*0.1f) / initialValue;
        }
	}

    public void SetScrollbarValue()
    {
        if (levelManager.enemyController)
        {
            initialValue = levelManager.enemyController.GetLife() * 0.1f;
        }
    }
}
