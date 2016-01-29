using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CombinationManager : MonoBehaviour {

        // button at the bottom
        //  3 rounds
        // timer
        // victory condition
    //rojo amarillo verde azul.

    
    [Header("Links")]
    public GameObject Canvas;
    public Button prefabButton;
    public Slider timerSlider;
    public Text sliderTimerText;
    public int combinationLength;                   //Combination length that the User will have to solve.
    public GameObject[] combinationArray;           //This is the Array with the GameObjects combination.
    public GameObject[] objectsCombinationPool;     //Pool with all the possible GameObjects.
    public float timeToResolveCombination = 5;

	// Use this for initialization
	void Start () {
        combinationArray = new GameObject[combinationLength];
        GenerateCombination();
        CreateButtonsAndPlaceThem();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTimer();
	}

    void GenerateCombination() 
    {
        int tempValue = 0;
        for (int i = 0; i < combinationLength; i++)
        {
            tempValue = Random.Range(0, objectsCombinationPool.Length);
            combinationArray[i] = objectsCombinationPool[tempValue];
        }
    }
    /// <summary>
    /// Change the Combination Length.
    /// </summary>
    /// <param name="newValue"></param>
    void ChangeCombinationLength(int newValue)
    {
        combinationLength = newValue;
    }

    /// <summary>
    /// Instantiate the gameobjects and place them in the middle top of the screen.
    /// </summary>
    void CreateButtonsAndPlaceThem()
    {
        float offset = 0;
        for (int i = 0; i < combinationLength; i++)
        {
            offset += 75;
            Vector3 screenPosition = GetScreenPosition(offset);
            GameObject buttonCloned = Instantiate(combinationArray[i]);
            buttonCloned.transform.parent = Canvas.transform;
            //buttonCloned.gameObject.transform.position = screenPosition;
            
        }
    }

    public Vector3 GetScreenPosition(float offset)
    {
        Vector3 screenPosition = new Vector3((Screen.width + offset)/2, Screen.height - 50, 0);
        Debug.Log("ScreenPosition returned:" + screenPosition);
        return screenPosition;
    }

    void UpdateTimer()
    {
        timerSlider.value -= Time.deltaTime / timeToResolveCombination;
        float tempTimer = timeToResolveCombination - Time.deltaTime;
        sliderTimerText.text = tempTimer.ToString(); 
    }
}
