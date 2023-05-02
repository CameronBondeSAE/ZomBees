using System;

[Flags] public enum CivCountryOfOrigin
{
    Polish= 1 << 0,
    Australian=1 << 1,
    German=1 << 2,
    French=1 << 3,
    British=1 << 4,
    Welsh=1 <<5,
    Greek=1 <<6,
    Czechoslovakian=1 <<7,
    Croatian = 1 <<8,
    Scottish= 1 <<9,
    Irish= 1 <<10,
    Dutch= 1 <<11,
    Italian= 1 <<12,
    Russian= 1 <<13,
    Spanish= 1 <<14
}