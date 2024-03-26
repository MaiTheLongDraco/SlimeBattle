using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static bool isLoaded;
    [SerializeField] private CurrencyManager currencyManager;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!isLoaded)
        {
            DontDestroyOnLoad(gameObject);
            isLoaded = true;
        }

        Instance = this;
    }

    private void Start()
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

    public void LoadSceneWithIndex(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
}