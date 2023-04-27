using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seq : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Grab a free Sequence to use
        Sequence mySequence = DOTween.Sequence();
        // Add a movement tween at the beginning
        mySequence.Append(transform.DOLocalMoveZ(15, 1)).SetEase(Ease.InOutQuad);
        // Add a rotation tween as soon as the previous one is finished
        mySequence.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), 1).SetEase(Ease.InOutExpo));
        mySequence.Append(transform.DOLocalMoveZ(-15, 1)).SetEase(Ease.InOutQuad);
        mySequence.AppendCallback(Thing);
        mySequence.Append(transform.DOLocalRotate(new Vector3(0, 180, 0), 1).SetEase(Ease.InOutExpo));
        // Delay the whole Sequence by 1 second
        mySequence.AppendInterval(1);
        // Insert a scale tween for the whole duration of the Sequence
        // mySequence.Insert(0, transform.DOScale(new Vector3(3, 3, 3), mySequence.Duration()));

        mySequence.SetLoops(-1);
        mySequence.Play();
    }

    void Thing()
    {
        Debug.Log("CAM!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
