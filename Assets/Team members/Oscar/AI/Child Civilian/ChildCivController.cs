using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Virginia;
using Lloyd;
using Marcus;

namespace Oscar
{
    public class ChildCivController : MonoBehaviour
    {
        
        #region Variables
        
        //managers if needed
        private ZombeeGameManager zombeeGameManager;
        public MemoryManager memoryManager;
        
        //general variables
        public LittleGuy littleGuy;
        public OscarVision vision;
        public CivGPT civGPT;
        public CivilianTraits civTraits;
        public TraitScriptableObject fear;
        public TraitScriptableObject hunger;
        public Health health;
        
        public Inventory inventory;
        
        //bools for AI conditions
        private bool iAmIdle = false;
        private bool iAmAlive = true;
        private bool iAmFollowing = false;
        private bool iAmConversing = false;
        private bool iAmScared = false;
        private bool iAmHungry = false;
        private bool iAmHiding = false;
        private bool seeObjects = false;
        private bool hasObjects = false;
        private bool deliverObjects = false;
        private bool getStuff = false;
        private bool hasStuff = false;
        private bool iDeliveredStuff = false;
        private bool seeFood = false;
        private bool hasFood = false;
        private bool doAsTold = false;

        public Vector3 goToPos;
        
        
                
        #endregion

        #region GPTStuff
        private void Awake()
        {
            civGPT.GPTPerformingActionEvent += GPTPerformingAction;    
            civGPT.GPTOutputDialogueEvent += CivGptOnGPTOutputDialogueEvent;
            zombeeGameManager = ZombeeGameManager.Instance;
        }
        
        private void CivGptOnGPTOutputDialogueEvent(object sender, string e)
        {
            Debug.Log("Look at character talking to me : "+civGPT.currentChat.CharacterBase.gameObject.name, gameObject);
            
            littleGuy.transform.DOLookAt(civGPT.currentChat.CharacterBase.transform.position, 1f, AxisConstraint.Y, Vector3.up);
            iAmIdle = true;
        }
        
        private void GPTPerformingAction(object sender, CivGPT.GPTResponseData gptResponseData)
        {
            switch (gptResponseData.CivAction)
            {
                case CivGPT.CivAction.ActivateLightsToAttractCreatures:
                    break;
                
                case CivGPT.CivAction.ActivateSonicWeapon:
                    break;
                
                case CivGPT.CivAction.CloseDoor:
                    break;
                
                case CivGPT.CivAction.CommitSuicide:
                    break;
                
                case CivGPT.CivAction.DoNothing:
                    MustCompleteInstructions = false;
                    AmIIdle = true;
                    AmIScared = false;
                    AmIFollowing = false;
                    ImHungry = false;
                    break;
                
                case CivGPT.CivAction.DropItem:
                    break;
                
                case CivGPT.CivAction.FindAndShootCreature:
                    break;
                
                case CivGPT.CivAction.FindSafeLocation:
                    break;
                
                case CivGPT.CivAction.FollowOtherCharacter:
                    MustCompleteInstructions = false;
                    ImHungry = false;
                    AmIIdle = false;
                    AmIFollowing = true;
                    ImHungry = false;
                    break;
                
                case CivGPT.CivAction.GoToLocation:
                    goToPos = zombeeGameManager.ConvertGridSpaceToWorldSpace(gptResponseData.GridCoordinateForAction);
                    
                    MustCompleteInstructions = true;
                    ImHungry = false;
                    AmIIdle = false;
                    AmIFollowing = true;
                    ImHungry = false;
                    break;
                
                case CivGPT.CivAction.FindFood:
                    // if (memoryManager.FindMemoryOfType<DynamicObject>().theThing.isFood)
                    // {
                    //     goToPos = zombeeGameManager.ConvertGridSpaceToWorldSpace(gptResponseData.GridCoordinateForAction);
                    // }
                    ImHungry = true;
                    AmIIdle = false;
                    AmIScared = false;
                    AmIFollowing = false;
                    MustCompleteInstructions = false;
                    break;
                
                case CivGPT.CivAction.FindBomb:
                    break;
                
                case CivGPT.CivAction.RunAndHide:
                    AmIScared = true;
                    MustCompleteInstructions = false;
                    ImHungry = false;
                    AmIIdle = false;
                    AmIFollowing = false;
                    break;
                
                case CivGPT.CivAction.ShootOtherCharacter:
                    break;
            }
        }
        #endregion

        #region GettersAndSetters
        public bool AmIIdle
        {
            get { return iAmIdle; }
            set { iAmIdle = value; }
        }
        public bool AmIAlive
        {
            get { return iAmAlive; }
            set { iAmAlive = value; }
        }

        public bool AmIFollowing
        {
            get { return iAmFollowing; }
            set { iAmFollowing = value; }
        }
        public bool AmIConversing
        {
            get { return iAmConversing; }
            set { iAmConversing = value; }
        }
        public bool AmIScared
        {
            get { return iAmScared; }
            set { iAmScared = value; }
        }
        public bool ImHungry
        {
            get { return iAmHungry; }
            set { iAmHungry = value; }
        }
        public bool ShouldIHide
        {
            get { return iAmHiding; }
            set { iAmHiding = value; }
        }
        public bool CanISeeObjects
        {
            get { return seeObjects; }
            set { seeObjects = value; }
        }
        public bool DoIHaveObjects
        {
            get { return hasObjects; }
            set { hasObjects = value; }
        }
        public bool DeliverTheObjects
        {
            get { return deliverObjects; }
            set { deliverObjects = value; }
        }
        public bool GetTheStuff
        {
            get { return getStuff; }
            set { getStuff = value; }
        }
        public bool DoIHaveStuff
        {
            get { return hasStuff; }
            set { hasStuff = value; }
        }
        public bool StuffDelivered
        {
            get { return iDeliveredStuff; }
            set { iDeliveredStuff = value; }
        }
        public bool ISeeFood
        {
            get { return seeFood; }
            set { seeFood = value; }
        }
        public bool DoIHaveFood
        {
            get { return hasFood; }
            set { hasFood = value; }
        }
        public bool MustCompleteInstructions
        {
            get { return doAsTold; }
            set { doAsTold = value; }
        }
        #endregion

        #region UpdateFunction
        private void FixedUpdate()
        {
            //IDLE |
            
            
            //ALIVE |
            
            
            //FOLLOWING |
            
            // if (vision.civsInSight.Count >= 1 && iAmScared == true)
            // {
            //     AmIFollowing = true;
            // }
            // else
            // {
            //     AmIFollowing = false;
            // }
            
            //Conversing |
            
            
            //Scared |
            if (civTraits.GetTrait(fear).thresholdHit)
            {
                AmIScared = true;
            }
                        
            //Hungry |

            //Hide |
            
            
            //SeeRocks |
            if (MustCompleteInstructions == false)
            {
                if (vision.objectsInSight.Count > 0)
                {
                    CanISeeObjects = true;
                }
                else
                {
                    CanISeeObjects = false;
                }
            }
            
            //HaveRocks

            //DeliverRocks |

            //GetStuff |
            
            
            //HaveStuff |
            
            // if (inventory.heldItem != null)
            // {
            //     if (inventory.heldItem as MonoBehaviour == gameObject.GetComponent<DynamicObject>().isObject)
            //     {
            //         DoIHaveStuff = true;
            //     }
            //     else
            //     {
            //         DoIHaveStuff = false;
            //     }
            // }
            // else
            // {
            //     DoIHaveStuff = false;
            // }

            //DeliverStuff |
            
            //SeeFood
            if (vision.foodInSight.Count > 0)
            {
                ISeeFood = true;
            }
            else
            {
                ISeeFood = false;
            }
            
            //HaveFood
            if (inventory.heldItem != null)
            {
                if (inventory.heldItem.Description() == "Food")
                {
                    DoIHaveFood = true;
                }
                else
                {
                    DoIHaveFood = false;
                }
            }
            else
            {
                DoIHaveFood = false;
            }

            //if (health.currHealth <= 0)
            //{
            //    EggManager.instance.StartEgg(littleGuy.gameObject);
            //}
        }
        #endregion

    }
}