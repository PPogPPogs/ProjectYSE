using UnityEngine;
using UnityEngine.UI;

public class CardPack : MonoBehaviour
{
    public float shakeAmount = 10f; // ��鸲�� ����
    public float shakeSpeed = 2f; // ��鸲�� �ӵ�
    public float shakeDuration = 2f; // ��鸲 ���� �ð�
    public float fadeDuration = 2f; // ������ ������ 0�� �Ǵ� �ð�
    public AudioClip shakeSound; // ��鸲 ����
    public AudioClip stopShakeSound; // ��鸲 ���� �� ����
    public float audioPitch = 1.0f; // ����� ��ġ (��� �ӵ�)
    public GetCard getCardScript; // GetCard ��ũ��Ʈ ����

    private float initialZ; // �ʱ� z�� ȸ����
    private bool isShaking = false; // ��鸲 ����
    private bool isFading = false; // ���� ��ȭ ����
    private float shakeEndTime; // ��鸲 ���� �ð�
    private Image buttonImage; // ��ư�� �̹��� ������Ʈ
    private Color initialColor; // �ʱ� ����
    private AudioSource audioSource; // ����� �ҽ�

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
                getCardScript.SpawnRandomCard(); // ��鸲�� ���� �� ī�� ����
            }
        }
        else if (isFading && buttonImage != null)
        {
            Color newColor = buttonImage.color;
            newColor.a = Mathf.Lerp(newColor.a, 0f, Time.deltaTime / fadeDuration);
            buttonImage.color = newColor;

            // ������ 0�� �����ϸ� fading ���� �� ��ư ��Ȱ��ȭ
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
        isFading = false; // ��鸲 ������ �� ���� ��ȭ�� ����
        shakeEndTime = Time.time + shakeDuration;

        // ���� �ʱ�ȭ
        Color newColor = initialColor;
        newColor.a = 1f;
        buttonImage.color = newColor;

        // ��鸲 ���� ���
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
        isFading = true; // ��鸲�� ���� �� ���� ��ȭ ����

        // ��鸲 ���� ����
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.Stop();
        }

        // ��鸲 ���� �� ���� ���
        if (audioSource != null && stopShakeSound != null)
        {
            audioSource.pitch = 1.0f; // ���� ��ġ�� ����
            audioSource.PlayOneShot(stopShakeSound);
        }
    }
}
