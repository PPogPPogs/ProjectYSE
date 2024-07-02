using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;       // ĳ���� �̸��� ǥ���ϴ� �ؽ�Ʈ ������Ʈ
    public Text dialogueText;   // ��ȭ ������ ǥ���ϴ� �ؽ�Ʈ ������Ʈ
    public GameObject dialoguePanel; // ��ȭâ �г�
    public GameObject GameChacter; // ���� ĳ����
    public Button startButton;  // ��ȭ�� �����ϴ� ��ư
    public Button nextButton;   // ���� ��ȭ�� ǥ���ϴ� ��ư
    public Button previousButton; // ���� ��ȭ�� ǥ���ϴ� ��ư
    public Image dialogueImage; // ��ȭ �̹����� ǥ���ϴ� �̹��� ������Ʈ
    public Dialogue dialogue;   // ��ȭ ������ �����ϴ� ����
    public AudioSource audioSource; // ���� ȿ���� ����� ����� �ҽ�
    public AudioClip typingSound; // ��� ��� �� ����� ���� ȿ��

    private Queue<Sentence> sentences;
    private Stack<Sentence> previousSentences;

    void Start()
    {
        sentences = new Queue<Sentence>();
        previousSentences = new Stack<Sentence>();
        dialoguePanel.SetActive(false); // ��ȭâ ��Ȱ��ȭ
        nextButton.gameObject.SetActive(false); // ���� ��ȭ ��ư ��Ȱ��ȭ
        previousButton.gameObject.SetActive(false); // ���� ��ȭ ��ư ��Ȱ��ȭ
    }

    public void StartDialogue()
    {
        Debug.Log("StartDialogue called.");

        // ��ȭâ�� ��ư ���� ����
        dialoguePanel.SetActive(true); // ��ȭâ Ȱ��ȭ
        startButton.gameObject.SetActive(false); // ��ȭ ���� ��ư ��Ȱ��ȭ
        nameText.text = dialogue.characterName; // ĳ���� �̸� ����
        sentences.Clear(); // ť �ʱ�ȭ
        previousSentences.Clear(); // ���� �ʱ�ȭ

        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // ��� ������ ť�� �߰�
        }

        // ��ư Ȱ��ȭ
        nextButton.gameObject.SetActive(true); // ���� ��ȭ ��ư Ȱ��ȭ
        previousButton.gameObject.SetActive(true); // ���� ��ȭ ��ư Ȱ��ȭ

        DisplayNextSentence(); // ù ��° ������ �ڵ����� ���
    }

    public void DisplayNextSentence()
    {
        Debug.Log("DisplayNextSentence called.");

        // ��� ������ ��µ� ��� ��ȭ ����
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // ���� ������ ���ÿ� ����
        if (dialogueText.text != "")
        {
            Sentence previousSentence = new Sentence { characterName = nameText.text, text = dialogueText.text, image = dialogueImage.sprite };
            previousSentences.Push(previousSentence);
        }

        // ť���� ���� ���� ������
        Sentence sentence = sentences.Dequeue();

        // ������ Ÿ���� �ڷ�ƾ �����ϰ� ���ο� �ڷ�ƾ ����
        StopAllCoroutines();
        StartCoroutine(DisplaySentence(sentence));
    }

    public void DisplayPreviousSentence()
    {
        Debug.Log("DisplayPreviousSentence called.");

        // ���� ������ ���� ���
        if (previousSentences.Count == 0)
        {
            Debug.Log("No previous sentences.");
            return;
        }

        // ���� ������ ť�� �� ������ �߰�
        if (dialogueText.text != "")
        {
            Sentence currentSentence = new Sentence { characterName = nameText.text, text = dialogueText.text, image = dialogueImage.sprite };
            // ���ο� ť�� ����� ���� ������ �տ� �߰��ϰ� ������ ������ �ڿ� �߰�
            Queue<Sentence> newQueue = new Queue<Sentence>();
            newQueue.Enqueue(currentSentence);
            while (sentences.Count > 0)
            {
                newQueue.Enqueue(sentences.Dequeue());
            }
            sentences = newQueue;
        }

        // ���ÿ��� ���� ���� ������
        Sentence previousSentence = previousSentences.Pop();

        // ������ Ÿ���� �ڷ�ƾ �����ϰ� ���ο� �ڷ�ƾ ����
        StopAllCoroutines();
        StartCoroutine(DisplaySentence(previousSentence));
    }

    IEnumerator DisplaySentence(Sentence sentence)
    {
        dialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        nameText.text = sentence.characterName; // �̸� ������Ʈ

        // �̹��� ������Ʈ
        if (sentence.image != null)
        {
            dialogueImage.sprite = sentence.image;
            dialogueImage.gameObject.SetActive(true); // �̹��� ǥ��
        }
        else
        {
            dialogueImage.gameObject.SetActive(false); // �̹��� �����
        }

        // �� ���ھ� �߰�
        foreach (char letter in sentence.text.ToCharArray())
        {
            dialogueText.text += letter;
            if (audioSource != null && typingSound != null)
            {
                audioSource.PlayOneShot(typingSound); // ���� ȿ�� ���
            }
            yield return new WaitForSeconds(0.06f); // 0.06�� ���
        }
    }

    void EndDialogue()
    {
        Debug.Log("EndDialogue called.");

        // ��ȭ ���� �� ���� ����
        dialoguePanel.SetActive(false); // ��ȭâ ��Ȱ��ȭ
        nextButton.gameObject.SetActive(false); // ���� ��ȭ ��ư ��Ȱ��ȭ
        previousButton.gameObject.SetActive(false); // ���� ��ȭ ��ư ��Ȱ��ȭ
        GameChacter.gameObject.SetActive(true); // ���� ĳ���� Ȱ��ȭ
        Debug.Log("End of conversation.");
    }
}
