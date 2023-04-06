using System;

[Flags]
public enum CivTraits
{
    Random = 1 << 0,

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

    Traumatised = 1 << 14
}