using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubButtonClickHandle : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private DisplaySubBtnInfo parentInfo;
    // Start is called before the first frame update
    void Start()
    {
        parentInfo = GetComponentInParent<DisplaySubBtnInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
