using UnityEngine;
using System.Collections;

public class CamerasManager : MonoBehaviour {

    private SmoothCamera smoothCamera;
    private GameCamera gameCarema;
    private GameObject player;
    private CollisionState collisionState;

	// Use this for initialization
	void Start () {
        
        player = GameObject.Find("Player");
        collisionState = player.GetComponent<CollisionState>();

        smoothCamera = GetComponent<SmoothCamera>();
        gameCarema = GetComponent<GameCamera>();

	}
	
	// Update is called once per frame
	void Update () {
        if (collisionState.onPlatform)
        {
            gameCarema.enabled = true;
            smoothCamera.enabled = false;
        }
        else
        {
            gameCarema.enabled = false;
            smoothCamera.enabled = true;
        }
	}
}
