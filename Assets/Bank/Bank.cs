using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    public int CurrentBallance
    {
        get
        {
            return currentBalance;
        }
    }

    void Awake()
    {
        currentBalance = startingBalance;
    }

    public void Deposit(int amout)
    {
        currentBalance += Mathf.Abs(amout);
    }

    public void Withdraw(int amout)
    {
        currentBalance -= Mathf.Abs(amout);

        if (currentBalance < 0)
        {
            //Lose game;
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
