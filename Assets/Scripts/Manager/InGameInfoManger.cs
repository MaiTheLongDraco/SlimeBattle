
using UnityEngine;
using UnityEngine.UI;

public class InGameInfoManger : MonoBehaviour
{
    [SerializeField] private Text runTimeSilver;
    [SerializeField] private Text cointAmount;
    [SerializeField] private Text gemAmount;
    [SerializeField] private Text currentHeath;
    [SerializeField] private Text totalHeath;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GamePlayManager gamePlayManager;
    [SerializeField] private SlimeData slimeDataClone;
	private void Start()
	{
        gameManager = GameManager.Instance;
        slimeDataClone = SlimeController.Instance.RootDataClone;
        InitTextInfo();
    }
    private void InitTextInfo()
	{
        runTimeSilver.text = gamePlayManager.RunTimeSilver.ToString();
        cointAmount.text = gameManager.GetCoinAmount().ToString();
        gemAmount.text = gameManager.GetGemAmount().ToString();
        currentHeath.text = slimeDataClone.SlimeDF.Heath.ToString();
        totalHeath.text = slimeDataClone.SlimeDF.Heath.ToString();
    }  
    public void SetTotalHeathTxt(float set )
	{
        totalHeath.text = set.ToString();
	}
    public void SetCurrentHeathTxt(float set )
	{
        currentHeath.text = set.ToString();
	}
    public void SetRuntimeSilverText(int set)
	{
        runTimeSilver.text = set.ToString();
	}

}
