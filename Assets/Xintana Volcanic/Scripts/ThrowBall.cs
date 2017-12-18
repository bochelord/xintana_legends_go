using UnityEngine;
using System.Collections;

public class ThrowBall : MonoBehaviour {

    public GameObject ball;
    public Transform spawnTransform;
    public float throwDelay;
    private float nextBall = 0f;
    public float timeToDestroyBall;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextBall)
        {
            nextBall = Time.time + throwDelay;
            GameObject cloneBall = Instantiate(ball, spawnTransform.position, Quaternion.identity) as GameObject;
            Destroy(cloneBall, timeToDestroyBall);
            //playerManager.ApplyDamage(damage);
            //rb2d.velocity = new Vector2(Random.Range(velocityXMin, velocityXMax), Random.Range(velocityYMin, velocityYMax));
        }
	}
}
