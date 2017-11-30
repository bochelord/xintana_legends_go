using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ZazucController : MonoBehaviour {


    public int life = 3;
    public Transform spawnTransform1;
    public Transform spawnTransform2;
    public GameObject fireball;
    public GameObject zazucParent;
    public SpriteRenderer zazucSprite;
    public GameObject zazucDeadPrefab;
    public float fire_delay = 2.0f;
    private float nextFire = 0f;
    public float distanceFireballTravel = 150f;
    public float fireballSpeed = 10f;
    public float timeToDestroy = 10f;
    public Color HitColor = new Color(1f, 0.21f, 0.21f, 1f);
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fire_delay;
            GameObject clone = Instantiate(fireball, spawnTransform1.position, Quaternion.identity) as GameObject;
            GameObject clone2 = Instantiate(fireball, spawnTransform2.position, Quaternion.identity) as GameObject;

            clone.transform.DOMoveX(spawnTransform1.position.x - distanceFireballTravel, fireballSpeed, false).SetSpeedBased();
            clone2.transform.DOMoveX(spawnTransform2.position.x + distanceFireballTravel, fireballSpeed, false).SetSpeedBased();

            Destroy(clone, timeToDestroy);
            Destroy(clone2, timeToDestroy);
        }

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            if (life > 0)
            {

                life -= (int)player.GetComponent<Attack>().damage;
                StartCoroutine(HitColorEffect());
                //ApplyDamage(player.GetComponent<Attack>().damage);
            }
            else
            {
                GameObject clone = Instantiate(zazucDeadPrefab, zazucParent.transform.position, Quaternion.identity) as GameObject;
                zazucParent.SetActive(false);
                Destroy(clone, 2f);
            }


        }
    }

    public IEnumerator HitColorEffect()
    {
        //Beingdamaged = true;
        Color orig_color = zazucSprite.color;
        
        zazucSprite.material.color = HitColor;
        //bossEyesRenderer.material.color = new Color(1f, 0.21f, 0.21f, 1f);
        yield return new WaitForSeconds(0.2f);

        zazucSprite.material.color = orig_color;
        //bossEyesRenderer.material.color = orig_color;
        //Beingdamaged = false;

    }
    

}
