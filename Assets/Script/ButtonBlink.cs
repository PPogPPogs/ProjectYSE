using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonBlink : MonoBehaviour
{
    public Button buttonToBlink; // 깜빡거릴 버튼
    public float blinkSpeed = 1.0f; // 깜빡거리는 속도 (초당 깜빡거림)

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
        while (true) // 무한 루프
        {
            // 투명도를 0으로 감소
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

            // 투명도를 1로 증가
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
