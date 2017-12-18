using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SmoothCamera : AbstractBehavior
{
    
    public float interpVelocity;
    public float interpVMultiplier;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            //if (!target.GetComponent<CollisionState>().onPlatform)
            //{
                interpVelocity = targetDirection.magnitude * interpVMultiplier;
            //}
            //else
            //{
            //    interpVelocity = targetDirection.magnitude;
            //}
            

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            if (target.GetComponent<InputState>().direction == Directions.Right)
            {
                
                //transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
                transform.DOMove(targetPos + offset, 0.1f, false);
            }
            else
            {
                //transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x + offset.x*-1, targetPos.y + offset.y, targetPos.z), 0.25f);
                transform.DOMove(new Vector3(targetPos.x + offset.x*-1, targetPos.y + offset.y, targetPos.z), 0.1f, false);
            }
            

        }
    }
}