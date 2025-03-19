using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    [SerializeField] TextMeshProUGUI displayBalance;

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amout)
    {
        currentBalance += Mathf.Abs(amout);
        UpdateDisplay();
    }

    public void Withdraw(int amout)
    {
        currentBalance -= Mathf.Abs(amout);
        UpdateDisplay();

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

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }
}
