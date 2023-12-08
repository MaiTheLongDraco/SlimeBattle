using UnityEngine;

public class EnemyMini : Enemy
{
    [SerializeField] private float chaseTime;
    [SerializeField] private SlimeController slime;

    private void Awake()
    {
        slime = FindObjectOfType<SlimeController>();
    }

    private void Update()
    {
        ChaseSlime(slime.transform.position, chaseTime);
    }

    public Vector2 GetSelfPos()
    {
        return transform.position;
    }
}