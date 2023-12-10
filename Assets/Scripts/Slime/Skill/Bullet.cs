using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private EnemyMini currentEnemy;
    private float damage;

    [SerializeField] private float speed = 0.015f;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        ShootToEnemy();
    }
    public void SetDamage(float set)
	{
        damage = set;
	}
    private void ShootToEnemy()
    {
        print($"is instance null {EnemySpawner.Instance.IsUnityNull()}");
        currentEnemy = SlimeController.Instance.GetCurrentEnemy();
        transform.position = Vector2.MoveTowards(transform.position, currentEnemy.GetSelfPos(), speed);
        if(Vector2.Distance(transform.position,currentEnemy.transform.position)<=0.1f)
		{
            print($"make damage to {currentEnemy.name}");
            currentEnemy.TakeDamage(damage);
            Destroy(this.gameObject);
		}
    }
    
}