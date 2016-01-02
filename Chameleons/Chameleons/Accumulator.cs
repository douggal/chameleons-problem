using System;
using System.Collections.Generic;

/// <summary>
/// Accumulator - maintains a running average of data values
/// based on Java code given in Sedgewick/Wayne textbook _Algorthms_, 4th ed.
/// added Hi and Lo properties.
/// </summary>
public class Accumulator
{
    private double _total;
    private int _N;
    private double _hi;
    private double _lo;

    public Accumulator()
    {
        _N = 0;
        _total = 0d;
        _hi = Double.MinValue;
        _lo = Double.MaxValue;
    }

    public void AddDataValue(double val)
    {
        _N++;
        _total += val;
        _hi = Math.Max(_hi, val);
        _lo = Math.Min(_lo, val);
    }

    public double Mean()
    {
        return _total / _N;
    }

    public double Lo()
    {
        return _lo;
    }

    public double Hi()
    {
        return _hi;
    }

    public override string ToString()
    {
        return "Mean (" + _N.ToString() + " values): " + String.Format("{0,5}", Mean());
    }
}