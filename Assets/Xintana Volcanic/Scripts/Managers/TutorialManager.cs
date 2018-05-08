using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialManager : MonoBehaviour {

    [Header("TutorialPanels")]
    public GameObject[] tutorialPanels;

  
    private int currentStep = 0; // variable to reference the current step of the array "TutorialPanels"

    // Variables for swipe gestures
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private Swipe _swipe;
    private enum Swipe
    {
        Left,
        Right
    }

    private MainMenuManager _menuManager;
    private bool tutorialIsDisplayed = false;

    // Use this for initialization
    void Start () {
        //DOTween.KillAll();
        if (FindObjectOfType<MainMenuManager>())
        {
            _menuManager = FindObjectOfType<MainMenuManager>();
        }
        
  
    }
	
	// Update is called once per frame
	void Update () {
        //ChangeTutorialPanel(0);
        if (tutorialIsDisplayed)
        {
            checkSwipeClick();
        }
        
    }

    void checkSwipeClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                //swipes left except in the last tutorial panel
                _swipe = Swipe.Left;
                Debug.Log("Swipe.Left");
                if (currentStep < 6)
                {
                    currentStep++;
                    ChangeTutorialPanel(currentStep);
                    
                }   else if (currentStep == 6) // Ends Tutorial after swiping left in last panel
                    {
                        tutorialIsDisplayed = false;
                        tutorialPanels[6].SetActive(false);
                        currentStep = 0;
                        Debug.Log("Tutorial End");
                    }

            }

            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                _swipe = Swipe.Right;
                //swipes right except in the first tutorial panel
                if (currentStep > 0)
                {
                    currentStep--;
                    ChangeTutorialPanel(currentStep);
                    
                }

            }
            Debug.Log(currentStep);
        }


    }

    public void StartTutorial()
    {
                
        this.gameObject.transform.DOLocalMoveX(0f, 0.75f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            tutorialIsDisplayed = true;
            this.tutorialPanels[0].gameObject.SetActive(true);
        });
    }

    public void StartTutorial2()
    {
        Debug.Log("tutorial 2 has been called!");
            tutorialIsDisplayed = true;
            this.tutorialPanels[0].gameObject.SetActive(true);

    }


    private void ChangeTutorialPanel(int tutorialStep)
    {
        int _TutorialPanelNumber;

        _TutorialPanelNumber = tutorialStep;

        switch (_TutorialPanelNumber)
        {
            case 0:

                for (int i = 0; i < tutorialPanels.Length; i++)
                {
                    if (i == 0)
                    {
                        tutorialPanels[i].SetActive(true);
                    }
                    else
                    {
                        tutorialPanels[i].SetActive(false);
                    }
                }
                break;

            case 1:
                for (int i = 0; i < tutorialPanels.Length; i++)
                {
                    if (i == 1)
                    {
                        tutorialPanels[i].SetActive(true);
                    }
                    else
                    {
                        tutorialPanels[i].SetActive(false);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < tutorialPanels.Length; i++)
                {
                    if (i == 2)
                    {
                        tutorialPanels[i].SetActive(true);
                    }
                    else
                    {
                        tutorialPanels[i].SetActive(false);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < tutorialPanels.Length; i++)
                {
                    if (i == 3)
                    {
                        tutorialPanels[i].SetActive(true);
                    }
                    else
                    {
                        tutorialPanels[i].SetActive(false);
                    }
                }
                break;
            case 4:
                for (int i = 0; i < tutorialPanels.Length; i++)
                {
                    if (i == 4)
                    {
                        tutorialPanels[i].SetActive(true);
                    }
                    else
                    {
                        tutorialPanels[i].SetActive(false);
                    }
                }
                break;
            case 5:
                for (int i = 0; i < tutorialPanels.Length; i++)
                {
                    if (i == 5)
                    {
                        tutorialPanels[i].SetActive(true);
                    }
                    else
                    {
                        tutorialPanels[i].SetActive(false);
                    }
                }
                break;
            case 6:
                for (int i = 0; i < tutorialPanels.Length; i++)
                {
                    if (i == 6)
                    {
                        tutorialPanels[i].SetActive(true);
                    }
                    else
                    {
                        tutorialPanels[i].SetActive(false);
                    }
                }
                break;
        }
    }

#if UNITY_EDITOR
    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 70, 50, 30), "Tutorial"))
    //    {
    //        StartTutorial();   
    //    }
    //}
#endif
}
