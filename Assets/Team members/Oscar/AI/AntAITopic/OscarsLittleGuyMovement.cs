using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using DG.Tweening;
using Lloyd;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Oscar
{
    public class OscarsLittleGuyMovement : AntAIState
    {
        #region Variables and Instantiate

        //basic ones
        public LittleGuy littleGuy;
        private Rigidbody rb;
        private float speed;
        private float turnSpeed;
        public Hearing ears;
        
        //perlin
        
        private float zoomX;
        private float zoomZ;
        private float perlin;
        
        //navmesh stuff
        private NavMeshAgent navMeshAgent;
        private float arrivedDistance = 1.5f;
        private Transform target;
        private NavMeshPath path;
        private Vector3 finalDestination;
        private float stoppingDistance = 2f;
        private float elapsed;
        public float distanceFromPoint;
        
        //events
        public delegate void OnArrivedEvent();
        public event OnArrivedEvent objectArrivedEvent;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            
            //for basic stuff
            littleGuy = aGameObject.GetComponentInParent<LittleGuy>();
            rb = littleGuy.GetComponent<LittleGuy>().rb;
            speed = littleGuy.GetComponent<LittleGuy>().speed;
            turnSpeed = littleGuy.GetComponent<LittleGuy>().turnSpeed;

            ears = aGameObject.GetComponent<Hearing>();
            
                //for perlin
            zoomX = Random.Range(-0.5f, 0.5f);
            zoomZ = Random.Range(-0.5f, 0.5f);
            
            //for navmesh
            navMeshAgent = aGameObject.GetComponentInParent<NavMeshAgent>();
            elapsed = 0f;
            path = new NavMeshPath();
            
        }

        #endregion
        
        #region Movement

        public void BasicMovement(float speedIncreaseMultiplyer)
        {
            float decidedSpeed = speed * speedIncreaseMultiplyer;
            rb.AddRelativeForce(Vector3.forward * decidedSpeed,ForceMode.Acceleration);
        }

        public void BasicStopping()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void FlightMode()
        {
            //TODO ask cam about do tween not setting the values but instead returning
            DOTween.To(DoTwenSetter, littleGuy.transform.position.y,littleGuy.transform.position.y + 10f, 5f);
        }

        public void LandMode()
        {
            //TODO ask cam about do tween not setting the values but instead returning
            
            //DOTween.To(DoTwenSetter, littleGuy.transform.position.y,littleGuy.transform.position.y - 10f, 5f);
        }

        private void DoTwenSetter(float newValue)
        {
            littleGuy.transform.position = new Vector3(littleGuy.transform.position.x, newValue,littleGuy.transform.position.z);
        }
        
        #endregion

        #region Stearing

        public void Turning(float turnDirectionForce)
        {
            rb.AddRelativeTorque(Vector3.up * turnDirectionForce);
        }
        
        public void Wondering()
        {
            float x = zoomX + Time.time;
            float z = zoomZ + Time.time;

            perlin = Mathf.PerlinNoise(x, z) * 2 - 1;
            
            rb.AddRelativeTorque(0,perlin * turnSpeed,0);
        }

        public void TurnTowards(Vector3 targetPos)
        {
            rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, 
                targetPos - transform.position, Vector3.up) * turnSpeed,0);
        }

        public void TurnAway(Vector3 targetPos)
        {
            Vector3 directionToRun = transform.position - targetPos;

            rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward,
                directionToRun, Vector3.up),0);
        }

        public void Spin()
        {
            rb.AddRelativeTorque(0,Vector3.SignedAngle(transform.forward, 
                new Vector3(0, Time.deltaTime, 0), Vector3.up) * turnSpeed,0);
        }

        #endregion

        #region NavmeshStuff

        public void NavmeshEnabled()
        {
            path = new NavMeshPath();
            elapsed = 0.0f;
        }
        
        public void NavmeshFindLocation(Vector3 targetLoc)
        {
            finalDestination = targetLoc;

            navMeshAgent.SetDestination(finalDestination);

            NavMesh.CalculatePath(transform.position, finalDestination, NavMesh.AllAreas, path);
            NavmeshToLocation();
        }
        public void NavmeshToLocation()
        {
            distanceFromPoint = Vector3.Distance(littleGuy.transform.position, finalDestination);
            if (distanceFromPoint <= stoppingDistance)
            {
                objectArrivedEvent?.Invoke();
                Finish();
            }
            
            elapsed += Time.deltaTime;
            if (elapsed > 1.0f)
            {
                elapsed -= 1.0f;
                NavMesh.CalculatePath(littleGuy.transform.position, finalDestination, NavMesh.AllAreas, path);
            }
            
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                Debug.DrawLine(path.corners[i],path.corners[i + 1],Color.red, 1f);
            }
        }
        public void NavMeshFinish()
        {
            navMeshAgent.enabled = false;
            navMeshAgent.enabled = true;
            Finish();
        }
        
        #endregion
    }
}