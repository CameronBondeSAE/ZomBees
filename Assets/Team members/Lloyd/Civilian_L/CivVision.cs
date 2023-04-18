using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivVision : MonoBehaviour
{
    //chat GPT was here
    public Vector3 boxOffset;
    public Vector3 boxSize;

    public List<GameObject> visibleObjects = new List<GameObject>();

    private Collider[] colliders = new Collider[100];
    
    private void OnEnable()
    {
        StartCoroutine(VisionCoroutine());
    }

    private IEnumerator VisionCoroutine()
    {
        while (true)
        {
            visibleObjects.Clear();
            
            Vector3 boxCenter = transform.position + boxOffset;
            int numColliders = Physics.OverlapBoxNonAlloc(boxCenter, boxSize * 0.5f, colliders);

            for (int i = 0; i < numColliders; i++)
            {
                Collider collider = colliders[i];
                
                    Vector3 directionToCollider = (collider.transform.position - transform.position).normalized;
                    
                    RaycastHit hit;
                        if (Physics.Raycast(transform.position, directionToCollider, out hit, boxSize.z))
                        {
                            if (hit.collider == collider)
                            {
                                if (!visibleObjects.Contains(collider.gameObject))
                                {
                                    visibleObjects.Add(collider.gameObject);
                                }
                            }
                        }
            }

            yield return new WaitForSeconds(0.6f);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}