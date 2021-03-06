﻿using UnityEngine;
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
    public Image JamLogo;

    public Text TouchmeText;

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

        for (int i = 0; i < 15; i++) {

            float x_aleatoria = UnityEngine.Random.Range(-4f, 4f);
            float y_aleatoria = UnityEngine.Random.Range(-3f, -3.3f);

            Transform tempobj = Instantiate(explosion_big, new Vector3(x_aleatoria, y_aleatoria, 0), Quaternion.identity) as Transform;

            tempobj.GetComponent<SpriteRenderer>().sortingOrder = 0;
            yield return new WaitForSeconds(0.8f);

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
        Logo.transform.DOPunchScale(Vector3.up, 0.5f, 7,0.5f);
        JamLogo.DOFade(1f,1f);
        //TouchmeText.DOFade(1f, 1.7f);

        //spaceCraftFortressText.DOText("#1 Spacecraft Fortress \n Time to raid and escape from the fortress", 1).SetRelative().SetEase(Ease.Linear).SetAutoKill(false).Pause();

        yield return new WaitForSeconds(0.5f);
        TouchmeText.DOText("TOUCH THE SCREEN", 1).SetRelative().SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Kickstart_explosions());


    }



    public void Screen_touched()
    {
        Application.LoadLevel("combinationDisplay_safe");
    }
}
