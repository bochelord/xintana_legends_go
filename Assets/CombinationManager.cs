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
    public GameObject explosionPrefab;
    public Slider timerSlider;
    public Text sliderTimerText;

    public int combinationLength;                   //Combination length that the User will have to solve.
    public GameObject[] combinationArray;           //This is the Array with the GameObjects combination.
    public GameObject[] copyCombinationArray;       //An Array store to delete later the combination game objects.
    public GameObject[] objectsCombinationPool;     //Pool with all the possible GameObjects.
    public float timeToResolveCombination = 5;

    private bool gameOn;
    private float tempTimer;                        //Auxiliary variable to work with the Timer.
    private int currentCombinationPosition = 0;     //The combination position to check, by default 0.
	// Use this for initialization
	void Start () {
        combinationArray = new GameObject[combinationLength];
        copyCombinationArray = new GameObject[combinationLength];
        GenerateCombination();
        CreateButtonsAndPlaceThem();
        tempTimer = timeToResolveCombination;
        gameOn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOn)
        {
            UpdateTimer();
        }
        
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
            //Vector3 screenPosition = GetScreenPosition(offset);
            GameObject buttonCloned = Instantiate(combinationArray[i]);
            copyCombinationArray[i] = buttonCloned; 
            buttonCloned.GetComponent<ColorButtonData>().position = i;
            buttonCloned.transform.parent = Canvas.transform;
            //buttonCloned.gameObject.transform.position = screenPosition;
            
        }
    }

    public Vector3 GetScreenPosition(float offset)
    {
        Vector3 screenPosition = new Vector3((Screen.width + offset)/2, Screen.height - 50, 0);
        return screenPosition;
    }

    /// <summary>
    /// Updates the Slider related to the timeToResolveCombination.
    /// Updates the SliderTimerText.text with the time to finish the combination.
    /// </summary>
    void UpdateTimer()
    {
        timerSlider.value -= Time.deltaTime / timeToResolveCombination;
        tempTimer -= Time.deltaTime;
        if (timerSlider.value > 0)
        {
            sliderTimerText.text = tempTimer.ToString("f0") + " SECS";
        }
         
    }

    /// <summary>
    /// Get the Color of the Pressed Button and then compare it with the current combination to solve.
    /// </summary>
    /// <param name="xButton"></param>
    public void CheckCombination(Button xButton){

        string buttonColor = xButton.GetComponent<ColorButtonData>().buttonColor;

        // Correct Combination, USER can continue.
        if (combinationArray[currentCombinationPosition].GetComponent<ColorButtonData>().buttonColor == buttonColor)
        {
            // instantiate explosion
            // destroy conbination color
            //GameObject explosionCloned = Instantiate(explosionPrefab, combinationArray[currentCombinationPosition].transform.position, Quaternion.identity) as GameObject;
            copyCombinationArray[currentCombinationPosition].GetComponent<Image>().enabled = false;
            Debug.Log("CORRECT");
            //explosionCloned.transform.SetParent(Canvas.transform);
            //explosionCloned.transform.localPosition = combinationArray[currentCombinationPosition].transform.localPosition;

            currentCombinationPosition++;

        }
        else  //  WRONG combination, USER lose.
        {
            Debug.Log("YOU LOSE");
            // We STOP the Game as the Player lose.
            gameOn = false;
        }

        //switch (buttonColor)
        //{
        //    case "red":
                
        //        break;
        //    case "yellow":
                
        //        break;
        //    case "green":
                
        //        break;
        //    case "blue":
                
        //        break;
        //    default:
        //        break;
        //}
    }
}
