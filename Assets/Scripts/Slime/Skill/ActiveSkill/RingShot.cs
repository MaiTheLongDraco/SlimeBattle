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
	public bool CanTriggerSkill()
	{
		if(skillReference.ShootingNumber%triggerNumber==0)
		{
			return true;
		}
		return false;
	}

	public void DoSkill()
	{
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
			bulletObj.transform.Translate(dir * shootSpeed * Time.deltaTime);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		skillReference = SkillReference.Instance;
		slimeController = SlimeController.Instance;

	}

}
