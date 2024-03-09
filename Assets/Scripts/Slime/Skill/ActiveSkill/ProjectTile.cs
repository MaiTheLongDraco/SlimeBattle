using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] private EnemyMini enemy;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float explodeRadius;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SetTarget(EnemyMini set)
    {
        enemy = set;
    }
    public void SetSpeed(float set)
    {
        speed = set;
    }
    public void SetDamage(float set)
    {
        damage = set;
    }
    // Update is called once per frame
    void Update()
    {

        if (enemy == null)
		{
            Destroy(this.gameObject);
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed);
        Explode();
        if (IsOutOfScreenBounds())
        { Destroy(this.gameObject); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
           
        }
    }
    private void Explode()
	{
        if(Vector2.Distance(transform.position,enemy.transform.position)<0.1f)
		{
            var hits = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
            if (hits.Length <= 0) return;
            foreach (var obj in hits)
            {
                if (!obj.GetComponent<EnemyMini>()) continue;
                print($"--projectile collide with --{ obj.name}--");
                obj.GetComponent<EnemyMini>().TakeDamage(damage);
                Destroy(gameObject);
            }
            
        }
	}        
    bool IsOutOfScreenBounds()
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        return (position.x < 0 || position.x > 1 || position.y < 0 || position.y > 1);
    }
	private void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
	}
}
