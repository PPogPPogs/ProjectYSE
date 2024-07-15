using UnityEngine;

public class IrisEffect : MonoBehaviour
{
    public Material irisMaterial;
    public float irisSpeed = 0.5f;
    private float cutoff = 1.0f;
    private bool isOpening = false;

    void Update()
    {
        // Ű���� �Է����� ���̸��� ȿ���� ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleIris();
        }

        // ���̸��� ȿ�� �ִϸ��̼�
        if (isOpening)
        {
            cutoff -= Time.deltaTime * irisSpeed;
            if (cutoff <= 0)
            {
                cutoff = 0;
                isOpening = false;
            }
        }
        else
        {
            cutoff += Time.deltaTime * irisSpeed;
            if (cutoff >= 1)
            {
                cutoff = 1;
                isOpening = true;
            }
        }

        irisMaterial.SetFloat("_Cutoff", cutoff);
    }

    public void ToggleIris()
    {
        isOpening = !isOpening;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, irisMaterial);
    }
}
