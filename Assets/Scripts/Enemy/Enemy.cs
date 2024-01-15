using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected void ChaseSlime(Vector3 slimePos, float chaseSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, slimePos, chaseSpeed);
       if(transform.position.x>=slimePos.x)
		{
            transform.localRotation = new Quaternion(0, 180, 0, 0);
		}
    }
    
}