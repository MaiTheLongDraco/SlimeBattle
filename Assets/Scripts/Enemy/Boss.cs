using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyMini
{
    [SerializeField] private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        skillReference = SkillReference.Instance;
        anim.applyRootMotion = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause) return;
        ChaseSlime(slime.transform.position, chaseTime);
        UpdateHeathBar();
        DetectDead();
    }
    private void UpdateHeathBar()
    {
        healthBar.fillAmount = heath / totalHeath;
    }
}
