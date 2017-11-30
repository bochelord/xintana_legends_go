using UnityEngine;
using System.Collections;

public class WeaponItem : Collectable {

    public int itemID = 1;

    protected override void OnCollect(GameObject target)
    {
        base.OnCollect(target);
        var equipBehavior = target.GetComponent<Equip>();
        if (equipBehavior != null)
        {
            equipBehavior.currentItem = itemID;
        }
    }



}
