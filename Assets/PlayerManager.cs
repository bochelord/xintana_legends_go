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

    private XintanaProfile profile;
    private Rad_GuiManager _guiManager;
    private XintanaStructure _newXintana;
    private bool _profileExperienceAdded = false;
    private float totalExpPerGame = 0;
    private Animator playerAnimator;

    void Awake()
    {
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        profile = Rad_SaveManager.profile;
        weaponEquipped = profile.weaponEquipped;
        level = profile.level;
        experience = profile.experience;
        _newXintana = new XintanaStructure().GenerateXintanaWithLevel(weaponEquipped, level);
        maxLife = _newXintana.life;
        life = GetMaxLife();
        attack = _newXintana.damage;

        playerAnimator = this.GetComponent<Animator>();

        playerAnimator.SetInteger("EquippedWeapon", (int)weaponEquipped);

        playerAnimator.SetInteger("EquippedWeapon", 0);
        playerAnimator.SetBool("Attacking", false);
        //playerAnimator.SetInteger("AnimState", 0);

        //Debug.Log("Weapon Equipped is " + weaponEquipped.ToString() + " with this index:" + (int)weaponEquipped);

    }

    public float GetMaxLife()
    {
        return maxLife;
    }



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
    public void OnAirAttackFinished()
    {
        playerAnimator.SetBool("Attacking", false);
        playerAnimator.SetInteger("AnimState", 20);
    }

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

    public void AddExperience(int value)
    {
        totalExpPerGame += value;
        experience += value;

        if (experience >= GetMaxExperience())
        {
            experience -= GetMaxExperience(); // Reset experience
        }
        _guiManager.AddExperienceToSlider(value);
    }
    public int GetMaxExperience()
    {
        int _maxExperience = (level + 1) * pointsPerLevel;
        return _maxExperience;
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
            totalExpPerGame -= (GetMaxExperience() - Rad_SaveManager.profile.experience);
            _profileExperienceAdded = true;
        }
        else
        {
            totalExpPerGame -= GetMaxExperience();
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
