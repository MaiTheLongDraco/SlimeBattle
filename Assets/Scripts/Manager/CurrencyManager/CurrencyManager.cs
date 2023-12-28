using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private int coinAmount;
    [SerializeField] private int gemAmount;
    public static CurrencyManager Instance;
	public int GemAmount { get => gemAmount; set => gemAmount = value; }
	public int CoinAmount { get => coinAmount; set => coinAmount = value; }
	private void Awake()
	{
        Instance = this;

    }
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
    public void LoadGameSceneAsync(string name)
	{
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
	}        
}
