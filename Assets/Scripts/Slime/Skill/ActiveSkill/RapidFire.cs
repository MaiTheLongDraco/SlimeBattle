using System.Collections;
using UnityEngine;

public class RapidFire : MonoBehaviour, ISkillInvokation, ICritical
{
    [SerializeField] private float dropRate;
    [SerializeField] private float upgradeFactor;
    [SerializeField] private ProjectTile projectTile;
    [SerializeField] private EnemyMini currentEnemy;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float damage;

    [SerializeField] private float initDamage;

    //[SerializeField] private  dropRate;
    [SerializeField] private SkillState state;

    // Start is called before the first frame update
    private void Start()
    {
        damage = SlimeController.Instance.SlimeATK.AttackDamage;
        SetState(SkillState.NOT_UNLOCK);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TriggerCritical(float set)
    {
        print($"CRIT==== rapid fire damage {set}");
        StartCoroutine(SetCriticalATKDamage(set));
    }

    public IEnumerator SetCriticalATKDamage(float set)
    {
        initDamage = damage;
        damage = set;
        yield return new WaitForSeconds(1);
        damage = initDamage;
    }

    public SkillState SkillState
    {
        get => state;
        set => state = value;
    }

    public void SetState(SkillState set)
    {
        state = set;
    }

    public bool CanTriggerSkill()
    {
        if (state != SkillState.UNLOCKED) return false;
        if (CheckSkillActivation(dropRate) && currentEnemy != null) return true;
        return false;
    }

    public void DoSkill()
    {
        if (CanTriggerSkill())
        {
            print("trigger rapid fire");
            var projectObj = Instantiate(projectTile.gameObject, transform.position, Quaternion.identity);
            projectObj.GetComponent<ProjectTile>().SetDamage(damage);
            projectObj.GetComponent<ProjectTile>().SetSpeed(shootSpeed);
            projectObj.GetComponent<ProjectTile>().SetTarget(currentEnemy);
        }
    }

    public void UpgradeSkill()
    {
        dropRate += upgradeFactor;
        shootSpeed += 0.001f;
        print($"====== upgrading {name} skill =====");
    }

    private bool CheckSkillActivation(float activationProbability)
    {
        var randomValue = Random.value;
        print($"random value {randomValue}");
        return randomValue <= activationProbability;
    }

    public void SetCurrentEnemy(EnemyMini target)
    {
        currentEnemy = target;
    }
}