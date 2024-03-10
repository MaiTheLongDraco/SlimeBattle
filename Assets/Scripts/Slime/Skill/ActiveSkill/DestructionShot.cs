using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionShot : MonoBehaviour,ISkillInvokation
{
    [SerializeField] private EnemyMini currentEnemy;
    [SerializeField] private float speed;
	[SerializeField] private Vector3 initialPosition;
	private SpriteRenderer spriteRenderer;
	[SerializeField] private bool canMoveToEnemy;
	[SerializeField] private SkillState state;
	public SkillState SkillState { get => state; set => state = value; }
	public void SetState(SkillState set)
	{
		state = set;
	}

	void Start()
	{
		SetState(SkillState.NOT_UNLOCK);
		spriteRenderer = GetComponent<SpriteRenderer>();
		initialPosition = transform.position;
		gameObject.SetActive(false);
	}
	private void SetActiveSprite(bool set)
	{
		spriteRenderer.enabled = set;
	}
	private void ResetToInitialPosition()
	{
		transform.position = initialPosition;
	}

	public bool CanTriggerSkill()
	{
		if (state != SkillState.UNLOCKED) return false;
		if (!currentEnemy) return false;
		if (currentEnemy.enemyType != EnemyType.BOSS) return false;
		return true;
	}
	public void SetCurrentEnemy(EnemyMini target)
	{
		if (target.enemyType != EnemyType.BOSS) return;
		currentEnemy = target;
	}
	public void DoSkill()
	{
		if(CanTriggerSkill())
		{
			canMoveToEnemy = true;
			gameObject.SetActive(true);
			SetActiveSprite(true);
		}
	}
    void Update()
    {
		MoveToEnemy();
	}
	private void MoveToEnemy()
	{
		if (!canMoveToEnemy||!currentEnemy) return;
		transform.position = Vector2.MoveTowards(transform.position, currentEnemy.GetSelfPos(), speed);
		if (Vector2.Distance(transform.position, currentEnemy.GetSelfPos()) <= 0.1f)
		{
			currentEnemy.TakeDamage(currentEnemy.Heath * 0.01f);
			gameObject.SetActive(false);
			ResetToInitialPosition();
			SetActiveSprite(false);
			canMoveToEnemy = false;
		}
	}
}
