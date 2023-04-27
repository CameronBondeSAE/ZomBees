using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class TestDoTweenFade : MonoBehaviour
{
    public Image image;
    
    [Button]
    void Fade()
    {
        image.transform.localScale = Vector3.one;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(image.DOColor(Color.black, 0f));
        mySequence.Append(image.DOColor(Color.white, 2f));
        mySequence.Insert(0, image.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 3f));
        mySequence.AppendCallback(MyCode);
        mySequence.Play();
        
    }

    void MyCode()
    {
        Debug.Log("CAM woz here");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
