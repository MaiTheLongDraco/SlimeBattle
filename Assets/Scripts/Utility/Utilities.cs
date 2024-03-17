using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Utilities : MonoBehaviour
{
    public static void SaveGoldData(int data)
	{
		PlayerPrefs.SetInt("GoldAmount", data);
	}
	public static void SaveStaminaData(int data)
	{
		PlayerPrefs.SetInt("StaminaAmount", data);
	}
	public static void SaveGemData(int data)
	{
		PlayerPrefs.SetInt("GemAmount", data);
	}
	public static void SaveAllData()
	{
		SaveGemData(CurrencyManager.Instance.GemAmount);
		SaveStaminaData(CurrencyManager.Instance.StaminaAmount);
		SaveGoldData(CurrencyManager.Instance.CoinAmount);
		PlayerPrefs.Save();
	}
	public static int GetGoldData()
	{
		return PlayerPrefs.GetInt("GoldAmount");
	}
	public static int GetGemData()
	{
		return PlayerPrefs.GetInt("GemAmount");
	}
	public static int GetStaminaData()
	{
		return PlayerPrefs.GetInt("StaminaAmount");
	}
}
