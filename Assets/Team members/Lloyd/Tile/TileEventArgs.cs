using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEventArgs : EventArgs
{       
    public TileTracker.SquareType[,] Board { get; set; }
}
