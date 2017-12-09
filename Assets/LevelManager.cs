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
public class LevelManager : MonoBehaviour {

    public GameObject enemy;
    public EnemyController enemyController;

    public GameObject HitPrefabRight;
    public GameObject damageFxPrefab;
    public GameObject player;
    public GameObject HUDTextContainer;
    public GameObject HUDTextPrefab;
    public GameObject HUDPlayAgainContainer;
    private PlayerManager playerManager;
    private CombinationManager combinationManager;
    private Rad_GuiManager _guiManager;
    private List<Transform> inactiveHUDTextList = new List<Transform>();
    public EnemiesPooler enemyPooler;
    public Transform enemyContainer;
    public bool enemyKilled = false;

    [Header("Backgrounds")]
    public GameObject worldspritesLevel1;
    public GameObject worldspritesLevel2;
    public GameObject worldspritesLevel3;

    private int _kogiKilled = 0;
    private int _zazucKilled = 0;
    private int _makulaKilled = 0;
    private float _playerScore = 0;
    private int _currentEnemyLevel = 0;
    private int _enemyCount = 0;
    private int _eachthreetimes = 3;
    private float timerSafe;

    private bool musiclevel2_AlreadyPlayed = false;
    private bool musiclevel3_AlreadyPlayed = false;


    void Awake()
    {
        combinationManager = FindObjectOfType<CombinationManager>();
        playerManager = player.GetComponent<PlayerManager>();
        _guiManager = FindObjectOfType<Rad_GuiManager>();
    }

    void Start()
    {
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
            //TODO ad view panel
            //Continue or go to main menu
            AudioManager.Instance.Play_XintanaDeath();
            player.GetComponent<Animator>().SetInteger("AnimState",4);
            StartCoroutine(FunctionLibrary.CallWithDelay(GameOverPanel, 1.5f));
            
        }
        
    }

    public void GetNewEnemy(float delay)
    {
        
        StartCoroutine(CoroGetNewEnemy(delay));
    }
    
    private void PrepareBackgroundLevel(int level)
    {
        worldspritesLevel1.SetActive(false);
        worldspritesLevel2.SetActive(false);
        worldspritesLevel3.SetActive(false);

        switch (level)
        {
            case 1:
                worldspritesLevel1.SetActive(true);
                break;
            case 2:
                worldspritesLevel2.SetActive(true);
                break;
            case 3:
                worldspritesLevel3.SetActive(true);
                break;
            //default:
            //    Debug.LogError("Level" + level + "does not exist!!! Beware DUDE!");
        }
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

    public void AddPlayerScore()
    {
        float timeRemaining = combinationManager.timerSlider.value;
        switch (enemyController.type)
        {
            case EnemyType.kogi:
                _playerScore += 2000 * GetCurrentEnemyLevel() * timeRemaining;
                break;
            case EnemyType.makula:
                _playerScore += 5000 * GetCurrentEnemyLevel() *timeRemaining;
                break;
            case EnemyType.zazuc:
                _playerScore += 1500 * GetCurrentEnemyLevel() * timeRemaining;
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

        if (_enemyCount == 7 || _enemyCount == 14 || _enemyCount == 21)//each 7 enemies killed we summon a final boss
        {
            enemy = enemyPooler.GetBossObject();
            timerSafe = combinationManager.timeToResolveCombination;
            combinationManager.timeToResolveCombination *= 2;

            AudioManager.Instance.PlayBossMusic();

            //AudioManager.Instance.PlayBossMusicAndPauseMain();

        }
        else if (_enemyCount == 8 || _enemyCount == 15 || _enemyCount == 22)//after a boss fight we reset the timer to whatever it was before it
        {
            combinationManager.timeToResolveCombination = timerSafe;
            enemy = enemyPooler.GetPooledObject();
            //AudioManager.Instance.StopBossMusicAndResumeMain();
        }
        else
        {
            enemy = enemyPooler.GetPooledObject();
        }
        
        //we control the worldsprites based on the amount of enemies 
        if (_enemyCount > 7 && _enemyCount<=14)
        {
            //level2
            PrepareBackgroundLevel(2);
            if (AudioManager.Instance.musicPlayer.clip != AudioManager.Instance.musicLevel2 && !musiclevel2_AlreadyPlayed)
            {
                AudioManager.Instance.PlayMusicLevel2();
                musiclevel2_AlreadyPlayed = true;
            }
            
        }

        if (_enemyCount > 14)
        {
            //level3
            PrepareBackgroundLevel(3);
            if (AudioManager.Instance.musicPlayer.clip != AudioManager.Instance.musicLevel3 && !musiclevel3_AlreadyPlayed)
            {
                AudioManager.Instance.PlayMusicLevel3();
                musiclevel3_AlreadyPlayed = true;
            }
        }

        enemyController = enemy.GetComponent<EnemyController>();
        enemy.transform.position = enemyContainer.position;
        _guiManager.enemyText.text = enemyController.type.ToString();
        enemy.SetActive(true);
        enemy.transform.SetParent(enemyContainer);
        enemyKilled = false;
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
        _guiManager.GameOverPanelOn();
        combinationManager.DisableButtonsInteraction();
        combinationManager.SetGameOn(false);
    }
    /// <summary>
    /// called in gameoverpanel
    /// </summary>
    public void RestartGame()
    {
        _playerScore = 0;
        _zazucKilled = 0;
        _makulaKilled = 0;
        _kogiKilled = 0;
        _enemyCount = 0;
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
    }

    //private int _kogiKilled = 0;
    //private int _zazucKilled = 0;
    //private int _makulaKilled = 0;
    //private int _playerScore = 0;
    //private int _currentEnemyLevel = 0;
    //private int _enemyCount = 0;

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

    public int GetTotalEnemyKilled()
    {
        return _kogiKilled + _zazucKilled + _makulaKilled;
    }



}
