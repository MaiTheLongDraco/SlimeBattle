using UnityEngine;

public class SilverGenerator : MonoBehaviour, ISkillInvokation
{
    [SerializeField] private SkillState state;
    [SerializeField] private GamePlayManager gamePlayManager;
    [SerializeField] private int additionSilver;
    [SerializeField] private int upgradeFactor;

    // Start is called before the first frame update
    private void Start()
    {
        SetState(SkillState.NOT_UNLOCK);
        gamePlayManager = GamePlayManager.Instance;
    }

    public SkillState SkillState
    {
        get => state;
        set => state = value;
    }

    public bool CanTriggerSkill()
    {
        if (state != SkillState.UNLOCKED) return false;
        return true;
    }

    public void DoSkill()
    {
        if (CanTriggerSkill()) gamePlayManager.IncreaseRuntimeSilver(additionSilver);
    }

    public void SetState(SkillState set)
    {
        state = set;
    }

    public void UpgradeSkill()
    {
        additionSilver += upgradeFactor;
        print($"====== upgrading {name} skill =====");
    }
}