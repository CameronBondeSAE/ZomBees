using Anthill.AI;
using Sirenix.OdinInspector;
using Team_members.Lloyd.Scripts_L.HearingComponent;
using UnityEngine;

namespace Team_members.Lloyd.Civilian_L
{
    public class CivilianBrain : AntAIAgent, ISense
    {
        public Team myTeam = Team.Human;

        #region Vision

        private CivVision civVision;

        public bool seesBee;

        public bool seesCiv;

        public bool seesInteract;

        public bool seesPickup;

        public bool seesResource;

        private void StartVision()
        {
            civVision = GetComponent<CivVision>();

           // civVision.LastSeenInteractableObj += SetLastSeenInteractableObject;
        }

        #endregion

        #region Interact

        public bool wantsToInteract;

        #endregion

        public FaceManager faceManager;

        public GameObject rightHand;

        private StatsComp stats;

        public bool dangerNearby;

        public bool wantToPickup;

        public float pickupRadius;

        public bool idle;

        public bool inRange;

        public bool talking;

        public bool moving;

        public bool looking;

        public bool hasResource;

        private Hearing _hearing;

        public bool hearingSomething;

        public bool listening;


        public Transform target;

        private void OnEnable()
        {
            StartVision();
            _hearing = GetComponent<Hearing>();
            stats = GetComponent<StatsComp>();

            pickupRadius = stats.pickupRadius;
        }

        public void Update()
        {
            seesBee = civVision.seesBees;

            seesCiv = civVision.seesCivs;

            seesPickup = civVision.seesResource;

            seesInteract = civVision.seesInteract;

            DecideMoveTarget();
        }

        public GameObject moveTarget;

        public GameObject interactTarget;

        private void SetLastSeenInteractableObject(GameObject lastSeenObj)
        {
            interactTarget = lastSeenObj;
        }

        private void DecideMoveTarget()
        {
            if (wantsToInteract)
            {
                if (interactTarget != null)
                {
                    moveTarget = interactTarget;
                    target = interactTarget.transform;
                }
            }
        }

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);

            aWorldState.Set("Idle", idle);

            aWorldState.Set("Talking", talking);

            aWorldState.Set("Moving", moving);

            aWorldState.Set("Looking", looking);

            aWorldState.Set("HasResource", hasResource);

            aWorldState.Set("InRange", inRange);

            aWorldState.Set("NeedToInteract", wantsToInteract);

            aWorldState.Set("DangerNearby", dangerNearby);

            aWorldState.EndUpdate();
        }
    }
}