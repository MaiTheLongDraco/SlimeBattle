using UnityEngine;

public class DestructionShot : MonoBehaviour, ISkillInvokation
{
    [SerializeField] private EnemyMini currentEnemy;
    [SerializeField] private float speed;
    [SerializeField] private float attackFactor;
    [SerializeField] private float upgradingFactor;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private SkillState state;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetState(SkillState.NOT_UNLOCK);
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        MoveToEnemy();
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
        if (!currentEnemy) return false;
        if (currentEnemy.enemyType != EnemyType.BOSS) return false;
        return true;
    }

    public void DoSkill()
    {
        if (CanTriggerSkill())
        {
            canMoveToEnemy = true;
            gameObject.SetActive(true);
            SetActiveSprite(true);
        }
    }

    public void UpgradeSkill()
    {
        attackFactor += upgradingFactor;
        print($"====== upgrading {name} skill =====");
    }

    private void SetActiveSprite(bool set)
    {
        spriteRenderer.enabled = set;
    }

    private void ResetToInitialPosition()
    {
        transform.position = initialPosition;
    }

    public void SetCurrentEnemy(EnemyMini target)
    {
        if (target.enemyType != EnemyType.BOSS) return;
        currentEnemy = target;
    }

    private void MoveToEnemy()
    {
        if (!canMoveToEnemy || !currentEnemy) return;
        transform.position = Vector2.MoveTowards(transform.position, currentEnemy.GetSelfPos(), speed);
        if (Vector2.Distance(transform.position, currentEnemy.GetSelfPos()) <= 0.1f)
        {
            currentEnemy.TakeDamage(currentEnemy.Heath * attackFactor);
            gameObject.SetActive(false);
            ResetToInitialPosition();
            SetActiveSprite(false);
            canMoveToEnemy = false;
        }
    }
}