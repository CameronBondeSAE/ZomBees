using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Oscar;

namespace Lloyd
{

    public class CivVision : MonoBehaviour
    {
        //chat GPT was here

        //has to "forget" seen things after a set time

        public float timeBetweenVisionChecks;

        public Vector3 boxOffset;
        public Vector3 boxSize;

        [Header("Obj disappears from sight memory after x time")]
        public float sightMemoryTime;

        public List<GameObject> visibleObjects = new List<GameObject>();

        public List<GameObject> beeObjects = new List<GameObject>();
        public bool seesBees;

        public List<GameObject> civObjects = new List<GameObject>();
        public bool seesCivs;

        public List<GameObject> switches = new List<GameObject>();
        public bool seesSwitch;

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
                /*visibleObjects.Clear();
                beeObjects.Clear();
                interactables.Clear();
                resources.Clear();
                civObjects.Clear();*/

                //layers to ignore
                int ignoreLayer = LayerMask.NameToLayer("Ground");
                LayerMask layerMask = ~(1 << ignoreLayer);

                Vector3 boxCenter = transform.TransformPoint(boxOffset);
                int numColliders =
                    Physics.OverlapBoxNonAlloc(boxCenter, boxSize * 0.5f, colliders, transform.rotation, layerMask);

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
                                StartCoroutine(RemoveGameObject(visibleObjects, collider.gameObject));

                                visibleObjects.Sort((a, b) =>
                                {
                                    float distanceToA = (a.transform.position - hit.point).sqrMagnitude;
                                    float distanceToB = (b.transform.position - hit.point).sqrMagnitude;
                                    return distanceToB.CompareTo(distanceToA);
                                });
                            }
                        }
                    }
                }
                
                CheckList();
                yield return new WaitForSeconds(timeBetweenVisionChecks);
            }
        }

        public void Update()
        {
            seesBees = beeObjects.Any();
            if (seesCivs = civObjects.Any())
            {
                seesCivs = true;
            }
            else seesCivs = false;

            seesResource = resources.Any();
            if (interactables.Any())
            {
                seesInteract = true;
            }
            else seesInteract = false;
        }

        //le copy paste
        private void CheckList()
        {
            foreach (GameObject obj in visibleObjects)
            {
                if (obj.GetComponent<ISwitchable>() != null)
                {
                    if (!switches.Contains(obj))
                        switches.Add(obj);
                    StartCoroutine(RemoveGameObject(switches, obj));
                }

                if (obj.GetComponent<ICivilian>() != null)
                {
                    if (!civObjects.Contains(obj))
                        civObjects.Add(obj);
                    StartCoroutine(RemoveGameObject(civObjects, obj));
                }

                if (obj.GetComponent<DynamicObject>() != null)
                {
                    DynamicObject newObj = obj.GetComponent<DynamicObject>();
                    if (newObj.isBee)
                    {
                        if (!beeObjects.Contains(obj))
                            beeObjects.Add(obj);
                        StartCoroutine(RemoveGameObject(beeObjects, obj));
                    }

                    if (newObj.isCiv)
                    {
                        if (!civObjects.Contains(obj))
                            civObjects.Add(obj);
                        StartCoroutine(RemoveGameObject(civObjects, obj));
                    }
                }
            }
        }

        private IEnumerator RemoveGameObject(List<GameObject> list, GameObject gameObject)
        {
            yield return new WaitForSeconds(sightMemoryTime);

            if (list.Contains(gameObject))
            {
                list.Remove(gameObject);
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
            visibleObjects.Clear();
            beeObjects.Clear();
            interactables.Clear();
            resources.Clear();
            civObjects.Clear();
            seesCivs = false;
            StopAllCoroutines();
        }
    }
}