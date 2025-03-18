using System;
using UnityEngine;

public class TargerLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float towersRange = 15f;
    Transform target;

    void Update()
    {
        FindClosetTarget();
        AimWeapon();
    }

    private void FindClosetTarget()
    {
        Enemy[] allEnemy = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        float distance = Mathf.Infinity;

        Transform closetTarget = null;
        var towerPosition = transform.position;

        foreach (var enemy in allEnemy)
        {

            var enemyPosition = enemy.transform.position;

            float distanceBetweenEnemyAndTower = Mathf.Sqrt(Mathf.Pow(enemyPosition.x - towerPosition.x, 2) + Mathf.Pow(enemyPosition.y - towerPosition.y, 2) + Mathf.Pow(enemyPosition.z - towerPosition.z, 2));

            if (distanceBetweenEnemyAndTower < distance)
            {
                distance = distanceBetweenEnemyAndTower;
                closetTarget = enemy.transform;
            }
        }

        target = closetTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Mathf.Sqrt(Mathf.Pow(target.position.x - transform.position.x, 2) + Mathf.Pow(target.position.y - transform.position.y, 2) + Mathf.Pow(target.position.z - transform.position.z, 2));

        weapon.LookAt(target);

        if (targetDistance > towersRange)
        {
            Attack(false);
        }
        else
        {

            Attack(true);
        }
    }

    void Attack(bool isActive)
    {
        var emmisionModule = projectileParticles.emission;
        emmisionModule.enabled = isActive;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, towersRange);
    }
}
