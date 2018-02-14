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
    public GameObject auraCombination;
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
    private Coroutine _auraCoroutine;
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
        original_timeToResolveCombination = timeToResolveCombination;
        adManager = FindObjectOfType<AdsManager>();
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        _initialButtonsPosition = combinationButtons.transform.localPosition;
    }

    
	void Start () {
        combinationArray = new GameObject[combinationLength];
        copyCombinationArray = new GameObject[combinationLength];
        
        GenerateCombination();
        InstantiateGemsAndPlaceThem();
        tempTimer = timeToResolveCombination;
        EnableButtonsInteraction();
        //gameOn = true;
	}
    public void ResetGame()
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
        InstantiateGemsAndPlaceThem();
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
        InstantiateGemsAndPlaceThem();
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
        InstantiateGemsAndPlaceThem();
        tempTimer = timeToResolveCombination;
        EnableButtonsInteraction();
    }

    public void DestroyCombination()
    {
        combinationArray = null;
        copyCombinationArray = null;
        foreach (Transform child in combinationPanel.transform)
        {
            Destroy(child.gameObject);
        }
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

    public void ResetTimer()
    {
        timerSlider.value = 1;
        tempTimer = timeToResolveCombination;
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
    void InstantiateGemsAndPlaceThem(){
        for (int i = combinationArray.Length-1; i > -1; i--){
            GameObject buttonCloned = Instantiate(combinationArray[i]);
            copyCombinationArray[i] = buttonCloned; 
            buttonCloned.GetComponent<ColorButtonData>().position = i;
            buttonCloned.transform.SetParent(combinationPanel.transform);
            Vector3 scale =buttonCloned.transform.localScale;
            //scale *= 0.75f;
            scale = new Vector3(15.0f, 15.0f, 15.0f);
            buttonCloned.transform.localScale=scale;
        }
        //moveButtonsToCenter(0,0.5f);
        moveGemsTopToMiddle(0, 0.5f);
    }
    /// <summary>
    /// Moves the button of the copyCombinationArray to the center of the combination panel.
    /// </summary>
    /// <param name="number">The position of the button you want to move.</param>
    /// <param name="time">Time needed to tween position</param>
    private void moveButtonsToCenter(int number,float time)
    {
        _auraCoroutine = StartCoroutine(AuraCombinationOn(time));
        copyCombinationArray[number].transform.DOMoveX(combinationPanel.transform.position.x, time);//.OnComplete(()=> 
        //{
        //    if(copyCombinationArray[number].activeSelf)auraCombination.SetActive(true);
        //});
    }

    /// <summary>
    /// Moves the Combination Gems of the copyCombinationArray to the center of the combination panel.
    /// </summary>
    /// <param name="number">The position of the button you want to move.</param>
    /// <param name="time">Time needed to tween position</param>
    private void moveGemsTopToMiddle(int number, float time)
    {
        _auraCoroutine = StartCoroutine(AuraCombinationOn(time));
        copyCombinationArray[number].transform.DOMoveY(combinationPanel.transform.position.y, time);//.OnComplete(()=> 
        //{
        //    if(copyCombinationArray[number].activeSelf)auraCombination.SetActive(true);
        //});
    }



    IEnumerator AuraCombinationOn(float time)
    {
        yield return new WaitForSeconds(time);
        auraCombination.SetActive(true);
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
                levelManager.AddNemesisCount();
                if (!adManager.adViewed && Rad_SaveManager.profile.adsSkipped <= levelManager.adsSkipped && !Rad_SaveManager.profile.noAds)
                {
                    MoveButtonsOut();
                    _guiManager.ShowAdPanel();
                }
                else if (!adManager.adViewed && Rad_SaveManager.profile.adsSkipped >= levelManager.adsSkipped && !Rad_SaveManager.profile.noAds)
                {
                    StartCoroutine(FunctionLibrary.CallWithDelay(adManager.ShowAdNoReward,1.5f));
                }
                else
                {
                    StartCoroutine(FunctionLibrary.CallWithDelay(levelManager.GameOverPanel, 1.5f));
                    adManager.adViewed = false;
                }
            }
        }
    }



    /// <summary>
    /// Function called from the UI buttons.
    /// Get the Color of the Pressed Button and then compare it with the current combination to solve.
    /// </summary>
    /// <param name="xButton"></param>
    public void CheckCombination(Button xButton){

        string buttonColor = xButton.GetComponent<ColorButtonData>().buttonColor;
        if(_auraCoroutine != null)
        {
            StopCoroutine(_auraCoroutine);
        }
        auraCombination.SetActive(false);
        // Correct Combination, USER can continue.
        if (combinationArray[currentCombinationPosition].GetComponent<ColorButtonData>().buttonColor == buttonColor)
        {
            audio.PlayOneShot(audioBongoClip, 1F); 
            copyCombinationArray[currentCombinationPosition].GetComponent<Image>().enabled = false;
            
            currentCombinationPosition++;
            //moves the next button to the center of the panel.
            //if(currentCombinationPosition< combinationArray.Length) moveButtonsToCenter(currentCombinationPosition,0.5f);
            if (currentCombinationPosition < combinationArray.Length) moveGemsTopToMiddle(currentCombinationPosition, 0.5f);
            
            // WINNING CONDITION <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            if (currentCombinationPosition == combinationArray.Length)
            {
                winningCondition = true;
                gameOn = false;
                DisableButtonsInteraction();

                levelManager.AttackEnemy(_playerManager.attack); //TODO GHet tje damage from the player!!!

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
                    //StartCoroutine(LoadNextRound(2.5f));
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
        combinationButtons.transform.DOLocalMove(new Vector3(-2.5f, -1000f, 0f), 1f);
    }
    public void MoveButtonsIn()
    {
        //combinationButtons.GetComponent<GUIAnimFREE>().m_MoveIn.Time = 0.5f;
        //combinationButtons.GetComponent<GUIAnimFREE>().MoveIn();

        combinationButtons.transform.DOLocalMove(new Vector3(-2.5f,-550f,0f),1f);
        //combinationButtons.transform.DOMoveY(100f, 1f);

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

    public void ChangeTimerSliderColor(float value)
    {
        timerSlider.fillRect.GetComponent<_2dxFX_HSV>()._Saturation = value;
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

    public void ResetCurrentLenghtCombination()
    {
        ResetGameButDontResetTime();
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
