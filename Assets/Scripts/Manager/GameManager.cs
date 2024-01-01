using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    public static GameManager Instance;
	// Start is called before the first frame update
	private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }
    void Start()
    {
        currencyManager = GetComponentInChildren<CurrencyManager>();
    }
    public int GetCoinAmount()
    {
        return currencyManager.CoinAmount;
    }
    public int GetGemAmount()
    {
        return currencyManager.GemAmount;
    }

    public void LoadGameSceneAsync(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }
}
