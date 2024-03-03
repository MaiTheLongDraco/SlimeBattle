using System;
using UnityEngine;

public class EnemyMini : Enemy
{
    [SerializeField] private float chaseTime;
    [SerializeField] private float heath;
    [SerializeField] private SlimeController slime;

	public float Heath { get => heath; set => heath = value; }

	private void Awake()
    {
        slime = FindObjectOfType<SlimeController>();
    }

    private void Update()
    {
        ChaseSlime(slime.transform.position, chaseTime);
        DetectDead();
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