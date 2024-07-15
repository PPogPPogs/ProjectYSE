using UnityEngine;
using UnityEngine.UI;

public class CardPack : MonoBehaviour
{
    public float shakeAmount = 10f; // 흔들림의 강도
    public float shakeSpeed = 2f; // 흔들림의 속도
    public float shakeDuration = 2f; // 흔들림 지속 시간
    public float fadeDuration = 2f; // 투명도가 서서히 0이 되는 시간
    public AudioClip shakeSound; // 흔들림 사운드
    public AudioClip stopShakeSound; // 흔들림 멈춘 후 사운드
    public float audioPitch = 1.0f; // 오디오 피치 (재생 속도)
    public GetCard getCardScript; // GetCard 스크립트 참조

    private float initialZ; // 초기 z축 회전값
    private bool isShaking = false; // 흔들림 여부
    private bool isFading = false; // 투명도 변화 여부
    private float shakeEndTime; // 흔들림 종료 시간
    private Image buttonImage; // 버튼의 이미지 컴포넌트
    private Color initialColor; // 초기 색상
    private AudioSource audioSource; // 오디오 소스

    void Start()
    {
        initialZ = transform.rotation.eulerAngles.z;
        buttonImage = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        if (buttonImage != null)
        {
            initialColor = buttonImage.color;
        }
        else
        {
            Debug.LogError("Image component is missing on this Button.");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component is missing on this GameObject.");
        }

        if (getCardScript == null)
        {
            Debug.LogError("GetCard script reference is missing.");
        }
    }

    void Update()
    {
        if (isShaking)
        {
            float zRotation = initialZ + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zRotation));

            if (Time.time >= shakeEndTime)
            {
                StopShaking();
                getCardScript.SpawnRandomCard(); // 흔들림이 멈출 때 카드 생성
            }
        }
        else if (isFading && buttonImage != null)
        {
            Color newColor = buttonImage.color;
            newColor.a = Mathf.Lerp(newColor.a, 0f, Time.deltaTime / fadeDuration);
            buttonImage.color = newColor;

            // 투명도가 0에 도달하면 fading 종료 및 버튼 비활성화
            if (newColor.a <= 0.01f)
            {
                isFading = false;
                gameObject.SetActive(false);
            }
        }
    }

    void OnButtonClick()
    {
        StartShaking();
    }

    public void StartShaking()
    {
        if (buttonImage == null)
        {
            Debug.LogError("Cannot start shaking because Image component is missing.");
            return;
        }

        isShaking = true;
        isFading = false; // 흔들림 시작할 때 투명도 변화를 멈춤
        shakeEndTime = Time.time + shakeDuration;

        // 투명도 초기화
        Color newColor = initialColor;
        newColor.a = 1f;
        buttonImage.color = newColor;

        // 흔들림 사운드 재생
        if (audioSource != null && shakeSound != null)
        {
            audioSource.clip = shakeSound;
            audioSource.loop = true;
            audioSource.pitch = audioPitch;
            audioSource.Play();
        }
    }

    private void StopShaking()
    {
        isShaking = false;
        isFading = true; // 흔들림이 멈출 때 투명도 변화 시작

        // 흔들림 사운드 정지
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }

        // 흔들림 멈춘 후 사운드 재생
        if (audioSource != null && stopShakeSound != null)
        {
            audioSource.pitch = 1.0f; // 원래 피치로 복구
            audioSource.PlayOneShot(stopShakeSound);
        }
    }
}
