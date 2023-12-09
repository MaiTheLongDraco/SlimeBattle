using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private EnemyMini currentEnemy;

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

    private void ShootToEnemy()
    {
        print($"is instance null {EnemySpawner.Instance.IsUnityNull()}");
        currentEnemy = SlimeController.Instance.GetCurrentEnemy();
        transform.position = Vector2.MoveTowards(transform.position, currentEnemy.GetSelfPos(), speed);
    }
    
}