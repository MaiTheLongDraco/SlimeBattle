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
	void Start()
	{
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
	public SkillState SkillState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

	public bool CanTriggerSkill()
	{
		if (currentEnemy.enemyType != EnemyType.BOSS||!currentEnemy) return false;
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
		if (!canMoveToEnemy) return;
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
