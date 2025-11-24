using UnityEngine;
using System.Collections;

public class SimpleLever : MonoBehaviour
{
    [Header("Lever Settings")]
    [SerializeField] private Transform lever;
    [SerializeField] private float pressedAngle = -60f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private string playerTag = "Player";

    [Header("Bucket & Rope")]
    [SerializeField] private Transform bucket;
    [SerializeField] private float bucketTopY = -1f;
    [SerializeField] private float bucketRaiseDuration = 0.4f;

    [SerializeField] private Transform rope;
    [SerializeField] private float ropeMinScaleY = 0.0f;
    [SerializeField] private float ropeDestroyThreshold = 0.07f;

    [SerializeField] private int pressesToActivate = 3;

    private int currentPresses = 0;
    private bool isAnimating = false;
    private float ropeInitialScaleY;
    private float bucketBottomY;

    private void Awake()
    {
        if (bucket != null)
            bucketBottomY = bucket.position.y;

        if (rope != null)
            ropeInitialScaleY = rope.localScale.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAnimating) return;

        if (collision.collider.CompareTag(playerTag) && collision.relativeVelocity.y < 0)
        {
            if (currentPresses < pressesToActivate)
            {
                currentPresses++;
                StartCoroutine(LeverActionStep());
            }
        }
    }

    private IEnumerator LeverActionStep()
    {
        isAnimating = true;

        yield return RotateToAngle(pressedAngle);

        if (bucket != null)
            yield return RaiseBucketStep();

        yield return RotateToAngle(0);

        isAnimating = false;
    }

    private IEnumerator RotateToAngle(float targetAngle)
    {
        while (Mathf.Abs(Mathf.DeltaAngle(lever.localEulerAngles.z, targetAngle)) > 0.1f)
        {
            float newZ = Mathf.MoveTowardsAngle(
                lever.localEulerAngles.z,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );

            lever.localRotation = Quaternion.Euler(0, 0, newZ);
            yield return null;
        }
    }

    private IEnumerator RaiseBucketStep()
    {
        float targetT = (float)currentPresses / pressesToActivate;
        float targetY = Mathf.Lerp(bucketBottomY, bucketTopY, targetT);

        Vector3 startPos = bucket.position;
        Vector3 endPos = new Vector3(bucket.position.x, targetY, bucket.position.z);

        float startRopeScaleY = rope != null ? rope.localScale.y : 1f;
        float targetRopeScaleY = Mathf.Lerp(ropeInitialScaleY, ropeMinScaleY, targetT);

        float elapsed = 0f;

        while (elapsed < bucketRaiseDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / bucketRaiseDuration);

            bucket.position = Vector3.Lerp(startPos, endPos, t);

            if (rope != null)
            {
                Vector3 s = rope.localScale;
                s.y = Mathf.Lerp(startRopeScaleY, targetRopeScaleY, t);
                rope.localScale = s;

                if (s.y < ropeDestroyThreshold)
                {
                    Destroy(rope.gameObject);
                    rope = null;
                }
            }

            yield return null;
        }

        bucket.position = endPos;

        if (rope != null)
        {
            Vector3 s = rope.localScale;
            s.y = targetRopeScaleY;
            rope.localScale = s;
        }
    }
}
