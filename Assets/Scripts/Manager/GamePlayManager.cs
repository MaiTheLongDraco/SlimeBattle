using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private int runTimeSilver;
    public int RunTimeSilver { get => runTimeSilver; set => runTimeSilver = value; }
	public InGameInfoManger InGameInfoManger { get => inGameInfoManger; set => inGameInfoManger = value; }

	[SerializeField] private InGameInfoManger inGameInfoManger;
    public static GamePlayManager Instance;
	private void Awake()
	{
        Instance = this;
	}
	public void SetTotalHeathTxt(float set)
	{
		inGameInfoManger.SetTotalHeathTxt(set);
	}
	public void SetCurrentHeathTxt(float set)
	{
		inGameInfoManger.SetCurrentHeathTxt(set);

	}
	public void SetRuntimeSilverText(int set)
	{
		inGameInfoManger.SetRuntimeSilverText(set);
	}
	public void IncreaseRuntimeSilver(int addition)
	{
		runTimeSilver += addition;
		SetRuntimeSilverText(runTimeSilver);
	}
}
