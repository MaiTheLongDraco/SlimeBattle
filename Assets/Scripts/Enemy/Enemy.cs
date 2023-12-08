using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected void ChaseSlime(Vector2 slimePos, float chaseSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, slimePos, chaseSpeed);
    }
}