using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private int runTimeSilver;
    public int RunTimeSilver { get => runTimeSilver; set => runTimeSilver = value; }
	public InGameInfoManger InGameInfoManger { get => inGameInfoManger; set => inGameInfoManger = value; }
	public GameObject TroopSkillObj { get => troopSkillObj; set => troopSkillObj = value; }

	[SerializeField] private GameObject troopSkillObj;

	[SerializeField] private InGameInfoManger inGameInfoManger;
    public static GamePlayManager Instance;
    public  EnemySpawner enemySpawner;
	private void Awake()
	{
        Instance = this;
	}

	private void Start()
	{
		enemySpawner = EnemySpawner.Instance;
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
	public void SetActiveTroopSkillUI(bool set)
	{
		TroopSkillObj.SetActive(set);
	}
	public void PauseGame()
	{
		enemySpawner.InvokeOnPauseGame();
		Time.timeScale = 0f;
	}
	public void ResumeGameplay()
	{
		enemySpawner.InvokeOnContinueGame();
		Time.timeScale = 1f;
	}
}
