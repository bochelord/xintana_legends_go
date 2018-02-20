using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { yellow,red,blue,green, black}
public class PlayerManager : MonoBehaviour {


    [Header("Stats")]
    private float maxLife = 9;
    public float life;
    public int level;
    public float experience;
    public float attack = 1.5f;
    public WeaponType weaponEquipped;
    [Tooltip("Every level will be increased by this amount")]
    public int pointsPerLevel = 1000;
    [Header("Particle")]
    public Transform particlePosition;
    [Header("Power Ups times")]
    public float slowtimerTime = 10;
    public float criticsPowerUpTime = 20;
    public float healPowerUpTime = 30;
    public float timeGemsPowerUp = 10;

    private LevelManager _levelManager;
    private CombinationManager _combinationManager;
    private XintanaProfile profile;
    private Rad_GuiManager _guiManager;
    private XintanaStructure _newXintana;
    private bool _profileExperienceAdded = false;
    private float totalExpPerGame = 0;
    private Animator playerAnimator;

    void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _combinationManager = FindObjectOfType<CombinationManager>();
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        profile = Rad_SaveManager.profile;
        weaponEquipped = profile.weaponEquipped;
        //weaponEquipped = WeaponType.black;
        level = profile.level;
        experience = profile.experience;
        _newXintana = new XintanaStructure().GenerateXintanaWithLevel(weaponEquipped, level);
        maxLife = _newXintana.life;
        life = GetMaxLife();
        attack = _newXintana.damage;

        playerAnimator = this.GetComponent<Animator>();

        playerAnimator.SetInteger("EquippedWeapon", (int)weaponEquipped);

        //playerAnimator.SetInteger("EquippedWeapon", 4);
        playerAnimator.SetBool("Attacking", false);
        //playerAnimator.SetInteger("AnimState", 0);

        //Debug.Log("Weapon Equipped is " + weaponEquipped.ToString() + " with this index:" + (int)weaponEquipped);

        //level = 5;
        //print("XP TO LEVEL UP FROM level "+level+" is:"+GetExperienceToLevelUp());

    }

    public float GetMaxLife()
    {
        return maxLife;
    }

    public void StartPowerUp()
    {
        switch (weaponEquipped)
        {
            case WeaponType.black:
                CriticsPowerUp();
                break;
            case WeaponType.blue:
                GemsPowerUp();
                break;
            case WeaponType.green:
                HealPowerUp();
                break;
            case WeaponType.yellow:
                SlowTimerPowerUp();
                break;
        }

    }
    #region PowerUps
    void SlowTimerPowerUp()
    {
        StartCoroutine(SlowTimer(slowtimerTime));
        _guiManager.RemovePowerUpSliderValueForPowerUpTime(slowtimerTime);
    }

    IEnumerator SlowTimer(float delay)
    {
        _guiManager.SetPowerUpOn(true);
        _combinationManager.timerSpeed = 0.5f;
        yield return new WaitForSeconds(delay);
        _combinationManager.timerSpeed = 1f;
        _guiManager.SetPowerUpOn(false);
    }

    void CriticsPowerUp()
    {
        StartCoroutine(Critics(criticsPowerUpTime));
        _guiManager.RemovePowerUpSliderValueForPowerUpTime(criticsPowerUpTime);
    }
    IEnumerator Critics(float delay)
    {
        _guiManager.SetPowerUpOn(true);
        _levelManager.SetCriticsPowerUp(true);
        yield return new WaitForSeconds(delay);
        _levelManager.SetCriticsPowerUp(false);
        _guiManager.SetPowerUpOn(false);
    }

    void HealPowerUp()
    {
        StartCoroutine(Heal(healPowerUpTime));
        _guiManager.RemovePowerUpSliderValueForPowerUpTime(healPowerUpTime);
    }

    IEnumerator Heal(float delay)
    {
        _guiManager.SetPowerUpOn(true);
        _levelManager.SetHealPowerUp(true);
        yield return new WaitForSeconds(delay);
        _levelManager.SetHealPowerUp(false);
        _guiManager.SetPowerUpOn(false);
    }

    void GemsPowerUp()
    {
        StartCoroutine(ChangeGemsPowerUp(timeGemsPowerUp));
        _guiManager.RemovePowerUpSliderValueForPowerUpTime(timeGemsPowerUp);
    }
    IEnumerator ChangeGemsPowerUp(float delay)
    {
        _guiManager.SetPowerUpOn(true);
        _combinationManager.SetGemsPowerUp(true);
        yield return new WaitForSeconds(delay);
        _combinationManager.SetGemsPowerUp(false);
        _guiManager.SetPowerUpOn(false);
    }
    #endregion
    #region Animations
    /// <summary>
    /// Callback from Animation Event 
    /// </summary>
    public void OnAttackFinished()
    {
        AnimationToIdle();
    }

    /// <summary>
    /// Callback for Animation Event Air Attack
    /// </summary>
    //public void OnAirAttackFinished()
    //{
    //    playerAnimator.SetBool("Attacking", false);
    //    playerAnimator.SetInteger("AnimState", 20);
    //}

    /// <summary>
    /// CAllback for Animation Event Wing Jump
    /// </summary>
    public void OnAirWingJumpFinished()
    {
        AnimationToIdle();
    }

    /// <summary>
    /// Sets the animation parameter to Idle Animation
    /// </summary>
    private void AnimationToIdle()
    {
        playerAnimator.SetBool("Attacking", false);
        playerAnimator.SetInteger("AnimState", 0);

        
        
    }

    #endregion



    public void ReceiveDamage(float damage)
    {
        life -= damage;
    }
    public void HealForValue(float damage)
    {
        life += damage;
    }
    public void AddExperience(int value)
    {
        totalExpPerGame += value;
        experience += value;

        if (experience >= GetExperienceToLevelUp())
        {
            experience -= GetExperienceToLevelUp(); // Reset experience
        }
        _guiManager.AddExperienceToSlider(value);
    }
    public int GetExperienceToLevelUp()
    {
        //int _neededToLevelUpExperience = (level + 1) * pointsPerLevel;

        int _neededToLevelUpExperience = Formulas.GetXpToLevelup(level);
        

        return _neededToLevelUpExperience;
    }

    public void SavePlayerStats()
    {
        Debug.Log("EXPERIENCE ADDED >>>>>>>>>>>>>>>>>>> " + experience);
        profile.experience = experience;
        profile.level = level;
    }
    public float GetTotalExpPerGame()
    {
        return totalExpPerGame;
    }
    
    public void LevelUpAndUpdateExperience()
    {
        if (!_profileExperienceAdded)
        {
            totalExpPerGame -= (GetExperienceToLevelUp() - Rad_SaveManager.profile.experience);
            _profileExperienceAdded = true;
        }
        else
        {
            totalExpPerGame -= GetExperienceToLevelUp();
        }
       
        level++;
        _newXintana = new XintanaStructure().GenerateXintanaWithLevel(weaponEquipped, level);
        maxLife = _newXintana.life;
        life = GetMaxLife();
        attack = _newXintana.damage;
    }
    public bool GetExperienceAddedFromProfile()
    {
        return _profileExperienceAdded;
    }
}
