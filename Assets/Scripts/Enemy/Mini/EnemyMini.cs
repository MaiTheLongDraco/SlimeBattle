using UnityEngine;

public class EnemyMini : Enemy
{
    [SerializeField] protected float chaseTime;
    [SerializeField] protected float heath;
    [SerializeField] protected float totalHeath;
    [SerializeField] protected SlimeController slime;
    [SerializeField] protected bool checkIsSLow;
    [SerializeField] protected bool isPause;
    [SerializeField] protected GameObject damagePopUp;
    [SerializeField] protected Animator anim;

    public float Heath
    {
        get => heath;
        set => heath = value;
    }

    private void Awake()
    {
        slime = FindObjectOfType<SlimeController>();
        anim = GetComponent<Animator>();
        totalHeath = heath;
    }

    private void Update()
    {
        if (isPause) return;
        ChaseSlime(slime.transform.position, chaseTime);
        DetectDead();
    }

    public bool IsDead()
    {
        if (heath <= 0) return true;
        return false;
    }

    public void SetIsPause(bool set)
    {
        isPause = set;
    }

    public bool IsSlow()
    {
        return isSlow;
    }

    public void SlowDown(float slowFactor)
    {
        checkIsSLow = isSlow;
        if (isSlow) return;
        print($"{gameObject.name} get slow down");
        chaseTime = chaseTime / slowFactor;
    }

    public void SetIsSlow(bool set)
    {
        isSlow = set;
    }

    public Vector3 GetSelfPos()
    {
        return transform.position;
    }

    public void SetAnim(string animName)
    {
        anim.SetTrigger(animName);
    }

    protected void DetectDead()
    {
        if (Heath <= 0)
        {
            SetAnim("Death");
            Invoke("TriggerDeathState", 3f);
            // TriggerDeathState();
        }
    }

    public void TakeDamage(float damage, bool isSecondDamage = false)
    {
        print($" CRIT==== enemy {gameObject.name} get damage {damage}");
        Heath -= damage;
        if (isSecondDamage)
        {
            var popUpnew = Instantiate(damagePopUp, transform.position + Vector3.up / 2 + Vector3.right / 2,
                Quaternion.identity);
            popUpnew.GetComponent<DamagePopUp>().SetText((int)damage);
        }

        var popUp = Instantiate(damagePopUp, transform.position + Vector3.up / 2, Quaternion.identity);
        popUp.GetComponent<DamagePopUp>().SetText((int)damage);
    }

    protected void TriggerDeathState()
    {
        skillReference = SkillReference.Instance;
        InvokeEnemyDeath();
        EnemySpawner.Instance.DecreaseListActive(this);
        print("dead");
        Destroy(gameObject);
    }
}