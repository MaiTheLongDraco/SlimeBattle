using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private DailyDealSO dailyDealData;
    [SerializeField] private List<DailyDealGridItem> dailyDealGridItems;
    [SerializeField] private CurrencyManager currencyManager;
    // Start is called before the first frame update
    void Start()
    {
        SetActiveItemIfHaveData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetActiveItemIfHaveData()
	{
        for(int i=0;i<dailyDealData.dailyDealItems.Count;i++)
		{
            dailyDealGridItems[i].gameObject.SetActive(true);
            dailyDealGridItems[i].SetItemAmount(dailyDealData.dailyDealItems[i].ItemAmount.ToString());
            dailyDealGridItems[i].SetItemCost(dailyDealData.dailyDealItems[i].ItemCost.ToString());
            dailyDealGridItems[i].SetItemName(dailyDealData.dailyDealItems[i].ItemName.ToString());
            dailyDealGridItems[i].SetItemIcon(dailyDealData.dailyDealItems[i].ItemIcon);
            dailyDealGridItems[i].SetItemType(dailyDealData.dailyDealItems[i].itemType);
            dailyDealGridItems[i].SetButtonID(i);
            dailyDealGridItems[i].AddBtnListener(SpecifiData);
            UpdateButtonState(currencyManager.GemAmount);
        }
	}
    private void UpdateButtonState(int compareNumber)
	{
        foreach(var item in dailyDealGridItems)
		{
            if(item.GetCostFromText()<=compareNumber)
			{
                item.buyButton.interactable = true;
			}
			else
			{
                item.buyButton.interactable = false;
			}
		}
	}
    private void SpecifiData(ShopItemType id)
	{
        print($"btn id {id}");
        switch(id)
		{
            case ShopItemType.GEM: {  
                    break; }
            case ShopItemType.STAMINA: {  break; }
            case ShopItemType.GOLD: {  break; }
		}
        UpdateButtonState(currencyManager.GemAmount);
    }
}
