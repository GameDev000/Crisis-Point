
using UnityEngine;
public class PlayerTaskInteraction : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private PlayerCoins playerCoins;

    [Header("Bucket Carry")]
    [SerializeField] private Transform bucketHoldPoint;
    [SerializeField] private WashingMachineMover washingMachine;

    [Header("Clothes")]
    [SerializeField] private Transform clothesHoldPoint;
    [SerializeField] private GameObject smallClothPrefab;

    private GameObject carriedBucket;
    private bool hasBucket;
    private GameObject carriedCloth;
    private bool hasCloth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bucket"))
        {
            TryPickBucket(other.gameObject);
        }
        else if (other.CompareTag("WashingMachine"))
        {
            TryInteractWithMachine();
        }
        else if (other.CompareTag("ClothesPile"))
        {
            TryPickClothes(other.gameObject);
        }
    }

    private void TryPickBucket(GameObject bucket)
    {
        if (hasBucket)
        {
            return;
        }
        if (hasCloth)
        {
            return;
        }
        if (washingMachine == null)
        {
            return;
        }

        hasBucket = true;
        carriedBucket = bucket;
        playerCoins.AddCoins(100);

        var rb = bucket.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        var col = bucket.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        bucket.transform.SetParent(bucketHoldPoint != null ? bucketHoldPoint : transform);
        bucket.transform.localPosition = Vector3.zero;
        bucket.transform.localRotation = Quaternion.identity;
    }

    private void TryInteractWithMachine()
    {
        if (washingMachine == null)
        {
            return;
        }

        if (hasBucket && !washingMachine.hasWater)
        {
            washingMachine.Activate();

            if (carriedBucket != null)
            {
                Destroy(carriedBucket);
            }
            carriedBucket = null;
            hasBucket = false;
            playerCoins.AddCoins(50);
            return;
        }

        if (hasCloth && washingMachine.hasWater)
        {
            if (carriedCloth != null)
            {
                Destroy(carriedCloth);
            }
            playerCoins.AddCoins(50);
            carriedCloth = null;
            hasCloth = false;
            return;
        }
    }

    private void TryPickClothes(GameObject clothesPile)
    {
        if (washingMachine == null)
        {
            return;
        }
        if (!washingMachine.hasWater)
        {
            return;
        }
        if (hasCloth)
        {
            return;
        }
        if (hasBucket)
        {
            return;
        }

        Destroy(clothesPile);

        hasCloth = true;
        carriedCloth = Instantiate(smallClothPrefab);

        var rb = carriedCloth.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        var col = carriedCloth.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        carriedCloth.transform.SetParent(clothesHoldPoint != null ? clothesHoldPoint : transform);
        carriedCloth.transform.localPosition = Vector3.zero;
        carriedCloth.transform.localRotation = Quaternion.identity;
    }
}
