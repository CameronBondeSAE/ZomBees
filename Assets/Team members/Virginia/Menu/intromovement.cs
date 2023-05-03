using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Virginia
{
    public class IntroMovement : MonoBehaviour
    {
        public Image image;
      
        void Start()
        {
            image.transform.localScale = Vector3.one; // justs sets the cover at 1,1,1 at the start
            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(image.DOColor(Color.black, 0f)); // starts off black then switches 
            mySequence.Append(image.DOColor(Color.white, 2f));
            mySequence.Insert(0, image.transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 3f));
 
            mySequence.AppendCallback(PressMe);
            mySequence.Play();


            void PressMe()
            {
                Debug.Log("I should have play");

            }
            // Update is called once per frame
            void Update()
            {

            }
        }
    }
}
