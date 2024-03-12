using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private int runTimeSilver;
    public int RunTimeSilver { get => runTimeSilver; set => runTimeSilver = value; }
	public InGameInfoManger InGameInfoManger { get => inGameInfoManger; set => inGameInfoManger = value; }
	public GameObject TroopSkillObj { get => troopSkillObj; set => troopSkillObj = value; }

	[SerializeField] private GameObject troopSkillObj;
	[SerializeField] private GameObject winPanel;
	[SerializeField] private GameObject losePanel;

	[SerializeField] private InGameInfoManger inGameInfoManger;
	[SerializeField] private TestLogActiveSkillInfo troopViewHandler;
    public static GamePlayManager Instance;
    public  EnemySpawner enemySpawner;
	[SerializeField] private UnityEvent onWin;
	[SerializeField] private UnityEvent onLose;
	private void Awake()
	{
        Instance = this;
	}

	private void Start()
	{
		enemySpawner = EnemySpawner.Instance;
		onWin.AddListener(() => SetAcitveGO(winPanel, true));
		onLose.AddListener(() => SetAcitveGO(losePanel, true));
	}
	public void SetAcitveGO(GameObject obj, bool set)
	{
		obj.SetActive(set);
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
		troopViewHandler.ReGenerateSkill();
		enemySpawner.InvokeOnContinueGame();
		Time.timeScale = 1f;
	}
	public void InvokeOnWinGame()
	{
		onWin?.Invoke();
	}
	public void InvokeOnLoseGame()
	{
		onLose?.Invoke();
	}
	public void LoadGameSceneAsync(string name)
	{
		SceneManager.LoadSceneAsync(name);
	}
	public void ReloadCurrentScene()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
	}
}
