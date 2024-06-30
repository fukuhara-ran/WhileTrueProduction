using System;

public class RomuQuad
{
    private ulong _w;
    private ulong _x;
    private ulong _y;
    private ulong _z;
    private static RomuQuad _instance;
    public static RomuQuad Instance => _instance ??= new RomuQuad();

    private RomuQuad() : this((ulong)DateTime.UtcNow.Ticks)
    {
    }

    private RomuQuad(ulong seed)
    {
        // Initialize the state with the seed
        _w = seed;
        _x = seed;
        _y = seed;
        _z = seed;

        // Warm up the generator
        for (int i = 0; i < 10; i++)
            Next();
    }

    public ulong Next()
    {
        ulong wp = _w;
        ulong xp = _x;
        ulong yp = _y;
        ulong zp = _z;

        _w = 15241094284759029579ul * zp;
        _x = zp + wp;
        _y = yp - xp;
        _z = yp + wp;
        _z = (_z << 52) | (_z >> 12);

        return xp;
    }

    public double NextDouble()
    {
        return (double)Next() / ulong.MaxValue;
    }

    public int Next(int minValue, int maxValue)
    {
        if (minValue > maxValue)
            throw new ArgumentOutOfRangeException(nameof(minValue), "minValue must be less than or equal to maxValue");

        ulong range = (ulong)(maxValue - minValue);
        return (int)(Next() % range) + minValue;
    }
}