using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillToPickInfoHolder : MonoBehaviour
{
	[SerializeField] private Image skillIcon;
	[SerializeField] private TextMeshProUGUI skillNameTxt;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private TextMeshProUGUI describeText;
	[SerializeField] private Button interactButton;
	[SerializeField] private UnityAction<SkillID> onClick;
	[SerializeField] private SkillID id;
	public SkillToPickInfoHolder(Sprite skillIcon,string skillName,int levelValue,string describeText)
	{
		this.skillIcon.sprite = skillIcon;
		this.skillNameTxt.text = skillName.ToString();
		this.levelText.text = levelValue.ToString();
		this.describeText.text = describeText;
	}
	private void Start()
	{
		interactButton = GetComponent<Button>();
		interactButton.onClick.AddListener(() => onClick(id));
	}
	public void SetSkillIcon(Sprite sprite)
	{
		skillIcon.sprite = sprite;
	}
	public void SetSkillNameText(string set)
	{
		skillNameTxt.text = set;
	}
	public void SetLevelText(int set)
	{
		levelText.text = "Level "+set.ToString();
	}
	public void SetDescribeText(string set)
	{
		describeText.text = set;
	}
	public void AddOnClickEvent(UnityAction<SkillID> callBack)
	{
		onClick += callBack;
	}
	public void SetSkillID(SkillID skillID)
	{
		id = skillID;
	}
	
}
