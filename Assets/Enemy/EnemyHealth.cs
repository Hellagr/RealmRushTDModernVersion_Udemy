using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int currentHitPoints;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        if (currentHitPoints - 1 <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            currentHitPoints--;
        }
    }
}
