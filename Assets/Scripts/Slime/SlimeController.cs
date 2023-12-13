using System;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public static SlimeController Instance;

    [Header("SlimeInfo")] [SerializeField] private Attack _slimeATK;

    [SerializeField] private Defense _slimeDF;
    [SerializeField] private Utility _slimeUti;
    [SerializeField] private EnemyMini currentTarget;
    [SerializeField] private float slimeHeath;

    [Header("Detect range")] [SerializeField]
    private GameObject detectImage;

    private SkillController skillController;


    private void Start()
    {
        Instance = this;
        skillController = GetComponent<SkillController>();
        ScaleDetectImage();
    }

    private void Update()
    {
        StateControl();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _slimeATK.AttackRange);
    }

    [ContextMenu("ScaleDetectImage")]
    private void ScaleDetectImage()
    {
        var currentScale = detectImage.transform.localScale;
        var detectScale = _slimeATK.AttackRange * 0.62f;
        detectImage.transform.localScale = new Vector3(currentScale.x * detectScale, currentScale.y * detectScale, 0);
    }

    public EnemyMini GetCurrentEnemy()
    {
        return currentTarget;
    }

    private void Attack()
    {
    }

    public Attack GetAttackInfo()
    {
        return _slimeATK;
    }

    private void StateControl()
    {
        if (slimeHeath <= 0)
        {
            skillController.SetState(SlimeState.DEAD);
            return;
        }

        var hittedEnemy = Physics2D.OverlapCircle(transform.position, _slimeATK.AttackRange);
        if (hittedEnemy == null)
        {
            skillController.SetState(SlimeState.UNATTACK);
            return;
        }

        currentTarget = hittedEnemy.gameObject.GetComponent<EnemyMini>();
        var passInterval = skillController.HasPastInterval();
        if (Vector2.Distance(transform.position, currentTarget.transform.position) <= _slimeATK.AttackRange)
        {
            skillController.SetState(SlimeState.ATTACK);
            if (!passInterval) return;
            skillController.StartNormal();
        }
        else
        {
            skillController.SetState(SlimeState.UNATTACK);
        }
    }


    public void SetCurrentTarget(EnemyMini newTarget)
    {
        currentTarget = newTarget;
    }
}

[Serializable]
public class Attack
{
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float RangeDamageBonus;

    public void SetAttackDamage(float set)
    {
        AttackDamage = set;
    }

    public void SetAttackRange(float set)
    {
        AttackRange = set;
    }

    public void SetAttackSpeed(float set)
    {
        AttackSpeed = set;
    }

    public void SetAttackDamageBonus(float set)
    {
        RangeDamageBonus = set;
    }
}

[Serializable]
public class Defense
{
    public float Heath;
    public float Armor;
    public float HealthRegen;
    public float BlockDamage;

    public void SetHeath(float set)
    {
        Heath = set;
    }

    public void SetArmor(float set)
    {
        Armor = set;
    }

    public void SetHeathRegen(float set)
    {
        HealthRegen = set;
    }

    public void SetBlockDamage(float set)
    {
        BlockDamage = set;
    }
}

[Serializable]
public class Utility
{
    public float SilverPerWave;
    public float GoldBonus;
    public float SilverBonus;
    public float GoldPerWave;

    public void SetSilverPerWave(float set)
    {
        SilverPerWave = set;
    }

    public void SetGoldBonus(float set)
    {
        GoldBonus = set;
    }

    public void SetSilverBonus(float set)
    {
        SilverBonus = set;
    }

    public void SetGoldPerWave(float set)
    {
        GoldPerWave = set;
    }
}

[Serializable]
public class Ultimate
{
}