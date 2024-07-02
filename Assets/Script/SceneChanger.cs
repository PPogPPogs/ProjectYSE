using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // 이동할 씬의 이름을 입력합니다.
    public string PlayScene;

    public void ChangeScene()
    {
        // 씬을 변경합니다.
        SceneManager.LoadScene(PlayScene);
    }
}
