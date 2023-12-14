using UnityEngine;

public class SubButton : MonoBehaviour
{
    [SerializeField] private TabInfo subTabType;

    public TabInfo SubTabType
    {
        get => subTabType;
        set => subTabType = value;
    }
}