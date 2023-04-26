using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class CivSensor : MonoBehaviour, ISense
    {
        public AntAIAgent antAIAgent;

        public bool hungry;

        public bool hasFood;

        public bool safePlace;

        public bool hasTarget;

        public bool wantsToAttack;

        public bool wantsToInteract;

        public bool wantsToTalk;

        public bool wantsToPickUpItem;

        public bool wantsToFollow;

        public LesserQueenLookAt look;

        private void Awake()
        {
            StartBeeParts();
        }

        public void ChangeRotateTarget(Transform newTarget)
        {
            look.target = newTarget;
        }

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.BeginUpdate(aAgent.planner);
            {
                aWorldState.Set(NormalCivScenario.Hungry, hungry);
                aWorldState.Set(NormalCivScenario.HasFood, hasFood);

                aWorldState.Set(NormalCivScenario.SafePlace, safePlace);

                aWorldState.Set(NormalCivScenario.HasTarget, hasTarget);

                aWorldState.Set(NormalCivScenario.WantsToInteract, wantsToInteract);

                aWorldState.Set(NormalCivScenario.WantsToAttack, wantsToAttack);    
                
                aWorldState.Set(NormalCivScenario.WantsToTalk, wantsToTalk);

                aWorldState.Set(NormalCivScenario.WantsToPickUpItem, wantsToPickUpItem);
                
                aWorldState.Set(NormalCivScenario.WantsToFollow, wantsToFollow);
                
            }
            aWorldState.EndUpdate();
        }

        public void BecomeEgg()
        {
            EggManager.instance.StartEgg(gameObject);
        }

        #region Head

        private BeePartsManager beeparts;

        private void StartBeeParts()
        {
            beeparts = GetComponentInChildren<BeePartsManager>();

            beeparts.HumanEyes();
            beeparts.LoseAntannae();
            beeparts.LoseMandibles();
        }

        #endregion
    }
}