using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public SceneFader1 sceneFader1;

    public void LoadScene1()
    {
        sceneFader1.FadeToScene("PlayScene");
    }
}