using System;
using UnityEngine;

public class EnemyMini : Enemy
{
    [SerializeField] private float chaseTime;
    [SerializeField] private int heath;
    [SerializeField] private SlimeController slime;

	public int Heath { get => heath; set => heath = value; }

	private void Awake()
    {
        slime = FindObjectOfType<SlimeController>();
    }

    private void Update()
    {
        ChaseSlime(slime.transform.position, chaseTime);
        DetectDead();
    }

    public Vector2 GetSelfPos()
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
    public void SetHeath(int set)
	{
        Heath = set;
	}
	private void TriggerDeathState()
	{
        print("dead");
	}
}