using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class MakulaController : MonoBehaviour {

    public Transform neutralPosition;

    public Transform leftPosition1;
    public Transform leftPosition2;
    public Transform leftPosition3;
    public Transform leftPosition4;
    
    public Transform rightPosition1;
    public Transform rightPosition2;
    public Transform rightPosition3;
    public Transform rightPosition4;

    //public PhysicsMaterial2D bouncingMaterial;
    public Animator makulaAnimator;
    public GameObject particleCombat;

    private bool startCombat;
    //public bool leftAttack;
    //public bool rightAttack;
    //public float force;
    private Rigidbody2D rb2d;

    private Sequence sequence;
    private bool onlyOnce = true;
    private bool leftAttackCompleted = false;
    private bool rightAttackCompleted = true;

    public float bossLife;
    public GameObject bossHealtBarObject;
    public Scrollbar bossHealthBar;
    private GameObject player;
    private float maxBossLife;
    private Color orig_color;
    private bool beingdamaged;
    public GameObject deathParticle;

    public GameObject dialogAfterCombat;
    public float makulaSpeed;
    public float makulaWaitingTime;
    public GameObject kogi;

    private bool hitable;

	void Start () {
        maxBossLife = bossLife;
        player = GameObject.FindWithTag("Player");
        rb2d = this.GetComponent<Rigidbody2D>();
        onlyOnce = true;
        orig_color = this.GetComponent<SpriteRenderer>().material.color;
        hitable = true;
        StartCoroutine(StartCombat());
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            if (bossLife > 0)
            {
                ApplyDamage(player.GetComponent<Attack>().damage);
            }
        }
    }

    void Update()
    {
        if (beingdamaged)
        {
            this.GetComponent<SpriteRenderer>().material.color = orig_color;
        }
    }

	// Update is called once per frame
	void FixedUpdate () {

        if (startCombat)
        {
            if (onlyOnce)
            {
                if (!leftAttackCompleted)
                {
                    StartCoroutine(LeftAttack());
                        
                }
                if (!rightAttackCompleted)
                {
                    StartCoroutine(RightAttack());
                        
                }
            }
        }
	}

    public IEnumerator LeftAttack()
    {
        onlyOnce = false;

        makulaAnimator.SetInteger("AnimState", 1);
        yield return new WaitForSeconds(1f);
        makulaAnimator.SetInteger("AnimState", 2);
        sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOMove(leftPosition1.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(leftPosition2.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(leftPosition3.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(leftPosition4.transform.position, makulaSpeed, false));
        yield return new WaitForSeconds(2);
        this.gameObject.transform.eulerAngles = new Vector2(0, 180);
        makulaAnimator.SetInteger("AnimState", 0);
        yield return new WaitForSeconds(makulaWaitingTime);


        makulaAnimator.SetInteger("AnimState", 1);
        yield return new WaitForSeconds(1f);
        makulaAnimator.SetInteger("AnimState", 2);
        sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOMove(leftPosition3.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(leftPosition2.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(leftPosition1.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(neutralPosition.transform.position, makulaSpeed, false));
        yield return new WaitForSeconds(makulaWaitingTime);
        
        makulaAnimator.SetInteger("AnimState", 0);
        yield return new WaitForSeconds(1);

        leftAttackCompleted = true;
        rightAttackCompleted = false;
        onlyOnce = true;

    }

    public IEnumerator RightAttack()
    {
        onlyOnce = false;

        makulaAnimator.SetInteger("AnimState", 1);
        yield return new WaitForSeconds(1f);
        makulaAnimator.SetInteger("AnimState", 2);
        sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOMove(rightPosition1.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(rightPosition2.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(rightPosition3.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(rightPosition4.transform.position, makulaSpeed, false));
        yield return new WaitForSeconds(2);
        this.gameObject.transform.eulerAngles = new Vector2(0, 0);
        makulaAnimator.SetInteger("AnimState", 0);
        yield return new WaitForSeconds(makulaWaitingTime);


        makulaAnimator.SetInteger("AnimState", 1);
        yield return new WaitForSeconds(1f);
        makulaAnimator.SetInteger("AnimState", 2);
        sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOMove(rightPosition3.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(rightPosition2.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(rightPosition1.transform.position, makulaSpeed, false));
        sequence.Append(this.transform.DOMove(neutralPosition.transform.position, makulaSpeed, false));
        yield return new WaitForSeconds(makulaWaitingTime);
        
        makulaAnimator.SetInteger("AnimState", 0);
        yield return new WaitForSeconds(1);

        leftAttackCompleted = false;
        rightAttackCompleted = true;
        onlyOnce = true;
    }


    public IEnumerator StartCombat()
    {
        bossHealtBarObject.SetActive(true);
        particleCombat.SetActive(true);
        yield return new WaitForSeconds(3);
        startCombat = true;
    }


    void ApplyDamage(float damage)
    {
        bossLife -= damage;
        bossHealthBar.size -= damage / maxBossLife;
        StartCoroutine(HitColorEffect());
        if (bossLife <= 0)
        {
            StopAllCoroutines();
            makulaAnimator.SetInteger("AnimState", 3);
            bossHealtBarObject.SetActive(false);
            Instantiate(deathParticle, this.gameObject.transform.position, Quaternion.identity);
            particleCombat.SetActive(false);
            kogi.SetActive(true);
            dialogAfterCombat.SetActive(true);
            //childCollider.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator HitColorEffect()
    {
        if (hitable)
        {

            hitable = false;        
            beingdamaged = true;
            //if (this.GetComponent<SpriteRenderer>().material.color == new Color(1f, 0.21f, 0.21f, 1f))
            //{
                orig_color = this.GetComponent<SpriteRenderer>().material.color;
            //}
        
            this.GetComponent<SpriteRenderer>().material.color = new Color(1f, 0.21f, 0.21f, 1f);
            yield return new WaitForSeconds(0.3f);

            this.GetComponent<SpriteRenderer>().material.color = orig_color;
        
            beingdamaged = false;
            hitable = true;
        }
    }
}
