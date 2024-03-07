using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingShotBullet : MonoBehaviour
{
    [SerializeField] private Vector2 targetPos;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetTargetPos(Vector2 newPos)
	{
        targetPos = newPos;
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
        
        if (targetPos == null)
            return;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed);
        if (IsOutOfScreenBounds())
        { Destroy(this.gameObject); }
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Enemy"))
		{
            collision.GetComponent<EnemyMini>().TakeDamage(damage);
            print("ring shot bullet collide with enemy");
		}
	}
    bool IsOutOfScreenBounds()
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        return (position.x < 0 || position.x > 1 || position.y < 0 || position.y > 1);
    }

}
