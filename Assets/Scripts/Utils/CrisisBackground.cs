using UnityEngine;

public class CrisisBackground : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float timeToCrisis = 15f;

    [Header("Background")]
    [SerializeField] private SpriteRenderer backgroundSprite;
    [SerializeField] private Camera mainCamera;      

    [Header("Crisis Look")]
    [SerializeField] private Color crisisColor = Color.gray;

    private float _timer = 0f;
    private bool _hasSwitched = false;

    private void Awake()
    {
        if (!mainCamera)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_hasSwitched)
            return;

        _timer += Time.deltaTime;

        if (_timer >= timeToCrisis)
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
