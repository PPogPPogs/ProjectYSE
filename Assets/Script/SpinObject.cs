using System.Collections;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float acceleration = 10f; // ȸ�� ���ӵ�
    public float maxRotationSpeed = 360f; // �ְ� ȸ�� �ӵ� (��/��)
    public Sprite newSprite; // ����� ��������Ʈ
    public float shrinkDuration = 2f; // ũ�� ��� �ð�
    public float targetScaleFactor = 0.5f; // ���� ũ�� ����
    public GameObject afterimagePrefab; // �ܻ� ������
    public GameObject panel; //Ȱ��ȭ�� �г�
    public float afterimageDuration = 0.5f; // �ܻ��� ���� �ð�
    public Color afterimageColor = Color.white; // �ܻ��� ����
    public AudioClip rotationSound; // ȸ�� ���� Ŭ��
    public AudioClip shrinkSound; // ��� ���� Ŭ��
    public AudioClip shrinkCompleteSound; // ��� �Ϸ� ���� Ŭ��
    public float rotationSoundVolume = 0.5f; // ȸ�� ���� ����
    public float shrinkSoundVolume = 0.5f; // ��� ���� ����
    public float shrinkCompleteSoundVolume = 0.5f; // ��� �Ϸ� ���� ����

    private float currentRotationSpeed = 0f;
    private bool isRotating = false;
    private bool isDecelerating = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(AccelerateRotation());
    }

    IEnumerator AccelerateRotation()
    {
        while (currentRotationSpeed < maxRotationSpeed)
        {
            currentRotationSpeed += acceleration * Time.deltaTime;
            transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = rotationSound;
                audioSource.volume = rotationSoundVolume; // ȸ�� ���� ���� ����
                audioSource.Play(); // ȸ���� ���۵� �� ���� ���
            }
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
        ChangeSprite(); // ���� ������ �� ��������Ʈ ����
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
        StartCoroutine(ShrinkObject());
    }

    void ChangeSprite()
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    IEnumerator ShrinkObject()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * targetScaleFactor;
        float elapsedTime = 0f;

        // ��� ���� ���
        audioSource.clip = shrinkSound;
        audioSource.volume = shrinkSoundVolume; // ��� ���� ���� ����
        audioSource.Play();

        while (elapsedTime < shrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);
            CreateAfterimage(); // �ܻ� ����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        // ��� �Ϸ� ���� ���
        audioSource.clip = shrinkCompleteSound;
        audioSource.volume = shrinkCompleteSoundVolume; // ��� �Ϸ� ���� ���� ����
        audioSource.Play();
        
        //�г� Ȱ��ȭ
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    void CreateAfterimage()
    {
        GameObject afterimage = Instantiate(afterimagePrefab, transform.position, transform.rotation);
        afterimage.transform.localScale = transform.localScale;
        SpriteRenderer afterimageRenderer = afterimage.GetComponent<SpriteRenderer>();
        afterimageRenderer.sprite = spriteRenderer.sprite;
        afterimageRenderer.color = afterimageColor;

        StartCoroutine(FadeAfterimage(afterimage));
    }

    IEnumerator FadeAfterimage(GameObject afterimage)
    {
        SpriteRenderer afterimageRenderer = afterimage.GetComponent<SpriteRenderer>();
        Color originalColor = afterimageRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < afterimageDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / afterimageDuration);
            afterimageRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(afterimage);
    }

    void Update()
    {
        if (isRotating || isDecelerating)
        {
            transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime);
        }
    }
}
