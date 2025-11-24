using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoins : MonoBehaviour
{
    [Header("Coins Settings")]
    [SerializeField] private int startCoins = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinsText;

    public int Coins { get; private set; }

    private void Start()
    {
        Coins = startCoins;
        UpdateCoinsUI();
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

        if (Coins < amount)
            return false;

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
}
