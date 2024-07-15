using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Chapter1 : MonoBehaviour
{
    public GameObject panel; // 비활성화할 패널
    public GameObject objectToMove; // 이동할 오브젝트
    public Button actionButton; // 버튼
    public Text conversationText; // 대화 텍스트를 표시할 UI Text
    public string fullText = "안녕하세요! 이 텍스트는 한 글자씩 출력됩니다."; // 전체 대화 내용
    public float typingDelay = 0.06f; // 글자 출력 대기 시간
    public AudioClip typingSound; // 글자 출력 사운드 클립
    public float typingSoundVolume = 0.6f; // 글자 출력 사운드 볼륨
    public Chapter1Button chapter1Button; // Chapter1Button 스크립트 참조

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (actionButton != null)
        {
            actionButton.onClick.AddListener(OnButtonClick);
        }

        if (conversationText != null)
        {
            StartCoroutine(TypeText());
        }
    }

    void OnButtonClick()
    {
        if (chapter1Button != null)
        {
            chapter1Button.EnableButton(); // Chapter1Button 스크립트의 EnableButton 메소드 호출
        }
        else
        {
            Debug.LogError("Chapter1Button reference is not set.");
        }

        if (panel != null)
        {
            panel.SetActive(false); // 패널 비활성화
        }

        if (objectToMove != null)
        {
            objectToMove.transform.position = new Vector3(-2.44f, -6.22f, 1.0f); // 오브젝트 좌표 이동
        }
    }

    IEnumerator TypeText()
    {
        conversationText.text = "";
        foreach (char letter in fullText.ToCharArray())
        {
            conversationText.text += letter;

            if (typingSound != null && audioSource != null)
            {
                audioSource.volume = typingSoundVolume; // 사운드 볼륨 설정
                audioSource.PlayOneShot(typingSound); // 글자 출력 시 사운드 재생
            }

            yield return new WaitForSeconds(typingDelay); // 글자 출력 대기 시간
        }
    }
}
