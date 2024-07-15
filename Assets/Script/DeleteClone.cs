using UnityEngine;

public class DeleteClones : MonoBehaviour
{
    

    public void DeleteCloneObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Clone"))
            {
                Destroy(obj);
            }
        }
    }
}
