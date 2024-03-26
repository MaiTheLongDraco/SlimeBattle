using UnityEngine;

public class SlowZone : MonoBehaviour, ISkillInvokation
{
    [SerializeField] private float slowRange;
    [SerializeField] private SkillState skillState;
    [SerializeField] private float scaleFactor;
    [SerializeField] private float slowFactor;
    [SerializeField] private float addingSlowFactor;
    [SerializeField] private SpriteRenderer sprite;

    // Start is called before the first frame update
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        SetState(SkillState.NOT_UNLOCK);
        slowRange = SlimeController.Instance.SlimeATK.AttackRange;
        sprite.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slowRange);
    }

    public SkillState SkillState
    {
        get => skillState;
        set => skillState = value;
    }

    public void SetState(SkillState set)
    {
        skillState = set;
    }

    public void DoSkill()
    {
        if (CanTriggerSkill())
        {
            sprite.enabled = true;
            SlowEnemy();
        }
    }

    public bool CanTriggerSkill()
    {
        if (SkillState == SkillState.UNLOCKED) return true;
        return false;
    }

    public void UpgradeSkill()
    {
        slowFactor += addingSlowFactor;
        print($"====== upgrading {name} skill =====");
    }

    public void SlowEnemy()
    {
        var hitObj = Physics2D.OverlapCircleAll(transform.position, slowRange);
        foreach (var enemy in hitObj)
        {
            if (enemy.tag != "Enemy") continue;
            var enemyTest = enemy.GetComponent<EnemyMini>();
            if (enemy.GetComponent<EnemyMini>().IsSlow()) continue;
            print("slowEnemy");
            enemy.GetComponent<EnemyMini>().SlowDown(slowFactor);
            enemy.GetComponent<EnemyMini>().SetIsSlow(true);
        }
    }

    public void ScaleSlowRange(float factor)
    {
        transform.localScale = new Vector3(factor * scaleFactor, factor * scaleFactor, 0);
    }
}