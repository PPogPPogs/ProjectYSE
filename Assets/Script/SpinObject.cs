using System.Collections;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float acceleration = 10f; // 회전 가속도
    public float maxRotationSpeed = 360f; // 최고 회전 속도 (도/초)
    public Sprite newSprite; // 변경될 스프라이트
    public float shrinkDuration = 2f; // 크기 축소 시간
    public float targetScaleFactor = 0.5f; // 최종 크기 비율
    public GameObject afterimagePrefab; // 잔상 프리팹
    public GameObject panel; //활성화할 패널
    public float afterimageDuration = 0.5f; // 잔상이 남는 시간
    public Color afterimageColor = Color.white; // 잔상의 색상
    public AudioClip rotationSound; // 회전 사운드 클립
    public AudioClip shrinkSound; // 축소 사운드 클립
    public AudioClip shrinkCompleteSound; // 축소 완료 사운드 클립
    public float rotationSoundVolume = 0.5f; // 회전 사운드 볼륨
    public float shrinkSoundVolume = 0.5f; // 축소 사운드 볼륨
    public float shrinkCompleteSoundVolume = 0.5f; // 축소 완료 사운드 볼륨

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
                audioSource.volume = rotationSoundVolume; // 회전 사운드 볼륨 설정
                audioSource.Play(); // 회전이 시작될 때 사운드 재생
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

        // 축소 사운드 재생
        audioSource.clip = shrinkSound;
        audioSource.volume = shrinkSoundVolume; // 축소 사운드 볼륨 설정
        audioSource.Play();

        while (elapsedTime < shrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);
            CreateAfterimage(); // 잔상 생성
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        // 축소 완료 사운드 재생
        audioSource.clip = shrinkCompleteSound;
        audioSource.volume = shrinkCompleteSoundVolume; // 축소 완료 사운드 볼륨 설정
        audioSource.Play();
        
        //패널 활성화
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
