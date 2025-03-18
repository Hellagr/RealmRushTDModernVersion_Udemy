using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    Bank bank;

    void Start()
    {
        bank = FindFirstObjectByType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null) return;

        bank.Deposit(goldReward);
    }

    public void SteelGold()
    {
        if (bank == null) return;

        bank.Withdraw(goldPenalty);
    }
}
