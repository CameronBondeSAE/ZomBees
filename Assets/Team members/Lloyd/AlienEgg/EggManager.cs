using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lloyd
{
    public class EggManager : MonoBehaviour
    {
        public GameObject eggObjectPrefab;

        public AlienEggPulse eggLogic;

        public GameObject originalCiv;

        public GameObject zombeeCiv;

        public GameObject basicBee;
        public GameObject beeStinger;
        public GameObject beenessIncreaser;

        public BeeStingAttack.BeeStingType eggType;

        public List<Vector3> eggList;
    
        public static EggManager instance;

        private BeeStingerSensor sensor;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [Button]
        public void StartEgg(GameObject newOriginalCiv)
        {
            originalCiv = newOriginalCiv;

            GameObject instantiateEgg = Instantiate(eggObjectPrefab, originalCiv.transform.position,originalCiv.transform.rotation);

            eggLogic = instantiateEgg.GetComponent<AlienEggPulse>();

            SubscribeToEggEvents();
        
            originalCiv.SetActive(false);
        }
    
        private void SubscribeToEggEvents()
        {
            eggLogic.SafeEvent += FreeCiv;
            eggLogic.TimesUpEvent += SpawnBee;
        }

        public void FreeCiv()
        {
            originalCiv.SetActive(true);
        }

        public void SpawnBee()
        {
            GameObject newZombeeCiv = Instantiate(zombeeCiv, Vector3.zero, Quaternion.identity) as GameObject;

            newZombeeCiv.transform.position = originalCiv.transform.position;
            newZombeeCiv.transform.rotation = originalCiv.transform.rotation;

            Rigidbody newRb = newZombeeCiv.GetComponent<Rigidbody>();
            newRb.velocity = Vector3.zero;

            sensor = newZombeeCiv.GetComponent<BeeStingerSensor>();
            sensor.SetHomePoint(originalCiv.transform.position);
        }

        private void OnDisable()
        {
            eggLogic.SafeEvent -= FreeCiv;
            eggLogic.TimesUpEvent -= SpawnBee;
        }
    }
}