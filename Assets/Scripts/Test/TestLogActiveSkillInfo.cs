using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLogActiveSkillInfo : MonoBehaviour
{
    [SerializeField] private HardSkillTemplate hardSkillTemplate;
    [SerializeField] private List<SkillToPickInfoHolder> SkillToPickInfoHolders;
    // Start is called before the first frame update
    void Start()
    {
        InitSkillInfo();
    }
    private void InitSkillInfo()
	{
		for(int i=0;i<SkillToPickInfoHolders.Count;i++)
		{
			SetActiveSkillInfo(hardSkillTemplate.ListInfo[i], i);
			SkillToPickInfoHolders[i].AddOnClickEvent(TestLog);
		}
	}

	private void SetActiveSkillInfo(HardSkillInfo data,int index)
	{
		SkillToPickInfoHolders[index].SetDescribeText(data.describeText);
		SkillToPickInfoHolders[index].SetLevelText(data.skillLevel);
		SkillToPickInfoHolders[index].SetSkillNameText(data.skillName);
		SkillToPickInfoHolders[index].SetSkillIcon(data.skillIcon);
	}
	private void TestLog(int buttonID)
	{
		print($"User click on this buttonID {buttonID}");
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
