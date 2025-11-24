using UnityEngine;

public class WashingMachineMover : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.1f;
    [SerializeField] private float frequency = 5f;
    public bool hasWater { get; private set; } = false;


    private bool isActive = false;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive) return;

        float offsetX = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(offsetX, 0f, 0f);
    }


    public void Activate()
    {
        isActive = true;
        hasWater = true;
    }


    public void Deactivate()
    {
        isActive = false;
        transform.position = startPosition;
    }
}
