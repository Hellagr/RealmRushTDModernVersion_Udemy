using System;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnTime;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTime);
            //Instantiate(obj, transform.position, Quaternion.identity);
        }
    }

    private void EnableObjectInPool()
    {
        foreach (var enemyObject in pool)
        {
            if (!enemyObject.activeSelf)
            {
                enemyObject.SetActive(true);
                return;
            }
        }
    }
}
