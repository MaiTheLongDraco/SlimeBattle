using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private EnemyMini currentEnemy;
    [SerializeField] private BulletType bulletType;
    [SerializeField] private float speed = 0.015f;
    [SerializeField] private float duration;
    private float damage;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    // Update is called once per frame
    private void Update()
    {
		ShootToEnemy();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (bulletType == BulletType.SPECIAL) other.GetComponent<EnemyMini>().TakeDamage(damage);
    }

    public BulletType GetType()
    {
        return bulletType;
    }

    public void SetDamage(float set)
    {
        damage = set;
    }
    public void SetCurrentEnemy(EnemyMini enemyMini)
	{
        currentEnemy = enemyMini;

    }        
    public void RemoveDeathEnemy(List<EnemyMini> list)
	{
        if(currentEnemy.IsDead())
        list.Remove(currentEnemy);
	}
    private void ShootToEnemy()
    {
        if (currentEnemy == null)
		{
            Destroy(this.gameObject);
            return;
		}
        var dir = currentEnemy.GetSelfPos() - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, currentEnemy.GetSelfPos(), speed);
        //transform.Translate(dir * speed * Time.deltaTime);
        //transform.LookAt(currentEnemy.transform);
        HandleWithType();
    }
    public void SetSpeed(float set)
	{
        speed = set;
	}
    private void HandleWithType()
    {
        switch (bulletType)
        {
            case BulletType.NORMAL:
            {
                if (Vector2.Distance(transform.position, currentEnemy.transform.position) <= 0.1f)
                {
                    print($"make damage to {currentEnemy.name}");
                    currentEnemy.TakeDamage(damage);
                    Destroy(gameObject);
                }

                break;
            }
            case BulletType.SPECIAL:
            {
                Destroy(gameObject, duration);
                break;
            }
        }
    }
}

public enum BulletType
{
    NORMAL,
    SPECIAL
}