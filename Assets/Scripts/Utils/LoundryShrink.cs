using UnityEngine;

public class ClothesPile : MonoBehaviour
{
    [SerializeField] private GameObject smallClothPrefab;
    [SerializeField] private int steps = 3;

    private int currentStep;

    private void Awake()
    {
        currentStep = steps;
    }

    public GameObject GetCloth()
    {
        if (currentStep <= 0) return null;

        currentStep--;

        Vector3 s = transform.localScale;
        float factor = (float)currentStep / (float)steps;
        transform.localScale = new Vector3(s.x, s.y * factor, s.z);

        GameObject cloth = Instantiate(smallClothPrefab);
        cloth.transform.position = transform.position;

        return cloth;
    }
}
