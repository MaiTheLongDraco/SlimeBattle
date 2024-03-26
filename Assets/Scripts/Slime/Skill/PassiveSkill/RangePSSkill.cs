using UnityEngine;

public class RangePSSkill : MonoBehaviour, ISkillInvokation
{
    [SerializeField] private SkillState state;
    [SerializeField] private DisplaySubBtnInfo attackInfo;
    [SerializeField] private float increaseValue;
    [SerializeField] private float upgradeFactor;

    // Start is called before the first frame update
    private void Start()
    {
        SetState(SkillState.NOT_UNLOCK);
        increaseValue = SlimeController.Instance.SlimeATK.AttackRange * 0.1f;
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
            attackInfo.UpdateValueWithId2(increaseValue);
            SetState(SkillState.NOT_UNLOCK);
            print(" increase 10% atk range");
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