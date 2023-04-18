using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NormalCivPathFinder : MonoBehaviour
{
    public List<GameObject> safePoints;
    public Transform nearestSafePoint;

    public List<GameObject> dangerousPoints;

    public List<GameObject> resourcePoints;

    public List<GameObject> interactPoints;

    public List<GameObject> civPoints;

    [Button]
    public void OnCalculatePoints(string key)
    {
        List<GameObject> targetList;
        if (key == "safePoints")
        {
            targetList = safePoints;
        }

        CalculateDist(safePoints);
    }

    public void CalculateDist(List<GameObject> listToSort)
    {
        float minDistance = float.MaxValue;

        foreach (GameObject obj in listToSort)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestSafePoint = obj.transform;
            }
        }
    }
}
