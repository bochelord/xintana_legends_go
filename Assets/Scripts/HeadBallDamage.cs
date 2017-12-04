using UnityEngine;
using System.Collections;

public class HeadBallDamage : MonoBehaviour {

    private GameObject player;
    private PlayerManager_Imported playerManager;
    public GameObject explosionPrefab;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        playerManager = player.GetComponent<PlayerManager_Imported>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Ground" || other.tag == "Platform")
        {
            if (other.gameObject.tag == "Player")
            {
                //Debug.Log("PAPOR");
                playerManager.ApplyDamage(10f,this.gameObject);
            }
            
            GameObject clone = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone, 2);
            Destroy(this.gameObject);
            //StartCoroutine(ApplyDamageToPlayer(other.transform));
        }
    }
}
