using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarControllerBoss : MonoBehaviour
{



    public LevelManager levelManager;

    private Slider bar;
    private float initialValue;
	// Use this for initialization
	void Start () {
        bar = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

        if (levelManager.enemyController != null)
        {
            bar.value = (levelManager.enemyController.GetLife()) / initialValue;
        }
	}

    public void SetScrollbarValue()
    {
        if (levelManager.enemyController)
        {
            initialValue = levelManager.enemyController.GetLife();
        }
    }
}
