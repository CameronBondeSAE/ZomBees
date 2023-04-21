using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Team_members.Lloyd.Civilian_L;
using UnityEngine;

public class CivVision : MonoBehaviour
{
    //chat GPT was here
    public Vector3 boxOffset;
    public Vector3 boxSize;

    public List<GameObject> visibleObjects = new List<GameObject>();

    public List<GameObject> beeObjects = new List<GameObject>();
    public bool seesBees;

    public List<GameObject> civObjects = new List<GameObject>();
    public bool seesCivs;

    public List<GameObject> interactables = new List<GameObject>();
    public bool seesInteract;

    public List<GameObject> resources = new List<GameObject>();
    public bool seesResource;
 
    private Collider[] colliders = new Collider[100];

    private float distanceBetween;

    private void OnEnable()
    {
        StartCoroutine(VisionCoroutine());
    }

    private IEnumerator VisionCoroutine()
    {
        while (true)
        {
            visibleObjects.Clear();
            beeObjects.Clear();
            interactables.Clear();
            resources.Clear();
            civObjects.Clear();
            
            Vector3 boxCenter = transform.TransformPoint(boxOffset);
            int numColliders = Physics.OverlapBoxNonAlloc(boxCenter, boxSize * 0.5f, colliders, transform.rotation);

            for (int i = 0; i < numColliders; i++)
            {
                Collider collider = colliders[i];
                
                    Vector3 directionToCollider = (collider.transform.position - transform.position).normalized;
                    
                    RaycastHit hit;
                    
                    //boxSize.z needs fixing!!! URGENT!!
                    //
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
            
            CheckList();
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void Update()
    {
        seesBees = beeObjects.Any();
        seesCivs = civObjects.Any();
        seesResource = resources.Any();

        if (interactables.Any())
        {
            seesInteract = true;
        }
    }

    private void CheckList()
    {
        foreach (GameObject obj in visibleObjects)
        {
            if (obj.GetComponent<IInteractable>() != null)
            {
                interactables.Add(obj);
            }

            if (obj.GetComponent<ICiv>() != null)
            {
                civObjects.Add(obj);
            }
        }
    }

    public Transform ReturnNearestCiv()
    {
        if (civObjects.Any())
        {
            List<(float, Transform)> distanceAndTransformList = new List<(float, Transform)>();
            foreach (GameObject civ in civObjects)
            {
                float distance = Vector3.Distance(transform.position, civ.transform.position);
                distanceAndTransformList.Add((distance, civ.transform));
            }

            distanceAndTransformList.Sort((a, b) => a.Item1.CompareTo(b.Item1));

            return distanceAndTransformList[0].Item2;
        }
        return null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
}