using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // �̵��� ���� �̸��� �Է��մϴ�.
    public string PlayScene;

    public void ChangeScene()
    {
        // ���� �����մϴ�.
        SceneManager.LoadScene(PlayScene);
    }
}
