using UnityEngine;
using TMPro;

public class CrisisBackground : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private PlayerCoins playerCoins;
    [SerializeField] private int timeToCrisis = 0;
    [Header("Background")]
    [SerializeField] private SpriteRenderer backgroundSprite;
    [SerializeField] private Camera mainCamera;
    [Header("Crisis Look")]
    [SerializeField] private Color crisisColor = Color.gray;
    private bool _hasSwitched = false;

    private void Awake()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        int coins = playerCoins.Coins;
        if (_hasSwitched)
        {
            if( coins > timeToCrisis)
            {
                _hasSwitched = false;

                if (backgroundSprite != null)
                {
                    backgroundSprite.color = Color.white;
                }
                if (mainCamera != null)
                {
                    mainCamera.backgroundColor = Color.white;
                }

                Debug.Log("Crisis mode OFF – background restored");
            }
        }
        if (coins <= timeToCrisis)
        {
            SwitchToCrisisMode();
        }
    }

    private void SwitchToCrisisMode()
    {
        _hasSwitched = true;

        if (backgroundSprite != null)
        {
            backgroundSprite.color = crisisColor;
        }
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = crisisColor;
        }

        Debug.Log("Crisis mode ON – background turned gray");
    }
}
