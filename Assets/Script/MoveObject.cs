using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveObject : MonoBehaviour
{
    public Vector3 position1 = new Vector3(1, 2, 0);
    public Vector3 position2 = new Vector3(2, 3, 0);
    public Vector3 position3 = new Vector3(3, 4, 0);

    public Vector3 scale1 = new Vector3(1, 1, 1);
    public Vector3 scale2 = new Vector3(2, 2, 2);
    public Vector3 scale3 = new Vector3(3, 3, 3);

    public float speed = 1.0f;
    public float scaleSpeed = 1.0f;

    public AudioClip moveSound;  // �̵� ���带 ������ �� �ִ� AudioClip ����
    private AudioSource audioSource;  // AudioSource ������Ʈ�� ������ ����

    private Vector3 targetPosition;
    private Vector3 targetScale;
    private bool shouldMove = false;

    private string targetScene;  // �̵��� ���� �̸��� ������ ����

    public SceneFader sceneFader; // SceneFader ������Ʈ�� ������ ����

    void Start()
    {
        // AudioSource ������Ʈ ��������
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = moveSound;
        audioSource.loop = true;  // �̵� �߿��� �ݺ� ���
    }

    void Update()
    {
        if (shouldMove)
        {
            if (transform.position != targetPosition || transform.localScale != targetScale)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();  // �̵� ���� ���
                }
            }
            else
            {
                shouldMove = false; // ��ǥ ��ġ�� ũ�⿡ �����ϸ� �̵� ����
                audioSource.Stop();  // ���� ����
                sceneFader.sceneToLoad = targetScene;  // ��ǥ �� ����
                sceneFader.FadeToScene();  // ���̵� �ƿ� �� ��ǥ ������ �̵�
            }
        }
    }

    public void MoveToPosition1()
    {
        targetPosition = position1;
        targetScale = scale1;
        targetScene = "Chapter1Scene";  // �̵��� �� �̸� ����
        shouldMove = true;
        Debug.Log("1�� ��ġ�� �̵�");
    }

    public void MoveToPosition2()
    {
        targetPosition = position2;
        targetScale = scale2;
        targetScene = "Chapter2Scene";  // �̵��� �� �̸� ����
        shouldMove = true;
        Debug.Log("2�� ��ġ�� �̵�");
    }

    public void MoveToPosition3()
    {
        targetPosition = position3;
        targetScale = scale3;
        targetScene = "Chapter3Scene";  // �̵��� �� �̸� ����
        shouldMove = true;
        Debug.Log("3�� ��ġ�� �̵�");
    }
}
