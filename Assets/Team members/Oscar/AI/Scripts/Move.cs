using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oscar
{
    public class Move : OscarsLittleGuyMovement
    {
        void Update()
        {
            BasicMovement(1f);
        }
    }
}

