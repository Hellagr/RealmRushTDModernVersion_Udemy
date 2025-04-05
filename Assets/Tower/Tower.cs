using System;
using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] GameObject towerBase;
    [SerializeField] GameObject towerTop;
    [SerializeField] ParticleSystem towerProjectiles;

    void OnEnable()
    {
        StartCoroutine(Build());
    }

    IEnumerator Build()
    {
        towerBase.SetActive(true);
        yield return new WaitForSeconds(1);
        towerTop.SetActive(true);
        yield return new WaitForSeconds(1);
        towerProjectiles.gameObject.SetActive(true);
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindFirstObjectByType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= cost)
        {
            bank.Withdraw(cost);
            Instantiate(tower, position, Quaternion.identity);
            return true;
        }

        return false;
    }
}
