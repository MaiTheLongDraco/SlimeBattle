using System;
using UnityEngine;

public class EnemyMini : Enemy
{
    [SerializeField] private float chaseTime;
    [SerializeField] private float heath;
    [SerializeField] private SlimeController slime;
    [SerializeField] private bool checkIsSLow;

	public float Heath { get => heath; set => heath = value; }

	private void Awake()
    {
        slime = FindObjectOfType<SlimeController>();
    }
    public bool IsDead()
	{
        if (heath <= 0) return true;
        return false;
	}
    private void Update()
    {
        ChaseSlime(slime.transform.position, chaseTime);
        DetectDead();
    }
    public bool IsSlow() => isSlow;
    public void SlowDown()
	{
        checkIsSLow = isSlow;
        if (isSlow) return;
        print($"{gameObject.name} get slow down");
        chaseTime = chaseTime / 2;
	}
    public void SetIsSlow(bool set)
	{
        isSlow = set;
	}
    public Vector3 GetSelfPos()
    {
        return transform.position;
    }
    private void DetectDead()
	{
        if(Heath<=0)
		{
            TriggerDeathState();
		}
	}
    public void TakeDamage(float damage)
	{
        Heath -= damage;
	}
	private void TriggerDeathState()
	{
        base.skillReference = SkillReference.Instance;
        base.InvokeEnemyDeath();
        print("dead");
		Destroy(this.gameObject);
	}
}
