using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Virginia;

namespace Oscar
{
    public class DeliverFood : OscarsLittleGuyMovement
    {
        private Inventory inventory;
        
        private bool finishDelivering;
        private bool runnningAwayFromDelivery;
    
        private ChildCivController childControl;
        
        private void OnEnable()
        {
            objectArrivedEvent += LocationArrivedAt;
        }
    
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            inventory = aGameObject.GetComponentInParent<Inventory>();
            
            childControl = aGameObject.GetComponent<ChildCivController>();
        }
    
        public override void Enter()
        {
            base.Enter();
            finishDelivering = false;
            NavmeshEnabled();
            Vector3 position = PatrolManager.singleton
                .indoors[Random.Range(0, PatrolManager.singleton.pathsWithIndoors.Count)].transform.position;
            NavmeshFindLocation(position);
        }
    
        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            base.Execute(aDeltaTime, aTimeScale);
    
            if (finishDelivering == false)
            {
                NavmeshToLocation();
            }
        }
    
        private void LocationArrivedAt()
        {
            inventory.Dispose();
            StartCoroutine(RunAway());
        }
    
        IEnumerator RunAway()
        {
            Vector3 position = PatrolManager.singleton
                .paths[Random.Range(0, PatrolManager.singleton.paths.Count)].transform.position;
            NavmeshFindLocation(position);
            
            yield return new WaitForSeconds(5f);
            finishDelivering = true;
            
            Finish();
        }
    
        public override void Exit()
        {
            base.Exit();

            childControl.ImHungry = false;
            childControl.DoIHaveFood = false;
            
            NavMeshFinish();
            Finish();
        }
    }
}