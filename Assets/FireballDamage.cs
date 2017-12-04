using UnityEngine;
using System.Collections;

public class FireballDamage : MonoBehaviour {


    private GameObject player;
    private PlayerManager_Imported playermanager;
    public GameObject explosionPrefab;
    

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        playermanager = player.GetComponent<PlayerManager_Imported>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Wall" || other.tag == "ground")
        {
            //Debug.Log("toco" + other.gameObject.name);

            if (other.tag == "Player")
            {
                playermanager.ApplyDamage(10f,this.gameObject);
            }
            GameObject clone = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone, 1f);
            Destroy(this.gameObject);
        }
    }
}
