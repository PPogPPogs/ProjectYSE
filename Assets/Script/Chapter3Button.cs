using UnityEngine;
using UnityEngine.UI;

public class Chapter3Button : MonoBehaviour
{
    // 제어할 버튼을 인스펙터에서 할당할 수 있도록 public으로 선언
    public Button myButton;

    // 게임 시작 시 버튼을 비활성화
    void Start()
    {
        if (myButton != null)
        {
            myButton.interactable = false;
        }
    }

    // 다른 이벤트나 조건에 따라 버튼을 활성화하는 함수
    public void EnableButton()
    {
        if (myButton != null)
        {
            myButton.interactable = true;
        }
    }

    // 버튼을 비활성화하는 함수
    public void DisableButton()
    {
        if (myButton != null)
        {
            myButton.interactable = false;
        }
    }
}
