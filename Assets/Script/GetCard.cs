using UnityEngine;
using UnityEngine.UI;

public class GetCard : MonoBehaviour
{
    public GameObject[] cardPrefabs; // 4������ ������ �迭
    public Transform spawnPoint; // �������� ������ ��ġ
    public float[] probabilities; // �� �������� ���� Ȯ�� (���� 1�̾�� ��)
    public Button specialButton; // ������ ������ �� Ȱ��ȭ�� ��ư

    void Start()
    {
        if (probabilities.Length != cardPrefabs.Length)
        {
            Debug.LogError("The length of probabilities array must match the length of cardPrefabs array.");
        }

        // Ȯ�� �հ谡 1�� �Ǵ��� Ȯ��
        float totalProbability = 0;
        foreach (float prob in probabilities)
        {
            totalProbability += prob;
        }

        if (Mathf.Abs(totalProbability - 1f) > 0.01f)
        {
            Debug.LogError("The total of probabilities must be equal to 1.");
        }

        // ��ư�� ��Ȱ��ȭ ���·� ����
        if (specialButton != null)
        {
            specialButton.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Special Button is not assigned.");
        }
    }

    // ī���ѿ��� ȣ���ϴ� �޼���
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

                // ������ ������ �� ��ư�� Ȱ��ȭ
                ActivateSpecialButton();

                break; // �������� �ϳ� ������ �� ������ ��������
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
