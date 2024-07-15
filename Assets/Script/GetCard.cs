using UnityEngine;
using UnityEngine.UI;

public class GetCard : MonoBehaviour
{
    public GameObject[] cardPrefabs; // 4가지의 프리팹 배열
    public Transform spawnPoint; // 프리팹이 생성될 위치
    public float[] probabilities; // 각 프리팹의 생성 확률 (합이 1이어야 함)
    public Button specialButton; // 조건을 만족할 때 활성화할 버튼

    void Start()
    {
        if (probabilities.Length != cardPrefabs.Length)
        {
            Debug.LogError("The length of probabilities array must match the length of cardPrefabs array.");
        }

        // 확률 합계가 1이 되는지 확인
        float totalProbability = 0;
        foreach (float prob in probabilities)
        {
            totalProbability += prob;
        }

        if (Mathf.Abs(totalProbability - 1f) > 0.01f)
        {
            Debug.LogError("The total of probabilities must be equal to 1.");
        }

        // 버튼을 비활성화 상태로 시작
        if (specialButton != null)
        {
            specialButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Special Button is not assigned.");
        }
    }

    // 카드팩에서 호출하는 메서드
    public void SpawnRandomCard()
    {
        if (cardPrefabs.Length == 0)
        {
            Debug.LogError("Card prefabs array is empty.");
            return;
        }

        float randomValue = Random.value;
        float cumulativeProbability = 0f;

        for (int i = 0; i < cardPrefabs.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue < cumulativeProbability)
            {
                Instantiate(cardPrefabs[i], spawnPoint.position, spawnPoint.rotation);

                // 조건을 만족할 때 버튼을 활성화
                ActivateSpecialButton();

                break; // 프리팹을 하나 생성한 후 루프를 빠져나옴
            }
        }
    }

    private void ActivateSpecialButton()
    {
        if (specialButton != null)
        {
            specialButton.gameObject.SetActive(true);
        }
    }
}
