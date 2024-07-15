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

    public AudioClip moveSound;  // 이동 사운드를 설정할 수 있는 AudioClip 변수
    private AudioSource audioSource;  // AudioSource 컴포넌트를 참조할 변수

    private Vector3 targetPosition;
    private Vector3 targetScale;
    private bool shouldMove = false;

    private string targetScene;  // 이동할 씬의 이름을 저장할 변수

    public SceneFader sceneFader; // SceneFader 컴포넌트를 참조할 변수

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = moveSound;
        audioSource.loop = true;  // 이동 중에는 반복 재생
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
                    audioSource.Play();  // 이동 사운드 재생
                }
            }
            else
            {
                shouldMove = false; // 목표 위치와 크기에 도달하면 이동 중지
                audioSource.Stop();  // 사운드 중지
                sceneFader.sceneToLoad = targetScene;  // 목표 씬 설정
                sceneFader.FadeToScene();  // 페이드 아웃 후 목표 씬으로 이동
            }
        }
    }

    public void MoveToPosition1()
    {
        targetPosition = position1;
        targetScale = scale1;
        targetScene = "Chapter1Scene";  // 이동할 씬 이름 설정
        shouldMove = true;
        Debug.Log("1번 위치로 이동");
    }

    public void MoveToPosition2()
    {
        targetPosition = position2;
        targetScale = scale2;
        targetScene = "Chapter2Scene";  // 이동할 씬 이름 설정
        shouldMove = true;
        Debug.Log("2번 위치로 이동");
    }

    public void MoveToPosition3()
    {
        targetPosition = position3;
        targetScale = scale3;
        targetScene = "Chapter3Scene";  // 이동할 씬 이름 설정
        shouldMove = true;
        Debug.Log("3번 위치로 이동");
    }
}
