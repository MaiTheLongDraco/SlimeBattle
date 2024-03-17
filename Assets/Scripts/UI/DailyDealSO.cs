using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="DailyDealSO",menuName ="ShopData/DailyDealData")]
public class DailyDealSO : ScriptableObject
{
    public List<DailyDealItem> dailyDealItems;
}
[Serializable]
public class DailyDealItem
{
    public ShopItemType itemType;
    public string ItemName;
    public Sprite ItemIcon;
    public int ItemAmount;
    public int ItemCost;
}
public enum ShopItemType
{
   GEM,
   STAMINA,
   GOLD
}