using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private int coinAmount;
    [SerializeField] private int gemAmount;
    [SerializeField] private int staminaAmount;
    [SerializeField] private Text staminaText;
    [SerializeField] private Text coinText;
    [SerializeField] private Text gemText;
	public int GemAmount { get => gemAmount; set => gemAmount = value; }
	public int CoinAmount { get => coinAmount; set => coinAmount = value; }
	public int StaminaAmount { get => staminaAmount; set => staminaAmount = value; }
    public static CurrencyManager Instance;
	// Start is called before the first frame update
	private void Awake()
	{
        Instance = this;
	}
	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
		UpdateText();
	}
    public bool IsEnoughCoin(int amount)
	{
        if (amount >= coinAmount) return true;
        return false;
	} public bool IsEnoughStamina(int amount)
	{
        if (amount >= staminaAmount) return true;
        return false;
	} public bool IsEnoughGem(int amount)
	{
        if (amount >= gemAmount) return true;
        return false;
	}
	public void UpdateText()
	{
		SetText(staminaText, staminaAmount);
		SetText(coinText, coinAmount);
		SetText(gemText, gemAmount);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
    private void SetText(Text text, int amount)
	{
        text.text = amount.ToString();
	}
    public void SetCointAmount(int set)
	{
        CoinAmount = set;
    }
    public void SetGemAmount(int set)
    {
        GemAmount = set;
    }  
    public void DecreaeGemAmount(int amount)
	{
        if (gemAmount <= 0) return;
        GemAmount -= amount;
	}
    
    public void DecreaeCoinAmount(int amount)
    {
        if (CoinAmount <= 0) return;
        CoinAmount -= amount;
    }
    public void DecreaesStaminaAmount(int amount)
    {
        if (StaminaAmount <= 0) return;
        StaminaAmount -= amount;
    }
    public void IncreaseGemAmount(int amount)
    {
        GemAmount += amount;
    }
    public void IncreaseStaminaAmount(int amount)
    {
        StaminaAmount += amount;
    }
    public void IncreaseCoinAmount(int amount)
    {
        CoinAmount += amount;
    }
    public void SetStaminaAmount(int set)
    {
        staminaAmount = set;
    }
       
}
