using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BeeHive : MonoBehaviour, IInteractable
{
    public int resources;
    private IInteractable _interactableImplementation;

    public Transform me;

    public void ScaleByPercentage(float percentage)
    {
        float scale = percentage / 100f;

        transform.localScale = Vector3.one * scale;
    }

    public void ChangeFloat(int amount)
    {
        resources += amount;
        
        ScaleByPercentage(amount);
    }

    public void Interact()
    {
    }

    public void Inspect()
    {
        throw new System.NotImplementedException();
    }
}
