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

    private void ShootToEnemy()
    {
        currentEnemy = SlimeController.Instance.GetCurrentEnemy();
        if (currentEnemy == null) return;
        transform.position = Vector2.MoveTowards(transform.position, currentEnemy.GetSelfPos(), speed);
        transform.LookAt(currentEnemy.transform);
        HandleWithType();
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