using System;

namespace Team_members.Lloyd.Tile.CivArgs
{
    [Flags]
    public enum EmotionalProfile
    {
        Random=1,
        
        //hunger goes up at a higher rate, etc
        Hungry=2,

        Calm=4,
        Hardened=8,

        Paranoid=16,
        Schizophrenic=32,
        Panic=64,

        Pious=128,

        Rambo=256,
        
        Obnoxious,
        
        Combative,
        
        Sarcastic,
        
        Contrarian,
        
        Introverted,
        
        Traumatised
    }
}