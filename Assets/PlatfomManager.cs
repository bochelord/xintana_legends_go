using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlatfomManager : MonoBehaviour {

    private Transform platform;         
    [Tooltip("The distance to travel Verticaly. +/-")]
    public float verticalDistance;      
    [Tooltip("The distance to travel Horizontally. +/-")]
    public float horizontalDistance;
    [Tooltip("The constant speed to Destination.")]
    public float speedToDestination;
    [Tooltip("The constant speed back to Origin.")]
    public float speedToOrigin;

    public float distance;
    private bool goingToDestination = true;
    //private float distance;
    //private bool goingToOrigin = false;

    private Vector3 originalPosition;
    private Vector3 destinationPosition;
	
    void Start () {
        platform = this.transform;
        originalPosition = platform.position;
        destinationPosition = new Vector3(originalPosition.x+horizontalDistance, originalPosition.y+verticalDistance, 0);
	}
	
	void Update () 
    {
        if (goingToDestination)
        {
            platform.DOMove(destinationPosition, speedToDestination, false).SetSpeedBased() ; // TEST WITH TRUE.
            distance = Vector3.Distance(this.transform.position, destinationPosition);
        }
        else
        {
            platform.DOMove(originalPosition, speedToOrigin, false).SetSpeedBased(); // TEST WITH TRUE.
            distance = Vector3.Distance(this.transform.position, originalPosition);
        }
        if (distance <=  0.1)
        {
            goingToDestination = !goingToDestination;
        }
	}

}
