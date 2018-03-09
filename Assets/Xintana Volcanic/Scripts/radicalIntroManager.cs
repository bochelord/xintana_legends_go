using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using System;

public class radicalIntroManager : MonoBehaviour
{


    #region variables
    [Header("Logo splitted and ordered by appareance")]
    public Image []ordered_images;

    //public Image hidef_redlogo;


    [Header("black panel")]
    public Image blackness;
    [Header("timer")]
    public Text timerText;

    

    private static LoadLevelData loadlevelData;


    private bool first;
    private float timepassed;
    private bool stopTimer = false;
    #endregion



    // Use this for initialization
	void Start () {
        first = true;
        timepassed = 0;
        StartCoroutine(Kickstart());
	}

    void Update()
    {
        if (!stopTimer) { 
            timepassed += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timepassed);
        
            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
        else
        {
            //loadlevelData = GameObject.FindObjectOfType<LoadLevelData>();

            //loadlevelData.SetSelectedLevel(1);
            //loadlevelData.SetLevelName("IntroScreen_PressStart");

            //Application.LoadLevel("GameIntroStart");
            //SceneManager.LoadScene("GameIntroStart");
            Skip_it();
        }

    }

    IEnumerator Kickstart()
    {

        blackness.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.5f);

        for (int n = 0; n <= ordered_images.Length-1; n++)
        {
            

            if (first) {
                ordered_images[n].gameObject.SetActive(true);
                ordered_images[n].DOFade(1f, 0.1f);
                ordered_images[n].transform.DOPunchScale(new Vector3(0f,0.3f,0f),0.3f,10,0.5f);

                _2dxFX_Pixel8bitsC64 fx_component = ordered_images[n].transform.GetComponent<_2dxFX_Pixel8bitsC64>();
                //we tween the pixel effect to desired values
                DOTween.To(()=> fx_component._Offset, x=> fx_component._Offset =x, 0.3f,2f);
                DOTween.To(() => fx_component._Size, x => fx_component._Size = x, 2f, 2f);

                //fx_component._Offset
                //                            // Tween a float called myFloat to 52 in 1 second
                //            DOTween.To(()=> myFloat, x=> myFloat = x, 52, 1);
                yield return new WaitForSeconds(1.8f);
                first = false;
            }
            else
            {
                //if (n == 2)
                //{
                //    hidef_redlogo.gameObject.SetActive(true);
                //    hidef_redlogo.DOFade(1f,0.2f);
                //}
                ordered_images[n].DOFade(1f, 0.7f);
                yield return new WaitForSeconds(0.5f);
            }
        }

        
        yield return new WaitForSeconds(0.5f);
        //this.GetComponent<AudioSource>().PlayOneShot(cunaoClip);
        yield return new WaitForSeconds(0.5f);
        
        

        for (int n = 0; n <= ordered_images.Length - 1; n++)
        {
            ordered_images[n].DOFade(0f, 0.5f);
            //hidef_redlogo.DOFade(0f,0.5f);
        }
        yield return new WaitForSeconds(0.7f);
        blackness.DOFade(1f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        stopTimer = true;
    }


    public void Skip_it()
    {
        //Application.LoadLevel("GameIntroStart");
        

        SceneManager.LoadScene("GameLauncher");

    }

}
