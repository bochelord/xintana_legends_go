using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

    public string targetTag = "Player";
    public GameObject pickup_Fx;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == targetTag)
        {
            OnCollect(target.gameObject);
            
        }
    }


    protected virtual void OnCollect(GameObject target)
    {
        var clone = Instantiate(pickup_Fx);
        clone.transform.position = target.transform.position;
        OnDestroy();
    }

    protected virtual void OnDestroy()
    {
        Destroy(gameObject);
    }


}
