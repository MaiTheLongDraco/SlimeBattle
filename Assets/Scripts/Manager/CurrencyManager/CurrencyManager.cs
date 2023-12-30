using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private int coinAmount;
    [SerializeField] private int gemAmount;
	public int GemAmount { get => gemAmount; set => gemAmount = value; }
	public int CoinAmount { get => coinAmount; set => coinAmount = value; }
	
	// Start is called before the first frame update
	void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCointAmount(int set)
	{
        CoinAmount = set;
    }
    public void SetGemAmount(int set)
    {
        GemAmount = set;
    }
       
}
