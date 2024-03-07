using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingShot : MonoBehaviour,ISkillInvokation
{
	[SerializeField] private SkillReference skillReference;
	[SerializeField] private int triggerNumber;
	[SerializeField] private SlimeController slimeController;
	[SerializeField] private List<Transform> refPos;
	[SerializeField] private GameObject bullet;
	[SerializeField] private float shootSpeed;
	[SerializeField] private float damage;
	public bool CanTriggerSkill()
	{
		if (skillReference.ShootingNumber <= 0) return false;
		if(skillReference.ShootingNumber%triggerNumber==0)
		{
			return true;
		}
		return false;
	}

	public void DoSkill()
	{
		if (SkillController.Instance.SlimeState != SlimeState.ATTACK) return;
		if(CanTriggerSkill())
		{
			TriggerRingShot();
		}
	}
	private void TriggerRingShot()
	{
		for(int i=0;i<refPos.Count;i++)
		{
			var bulletObj = Instantiate(bullet, transform.position, Quaternion.identity);
			var dir = refPos[i].position - transform.position;
			bulletObj.GetComponent<RingShotBullet>().SetTargetPos(dir * 10);
			bulletObj.GetComponent<RingShotBullet>().SetSpeed(shootSpeed);
			bulletObj.GetComponent<RingShotBullet>().SetDamage(damage);
			//bulletObj.transform.position = Vector2.MoveTowards(bulletObj.transform.position, dir * 100, shootSpeed);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		skillReference = SkillReference.Instance;
		slimeController = SlimeController.Instance;
	}

}
