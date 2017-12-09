using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class CombinationManager : MonoBehaviour {

    // button at the bottom
    //  3 rounds
    // timer
    // victory condition
    //rojo amarillo verde azul.


    [Header("Links")]
    public GameObject combinationButtons;
    public GameObject combinationPanel;
    public Button prefabButton;
    public GameObject explosionPrefab;
    public Slider timerSlider;
    public Text sliderTimerText;
    public AudioSource audio;
    public AudioClip audioBongoClip;

    [Header("UI Elements")]
    public Text youWin_Text;
    public Text youLose_Text;
    [Header("UI Buttons")]
    public GameObject[] uiButtons;
    public int combinationLength;                   //Combination length that the User will has to solve.
    public Text combinationLengthText;
    public GameObject[] combinationArray;           //This is the Array with the GameObjects combination.
    public GameObject[] copyCombinationArray;       //An Array store to delete later the combination game objects.
    public GameObject[] objectsCombinationPool;     //Pool with all the possible GameObjects.
    public float timeToResolveCombination = 5;

    private bool gameOn = true;
    private bool winningCondition = false;
    private float tempTimer;                        //Auxiliary variable to work with the Timer.
    private int currentCombinationPosition = 0;     //The combination position to check, by default 0.
    private int minimCombinationValue = 1;          
    private LevelManager levelManager;
    private PlayerManager _playerManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    
	void Start () {
       
        combinationArray = new GameObject[combinationLength];
        copyCombinationArray = new GameObject[combinationLength];
        
        GenerateCombination();
        CreateButtonsAndPlaceThem();
        tempTimer = timeToResolveCombination;
        EnableButtonsInteraction();
        //gameOn = true;
	}
    void ResetGame()
    {
        combinationArray = null;
        copyCombinationArray = null;

        int _tempLength = Random.Range(minimCombinationValue, combinationLength);
        combinationArray = new GameObject[_tempLength];
        copyCombinationArray = new GameObject[_tempLength];
        foreach (Transform child in combinationPanel.transform)
        {
            Destroy(child.gameObject);
        }
        currentCombinationPosition = 0;
        GenerateCombination();
        CreateButtonsAndPlaceThem();
        HideWinLoseText();
        EnableButtonsInteraction();
        ResetTimer();
        gameOn = true;
    }

    void ResetGameButDontResetTime()
    {
        combinationArray = null;
        copyCombinationArray = null;

        int _tempLength = Random.Range(minimCombinationValue, combinationLength);
        combinationArray = new GameObject[_tempLength];
        copyCombinationArray = new GameObject[_tempLength];
        foreach (Transform child in combinationPanel.transform)
        {
            Destroy(child.gameObject);
        }
        currentCombinationPosition = 0;
        GenerateCombination();
        CreateButtonsAndPlaceThem();
        HideWinLoseText();
        EnableButtonsInteraction();
        gameOn = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameOn)
        {
            UpdateTimer();
        }
        //if (winningCondition)
        //{
        //    //Debug.Log("YOU WON");
        //    ResetTimer();
        //}
	}

    /// <summary>
    /// called when player loses, it's possible that later will be removed
    /// </summary>
    public void ResetCombination()
    {
        combinationArray = null;
        copyCombinationArray = null;
        foreach (Transform child in combinationPanel.transform)
        {
            Destroy(child.gameObject);
        }
        currentCombinationPosition = 0;
        minimCombinationValue = 1;
        timerSlider.value = 1;
        combinationLength = 1;
        combinationArray = new GameObject[combinationLength];
        copyCombinationArray = new GameObject[combinationLength];

        GenerateCombination();
        CreateButtonsAndPlaceThem();
        tempTimer = timeToResolveCombination;
        EnableButtonsInteraction();
    }
    /// <summary>
    /// Generates a Random combination with a combinationLength.
    /// </summary>
    void GenerateCombination() 
    {
        int tempValue = 0;
        for (int i = 0; i < combinationArray.Length; i++)
        {
            tempValue = Random.Range(0, objectsCombinationPool.Length);
            combinationArray[i] = objectsCombinationPool[tempValue];
            
        }
        combinationLengthText.text = combinationArray.Length.ToString();
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
    /// Change minimCombinationValue
    /// </summary>
    /// <param name="newValue"></param>
    void ChangeMinimCombinationValue(int newValue)
    {
        if(combinationLength > 3)
        {
            minimCombinationValue = newValue;
        }

    }
    /// <summary>
    /// Instantiate the gameobjects and place them in the middle top of the screen.
    /// </summary>
    void CreateButtonsAndPlaceThem()
    {
        float offset = 0;
        for (int i = 0; i < combinationArray.Length; i++)
        {
            offset += 75;
            //Vector3 screenPosition = GetScreenPosition(offset);
            GameObject buttonCloned = Instantiate(combinationArray[i]);
            copyCombinationArray[i] = buttonCloned; 
            buttonCloned.GetComponent<ColorButtonData>().position = i;
            buttonCloned.transform.parent = combinationPanel.transform;
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
        if (timerSlider.value <= 0)
        {
            // We STOP the Game as the Player lose.
            gameOn = false;
            levelManager.GameOverPanel();

        }
         
    }

    void ResetTimer()
    {
        tempTimer = timeToResolveCombination;
        timerSlider.value = tempTimer;
    }

    /// <summary>
    /// Function called from the UI buttons.
    /// Get the Color of the Pressed Button and then compare it with the current combination to solve.
    /// </summary>
    /// <param name="xButton"></param>
    public void CheckCombination(Button xButton){

        string buttonColor = xButton.GetComponent<ColorButtonData>().buttonColor;

        // Correct Combination, USER can continue.
        if (combinationArray[currentCombinationPosition].GetComponent<ColorButtonData>().buttonColor == buttonColor)
        {
            audio.PlayOneShot(audioBongoClip, 1F); 
            copyCombinationArray[currentCombinationPosition].GetComponent<Image>().enabled = false;
            
            currentCombinationPosition++;
            
            // WINNING CONDITION <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            if (currentCombinationPosition == combinationArray.Length)
            {
                winningCondition = true;
                gameOn = false;
                DisableButtonsInteraction();

                levelManager.AttackEnemy();

                if (levelManager.enemyKilled)
                {
                    ShowWinText();
                    ChangeCombinationLength(combinationLength + 1);

                    if (levelManager.GetTotalEnemyKilled() % 3 ==0) //each three kills we gorw the combination
                    {
                        ChangeMinimCombinationValue(minimCombinationValue + 1);
                    }
                    else
                    {
                        ChangeMinimCombinationValue(minimCombinationValue);
                    }
                    StartCoroutine(LoadNextRound(2.5f));
                }
                else
                {
                    ChangeCombinationLength(combinationLength);
                    StartCoroutine(LoadNextRoundEnemy(0.1f));

                }
                //Debug.Log("COMBINATION LENGTH> " + combinationLength);
                
            }
        }
        else  // WRONG combination, USER lose. <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        {

            levelManager.AttackPlayer();
            DisableButtonsInteraction();
            ShowLoseText();
            if(_playerManager.life > 0)
            {
                StartCoroutine(LoadNextRoundEnemy(0.1f));
            }

            // We STOP the Game as the Player lose.
            gameOn = false;
        }
    }

    public void MoveButtonsOut()
    {
        //combinationButtons.GetComponent<GUIAnimFREE>().MoveOut();
        //combinationButtons.transform.DOLocalMove
        //combinationButtons.transform.DOLocalMove(new Vector3(-2.5f, -51f, 0f), 1f);
        combinationButtons.transform.DOMoveY(-68f,1f);
    }
    public void MoveButtonsIn()
    {
        //combinationButtons.GetComponent<GUIAnimFREE>().m_MoveIn.Time = 0.5f;
        //combinationButtons.GetComponent<GUIAnimFREE>().MoveIn();

        //combinationButtons.transform.DOLocalMove(new Vector3(-2.5f,41f,0f),1f);
        combinationButtons.transform.DOMoveY(58f, 1f);
    }
    /// <summary>
    /// Buttons will not be interactable.
    /// </summary>
    public void DisableButtonsInteraction()
    {

        for (int i=0; i < 4; i++)
        {
            uiButtons[i].GetComponent<Button>().interactable = false;

        }
    }


    /// <summary>
    /// Buttons will be interactable.
    /// </summary>
    public void EnableButtonsInteraction()
    {

        for (int i = 0; i < 4; i++)
        {
            if (uiButtons[i])
            {
                //Debug.Log("uiButton number: " + i);
                uiButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ShowWinText()
    {
        youWin_Text.gameObject.SetActive(true);
    }

    public void ShowLoseText()
    {
        youLose_Text.gameObject.SetActive(true);
    }

    IEnumerator LoadNextRoundEnemy(float delay){

        yield return new WaitForSeconds(delay);
        ResetGameButDontResetTime();
        
        //Application.LoadLevel(Application.loadedLevel);
    }

    IEnumerator LoadNextRound(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetGame();
    }

    void HideWinLoseText(){
        if (youLose_Text)
        {
            youLose_Text.gameObject.SetActive(false);
        }
        if (youWin_Text)
        {
            youWin_Text.gameObject.SetActive(false);
        }
    }
    
    public void SetGameOn(bool value)
    {
        gameOn = value;
    }
}
