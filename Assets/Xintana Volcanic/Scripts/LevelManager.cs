﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public CoinsPooler coinsPooler;
    public ParticlePooler particlePooler;
    public EnemiesPooler enemyPooler;
    public Transform enemyContainer;
    public bool enemyKilled = false;
    public GameObject GIFRecordingprefab;

    [Header("Level Control Values")]
    [Tooltip("This is measure in number of fights")]
    public int bossFightFrecuency = 7; 
    public int combinationIncreaseFrecuency = 3;

    [Header("Max ads skipped")]
    public int adsSkipped = 5;
    [Header("Backgrounds")]
    //public GameObject worldspritesLevel1;
    //public GameObject worldspritesLevel2;
    //public GameObject worldspritesLevel3;
    public GameObject[] worldspritesLevelList;

    private int _kogiKilled = 0;
    private int _zazucKilled = 0;
    private int _makulaKilled = 0;
    private float _playerScore = 0;
    private int _currentEnemyLevel = 1;
    private int _enemyCount = 0;
    private int _eachthreetimes = 3;
    private float timerSafe;
    private int _worldNumber = 1;
    private int numberOfRounds = 0;
    private float playerScoreUI;

    public ScreenShot screenshot;
    private PlayerManager _playerManager;
    private CombinationManager combinationManager;
    private Rad_GuiManager _guiManager;
    private AdsManager adManager;
    private HealthBarControllerBoss healthBarController;
    private List<Transform> inactiveHUDTextList = new List<Transform>();


    private bool musiclevel2_AlreadyPlayed = false;
    private bool musiclevel3_AlreadyPlayed = false;
    private bool _criticsPowerUp = false;
    private bool _extraLifePurchased = false;
    private bool _healPowerUp = false;
    //private int currentWorld = 1;

    void Awake()
    {
        screenshot = FindObjectOfType<ScreenShot>();
        combinationManager = FindObjectOfType<CombinationManager>();
        _playerManager = player.GetComponent<PlayerManager>();
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        adManager = FindObjectOfType<AdsManager>();
        healthBarController = FindObjectOfType<HealthBarControllerBoss>();
        _extraLifePurchased = Rad_SaveManager.profile.extraLife;
    }

    void Start()
    {
        _guiManager.UpdateIcons();
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

        if (_criticsPowerUp)
        {
            damagedone = in_damage * 2;
            critico = true;
        }
        else
        {
            damagedone = Formulas.GetDamageCalculated(in_damage, out critico);
        }



        if (damagedone > enemyController.GetLife())
        {
            //we are goinna kill the enemy so we trigger the double jumping animation
            player.GetComponent<Animator>().SetInteger("AnimState", 20);
            if (_healPowerUp)
            {
                _playerManager.HealForValue(damagedone);
                LaunchShowHUDText(player.transform.position + new Vector3(0.25f, 1.5f, 0), damagedone.ToString("f1"), new Color32(0,255, 0, 255),false);
                StartCoroutine(HealParticle(3));
                Debug.Log("asdasdada");

            }
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

        enemyController.DamagedAnimation();
        
        LaunchShowHUDText(enemyContainer.transform.position + new Vector3(-0.25f,1.5f,0), damagedone.ToString("F1"), new Color32(245, 141, 12, 255),false); /// TODO This has to be feed with the proper damage coming from the playerManager

        AudioManager.Instance.Play_XintanaAttack_1();

        //damage to enemy
        //
        _guiManager.AddPowerUpSlider(damagedone);
        enemyController.ApplyDamageToEnemy(damagedone);
        if (critico)
        {
            LaunchShowHUDText(enemyContainer.transform.position + new Vector3(0.25f, 1.5f, 0), "crit!", new Color32(234, 57, 4, 255),true);
        }
    }
    
    IEnumerator HealParticle(float delay)
    {
        GameObject obj = particlePooler.GetPooledHealPlayerParticle();
        obj.transform.position = player.transform.position;
        yield return new WaitForSeconds(delay);
        particlePooler.RemoveElement(obj.transform);
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

        enemyController.AttackAnimation();


        float _damageDone = enemyController.GetDamageDoneByEnemy();
        LaunchShowHUDText(player.transform.position + new Vector3(0, 1.5f, 0), _damageDone.ToString("F1"), new Color32(245, 141, 12, 255),false);

        _playerManager.ReceiveDamage(_damageDone);

        AudioManager.Instance.Play_XintanaHit();


        if(_playerManager.life <= 0 && SIS.DBManager.GetPurchase("si_1up") <= 0)
        {
            screenshot.TakeDeathScreenshot();

            //screenshot.GetCombatScreenshot();

            //Continue or go to main menu
            AudioManager.Instance.Play_XintanaDeath();
            player.GetComponent<Animator>().SetInteger("AnimState",4);
            AnalyticsManager.Instance.GameOver_Event((int)_playerScore, _enemyCount + 1, _worldNumber);
            AddNemesisCount();
            if (!adManager.adViewed && Rad_SaveManager.profile.adsSkipped <= adsSkipped && !Rad_SaveManager.profile.noAds)
            {
                combinationManager.SetGameOn(false);
                combinationManager.MoveButtonsOut();
                combinationManager.DestroyCombination();
                StartCoroutine(FunctionLibrary.CallWithDelay(_guiManager.ShowAdPanel,2f));
                //_guiManager.ShowAdPanel();
            }
            else if (!adManager.adViewed && Rad_SaveManager.profile.adsSkipped >= adsSkipped && !Rad_SaveManager.profile.noAds)
            {
                combinationManager.SetGameOn(false);
                combinationManager.MoveButtonsOut();
                combinationManager.DestroyCombination();
                StartCoroutine(FunctionLibrary.CallWithDelay(adManager.ShowAdNoReward, 2f));
            }
            else
            {
                StartCoroutine(FunctionLibrary.CallWithDelay(GameOverPanel, 2.5f));
                adManager.adViewed = false; 
            }
        }
        else if (_playerManager.life <= 0 && SIS.DBManager.GetPurchase("si_1up") > 0)
        {
            combinationManager.SetGameOn(false);
            //SIS.DBManager.IncreasePurchase("shop_item_01", -1);
            SIS.DBManager.RemovePurchase("si_1up");
            SIS.DBManager.RemovePurchaseUI("si_1up");
            Rad_SaveManager.profile.extraLife = false;
            _extraLifePurchased = false;
            combinationManager.MoveButtonsOut();
            StartCoroutine(FunctionLibrary.CallWithDelay(_guiManager.ShowContinuePanel, 2f));
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

    public void AddPlayerScoreAndGetNewEnemy(EnemyType typein, int levelin)
    {
        float timeRemaining = combinationManager.timerSlider.value;
        int _enemyPoints = 0;
        //Debug.Log("MECAGO EN TODA TU PUTA MADRE");


        // TODO -- TODO
        // GIF recording is not working cause it corrupts the HUD GUI at least on the Editor
        //
        //We start the GIF recording
        //Instantiate(GIFRecordingprefab);

        _enemyPoints = enemyController.score;

        //scoreParticle On
        //score sound on
        combinationManager.ChangeTimerSliderColor(-1);
        AudioManager.Instance.Play_AddScore();
        DOTween.To(() => playerScoreUI, x => playerScoreUI = x, playerScoreUI + _enemyPoints * levelin * timeRemaining, 3);
        DOTween.To(() => combinationManager.timerSlider.value, x => combinationManager.timerSlider.value = x, 0, 3f).OnComplete(() =>
         {
             combinationManager.ChangeTimerSliderColor(1);
             GetNewEnemy(1.5f);
             _playerScore += _enemyPoints * levelin * timeRemaining;
             GetEnemyDeadParticle();
             enemyPooler.RemoveElement(enemyController.transform);
             AudioManager.Instance.Stop_AddScore();

         });

    }
    public void GetEnemyDeadParticle()
    {
        StartCoroutine(EnemyDeadParticle(2f, enemyController.transform.position));

    }
    IEnumerator EnemyDeadParticle(float time, Vector2 position)
    {
        if (enemyController.type == EnemyType.makula)
        {
            SpawnShellForEndWorld();
        }
        GameObject obj = particlePooler.GetPooledDeadEnemyParticle();
        obj.SetActive(true);
        obj.transform.position = position;
        yield return new WaitForSeconds(time);
        particlePooler.RemoveElement(obj.transform);

    }

    private void SpawnShellForEndWorld()
    {
        Rad_SaveManager.profile.shells++;
        GameObject _shell = coinsPooler.GetPooledShell();
        _shell.SetActive(true);
        _shell.transform.position = enemyController.transform.position;
        _shell.transform.DOScale(0, 0);
        _shell.transform.DOScale(2, 1.5f).OnComplete(() =>
        {
            AudioManager.Instance.Play_GemCollect();
            _shell.transform.DOScale(1, 0);
            StartCoroutine(SpawnShellCollecetedParticle(_shell.transform.position));
            coinsPooler.RemoveElement(_shell.transform);
            
        });
    }
    IEnumerator SpawnShellCollecetedParticle(Vector3 pos)
    {
        GameObject _shellparticle = particlePooler.GetPooledShellCollectedParticle();
        _shellparticle.transform.position = pos;
        _shellparticle.SetActive(true);
        yield return new WaitForSeconds(2);
        particlePooler.RemoveElement(_shellparticle.transform);
    }
    public void VanishPlayer()
    {
        StartCoroutine(PlayerDeadParticle(1.5f, _playerManager.particlePosition.position));
    }
    IEnumerator PlayerDeadParticle(float time, Vector2 position)
    {
        GameObject obj = particlePooler.GetPooledDeadEnemyParticle();
        obj.SetActive(true);
        obj.transform.position = position;
        _playerManager.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        particlePooler.RemoveElement(obj.transform);
    }
    public void AddNemesisCount()
    {
        switch (enemyController.type)
        {
            case EnemyType.blackKnight:
                Rad_SaveManager.profile.timesKilledByBlackKnight++;
                break;
            case EnemyType.kogi:
                Rad_SaveManager.profile.timesKilledByKogi++;
                break;
            case EnemyType.makula:
                Rad_SaveManager.profile.timesKilledByMakula++;
                break;
            case EnemyType.zazuc:
                Rad_SaveManager.profile.timesKilledByZazuc++;
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

        if (_enemyCount == (bossFightFrecuency * (_worldNumber + numberOfRounds * worldspritesLevelList.Length)))//final boss is summont on its frecuency and per world
        {
            enemy = enemyPooler.GetBossObject();
            timerSafe = combinationManager.timeToResolveCombination;
            combinationManager.timeToResolveCombination *= 2;

            AudioManager.Instance.PlayBossMusic();
            AudioManager.Instance.Play_Boss();
        }
        //numberOfRounds * sprites.length because probably player will end worlds, we reset _worldnumber to 1 in PrepareBackgroundLevel()
        else if (_enemyCount == (bossFightFrecuency *( _worldNumber + numberOfRounds * worldspritesLevelList.Length)) + 1)//after a boss fight we reset the timer to whatever it was before it and
        {
            combinationManager.timeToResolveCombination = timerSafe;
            enemy = enemyPooler.GetPooledObject(_worldNumber);

            _worldNumber++;

            //We change the level to another world
            PrepareBackgroundLevel(_worldNumber);

            int _musicArrayIndex = 0;
            if (_worldNumber > AudioManager.Instance.musicArray.Length)
            {
                _musicArrayIndex = _worldNumber - AudioManager.Instance.musicArray.Length;
            }
            else
            {
                _musicArrayIndex = _worldNumber - 1;
            }

            if (AudioManager.Instance.musicPlayer.clip != AudioManager.Instance.musicArray[_musicArrayIndex])
            {
                AudioManager.Instance.PlayMusicLevel(_worldNumber);

                AudioManager.Instance.Play_NextStage();
            }
        }
        else
        {
            enemy = enemyPooler.GetPooledObject(_worldNumber);
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
        combinationManager.ResetGame();
        state = GameState.Running;
    }
    

    public void LaunchShowHUDText(Vector2 pos, string texto, Color color_in, bool isCrit)
    {
        StartCoroutine(ShowHUDText(pos, texto, color_in, isCrit));
    }

    public IEnumerator ShowHUDText(Vector2 pos, string texto, Color color_in, bool isCrit)
    {
        Vector2 posConverted = RectTransformUtility.WorldToScreenPoint(null, pos);

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

        if (isCrit)
        {
            temptext.transform.DOShakeScale(0.5f, 1f, 10, 90f);
        }
        else
        {
            temptext.transform.DOShakeScale(0.3f, 0.9f, 8, 80f);
        }
        
        //temptext.transform.DOShakeScale(0.8f, 1f, 8, 80f);
        yield return new WaitForSeconds(0.8f);

        temptext.GetComponent<Text>().material.DOColor(Color.clear, 2f);
        temptext.transform.DOMoveY(temptext.transform.position.y + 1f, 1.5f);
        yield return new WaitForSeconds(0.7f);

        temptext.GetComponent<Text>().material = text_material;
        temptext.GetComponent<Text>().color = Color.white;
        temptext.transform.DOKill();

        temptext.SetActive(false);
        inactiveHUDTextList.Add(temptext.transform);
    }

    public int GetCurrentEnemyLevel()
    {
        return _currentEnemyLevel;
    }

    public void GameOverPanel()
    {
        VanishPlayer();
        combinationManager.MoveButtonsOut();
        StartCoroutine(FunctionLibrary.CallWithDelay(_guiManager.PlayerGameOverPanelOn,2));
        combinationManager.DisableButtonsInteraction();
        combinationManager.SetGameOn(false);
    }
    /// <summary>
    /// called in gameoverpanel
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("LoadingScreen");
    }


    /// <summary>
    /// called when you see an ad
    /// </summary>
    public void ContinueGame()
    {
        _guiManager.UpdateIcons();
        combinationManager.timeToResolveCombination = combinationManager.original_timeToResolveCombination;
        //_guiManager.GameOverPanelOff();
        enemyPooler.RemoveElement(enemyController.transform);
        combinationManager.MoveButtonsIn();
        //combinationManager.ResetCurrentLenghtCombination();
        PrepareBackgroundLevel(_worldNumber);
        AudioManager.Instance.PlayMusicLevel1();
        _playerManager.OnAttackFinished();
        _playerManager.life = _playerManager.GetMaxLife();
        GetNewEnemy(1);
    }

    public void PauseGame()
    {
        state = GameState.Paused;
        combinationManager.MoveButtonsOut();
    }

    public void UnPauseGame()
    {
        state = GameState.Running;
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
        if (worldlevel > worldspritesLevelList.Length)
        {
            numberOfRounds++;
            worldlevel = 1;
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
    public void SetPlayerScore(int value)
    {
        _playerScore = value;
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
        return _enemyCount;
    }

    public int GetCurrentWorldNumber()
    {
        return _worldNumber + (numberOfRounds * worldspritesLevelList.Length);
    }

    public void SetCriticsPowerUp(bool value)
    {
        _criticsPowerUp = value;
    }

    public void SetHealPowerUp(bool value)
    {
        _healPowerUp = value;
    }
}