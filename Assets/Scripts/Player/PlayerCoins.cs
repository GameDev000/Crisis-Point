using TMPro;
using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    [Header("Coins Settings")]
    [SerializeField] private int startCoins = 500;     // Start from 500
    [SerializeField] private int endCoins = -500;      // Stop at -500
    [SerializeField] private int coinsPerSecond = 5;   // 5 per second

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinsText;

    public int Coins { get; private set; }

    private void Start()
    {
        Coins = startCoins;
        UpdateCoinsUI();

        StartCoinsCountdown();   // Start decreasing automatically
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        Coins += amount;
        UpdateCoinsUI();
    }

    public bool TrySpendCoins(int amount)
    {
        if (amount <= 0) return true;
        if (Coins < amount) return false;

        Coins -= amount;
        UpdateCoinsUI();
        return true;
    }

    private void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins: " + Coins;
        }
    }

    public void StartCoinsCountdown()
    {
        StartCoroutine(CoinsCountdownRoutine());
    }

    private System.Collections.IEnumerator CoinsCountdownRoutine()
    {
        while (Coins > endCoins)
        {
            for (int i = 0; i < coinsPerSecond; i++)
            {
                Coins -= 1;
                UpdateCoinsUI();
                yield return new WaitForSeconds(1f / coinsPerSecond);
            }
            
        }
    }
}
