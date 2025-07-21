using System;

namespace FTN.Common
{
    public enum CurveStyle
    {
        constantYValue = 1,
        rampYValue = 2,
        straightLineYValues = 3
    }

    public enum UnitMultiplier
    {
        nano = 1,
        micro = 2,
        milli = 3,
        centi = 4,
        deci = 5,
        none = 6,
        deca = 7,
        hecto = 8,
        kilo = 9,
        mega = 10,
        giga = 11,
        tera = 12
    }

    public enum UnitSymbol
    {
        none = 1,
        V = 2,
        A = 3,
        Hz = 4,
        ohm = 5,
        W = 6,
        VA = 7,
        var = 8,
        deg = 9,
        rad = 10,
        s = 11,
        min = 12,
        h = 13,
        degC = 14,
        F = 15,
        N = 16,
        m = 17,
        kg = 18,
        m2 = 19,
        m3 = 20,
        mps = 21,
        mps2 = 22,
        m3pers = 23
    }
}
