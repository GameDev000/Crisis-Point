using UnityEngine;
using System.Collections;

public class SimpleLever : MonoBehaviour
{
    [Header("Lever Settings")]
    [SerializeField] private Transform lever;
    [SerializeField] private float pressedAngle = -60f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private string playerTag = "Player";

    private bool isAnimating = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAnimating && collision.collider.CompareTag(playerTag))
        {
            if (collision.relativeVelocity.y < 0)  
            {
                StartCoroutine(LeverAction());
            }
        }
    }

    private IEnumerator LeverAction()
    {
        isAnimating = true;
        yield return RotateToAngle(pressedAngle);
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
                rotationSpeed * Time.deltaTime);

            lever.localRotation = Quaternion.Euler(0, 0, newZ);

            yield return null;
        }
    }
}
