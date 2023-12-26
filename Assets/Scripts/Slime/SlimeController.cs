using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public static SlimeController Instance;
    [SerializeField] private List<SlimeTemplate> listData;
    [SerializeField] private SlimeData rootData;
    [SerializeField] private SlimeData rootDataClone;

    [Header("SlimeInfo")] [SerializeField] private Attack _slimeATK;

    [SerializeField] private Defense _slimeDF;
    [SerializeField] private Utility _slimeUti;
    [SerializeField] private EnemyMini currentTarget;
    [SerializeField] private float slimeHeath;

    [Header("Detect range")] [SerializeField]
    private GameObject detectImage;

    private SkillController skillController;

    private void Awake()
    {
        Instance = this;
        SetRootData();
        SetOriginData();
    }

    private void Start()
    {
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

    private void SetRootData()
    {
        // rootData.SlimeATK = _slimeATK;
        // rootData.SlimeDF = _slimeDF;
        // rootData.SlimeUti = _slimeUti;
        _slimeATK = rootData.SlimeATK;
        _slimeDF = rootData.SlimeDF;
        _slimeUti = rootData.SlimeUti;
        rootDataClone = rootData.Clone() as SlimeData;
        print(
            $"rootdata Clone atk {rootDataClone.SlimeATK.AttackDamage} - {rootDataClone.SlimeATK.AttackRange} -- {rootDataClone.SlimeATK.AttackSpeed}");
    }

    private void SetOriginData()
    {
        foreach (var data in listData) HandleWithSpecifySkillType(data);
    }

    private void HandleWithSpecifySkillType(SlimeTemplate slimeTemplate)
    {
        switch (slimeTemplate.skillType)
        {
            case SkillType.ATTACK:
            {
                var slimeInfo = slimeTemplate.ListInfo;
                SetATKOrigin(slimeInfo, rootDataClone.SlimeATK);
            }
                break;
            case SkillType.DEFEND:
            {
                var slimeInfo = slimeTemplate.ListInfo;
                SetDefendOrigin(slimeInfo, rootDataClone.SlimeDF);
            }
                break;
            case SkillType.UTILITY:
            {
                var slimeInfo = slimeTemplate.ListInfo;
                SetUtilityOrigin(slimeInfo, rootDataClone.SlimeUti);
            }
                break;
        }
    }

    private void SetATKOrigin(List<SubButtonInfo> originData, Attack attackInfo)
    {
        for (var i = 0; i < originData.Count; i++)
            switch (originData[i].id)
            {
                case 0:
                {
                    ChangeDataValue(originData[i], attackInfo.AttackDamage);
                }
                    break;
                case 1:
                {
                    ChangeDataValue(originData[i], attackInfo.AttackSpeed);
                }
                    break;
                case 2:
                {
                    ChangeDataValue(originData[i], attackInfo.AttackRange);
                }
                    break;
                case 3:
                {
                    ChangeDataValue(originData[i], attackInfo.RangeDamageBonus);
                }
                    break;
            }
    }

    private void SetDefendOrigin(List<SubButtonInfo> originData, Defense defendInfo)
    {
        for (var i = 0; i < originData.Count; i++)
            switch (originData[i].id)
            {
                case 0:
                {
                    ChangeDataValue(originData[i], defendInfo.Heath);
                }
                    break;
                case 1:
                {
                    ChangeDataValue(originData[i], defendInfo.HealthRegen);
                }
                    break;
                case 2:
                {
                    ChangeDataValue(originData[i], defendInfo.Armor);
                }
                    break;
                case 3:
                {
                    ChangeDataValue(originData[i], defendInfo.BlockDamage);
                }
                    break;
            }
    }

    private void SetUtilityOrigin(List<SubButtonInfo> originData, Utility utilityInfo)
    {
        for (var i = 0; i < originData.Count; i++)
            switch (originData[i].id)
            {
                case 0:
                {
                    ChangeDataValue(originData[i], utilityInfo.SilverPerWave);
                }
                    break;
                case 1:
                {
                    ChangeDataValue(originData[i], utilityInfo.SilverBonus);
                }
                    break;
                case 2:
                {
                    ChangeDataValue(originData[i], utilityInfo.GoldBonus);
                }
                    break;
                case 3:
                {
                    ChangeDataValue(originData[i], utilityInfo.GoldPerWave);
                }
                    break;
            }
    }

    private void ChangeDataValue(SubButtonInfo info, float newValue)
    {
        info.slimePropertyValue = newValue;
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