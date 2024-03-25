using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour,ISkillInvokation
{
    [SerializeField] private float slowRange;
	[SerializeField] private SkillState skillState;
	[SerializeField] private float scaleFactor;
	[SerializeField] private SpriteRenderer sprite;
	public SkillState SkillState { get => skillState; set => skillState=value; }

	// Start is called before the first frame update
	void Start()
    {
		sprite = GetComponent<SpriteRenderer>();
		SetState(SkillState.NOT_UNLOCK);
		slowRange = SlimeController.Instance.SlimeATK.AttackRange;
		sprite.enabled = false;
	}
	public void SetState(SkillState set)
	{
		skillState = set;
	}
	public void SlowEnemy()
	{
        var hitObj = Physics2D.OverlapCircleAll(transform.position, slowRange);
        foreach(var  enemy in hitObj)
		{
			if (enemy.tag != "Enemy") continue;
			var enemyTest = enemy.GetComponent<EnemyMini>();
			if (enemy.GetComponent<EnemyMini>().IsSlow()) continue;
			print("slowEnemy");
			enemy.GetComponent<EnemyMini>().SlowDown();
            enemy.GetComponent<EnemyMini>().SetIsSlow(true);
		}
	}
    // Update is called once per frame
    void Update()
    {
        
    }
	public void ScaleSlowRange(float factor)
	{
		transform.localScale = new Vector3(factor* scaleFactor, factor* scaleFactor, 0);
	}

	public void DoSkill()
	{
		if(CanTriggerSkill())
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
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, slowRange);
	}

	public void UpgradeSkill()
	{
		print($"====== upgrading {name} skill =====");
	}
}
