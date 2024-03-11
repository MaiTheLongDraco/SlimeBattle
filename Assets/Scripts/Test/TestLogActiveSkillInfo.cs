using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLogActiveSkillInfo : MonoBehaviour
{
    [SerializeField] private HardSkillTemplate hardSkillTemplate;
    [SerializeField] private List<SkillToPickInfoHolder> SkillToPickInfoHolders;
    [SerializeField] private List<HardSkillInfo> hardSkillHasGenerate;
	[SerializeField] private SkillReference skillReference;
	[SerializeField] private GameObject skillToPickPrefab;
	[SerializeField] private int numberOfCreate;
	[SerializeField] private Transform troopTranform;
    // Start is called before the first frame update
    void Start()
    {
		skillReference = GetComponent<SkillReference>();
		GenerateListSkillToPick();
		InitSkillInfo();
    }
    private void InitSkillInfo()
	{
		for(int i=0;i<SkillToPickInfoHolders.Count;i++)
		{
			var rand = Random.Range(0, hardSkillTemplate.ListInfo.Count);
			if (hardSkillHasGenerate.Contains(hardSkillTemplate.ListInfo[rand])) continue;
			hardSkillHasGenerate.Add(hardSkillTemplate.ListInfo[rand]);
			SetActiveSkillInfo(hardSkillTemplate.ListInfo[rand], i);
			SkillToPickInfoHolders[i].AddOnClickEvent(UnlockNewSkill);
		}
	}
	public void ReGenerateSkill()
	{
		for(int i=0;i<SkillToPickInfoHolders.Count;i++)
		{
			Destroy(SkillToPickInfoHolders[i].gameObject);
		}
		SkillToPickInfoHolders.Clear();
		hardSkillHasGenerate.Clear();
		GenerateListSkillToPick();
		InitSkillInfo();
	}	
	private void GenerateListSkillToPick()
	{
		for(int i=0;i<numberOfCreate;i++)
		{
			var skillObj = Instantiate(skillToPickPrefab, troopTranform);
			if (SkillToPickInfoHolders.Contains(skillObj.GetComponent<SkillToPickInfoHolder>())) continue;
			SkillToPickInfoHolders.Add(skillObj.GetComponent<SkillToPickInfoHolder>());
		}
	}

	private void SetActiveSkillInfo(HardSkillInfo data, int index)
	{
		SkillToPickInfoHolders[index].SetDescribeText(data.describeText);
		SkillToPickInfoHolders[index].SetLevelText(data.skillLevel);
		SkillToPickInfoHolders[index].SetSkillNameText(data.skillName);
		SkillToPickInfoHolders[index].SetSkillIcon(data.skillIcon);
		SkillToPickInfoHolders[index].SetSkillID(data.id);
	}
	private void UnlockNewSkill(SkillID buttonID)
	{
		print($"User click on this buttonID {buttonID}");
		skillReference.HandleWithTypeSkillTroop(buttonID);
	}
}
