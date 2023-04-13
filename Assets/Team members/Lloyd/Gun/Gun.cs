using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammo;
    public int maxAmmo;

    private GameObject origin;

    public GameObject target;

    public int gunDamage;

    public int gunDistance;
    
    private void OnEnable()
    {
        ammo = maxAmmo;
        origin = this.gameObject;
    }

    [Button]
    public void Shoot()
    {
        if (ammo > 0)   
        {
            ammo--;
            //SFX

            Vector3 direction = target.transform.position - origin.transform.position;
            origin.transform.LookAt(target.transform);
            float maxDistance = Vector3.Distance(origin.transform.position, target.transform.position);
            Ray ray = new Ray(origin.transform.position, direction.normalized);
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Clamp(gunDistance, 0, maxDistance), Color.red, 2f);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Clamp(gunDistance, 0, maxDistance)))
            {
                Health health = hit.collider.GetComponent<Health>();
                if (health != null)
                {
                    Debug.Log("Hit");
                    health.Change(-100000);
                }
            }
        }
    }
}