using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Chapter1 : MonoBehaviour
{
    public GameObject panel; // ��Ȱ��ȭ�� �г�
    public GameObject objectToMove; // �̵��� ������Ʈ
    public Button actionButton; // ��ư
    public Text conversationText; // ��ȭ �ؽ�Ʈ�� ǥ���� UI Text
    public string fullText = "�ȳ��ϼ���! �� �ؽ�Ʈ�� �� ���ھ� ��µ˴ϴ�."; // ��ü ��ȭ ����
    public float typingDelay = 0.06f; // ���� ��� ��� �ð�
    public AudioClip typingSound; // ���� ��� ���� Ŭ��
    public float typingSoundVolume = 0.6f; // ���� ��� ���� ����
    public Chapter1Button chapter1Button; // Chapter1Button ��ũ��Ʈ ����

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
            chapter1Button.EnableButton(); // Chapter1Button ��ũ��Ʈ�� EnableButton �޼ҵ� ȣ��
        }
        else
        {
            Debug.LogError("Chapter1Button reference is not set.");
        }

        if (panel != null)
        {
            panel.SetActive(false); // �г� ��Ȱ��ȭ
        }

        if (objectToMove != null)
        {
            objectToMove.transform.position = new Vector3(-2.44f, -6.22f, 1.0f); // ������Ʈ ��ǥ �̵�
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
                audioSource.volume = typingSoundVolume; // ���� ���� ����
                audioSource.PlayOneShot(typingSound); // ���� ��� �� ���� ���
            }

            yield return new WaitForSeconds(typingDelay); // ���� ��� ��� �ð�
        }
    }
}
