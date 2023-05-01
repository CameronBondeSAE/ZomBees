using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lloyd
{
    public class FaceManager : MonoBehaviour
    {
        private Mouth mouth;

        public Eyebrows eyebrows;

        public bool emoting = false;

        [Header("How long til features fade")] public float waitTime;
        
        public void Start()
        {
            //mouth = GetComponentInChildren<Mouth>();
            //eyebrows = GetComponent<Eyebrows>();

            //mouth.StartGame(waitTime);
            eyebrows.StartGame(waitTime);

            //ChangeEmotionEvent += mouth.ChangeMouth;
            ChangeEmotionEvent += eyebrows.ChangeEmotion;

            Neutral(CivEmotions.Angry, CivEmotions.Angry);
        }

        public event Action<CivEmotions, CivEmotions> ChangeEmotionEvent;

        public void OnChangeEmotion(CivEmotions firstEmote, CivEmotions secondEmote)
        {
            ChangeEmotionEvent?.Invoke(firstEmote, secondEmote);
        }

        private IEnumerator Emoting()
        {
            emoting = true;
            yield return new WaitForSeconds(waitTime);
            emoting = false;
        }

        public void RandomSadEmote()
        {
            CivEmotions newEmote;
            int randomIndex = Random.Range(0, 3);
            if (randomIndex == 0)
            {
                newEmote = CivEmotions.Angry;
            } else if (randomIndex == 1)
            {
                newEmote = CivEmotions.Sad;
            } else
            {
                newEmote = CivEmotions.Surprised;
            }
            
            OnChangeEmotion(CivEmotions.Neutral, newEmote);
        }

        [Button]
        public void OnChangeEmotionInspector(CivEmotions firstEmote, CivEmotions secondEmote)
        {
            if (!emoting)
            {
                StartCoroutine(Emoting());

                OnChangeEmotion(firstEmote, secondEmote);
            }
        }

        [Button]
        public void Neutral(CivEmotions firstEmote, CivEmotions secondEmote)
        {
            firstEmote = CivEmotions.Neutral;
            secondEmote = CivEmotions.Neutral;
            OnChangeEmotion(firstEmote, secondEmote);
        }


        [Button]
        public void Happy(CivEmotions firstEmote, CivEmotions secondEmote)
        {
            firstEmote = CivEmotions.Neutral;
            secondEmote = CivEmotions.Happy;
            OnChangeEmotion(firstEmote, secondEmote);
        }

        [Button]
        public void Sad(CivEmotions firstEmote, CivEmotions secondEmote)
        {
            firstEmote = CivEmotions.Neutral;
            secondEmote = CivEmotions.Sad;
            OnChangeEmotion(firstEmote, secondEmote);
        }

        [Button]
        public void Angry(CivEmotions firstEmote, CivEmotions secondEmote)
        {
            firstEmote = CivEmotions.Neutral;
            secondEmote = CivEmotions.Angry;
            OnChangeEmotion(firstEmote, secondEmote);
        }

        [Button]
        public void Surprised(CivEmotions firstEmote, CivEmotions secondEmote)
        {
            firstEmote = CivEmotions.Neutral;
            secondEmote = CivEmotions.Surprised;
            OnChangeEmotion(firstEmote, secondEmote);
        }
    }
}