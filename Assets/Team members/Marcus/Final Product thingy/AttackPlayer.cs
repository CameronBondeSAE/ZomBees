using System;
using System.Collections;
using System.Collections.Generic;
using Lloyd;
using Oscar;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Marcus
{
    public class AttackPlayer : TurnFunction
    {
        public OscarVision vision;
        public Hearing hearing;
        
        public Rigidbody rb;
        public Renderer renderer;

        private float detection;

        [ReadOnly]
        public bool attacking;
        [ReadOnly]
        public bool tracking;

        private SoundProperties sound;
        
        private float noticeTimer = 3.5f;
        private float noticeCounter;

        private Collider[] hits;

        private void Awake()
        {
            sound = hearing.loudestRecentSound;
        }

        private void FixedUpdate()
        {
            // Move towards loud sounds such as gun fire
            if (hearing.heardSound)
            {
                if (sound.SoundType != SoundEmitter.SoundType.Bee && sound.SoundType != SoundEmitter.SoundType.CreatureRepellant)
                {
                    TurnTowards(rb, sound.Source, 1000f);
                }
                // Move away from repellant sounds
                else if(sound.SoundType == SoundEmitter.SoundType.CreatureRepellant)
                {
                    TurnTowards(rb, sound.Source.transform.position * -1f, 5000f);
                }
            }
            
            // Turn towards visible light
            if (vision.lightInSight.Count > 0)
            {
                TurnTowards(rb, vision.lightInSight[0].gameObject, 1000f);
                SetView(detection + 0.2f);

                tracking = true;
                noticeCounter += Time.deltaTime;
            }
            else
            {
                ResetValues();
            }

            // Attack light source once noticed
            if (noticeCounter >= noticeTimer && !attacking)
                attacking = true;

            if (attacking)
            {
                Attack();
            }
        }

        void Attack()
        {
            hits = Physics.OverlapSphere(transform.position, 1f);

            foreach (Collider item in hits)
            {
                var thing = item.GetComponent<Health>();
                
                if (thing != null && thing.currHealth > 0)
                    thing.Change(-1000000000000000f);
            }
        }

        private void ResetValues()
        {
            noticeCounter = 0f;
            SetView(0f);

            tracking = false;
            attacking = false;
        }
        
        void SetView(float newValue)
        {
            detection = newValue;
            renderer.material.SetFloat("_Detection", detection);
        }
    }
}
