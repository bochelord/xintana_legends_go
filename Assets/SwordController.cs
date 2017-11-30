using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {

    public GameObject swordHitprefab;
    private int damagePoints;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            GameObject clone = Instantiate(swordHitprefab, other.transform.position, Quaternion.identity) as GameObject;
            Destroy(clone, 2f);

            damagePoints = (int)Random.Range(5, 13);        

            PopUpText.ShowMoneyPopup(damagePoints.ToString(), 5f, Camera.main.WorldToScreenPoint(new Vector2(other.transform.position.x,other.transform.position.y+0.5f)), 2f);
        }
    }

}
