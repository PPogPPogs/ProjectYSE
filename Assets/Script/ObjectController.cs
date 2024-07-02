using System.Collections;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float acceleration = 10f; // 회전 가속도
    public float maxRotationSpeed = 360f; // 최고 회전 속도 (도/초)
    public Sprite newSprite; // 변경될 스프라이트

    private float currentRotationSpeed = 0f;
    private bool isRotating = false;
    private bool isDecelerating = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AccelerateRotation());
    }

    IEnumerator AccelerateRotation()
    {
        while (currentRotationSpeed < maxRotationSpeed)
        {
            currentRotationSpeed += acceleration * Time.deltaTime;
            transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
            yield return null;
        }

        currentRotationSpeed = maxRotationSpeed;
        isRotating = true;
        StartCoroutine(RotateForTime(5f));
    }

    IEnumerator RotateForTime(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isRotating = false;
        isDecelerating = true;
        ChangeSprite(); // 감속 시작할 때 스프라이트 변경
        StartCoroutine(DecelerateRotation());
    }

    IEnumerator DecelerateRotation()
    {
        while (currentRotationSpeed > 0)
        {
            currentRotationSpeed -= acceleration * Time.deltaTime;
            if (currentRotationSpeed < 0) currentRotationSpeed = 0;
            transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
            yield return null;
        }

        currentRotationSpeed = 0;
    }

    void ChangeSprite()
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    void Update()
    {
        if (isRotating || isDecelerating)
        {
            transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
        }
    }
}
