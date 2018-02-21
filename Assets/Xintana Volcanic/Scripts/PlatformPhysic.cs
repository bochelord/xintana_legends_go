using UnityEngine;
using System.Collections;

public class PlatformPhysic : MonoBehaviour
{

    private GameObject player;
    private bool playerOnPlatform;
    //public float speed = 2f;

    void Start()
    {

        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    void OnTriggerStay2D(Collider2D other){
         if(other.gameObject.tag == "Player"){
             player.transform.parent = this.transform;
 
         }
     }
 
    void OnTriggerExit2D(Collider2D other){
        
        if(other.gameObject.tag == "Player"){
             player.transform.parent = null;
             
         }
     }  
    //void FixedUpdate()
    //{
    //    //if(thisTerrain == TerrainType.PlatformMover)
    //    //{

    //    float weight = Mathf.Cos(Time.time * speed * 2 * Mathf.PI) * 0.5f + 0.5f;
    //    //Vector3 newPosition = targetA.position *weight + targetB.position*(1-weight);
    //    Vector3 changeRate = -this.transform.position; // + newPosition;
    //    this.transform.position = changeRate;//newPosition;

    //    // Following code for moving the player
    //    if (playerOnPlatform)
    //    {
    //        player.transform.position += new Vector3(changeRate.x, 0, changeRate.z);
    //    }
    //    //}
    //}

    //void OnTriggerEnter(Collider hit)
    //{
    //    if (hit.gameObject == player)
    //    {
    //        print("Player is riding the platform!");
    //        playerOnPlatform = true;
    //    }
    //}

    //void OnTriggerExit(Collider hit)
    //{
    //    if (hit.gameObject == player)
    //    {
    //        print("Player is off the paltform =/");
    //        playerOnPlatform = false;
    //    }
    //}
}