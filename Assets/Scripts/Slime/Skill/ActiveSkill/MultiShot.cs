using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShot : MonoBehaviour,ISkillInvokation
{
	[SerializeField] private List<EnemyMini> listEnemy= new List<EnemyMini>();
	[SerializeField] private int numberOfBullet;
	[SerializeField] private GameObject multishotBullet;
	[SerializeField] private float damage;
	[SerializeField] private float speed;

	public SkillState SkillState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
	public float Damage { get { damage = SlimeController.Instance.SlimeATK.AttackDamage;
			return damage;
		} set => damage = value; }

	public bool CanTriggerSkill()
	{
		if (listEnemy.Count<=1) return false;
		return true;
	}
	public void ResetListEnemy()
	{
		listEnemy.Clear();
	}
	public void DoSkill()
	{
		if(CanTriggerSkill())
		{
			print(" trigger multishot");
			for(int i=0;i<numberOfBullet;i++)
			{
				CreateBullet(i, listEnemy[i]);
			}
		}
	}
	private void CreateBullet(int index,EnemyMini target)
	{
		if (!target) return;
		switch(index)
		{
			case 0: {
					var firstBullet = Instantiate(multishotBullet, transform.position, Quaternion.identity);
					firstBullet.GetComponent<Bullet>().SetCurrentEnemy(target);
					firstBullet.GetComponent<Bullet>().SetSpeed(speed);
					firstBullet.GetComponent<Bullet>().SetDamage(damage);
					firstBullet.GetComponent<Bullet>().RemoveDeathEnemy(listEnemy);
					RemoveDeathEnemyFromList();
					//firstBullet.transform.position = Vector2.MoveTowards(firstBullet.transform.position, target.transform.position, speed);
					//if(Vector2.Distance(firstBullet.transform.position,target.transform.position)<0.1f)
					//{
					//	target.TakeDamage(damage);
					//	RemoveDeathEnemyFromList();
					//	Destroy(firstBullet.gameObject);
					//}
					break; }
			case 1: {
					var secondBullet = Instantiate(multishotBullet, transform.position, Quaternion.identity);
					secondBullet.GetComponent<Bullet>().SetCurrentEnemy(target);
					secondBullet.GetComponent<Bullet>().SetSpeed(speed);
					secondBullet.GetComponent<Bullet>().SetDamage(damage*0.03f);
					secondBullet.GetComponent<Bullet>().RemoveDeathEnemy(listEnemy);
					RemoveDeathEnemyFromList();
					//secondBullet.transform.position = Vector2.MoveTowards(secondBullet.transform.position, target.transform.position, speed);
					//if (Vector2.Distance(secondBullet.transform.position, target.transform.position) < 0.1f)
					//{
					//	var subDam = damage * 0.03f;
					//	print($" multishot Sub damage");
					//	target.TakeDamage(subDam);
					//	RemoveDeathEnemyFromList();
					//	Destroy(secondBullet.gameObject);
					//}
					break; }
		}
	}
	private void RemoveDeathEnemyFromList()
	{
		for(int i=0;i<listEnemy.Count;i++)
		{
			if(listEnemy[i]==null)
			{
				listEnemy.Remove(listEnemy[i]);
			}
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		damage = SlimeController.Instance.SlimeATK.AttackDamage;
	}
	public void SetListEnemy(List<EnemyMini> set)
	{
		listEnemy = set;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
