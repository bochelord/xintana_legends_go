using UnityEngine;
using System.Collections;

public class startLevelSavepoint : MonoBehaviour {

    public int level;
    private GameObject player;
	// Use this for initialization
	void Start () {

        player = GameObject.FindWithTag("Player");
        
	}
	
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag=="Player"){
            SaveCheckPoint();
            this.gameObject.SetActive(false);
        }
    }

    void SaveCheckPoint()
    {
        if (SaveManager.xintanaLife < 50)
        {
            player.GetComponent<PlayerManager>().xintanaLife = 50;
            SaveManager.xintanaLife = player.GetComponent<PlayerManager>().xintanaLife;
        }
        else
        {
            player.GetComponent<PlayerManager>().xintanaLife = SaveManager.xintanaLife;
        }
        
        
        SaveManager.checkpointXPosition = player.transform.position.x;
        SaveManager.checkpointYPosition = player.transform.position.y;
        SaveManager.level = level;
        if (level <= 2)
        {
            player.GetComponent<Animator>().SetInteger("EquippedItem", 0);
            SaveManager.sword = 0;
        }
        else
        {
            player.GetComponent<Animator>().SetInteger("EquippedItem", 1);
            SaveManager.sword = 1;
        }
        //if (level == 5) // Final Boss Fight.
        //{
        //    if (SaveManager.finalBossStarted == 0)
        //        SaveManager.finalBossStarted = 1;
        //}

        SaveManager.SaveData();
        //audioFX.Play();
        Debug.Log("DataSaved as:\n-- Xintana Life: " + SaveManager.xintanaLife + "\n-- X Position: " + player.transform.position.x + "\n-- Y Position: " + player.transform.position.y + "\n-- Level: " + SaveManager.level);
        //Debug.Log("-- Xintana Life: " + SaveManager.xintanaLife);
        //Debug.Log("-- X Position: " + player.transform.position.x);
        //Debug.Log("-- Y Position: " + player.transform.position.y);
        Debug.Log("-- Level: " + SaveManager.level);
        Debug.Log("-- Final Boss Started: " + SaveManager.finalBossStarted);
        
        

        //StartCoroutine(ChangeParticlesColor());

        //targetScreenPos = Camera.main.WorldToScreenPoint(fatherTransform.position);
        //recttransform = this.gameObject.GetComponent<RectTransform>();
        //recttransform.anchoredPosition = new Vector2(targetScreenPos.x + horizontalOffset - Screen.width / 2, targetScreenPos.y + verticalOffset - Screen.height / 2);

        //PopUpText.ShowMoneyPopup("Game Saved", 100f, targetScreenPos, 1f);
    }
}
