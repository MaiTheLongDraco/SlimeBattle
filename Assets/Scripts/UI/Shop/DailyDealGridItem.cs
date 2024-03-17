using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyDealGridItem : MonoBehaviour
{
    public Text ItemName;
    public Image ItemIcon;
    public Text ItemAmount;
    public Text ItemCost;
    public Button buyButton;
    public int btnID;
    public ShopItemType shopItemType;
    public UnityAction<ShopItemType> onclick;
    // Start is called before the first frame update
    void Start()
    {
        buyButton.onClick.AddListener(DecreaseGemAmount);
        buyButton.onClick.AddListener(IncreaseCurrencyAmount);
        buyButton.onClick.AddListener(() => onclick(shopItemType));
    }
    public void SetItemName(string name)
	{
        ItemName.text = name;
	}
    public void SetItemType(ShopItemType set)
    {
        shopItemType = set;
    }
    public void SetItemAmount(string set)
    {
        ItemAmount.text = set;
    }
    public void SetButtonID(int set)
    {
        btnID = set;
    }
    public void SetItemCost(string set)
    {
       ItemCost.text = set;
    }
    public void SetItemIcon(Sprite set)
	{
        ItemIcon.sprite = set;
	}
    public int GetCostFromText()
	{
        print($"cost from Text {int.Parse(ItemCost.text)}");
        return int.Parse(ItemCost.text);
	}
    public int GetItemAmountFromText()
    {
        print($"item amount from Text {int.Parse(ItemAmount.text)}");
        return int.Parse(ItemAmount.text);
    }
    public void AddBtnListener(UnityAction<ShopItemType> callBack)
	{
        onclick += callBack;
    }
    private void DecreaseGemAmount()
	{
        CurrencyManager.Instance.DecreaeGemAmount(GetCostFromText());
        CurrencyManager.Instance.UpdateText();
	}
    private void IncreaseCurrencyAmount()
	{
        switch(shopItemType)
		{
            case ShopItemType.GEM:
                {
                    CurrencyManager.Instance.IncreaseGemAmount(GetItemAmountFromText());
                    break;
                }
            case ShopItemType.STAMINA: {
                    CurrencyManager.Instance.IncreaseStaminaAmount(GetItemAmountFromText());
                    break; }
            case ShopItemType.GOLD: { CurrencyManager.Instance.IncreaseCoinAmount(GetItemAmountFromText()); break; }
        }
        CurrencyManager.Instance.UpdateText();
    }        

   
}
