using System;
using System.Collections.Generic;

/// <summary>
/// Accumulator - maintains a running average of data values
/// based on Sedgewick/Wayne Algorthms 4th ed.
/// </summary>
public class Accumulator
{
    private double total;
    private int N;

    public Accumulator()
    {
        N = 0;
        total = 0d;
    }

    public void AddDataValue(double val)
    {
        N++;
        total += val;
    }

    public double Mean()
    {
        return total / N;
    }

    public override string ToString()
    {
        return "Mean (" + N.ToString() + " values): " + String.Format("{0,5}", Mean());
    }
}