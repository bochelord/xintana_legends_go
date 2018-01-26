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
    [HideInInspector]
    public int combinationLength=1;                   //Combination length that the User will has to solve.

    public Text fightNumberValueText;
    public GameObject[] combinationArray;           //This is the Array with the GameObjects combination.
    public GameObject[] copyCombinationArray;       //An Array store to delete later the combination game objects.
    public GameObject[] objectsCombinationPool;     //Pool with all the possible GameObjects.

    public float timeToResolveCombination = 15;

    private bool gameOn = true;
    private bool winningCondition = false;
    private float tempTimer;                        //Auxiliary variable to work with the Timer.
    private int currentCombinationPosition = 0;     //The combination position to check, by default 0.
    private int minimCombinationLenght = 1;          
    private LevelManager levelManager;
    private PlayerManager _playerManager;
    private int _combinationFrecuency = 3;//bydefault
    public float original_timeToResolveCombination;
    private AdsManager adManager;
    private Rad_GuiManager _guiManager;
    private Vector2 _initialButtonsPosition;
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
        original_timeToResolveCombination = timeToResolveCombination;
        adManager = FindObjectOfType<AdsManager>();
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        _initialButtonsPosition = combinationButtons.transform.position;
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

        int _tempLength = Random.Range(minimCombinationLenght, combinationLength);
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

        int _tempLength = Random.Range(minimCombinationLenght, combinationLength);
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
        minimCombinationLenght = 1;
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
    public void GenerateCombination() 
    {
        int tempValue = 0;
        for (int i = 0; i < combinationArray.Length; i++)
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
    /// Change minimCombinationValue
    /// </summary>
    /// <param name="newValue"></param>
    void ChangeMinimCombinationLength(int newValue)
    {
        //if(combinationLength > 3)
        //{
            minimCombinationLenght = newValue;
        //}

    }
    /// <summary>
    /// Instantiate the gameobjects and place them in the middle top of the screen.
    /// </summary>
    void CreateButtonsAndPlaceThem(){
        for (int i = combinationArray.Length-1; i > -1; i--){
            GameObject buttonCloned = Instantiate(combinationArray[i]);
            copyCombinationArray[i] = buttonCloned; 
            buttonCloned.GetComponent<ColorButtonData>().position = i;
            buttonCloned.transform.SetParent(combinationPanel.transform);
        }
        moveButtonsToCenter(0);
    }
    /// <summary>
    /// Moves the button of the copyCombinationArray to the center of the combination panel.
    /// </summary>
    /// <param name="number">The position of the button you want to move.</param>
    private void moveButtonsToCenter(int number) {
        copyCombinationArray[number].transform.DOMoveX(combinationPanel.transform.position.x, 0.5f);
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
        if (levelManager.state == GameState.Running)
        {
            timerSlider.value -= Time.deltaTime / timeToResolveCombination;
            tempTimer -= Time.deltaTime;
            if (timerSlider.value > 0)
            {
                sliderTimerText.text = tempTimer.ToString("f0");
            }
            if (timerSlider.value <= 0)
            {
                ShowLoseText();
                // We STOP the Game as the Player lose.
                gameOn = false;
                //levelManager.GameOverPanel();

                if (!adManager.adViewed && adManager.AdsViewed <= 4)
                {
                    _guiManager.ShowAdPanel();
                }
                else
                {
                    StartCoroutine(FunctionLibrary.CallWithDelay(levelManager.GameOverPanel, 1.5f));
                    adManager.adViewed = false;
                }
            }
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
            //moves the next button to the center of the panel.
            if(currentCombinationPosition< combinationArray.Length) moveButtonsToCenter(currentCombinationPosition);
            // WINNING CONDITION <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            if (currentCombinationPosition == combinationArray.Length)
            {
                winningCondition = true;
                gameOn = false;
                DisableButtonsInteraction();

                levelManager.AttackEnemy(1.5f); //TODO GHet tje damage from the player!!!

                if (levelManager.enemyKilled)
                {
                    ShowWinText();
                    

                    if (levelManager.GetTotalEnemyKilled() == _combinationFrecuency) //each three kills we gorw the combination and also give more time to solve it...
                    {
                        ChangeMinimCombinationLength(minimCombinationLenght + 1);
                        ChangeCombinationLength(combinationLength + 1);
                        timeToResolveCombination += 1;
                        _combinationFrecuency += _combinationFrecuency;
                    }
                    else
                    {
                        ChangeCombinationLength(combinationLength);
                        ChangeMinimCombinationLength(minimCombinationLenght);
                    }
                    StartCoroutine(LoadNextRound(2.5f));
                }
                else
                {
                    ChangeCombinationLength(combinationLength);
                    StartCoroutine(LoadNextEnemy(0.1f));

                }
                //Debug.Log("COMBINATION LENGTH> " + combinationLength);
                
            }
        }
        else  // WRONG combination, USER lose. <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        {

            levelManager.AttackPlayer();
            DisableButtonsInteraction();
            if(_playerManager.life > 0)
            {
                StartCoroutine(LoadNextEnemy(0.1f));
            }
            else {
                ShowLoseText();
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
        combinationButtons.transform.DOMoveY(_initialButtonsPosition.y,1f);
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

    public void ShowWinText(){
        youWin_Text.gameObject.SetActive(true);
        AudioManager.Instance.Play_YouWin();
    }

    public void ShowLoseText(){
        youLose_Text.gameObject.SetActive(true);
        AudioManager.Instance.Play_YouLose();
    }

    IEnumerator LoadNextEnemy(float delay){

        yield return new WaitForSeconds(delay);
        ResetGameButDontResetTime();
        
        //Application.LoadLevel(Application.loadedLevel);
    }

    IEnumerator LoadNextRound(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetGame();
    }

    public void HideWinLoseText(){
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


    public void SetCombinationFrecuency(int newfrecu)
    {
        _combinationFrecuency = newfrecu;
    }
}
