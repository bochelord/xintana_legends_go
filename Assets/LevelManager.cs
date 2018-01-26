using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
/// <summary>
/// Level Manager for Xintana Volcanic
/// (c)2017 Radical Graphics Studios
/// </summary>
/// 

    public enum GameState { Running,Paused}
public class LevelManager : MonoBehaviour {

    public GameObject enemy;
    public EnemyController enemyController;
    public GameState state = GameState.Running;
    public GameObject HitPrefabRight;
    public GameObject damageFxPrefab;
    public GameObject player;
    public GameObject HUDTextContainer;
    public GameObject HUDTextPrefab;
    public GameObject HUDPlayAgainContainer;
    private PlayerManager playerManager;
    private CombinationManager combinationManager;
    private Rad_GuiManager _guiManager;
    private AdsManager adManager;
    private HealthBarControllerBoss healthBarController;
    private List<Transform> inactiveHUDTextList = new List<Transform>();
    public EnemiesPooler enemyPooler;
    public Transform enemyContainer;
    public bool enemyKilled = false;

    [Header("Level Control Values")]
    [Tooltip("This is measure in number of fights")]
    public int bossFightFrecuency = 7; 
    public int combinationIncreaseFrecuency = 3;


    [Header("Backgrounds")]
    //public GameObject worldspritesLevel1;
    //public GameObject worldspritesLevel2;
    //public GameObject worldspritesLevel3;
    public GameObject[] worldspritesLevelList;

    private int _kogiKilled = 0;
    private int _zazucKilled = 0;
    private int _makulaKilled = 0;
    private float _playerScore = 0;
    private int _currentEnemyLevel = 0;
    private int _enemyCount = 0;
    private int _eachthreetimes = 3;
    private float timerSafe;
    private int _worldNumber = 1;

    private float playerScoreUI;

    private bool musiclevel2_AlreadyPlayed = false;
    private bool musiclevel3_AlreadyPlayed = false;

    //private int currentWorld = 1;

    void Awake()
    {
        combinationManager = FindObjectOfType<CombinationManager>();
        playerManager = player.GetComponent<PlayerManager>();
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        adManager = FindObjectOfType<AdsManager>();
        healthBarController = FindObjectOfType<HealthBarControllerBoss>();
    }

    void Start()
    {
        AnalyticsManager.Instance.DeviceModel_Event();
        combinationManager.SetCombinationFrecuency(combinationIncreaseFrecuency);
        PrepareBackgroundLevel(1);
        GetNewEnemy(0);
        //fixscreeperra();
        //LaunchShowHUDText(enemyContainer.transform.position, enemyController.GetDamageDoneByEnemy().ToString("F1"), new Color32(245, 141, 12, 255));
    }


    public void AttackEnemy(float in_damage)
    {
        // MiniPlan
        // Effect on Player
        // Effect on Enemy
        // Enemy Data update if dead, respawn

        float damagedone = 0;
        bool critico = false;
        damagedone = Formulas.GetDamageCalculated(in_damage, out critico);

        if (damagedone > enemyController.GetLife())
        {
            //we are goinna kill the enemy so we trigger the jumping attack animation
            player.GetComponent<Animator>().SetInteger("AnimState", 11);
        } 
        else
        {
            player.GetComponent<Animator>().SetBool("Attacking", true);
            player.GetComponent<Animator>().SetInteger("AnimState", 10);

        }

        //Effect on player
        ///////////////////////////////////////
        //This should be on the player manager...
        GameObject clone_prefab;
        //player.GetComponent<Animator>().SetBool("Attacking", true);
        //animator.SetBool("Attacking", true);
        //player.GetComponent<Animator>().Play("Xintana_Attack");
        //player.GetComponent<Animator>().SetInteger("AnimState", 10);
        //playermanager.ChangeAnimationState(10);
        clone_prefab = Instantiate(HitPrefabRight);
        clone_prefab.transform.position = player.transform.position;


        //////////////
        ///Effect on Enemy
        ///
        GameObject clone_damageFxprefab;
        clone_damageFxprefab = Instantiate(damageFxPrefab);
        clone_damageFxprefab.transform.position = enemy.transform.position;

        
        LaunchShowHUDText(enemyContainer.transform.position + new Vector3(0,1.5f,0), damagedone.ToString("F1"), new Color32(245, 141, 12, 255)); /// TODO This has to be feed with the proper damage coming from the playerManager

        AudioManager.Instance.Play_XintanaAttack_1();

        //damage to enemy
        //

        enemyController.ApplyDamageToEnemy(damagedone);
        if (critico)
        {
            LaunchShowHUDText(enemyContainer.transform.position + new Vector3(1.5f, 1.5f, 0), "crit!", new Color32(245, 141, 12, 255));
        }
    }
    
    /// <summary>
    /// called when player miss combination
    /// </summary>
    public void AttackPlayer()
    {
        player.GetComponent<Animator>().SetInteger("AnimState", 3);

        GameObject clone_damageFxprefab;
        clone_damageFxprefab = Instantiate(damageFxPrefab);
        clone_damageFxprefab.transform.position = player.transform.position;

        float _damageDone = enemyController.GetDamageDoneByEnemy();
        LaunchShowHUDText(player.transform.position + new Vector3(0, 1.5f, 0), _damageDone.ToString("F1"), new Color32(245, 141, 12, 255));
        playerManager.ReceiveDamage(_damageDone);

        AudioManager.Instance.Play_XintanaHit();


        if(playerManager.life <= 0)
        {
            
            //Continue or go to main menu
            AudioManager.Instance.Play_XintanaDeath();
            player.GetComponent<Animator>().SetInteger("AnimState",4);
            AnalyticsManager.Instance.GameOver_Event((int)_playerScore, _enemyCount + 1, _worldNumber);

            if (!adManager.adViewed && adManager.AdsViewed <=4)
            {
                combinationManager.MoveButtonsOut();
                StartCoroutine(FunctionLibrary.CallWithDelay(_guiManager.ShowAdPanel,2f));
                //_guiManager.ShowAdPanel();
            }
            else
            {
                StartCoroutine(FunctionLibrary.CallWithDelay(GameOverPanel, 2f));
                //Debug.Log("papor");
                //Invoke("GameOverPanel", 4.5f);
                adManager.adViewed = false; 
            }

        }
        
    }

    public void GetNewEnemy(float delay)
    {
        StartCoroutine(CoroGetNewEnemy(delay));
    }

    /// <summary>
    /// called in combinationManager, when you kill an enemy
    /// </summary>
    public void AddEnemyCount()
    {
        switch (enemyController.type)
        {
            case EnemyType.kogi:
                _kogiKilled++;
                break;
            case EnemyType.makula:
                _makulaKilled++;
                break;
            case EnemyType.zazuc:
                _zazucKilled++;
                break;
        }
    }

    public void AddPlayerScore(EnemyType typein, int levelin)
    {
        float timeRemaining = combinationManager.timerSlider.value;

        //Debug.Log("MECAGO EN TODA TU PUTA MADRE");
        switch (typein)
        {
            case EnemyType.kogi:

                DOTween.To(() => playerScoreUI, x => playerScoreUI = x, playerScoreUI + 2000 * levelin * timeRemaining, 0.5f);
                _playerScore += 2000 * levelin * timeRemaining;
                break;
            case EnemyType.makula:
                DOTween.To(() => playerScoreUI, x => playerScoreUI = x, playerScoreUI + 5000 * levelin * timeRemaining, 0.5f);
                _playerScore += 5000 * levelin * timeRemaining;
                break;
            case EnemyType.zazuc:
                DOTween.To(() => playerScoreUI, x => playerScoreUI = x, playerScoreUI + 1500 * levelin * timeRemaining, 0.5f);
                _playerScore += 1500 * levelin * timeRemaining;
                break;
        }
    }
    private IEnumerator CoroGetNewEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_enemyCount > _eachthreetimes)//Each 3 enemies killed we level up the upcoming enemies by one level
        {
            _currentEnemyLevel++;
            _eachthreetimes += 3;
        }

        
        _enemyCount++;

        combinationManager.fightNumberValueText.text = _enemyCount.ToString();

        if (_enemyCount == (bossFightFrecuency * _worldNumber))//final boss is summont on its frecuency and per world
        {
            enemy = enemyPooler.GetBossObject();
            timerSafe = combinationManager.timeToResolveCombination;
            combinationManager.timeToResolveCombination *= 2;

            AudioManager.Instance.PlayBossMusic();
            AudioManager.Instance.Play_Boss();
        } 
        else if (_enemyCount == (bossFightFrecuency * _worldNumber) + 1)//after a boss fight we reset the timer to whatever it was before it and
        {
            combinationManager.timeToResolveCombination = timerSafe;
            enemy = enemyPooler.GetPooledObject();

            _worldNumber++;

            //We change the level to another world
            PrepareBackgroundLevel(_worldNumber);

            if (AudioManager.Instance.musicPlayer.clip != AudioManager.Instance.musicArray[_worldNumber - 1]){
                AudioManager.Instance.PlayMusicLevel(_worldNumber);

                AudioManager.Instance.Play_NextStage();
            }
        }
        else
        {
            enemy = enemyPooler.GetPooledObject();
        }


        enemyController = enemy.GetComponent<EnemyController>();
        if(enemyController == null)
        {
            enemyController = enemy.GetComponentInChildren<EnemyController>();
        }
        enemy.transform.position = enemyContainer.position;

        if (_guiManager.enemyText)
        {
            _guiManager.enemyText.text = enemyController.type.ToString();
        }
        enemy.SetActive(true);
        enemy.transform.SetParent(enemyContainer);
        enemyKilled = false;
        healthBarController.SetScrollbarValue();
    }
    

    public void LaunchShowHUDText(Vector2 pos, string texto, Color color_in)
    {
        StartCoroutine(ShowHUDText(pos, texto, color_in));
    }

    public IEnumerator ShowHUDText(Vector2 pos, string texto, Color color_in)
    {
        Vector2 posConverted = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);

        //Color color_to_show = color_in;

        GameObject temptext;

        if (inactiveHUDTextList.Count > 1)
        {
            temptext = inactiveHUDTextList[0].gameObject;

            inactiveHUDTextList.Remove(temptext.transform);


            //score_texto.transform.position = posConverted;
            RectTransform health_texto_rect = temptext.GetComponent<RectTransform>();
            health_texto_rect.position = posConverted;
            //score_texto_rect.position = pos;
        }
        else
        {
            temptext = (GameObject)Instantiate(HUDTextPrefab, new Vector2(0, 0), Quaternion.identity);
            RectTransform health_texto_rect = temptext.GetComponent<RectTransform>();
            health_texto_rect.position = posConverted;
            //xp_texto_rect.sizeDelta = new Vector2(1, 1);

            //score_texto_rect.position = pos;
        }



        //temptext = (GameObject)Instantiate(HUDText_prefab, new Vector2(0, 0), Quaternion.identity);
        //RectTransform health_texto_rect = temptext.GetComponent<RectTransform>();
        //health_texto_rect.position = posConverted;

        //health_texto.transform.parent = healthTextContainer.transform;
        temptext.transform.SetParent(HUDTextContainer.transform);

        Material text_material = new Material(temptext.GetComponent<Text>().material);
        text_material.color = HUDTextPrefab.GetComponent<Text>().material.color;

        temptext.GetComponent<Text>().material = text_material;
        temptext.GetComponent<Text>().color = color_in;
        temptext.GetComponent<Text>().text = texto;

        temptext.SetActive(true);
        temptext.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

        temptext.transform.DOShakeScale(0.3f, 0.9f, 8, 80f);
        yield return new WaitForSeconds(0.8f);

        temptext.GetComponent<Text>().material.DOColor(Color.clear, 2f);
        yield return new WaitForSeconds(0.7f);

        temptext.GetComponent<Text>().material = text_material;
        temptext.GetComponent<Text>().color = Color.white;
        temptext.transform.DOKill();

        temptext.SetActive(false);
    }

    public int GetCurrentEnemyLevel()
    {
        return _currentEnemyLevel;
    }

    public void GameOverPanel()
    {
        combinationManager.MoveButtonsOut();
        _guiManager.PlayerGameOverPanelOn();
        combinationManager.DisableButtonsInteraction();
        combinationManager.SetGameOn(false);
    }
    /// <summary>
    /// called in gameoverpanel
    /// </summary>
    public void RestartGame()
    {
        _playerScore = 0;
        playerScoreUI = 0;
        _zazucKilled = 0;
        _makulaKilled = 0;
        _kogiKilled = 0;
        _enemyCount = 0;
        _worldNumber = 0;
        combinationManager.MoveButtonsIn();
        combinationManager.timeToResolveCombination = combinationManager.original_timeToResolveCombination;
        _guiManager.GameOverPanelOff();
        enemyPooler.RemoveElement(enemyController.transform);
        combinationManager.EnableButtonsInteraction();
        _currentEnemyLevel = 0;
        combinationManager.ResetCombination();
        PrepareBackgroundLevel(1);
        AudioManager.Instance.PlayMusicLevel1();
        playerManager.OnAttackFinished();
        playerManager.life = 9; // TODO remove when real implementation is done
        GetNewEnemy(1);
        combinationManager.SetGameOn(true);
        combinationManager.HideWinLoseText();
    }

    /// <summary>
    /// called when you see an ad
    /// </summary>
    public void ContinueGame()
    {
        combinationManager.MoveButtonsIn();
        combinationManager.timeToResolveCombination = combinationManager.original_timeToResolveCombination;
        _guiManager.GameOverPanelOff();
        enemyPooler.RemoveElement(enemyController.transform);
        combinationManager.EnableButtonsInteraction();
        combinationManager.GenerateCombination();
        PrepareBackgroundLevel(1);
        AudioManager.Instance.PlayMusicLevel1();
        playerManager.OnAttackFinished();
        playerManager.life = 9; // TODO remove when real implementation is done
        GetNewEnemy(1);
        combinationManager.SetGameOn(true);

    }

    public void PauseGame()
    {
        state = GameState.Paused;
        _guiManager.PausePanelOn();
        combinationManager.MoveButtonsOut();
    }

    public void UnPauseGame()
    {
        state = GameState.Running;
        _guiManager.PausePanelOff();
        combinationManager.MoveButtonsIn();
    }
    //private int _kogiKilled = 0;
    //private int _zazucKilled = 0;
    //private int _makulaKilled = 0;
    //private int _playerScore = 0;
    //private int _currentEnemyLevel = 0;
    //private int _enemyCount = 0;

    #region World Level Generation

    private void PrepareBackgroundLevel(int worldlevel)
    {
        for (int i = 0; i < worldspritesLevelList.Length; i++)
        {
            worldspritesLevelList[i].SetActive(false);
        }

        worldspritesLevelList[worldlevel - 1].SetActive(true); //array starts at 0 so world 1 is item[0];

        _worldNumber = worldlevel;
    }

    #endregion




    public int GetKogiKilled()
    {
        return _kogiKilled;
    }
    public int GetZazuKilled()
    {
        return _zazucKilled;
    }
    public int GetMakulaKilled()
    {
        return _makulaKilled;
    }
    public float GetPlayerScore()
    {
        return _playerScore;
    }

    public int GetPlayerScoreUI()
    {
        return (int)playerScoreUI;
    }

    public int GetTotalEnemyKilled()
    {
        return _kogiKilled + _zazucKilled + _makulaKilled;
    }

    public int GetCurrentWorldNumber()
    {
        return _worldNumber;
    }

}
