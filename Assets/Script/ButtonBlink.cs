using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonBlink : MonoBehaviour
{
    public Button buttonToBlink; // �����Ÿ� ��ư
    public float blinkSpeed = 1.0f; // �����Ÿ��� �ӵ� (�ʴ� �����Ÿ�)

    private Image buttonImage;

    void Start()
    {
        if (buttonToBlink != null)
        {
            buttonImage = buttonToBlink.GetComponent<Image>();
            if (buttonImage != null)
            {
                StartCoroutine(Blink());
            }
        }
    }

    private IEnumerator Blink()
    {
        while (true) // ���� ����
        {
            // ������ 0���� ����
            for (float alpha = 1.0f; alpha >= 0.0f; alpha -= Time.deltaTime * blinkSpeed)
            {
                if (buttonImage != null)
                {
                    Color color = buttonImage.color;
                    color.a = alpha;
                    buttonImage.color = color;
                }
                yield return null;
            }

            // ������ 1�� ����
            for (float alpha = 0.0f; alpha <= 1.0f; alpha += Time.deltaTime * blinkSpeed)
            {
                if (buttonImage != null)
                {
                    Color color = buttonImage.color;
                    color.a = alpha;
                    buttonImage.color = color;
                }
                yield return null;
            }
        }
    }
}
