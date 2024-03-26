using UnityEngine;

public class CriticalChance : MonoBehaviour, ISkillInvokation
{
    [SerializeField] private SkillState state;
    [SerializeField] private SlimeController slimeController;
    [SerializeField] private float critAddingValue;
    [SerializeField] private float upgradeFactor;

    // Start is called before the first frame update
    private void Start()
    {
        SetState(SkillState.NOT_UNLOCK);
        slimeController = SlimeController.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
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
            slimeController.IncreaseCritChance(slimeController.CriticalRate * critAddingValue);
            SetState(SkillState.NOT_UNLOCK);
        }
    }

    public void SetState(SkillState set)
    {
        state = set;
    }

    public void UpgradeSkill()
    {
        critAddingValue += upgradeFactor;
        print($"====== upgrading {name} skill =====");
    }
}