using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System;

public class overlord_manager : MonoBehaviour
{

    #region variables
    public Transform explosion_big;

    public Transform cielo;
    public Transform mountains;

    public Camera MainCamera;

    public Image blackness; 
    public Image Logo;
    #endregion variables



    // Use this for initialization
	void Start () {
        //StartCoroutine(Kickme());

        //Intro_Sequence();

        StartCoroutine(Intro_Sequence());
	}
	
	// Update is called once per frame
	void Update () {

        
	}


    IEnumerator Kickstart_explosions()
    {

        for (int i = 0; i < 10; i++) {

            float x_aleatoria = UnityEngine.Random.Range(-2f, 2f);
            float y_aleatoria = UnityEngine.Random.Range(-2f, 2f);

            Transform tempobj = Instantiate(explosion_big, new Vector3(x_aleatoria, y_aleatoria, 0), Quaternion.identity) as Transform;

            yield return new WaitForSeconds(0.6f);

            tempobj.gameObject.SetActive(false);
        }

    }


    /// <summary>
    /// Pues eso , la secuencia de la intro (se sube el cielo y se suben las montañas tambien
    /// </summary>
    IEnumerator Intro_Sequence()
    {




        //Logo.DOFade(1f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        blackness.DOFade(0f, 1f);
        //Logo.DOFade(0f, 1f);

        yield return new WaitForSeconds(1.0f);


        //El background turns purple (background color from camera)
        //MainCamera.DOColor(new Color(136f, 93f, 131f, 255f), 1f);
        
        // El cielo sube

        //cielo.GetComponent<SpriteRenderer>()

        
        cielo.DOMove(new Vector3(0,1.0f,0), 3f, false);

        yield return new WaitForSeconds(1.5f);

        //Las montañas suben...
        mountains.DOMoveY(-1.6f, 1.7f);

        Logo.DOFade(1f, 1f);

        // Aparece la pasarela

        // Aparecen los dos enemigos

        //etc...


    }
}
