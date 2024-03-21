using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlimeController : MonoBehaviour, ICritical
{
    public static SlimeController Instance;
    [SerializeField] private List<SlimeTemplate> listData;
    [SerializeField] private SlimeData rootData;
    [SerializeField] private SlimeData rootDataClone;

    [Header("SlimeInfo")] [SerializeField] private Attack _slimeATK;

    [SerializeField] private Defense _slimeDF;
    [SerializeField] private Utility _slimeUti;
    [SerializeField] private EnemyMini currentTarget;
    [SerializeField] private float criticalRate;
    [SerializeField] private float currentDamage;

    [Header("Detect range")] [SerializeField]
    private GameObject detectImage;

    private SkillController skillController;

	public SlimeData RootDataClone { get => rootDataClone; set => rootDataClone = value; }
	public SkillController SkillController { get => skillController; set => skillController = value; }
	public Attack SlimeATK { get => _slimeATK; set => _slimeATK = value; }
	public Defense SlimeDF { get => _slimeDF; set => _slimeDF = value; }
	public Utility SlimeUti { get => _slimeUti; set => _slimeUti = value; }
	public float CriticalRate { get => criticalRate; set => criticalRate = value; }

	[Header("Other Reference")]
    [SerializeField] private GamePlayManager gamePlayManager;
    [SerializeField] private UnityEvent<float> onCiritcal;
    private void Awake()
    {
        Instance = this;
        SetRootData();
        SetOriginData();
    }
    public void IncreaseCritChance(float adding)
	{
        CriticalRate += adding;
	}        

    private void Start()
    {
        SkillController = GetComponent<SkillController>();
        ScaleDetectImage();
        onCiritcal.AddListener(TriggerCritical);
        onCiritcal.AddListener(skillController.TriggerCriticalState);
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
        RootDataClone = rootData.Clone() as SlimeData;
        print(
            $"rootdata Clone atk {RootDataClone.SlimeATK.AttackDamage} - {RootDataClone.SlimeATK.AttackRange} -- {RootDataClone.SlimeATK.AttackSpeed}");
    }
    
    public void UpdateRuntimeValue()
	{
        _slimeATK = RootDataClone.SlimeATK;
        _slimeDF = RootDataClone.SlimeDF;
        _slimeUti = RootDataClone.SlimeUti;
        ScaleDetectImage();
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
                SetATKOrigin(slimeInfo, RootDataClone.SlimeATK);
            }
                break;
            case SkillType.DEFEND:
            {
                var slimeInfo = slimeTemplate.ListInfo;
                SetDefendOrigin(slimeInfo, RootDataClone.SlimeDF);
            }
                break;
            case SkillType.UTILITY:
            {
                var slimeInfo = slimeTemplate.ListInfo;
                SetUtilityOrigin(slimeInfo, RootDataClone.SlimeUti);
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
        var detectScale = _slimeATK.AttackRange * 0.3f;
        detectImage.transform.localScale = new Vector3( detectScale,  detectScale, 0);
        skillController.ActiveSkill.slowZone.ScaleSlowRange(detectScale);
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
		if (_slimeDF.Heath <= 0)
		{
			SkillController.SetState(SlimeState.DEAD);
            gamePlayManager.InvokeOnLoseGame();
			return;
		}
		CheckRaycastMultiEnemy();
        var hittedEnemy = Physics2D.OverlapCircle(transform.position, _slimeATK.AttackRange);

		if (hittedEnemy == null)
		{
			SkillController.SetState(SlimeState.UNATTACK);
			return;
		}

		currentTarget = hittedEnemy.gameObject.GetComponent<EnemyMini>();
        var passInterval = SkillController.HasPastInterval();
        if (currentTarget == null) return;
        if (Vector2.Distance(transform.position, currentTarget.transform.position) <= _slimeATK.AttackRange)
        {
            if(Vector2.Distance(transform.position, currentTarget.transform.position) <= _slimeATK.AttackRange/5)
			{
                currentTarget.SetAnim("Attack");
                RootDataClone.SlimeDF.Heath -= 3;
                UpdateRuntimeValue();
            }
            gamePlayManager.SetCurrentHeathTxt(_slimeDF.Heath);
            SkillController.SetState(SlimeState.ATTACK);
            if (!passInterval) return;
            var rand = UnityEngine.Random.value;
            if(rand<=CriticalRate)
			{
                print($"CRIT==== trigger critical with rand{rand}");
                onCiritcal?.Invoke(rootDataClone.SlimeATK.AttackDamage + rootDataClone.SlimeATK.AttackDamage * CriticalRate);
                //StartCoroutine(SetCriticalATKDamage())
            }
            SkillController.StartNormal(currentTarget);
        }
        else
        {
            SkillController.SetState(SlimeState.UNATTACK);
        }
    }

    public void CheckRaycastMultiEnemy()
	{
        var hitsEnemy = Physics2D.OverlapCircleAll(transform.position, _slimeATK.AttackRange);
        if(hitsEnemy.Length>=1)
		{
            var random = UnityEngine.Random.Range(0, hitsEnemy.Length - 1);
            if (hitsEnemy[random].GetComponent<Enemy>() != null)
			{
                skillController.ActiveSkill.rapidFire.SetCurrentEnemy(hitsEnemy[random].GetComponent<EnemyMini>());
            }
        }
        if (hitsEnemy.Length>=2)
		{
           var listCollie= GetListEnemy(2,hitsEnemy);
            var listEnemy = new List<EnemyMini>();
            foreach(var obj in listCollie)
            {
                if (obj.tag != "Enemy") continue;
                listEnemy.Add(obj.GetComponent<EnemyMini>());
            }
            skillController.SetDataForMultiShot(listEnemy);
            if(listEnemy.Count<2)
			{
                print("Reset list enemy");
                skillController.ActiveSkill.multiShot.ResetListEnemy();
            }
		}
		else
		{
            skillController.ActiveSkill.multiShot.ResetListEnemy();
        }

    }
    private List<T> GetListEnemy<T>(int getNumber, params T[] elements)
    {
        List<T> list = new List<T>();
        for(int i=0; i<getNumber;i++)
		{
            var rand = UnityEngine.Random.Range(0, elements.Length);
            list.Add(elements[rand]);
        }
        return list;
    }

    public void SetCurrentTarget(EnemyMini newTarget)
    {
        currentTarget = newTarget;
    }
  
	public IEnumerator SetCriticalATKDamage(float set)
	{
            currentDamage = _slimeATK.AttackDamage;
            _slimeATK.AttackDamage = set;
            yield return new WaitForSeconds(1);
            _slimeATK.AttackDamage = currentDamage;
    }

	public void TriggerCritical(float set)
	{
        print($" CRIT==== crit damage invoke {set}");
        StartCoroutine(SetCriticalATKDamage(set));
    }
}

[Serializable]
public class Attack:ICloneable
{
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float RangeDamageBonus;

	public Attack(float attackDamage, float attackRange, float attackSpeed, float rangeDamageBonus)
	{
		AttackDamage = attackDamage;
		AttackRange = attackRange;
		AttackSpeed = attackSpeed;
		RangeDamageBonus = rangeDamageBonus;
	}

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

	public object Clone()
	{
        return new Attack(AttackDamage, AttackRange, AttackSpeed, RangeDamageBonus);
	}
	
}

[Serializable]
public class Defense : ICloneable
{
    public float Heath;
    public float Armor;
    public float HealthRegen;
    public float BlockDamage;

	public Defense(float heath, float armor, float healthRegen, float blockDamage)
	{
		Heath = heath;
		Armor = armor;
		HealthRegen = healthRegen;
		BlockDamage = blockDamage;
	}

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

	public object Clone()
	{
        return new Defense(Heath, Armor, HealthRegen, BlockDamage);
	}
}

[Serializable]
public class Utility:ICloneable
{
    public float SilverPerWave;
    public float GoldBonus;
    public float SilverBonus;
    public float GoldPerWave;

	public Utility(float silverPerWave, float goldBonus, float silverBonus, float goldPerWave)
	{
		SilverPerWave = silverPerWave;
		GoldBonus = goldBonus;
		SilverBonus = silverBonus;
		GoldPerWave = goldPerWave;
	}

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

	public object Clone()
	{
        return new Utility(SilverPerWave, GoldBonus, SilverBonus, GoldPerWave);
	}
}

public interface ICritical
{
    public void TriggerCritical(float set);
    public IEnumerator SetCriticalATKDamage(float set);
}