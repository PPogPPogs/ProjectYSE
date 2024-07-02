using UnityEngine;

public class MoveAndScaleObject : MonoBehaviour
{
    public Vector3 position1 = new Vector3(1, 2, 0);
    public Vector3 position2 = new Vector3(2, 3, 0);
    public Vector3 position3 = new Vector3(3, 4, 0);

    public Vector3 scale1 = new Vector3(1, 1, 1);
    public Vector3 scale2 = new Vector3(2, 2, 2);
    public Vector3 scale3 = new Vector3(3, 3, 3);

    public float speed = 1.0f;
    public float scaleSpeed = 1.0f;

    private Vector3 targetPosition;
    private Vector3 targetScale;

    void Start()
    {
        transform.position = position1;
        transform.localScale = scale1;
        targetPosition = position1;
        targetScale = scale1;
    }

    void Update()
    {
        if (transform.position != targetPosition || transform.localScale != targetScale)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
        }
    }

    public void MoveToPosition1()
    {
        targetPosition = position1;
        targetScale = scale1;
        Debug.Log("1번위치");
    }

    public void MoveToPosition2()
    {
        targetPosition = position2;
        targetScale = scale2;
        Debug.Log("2번위치");
    }

    public void MoveToPosition3()
    {
        targetPosition = position3;
        targetScale = scale3;
        Debug.Log("3번위치");
    }
}
