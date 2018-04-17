using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public string nameID;
    public int level;
    public EnemyType type;
    public int score;
    private int _experience;
    public int appearsOnWorld;

    private Animator animator;
    private _2dxFX_HSV hsv_spriteFX;
    private float[] genotype;// /// - HueShift (0,360)          [0]
                                /// - Color Saturation (-2,2)   [1]
                                /// - Brightness (-2,2)         [2]
                                /// - size (0.275,0.325)        [3]
 
    private float life;
    private float startLife;
   
    private float damage;

    private EnemyStructure new_enemy;
    private bool critico;
    private EnemiesPooler pooler;
    private LevelManager levelManager;
    private HealthBarControllerBoss healthBarController;
    private PlayerManager _playerManager;

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        hsv_spriteFX = this.GetComponent<_2dxFX_HSV>();
        pooler = FindObjectOfType<EnemiesPooler>();
        levelManager = FindObjectOfType<LevelManager>();
        healthBarController = FindObjectOfType<HealthBarControllerBoss>();
        _playerManager = FindObjectOfType<PlayerManager>();

    }

    //void Start () {

    //    //new_enemy = new EnemyStructure().GenerateWithLevel(type,level);
    //    //yield return new WaitForSeconds(0.1f);
    //    animator = this.GetComponent<Animator>();
    //    hsv_spriteFX = this.GetComponent<_2dxFX_HSV>();
    //    pooler = this.GetComponentInParent<Pooler>();
    //}


    void OnEnable() // On Enable we need to replenish its life otherwise would appear as a Zombie
    {
        //yield return new WaitForSeconds(0.1f);
        if (levelManager)
        {
            new_enemy = new EnemyStructure().GenerateWithLevel(type, levelManager.GetCurrentEnemyLevel());
            level = levelManager.GetCurrentEnemyLevel();
        }else
        {
            new_enemy = new EnemyStructure().GenerateWithLevel(type, 1);
            level = 1;
        }

        _experience = (int)new_enemy.xp;
        //new_enemy = new EnemyStructure().GenerateWithLevel(type, level);
        life = new_enemy.life;
        startLife = new_enemy.life;
        damage = new_enemy.damage;

        //if (new_enemy.type == EnemyType.blackKnight || new_enemy.type == EnemyType.lavabeast || new_enemy.type == EnemyType.alchemist || new_enemy.type == EnemyType.devil || new_enemy.type == EnemyType.explorer || new_enemy.type == EnemyType.fireMage)

        if (new_enemy.type != EnemyType.makula && new_enemy.type != EnemyType.zazuc)
        {
            foreach (_2dxFX_HSV child_HSV in this.transform.GetComponentsInChildren(typeof(_2dxFX_HSV),true))
            {
                child_HSV._HueShift = new_enemy.dna_hue;
                child_HSV._Saturation = new_enemy.dna_colorsat;
                child_HSV._ValueBrightness = new_enemy.dna_brightness;
            }

        }
        else
        {
            this.GetComponent<_2dxFX_HSV>()._HueShift = new_enemy.dna_hue;
            this.GetComponent<_2dxFX_HSV>()._Saturation = new_enemy.dna_colorsat;
            this.GetComponent<_2dxFX_HSV>()._ValueBrightness = new_enemy.dna_brightness;
        }

        if (healthBarController)
        {
            healthBarController.SetScrollbarValue();
        }

    }
    /// <summary>
    /// Retu rns damage done by enemy based on standard fumble,critic (Fumble: less than 4 , Critic: more than 95)
    /// </summary>
    public float GetDamageDoneByEnemy()
    {
        damage = Formulas.GetDamageCalculated(damage, out critico);
        return damage;
    }

    /// <summary>
    /// Returns damage done by enemy with fumble and critic as parameters
    /// </summary>
    /// <param name="fumbleThreshold"></param>
    /// <param name="criticThreshold"></param>
    /// <returns></returns>
    public float GetDamageDoneByEnemy(int fumbleThreshold, int criticThreshold)
    {
        damage = Formulas.GetDamageCalculated(damage,fumbleThreshold,criticThreshold, out critico);
        return damage;
    }


    public void ApplyDamageToEnemy(float damage_in)
    {
        life = life - damage_in;
        levelManager.screenshot.TakeDeathScreenshot();
        //Screenshot added everytime you hit an enemy
        //levelManager.screenshot.TakeDeathScreenshot();

        if (life < 0)
        {
            KillEnemy();
        }
        
    }

    private void KillEnemy()
    {
        levelManager.state = GameState.Paused;
        levelManager.AddEnemiesKilledCount();
        levelManager.AddPlayerScoreAndGetNewEnemy(this.type, level);
        levelManager.enemyKilled = true;
        AchievementsManager.Instance.IncrementKillsAchievements();
        float _expMultiplier = (float)level / _playerManager.level;
        if(_expMultiplier > 1)
        {
            _expMultiplier = 1;
        }

        _playerManager.AddExperience(_experience * _expMultiplier);
        levelManager.LaunchShowHUDText(this.transform.position + new Vector3(-0.25f,0.25f,0f), "+" + _experience * _expMultiplier + "  XP", new Color32(85, 187, 17, 255),false);
        //levelManager.GetNewEnemy(2.5f);
        DeadAnimation();
    }

    

    public float GetLife()
    {
        return life;
    }
    public void SetLife(float value)
    {
        life = value;
    }
    public float GetStartLife()
    {
        return startLife;
    }


    #region Animations

    public void IdleAnimation()
    {
        switch (type)
        {
            case EnemyType.makula:
                //animator.SetFloat("AnimState", 0);
                animator.SetTrigger("idle_1");
                break;
            default:
                animator.SetTrigger("idle_1");
                break;

        }

        
    }

    public void AttackAnimation()
    {

        switch (type)
        {
            case EnemyType.makula:
                //animator.SetFloat("AnimState", 1);
                animator.SetTrigger("skill_1");
                print("MAKULA muevete mamona,....");
                break;
            default:
                if (RadUtils.d100() <= 49)
                {
                    animator.SetTrigger("skill_1");
                } else
                {
                    animator.SetTrigger("skill_2");
                }
                break;

        }



       

        
    }

    public void DamagedAnimation()
    {
        switch (type)
        {
            case EnemyType.makula:
                //animator.SetFloat("AnimState", 1);
                animator.SetTrigger("hit_1");
                print("MAKULA muevete mamona,....");
                break;
            default:
                if (RadUtils.d100() <= 49)
                {
                    animator.SetTrigger("hit_1");
                } else
                {
                    animator.SetTrigger("hit_2");
                }
                break;

        }


    }

    public void DeadAnimation()
    {
        animator.SetTrigger("death");
    }

    #endregion
}
