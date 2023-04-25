using Anthill.AI;
using UnityEngine;

namespace Lloyd
{
    public class LesserQueenAttack : AntAIState
    {

        public GameObject zombeeStinger;
        public GameObject zombeenessIncreaser;
        
        public override void Enter()
        {
            base.Enter();
            
            int randomIndex = Random.Range(0, 2);

            GameObject selectedObject = randomIndex == 0 ? zombeeStinger : zombeenessIncreaser;

            Instantiate(selectedObject, transform.position, Quaternion.identity);
            
        }
    }
}