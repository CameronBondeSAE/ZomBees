using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEditor.TerrainTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Virginia
{
    public class IntroMovement : MonoBehaviour
    {
        public Image image;
        public GameObject intro;


        void Start()
        {
            image.transform.localScale = Vector3.one; // just sets the cover at 1,1,1 at the start
            Sequence mySequence = DOTween.Sequence();
            
            mySequence.Append(image.DOColor(Color.black, 0f)); // starts off black then switches 
            mySequence.Append(image.DOColor(Color.white, 2f));
            mySequence.Insert(0, image.transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 3f));
            //this makes the image expand in size by 2.5 times
            
            mySequence.Play();
            
           
        }
        
        // Update is called once per frame
        void Update()
        {
                
            if (InputSystem.GetDevice<Keyboard>().anyKey.wasPressedThisFrame)
            {
                gameObject.SetActive(false); //This just sets the canvas of the title as not visible
                intro.SetActive(true); //this sets intro to the game as visible
                
            }
        }
    }
}
