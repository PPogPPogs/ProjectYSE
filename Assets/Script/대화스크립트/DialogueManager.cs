using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;       // 캐릭터 이름을 표시하는 텍스트 컴포넌트
    public Text dialogueText;   // 대화 내용을 표시하는 텍스트 컴포넌트
    public GameObject dialoguePanel; // 대화창 패널
    public GameObject GameChacter; // 염율 캐릭터
    public Button startButton;  // 대화를 시작하는 버튼
    public Button nextButton;   // 다음 대화를 표시하는 버튼
    public Button previousButton; // 이전 대화를 표시하는 버튼
    public Image dialogueImage; // 대화 이미지를 표시하는 이미지 컴포넌트
    public Dialogue dialogue;   // 대화 내용을 저장하는 변수
    public AudioSource audioSource; // 음향 효과를 재생할 오디오 소스
    public AudioClip typingSound; // 대사 출력 시 재생할 음향 효과

    private Queue<Sentence> sentences;
    private Stack<Sentence> previousSentences;

    void Start()
    {
        sentences = new Queue<Sentence>();
        previousSentences = new Stack<Sentence>();
        dialoguePanel.SetActive(false); // 대화창 비활성화
        nextButton.gameObject.SetActive(false); // 다음 대화 버튼 비활성화
        previousButton.gameObject.SetActive(false); // 이전 대화 버튼 비활성화
    }

    public void StartDialogue()
    {
        Debug.Log("StartDialogue called.");

        // 대화창과 버튼 상태 설정
        dialoguePanel.SetActive(true); // 대화창 활성화
        startButton.gameObject.SetActive(false); // 대화 시작 버튼 비활성화
        nameText.text = dialogue.characterName; // 캐릭터 이름 설정
        sentences.Clear(); // 큐 초기화
        previousSentences.Clear(); // 스택 초기화

        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // 모든 문장을 큐에 추가
        }

        // 버튼 활성화
        nextButton.gameObject.SetActive(true); // 다음 대화 버튼 활성화
        previousButton.gameObject.SetActive(true); // 이전 대화 버튼 활성화

        DisplayNextSentence(); // 첫 번째 문장을 자동으로 출력
    }

    public void DisplayNextSentence()
    {
        Debug.Log("DisplayNextSentence called.");

        // 모든 문장이 출력된 경우 대화 종료
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // 현재 문장을 스택에 저장
        if (dialogueText.text != "")
        {
            Sentence previousSentence = new Sentence { characterName = nameText.text, text = dialogueText.text, image = dialogueImage.sprite };
            previousSentences.Push(previousSentence);
        }

        // 큐에서 다음 문장 꺼내기
        Sentence sentence = sentences.Dequeue();

        // 기존의 타이핑 코루틴 중지하고 새로운 코루틴 시작
        StopAllCoroutines();
        StartCoroutine(DisplaySentence(sentence));
    }

    public void DisplayPreviousSentence()
    {
        Debug.Log("DisplayPreviousSentence called.");

        // 이전 문장이 없는 경우
        if (previousSentences.Count == 0)
        {
            Debug.Log("No previous sentences.");
            return;
        }

        // 현재 문장을 큐의 맨 앞으로 추가
        if (dialogueText.text != "")
        {
            Sentence currentSentence = new Sentence { characterName = nameText.text, text = dialogueText.text, image = dialogueImage.sprite };
            // 새로운 큐를 만들어 현재 문장을 앞에 추가하고 나머지 문장을 뒤에 추가
            Queue<Sentence> newQueue = new Queue<Sentence>();
            newQueue.Enqueue(currentSentence);
            while (sentences.Count > 0)
            {
                newQueue.Enqueue(sentences.Dequeue());
            }
            sentences = newQueue;
        }

        // 스택에서 이전 문장 꺼내기
        Sentence previousSentence = previousSentences.Pop();

        // 기존의 타이핑 코루틴 중지하고 새로운 코루틴 시작
        StopAllCoroutines();
        StartCoroutine(DisplaySentence(previousSentence));
    }

    IEnumerator DisplaySentence(Sentence sentence)
    {
        dialogueText.text = ""; // 텍스트 초기화
        nameText.text = sentence.characterName; // 이름 업데이트

        // 이미지 업데이트
        if (sentence.image != null)
        {
            dialogueImage.sprite = sentence.image;
            dialogueImage.gameObject.SetActive(true); // 이미지 표시
        }
        else
        {
            dialogueImage.gameObject.SetActive(false); // 이미지 숨기기
        }

        // 한 글자씩 추가
        foreach (char letter in sentence.text.ToCharArray())
        {
            dialogueText.text += letter;
            if (audioSource != null && typingSound != null)
            {
                audioSource.PlayOneShot(typingSound); // 음향 효과 재생
            }
            yield return new WaitForSeconds(0.06f); // 0.06초 대기
        }
    }

    void EndDialogue()
    {
        Debug.Log("EndDialogue called.");

        // 대화 종료 시 상태 설정
        dialoguePanel.SetActive(false); // 대화창 비활성화
        nextButton.gameObject.SetActive(false); // 다음 대화 버튼 비활성화
        previousButton.gameObject.SetActive(false); // 이전 대화 버튼 비활성화
        GameChacter.gameObject.SetActive(true); // 염율 캐릭터 활성화
        Debug.Log("End of conversation.");
    }
}
