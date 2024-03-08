using UnityEngine;

public class Enemy : MonoBehaviour
{
	public EnemyType enemyType;
    [SerializeField] protected SkillReference skillReference;
	private void Start()
	{
		skillReference = SkillReference.Instance;
	}
	protected void ChaseSlime(Vector3 slimePos, float chaseSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, slimePos, chaseSpeed);
       if(transform.position.x>=slimePos.x)
		{
            transform.localRotation = new Quaternion(0, 180, 0, 0);
		}
    }

    protected void InvokeEnemyDeath()
	{
		skillReference.InvokeOnDefeatEnemy();
	}
}
public enum EnemyType
{
	BASIC,
	BOSS,
	WIZARD
}