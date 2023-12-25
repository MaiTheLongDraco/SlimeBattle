using UnityEngine;

public class DisplaySubBtnInfo : MonoBehaviour
{
    [SerializeField] private SlimeTemplate slimeTemplate;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private SubButton subButton;
    [SerializeField] private SlimeTemplate runtimeTemplate;
    //[SerializeField]private 

    private void Start()
    {
        runtimeTemplate = slimeTemplate.Clone();
        Debug.Log($" runtimeTemplate {slimeTemplate.skillType}");
        subButton = GetComponent<SubButton>();
        DisplayButtonInfo();
    }

    private void DisplayButtonInfo()
    {
        var listInfo = slimeTemplate.ListInfo;
        for (var i = 0; i < listInfo.Count; i++)
        {
            var cloneValue = (SubButtonInfo)listInfo[i].Clone();
            var buttonObj = Instantiate(buttonPrefab, this.transform, false);
            SetValueForButton(cloneValue, buttonObj);
        }
    }
    private void SetValueForButton(SubButtonInfo set,GameObject button)
	{
        var info=button.GetComponent<SubButtonInfoHandle>();
        info.SetCurrencyCostTxt(set.CurrencyCost.ToString());
        info.SetSlimePropertyName(set.slimePropertyName);
        info.SetSlimePropertyValue(set.slimePropertyValue.ToString());
        info.SetCurrencyIcon(set.CurrencyIcon);
	}
}