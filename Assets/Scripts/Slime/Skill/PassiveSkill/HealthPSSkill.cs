using UnityEngine;

public class HealthPSSkill : MonoBehaviour, ISkillInvokation
{
    [SerializeField] private SkillState state;
    [SerializeField] private DisplaySubBtnInfo defenseInfo;
    [SerializeField] private float increaseValue;
    [SerializeField] private float upgradeFactor;

    // Start is called before the first frame update
    private void Start()
    {
        increaseValue = SlimeController.Instance.SlimeDF.Heath * 0.3f;
        SetState(SkillState.NOT_UNLOCK);
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
        if (CanTriggerSkill())
        {
            defenseInfo.UpdateValueWithId0(increaseValue);
            SetState(SkillState.NOT_UNLOCK);
        }
    }

    public void SetState(SkillState set)
    {
        state = set;
    }

    public void UpgradeSkill()
    {
        increaseValue += upgradeFactor;
        print($"====== upgrading {name} skill =====");
    }
}