using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int currentHitPoints;

    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (currentHitPoints - 1 <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
        else
        {
            currentHitPoints--;
        }
    }
}
