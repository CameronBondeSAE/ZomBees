using System;

[Flags]
public enum CivEmotions
{
    Random = 1 << 0,

    //hunger goes up at a higher rate, etc
    Hungry = 1 << 1,

    Calm = 1 << 2,
    Hardened = 1 << 3,

    Paranoid = 1 << 4,
    Schizophrenic = 1 << 5,
    Panic = 1 << 6,
    Pious = 1 << 7,

    Rambo = 1 << 8,

    Obnoxious = 1 << 9,

    Combative = 1 << 10,

    Sarcastic = 1 << 11,

    Contrarian = 1 << 12,

    Introverted = 1 << 13,

    Traumatised = 1 << 14,

    Neutral = 1 << 15,

    Happy = 1 << 16,
    
    Sad = 1 << 17,
    
    Angry = 1 << 18,
    
    Surprised = 1 << 19
}