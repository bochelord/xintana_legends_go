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

    private ParticlePooler _particlePooler;
    private LevelManager _levelManager;
    private CombinationManager _combinationManager;
    private XintanaProfile profile;
    private Rad_GuiManager _guiManager;
    private XintanaStructure _newXintana;
    private bool _profileExperienceAdded = false;
    private float totalExpPerGame = 0;
    private Animator playerAnimator;
    private GameObject _powerUpParticle;
    private GameObject _powerUpButtonParticle;
    private bool _powerUpOn = false;
    
    void Awake()
    {
        _particlePooler = FindObjectOfType<ParticlePooler>();
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
        _powerUpOn = true;
        switch (weaponEquipped)
        {
            case WeaponType.black:
                _guiManager.SetAndActivatePowerUpText("Critics!!");
                CriticsPowerUp();
                break;
            case WeaponType.blue:
                _guiManager.SetAndActivatePowerUpText("Break any gem !!");
                GemsPowerUp();
                break;
            case WeaponType.green:
                _guiManager.SetAndActivatePowerUpText("Heal!!");
                HealPowerUp();
                break;
            case WeaponType.yellow:
                _guiManager.SetAndActivatePowerUpText("Time Freezed !!");
                SlowTimerPowerUp();
                break;
        }

    }
    public void StopPowerUp()
    {
        _powerUpOn = false;
        switch (weaponEquipped)
        {
            case WeaponType.black:
                StopCriticsPowerUp();
                break;
            case WeaponType.blue:
                StopGemsPowerUp();
                break;
            case WeaponType.green:
                StopHealPowerUp();
                break;
            case WeaponType.yellow:
                StopTimerPowerUp();
                break;
        }

    }
    #region PowerUps
    void SlowTimerPowerUp()
    {
        _powerUpParticle = _particlePooler.GetPooledFreezeParticle();
        _powerUpParticle.SetActive(true);
        _guiManager.EnablePowerUpButtonVFX_SlowTime();
        _guiManager.RemovePowerUpSliderValueForPowerUpTime(slowtimerTime);
        _guiManager.SetPowerUpOn(true);
        _combinationManager.timerSpeed = 0.5f;
    }

    void StopTimerPowerUp()
    {
        _particlePooler.RemoveElement(_powerUpParticle.transform);
        _combinationManager.timerSpeed = 1f;
        _guiManager.SetPowerUpOn(false);
        _guiManager.DisablePowerUpButtonVFX_SlowTime();
    }

    void CriticsPowerUp()
    {
        _powerUpParticle = _particlePooler.GetPooledCriticsParticle();
        _powerUpParticle.SetActive(true);
        _guiManager.RemovePowerUpSliderValueForPowerUpTime(criticsPowerUpTime);
        _guiManager.SetPowerUpOn(true);
        _guiManager.EnablePowerUpButtonVFX_Crits();
        _levelManager.SetCriticsPowerUp(true);
    }
    void StopCriticsPowerUp()
    {
        _particlePooler.RemoveElement(_powerUpParticle.transform);
        _levelManager.SetCriticsPowerUp(false);
        _guiManager.SetPowerUpOn(false);
        _guiManager.DisablePowerUpButtonVFX_Crits();
    }
    void HealPowerUp()
    {
        _powerUpParticle = _particlePooler.GetPooledHealParticle();
        _powerUpParticle.SetActive(true);
        _guiManager.RemovePowerUpSliderValueForPowerUpTime(healPowerUpTime);
        _guiManager.SetPowerUpOn(true);
        _guiManager.EnablePowerUpButtonVFX_Heal();
        _levelManager.SetHealPowerUp(true);
    }
    void StopHealPowerUp()
    {
        _particlePooler.RemoveElement(_powerUpParticle.transform);
        _levelManager.SetHealPowerUp(false);
        _guiManager.SetPowerUpOn(false);
        _guiManager.EnablePowerUpButtonVFX_Heal();
    }
    void GemsPowerUp()
    {
        _powerUpParticle = _particlePooler.GetPooledGemsPowerUpParticle();
        _powerUpParticle.SetActive(true);
        _guiManager.RemovePowerUpSliderValueForPowerUpTime(timeGemsPowerUp);
        _guiManager.SetPowerUpOn(true);
        _guiManager.EnablePowerUpButtonVFX_GemBreaker();
        _combinationManager.SetGemsPowerUp(true);
    }
    void StopGemsPowerUp()
    {
        _particlePooler.RemoveElement(_powerUpParticle.transform);
        _combinationManager.SetGemsPowerUp(false);
        _guiManager.SetPowerUpOn(false);
        _guiManager.EnablePowerUpButtonVFX_GemBreaker();
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
    public void AddExperience(float value)
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
        AmazonAchievementsManager.Instance.IncrementLevelAchievements(level);
    }
    public bool GetExperienceAddedFromProfile()
    {
        return _profileExperienceAdded;
    }
    public bool GetPowerUpOn()
    {
        return _powerUpOn;
    }
}
