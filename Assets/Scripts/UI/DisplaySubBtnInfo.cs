using UnityEngine;

public class DisplaySubBtnInfo : MonoBehaviour
{
    [SerializeField] private SlimeTemplate slimeTemplate;

    private void Start()
    {
        DisplayButtonInfo();
    }

    private void DisplayButtonInfo()
    {
        var listInfo = slimeTemplate.ListInfo;
        for (var i = 0; i < listInfo.Count; i++)
        {
            var cloneValue = (SubButtonInfo)listInfo[i].Clone();
            TestSetData(cloneValue, "testname");
            print($"slimePropertyValue : {cloneValue.slimePropertyValue}");
            print($"slimePropertyName : {cloneValue.slimePropertyName}");
            print($"CurrencyCost : {cloneValue.CurrencyCost}");
        }
    }

    private void TestSetData(SubButtonInfo testObj, string test)
    {
        var listInfo = slimeTemplate.ListInfo[0];
        listInfo.slimePropertyName = test;
    }
}