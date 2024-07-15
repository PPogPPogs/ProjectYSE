using UnityEngine;
using UnityEngine.UI;

public class Chapter3Button : MonoBehaviour
{
    // ������ ��ư�� �ν����Ϳ��� �Ҵ��� �� �ֵ��� public���� ����
    public Button myButton;

    // ���� ���� �� ��ư�� ��Ȱ��ȭ
    void Start()
    {
        if (myButton != null)
        {
            myButton.interactable = false;
        }
    }

    // �ٸ� �̺�Ʈ�� ���ǿ� ���� ��ư�� Ȱ��ȭ�ϴ� �Լ�
    public void EnableButton()
    {
        if (myButton != null)
        {
            myButton.interactable = true;
        }
    }

    // ��ư�� ��Ȱ��ȭ�ϴ� �Լ�
    public void DisableButton()
    {
        if (myButton != null)
        {
            myButton.interactable = false;
        }
    }
}
