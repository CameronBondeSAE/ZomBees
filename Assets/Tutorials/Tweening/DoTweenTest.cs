using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(4f, 4f, 4f), 2f).SetEase(Ease.OutBounce);
   }

    // Update is called once per frame
    void Update()
    {
        /////// NO
    }
}
