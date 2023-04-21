using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BeeHive : MonoBehaviour, IInteractable
{
    public int resources;
    private IInteractable _interactableImplementation;

    public Transform me;

    public void Start()
    {
        me = transform;
    }

    public void ScaleByPercentage(float percentage)
    {
        float scale = percentage * 100f;

        me.localScale = Vector3.one * scale;
    }

    public void ChangeFloat(int amount)
    {
        resources += amount;
        
        ScaleByPercentage(1 * amount);
    }

    public void Interact()
    {
    }

    public void Inspect()
    {
        throw new System.NotImplementedException();
    }
}
