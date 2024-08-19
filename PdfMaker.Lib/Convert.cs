namespace PdfMaker.Lib;

public static class Convert
{
    public static double MillimiterToPointMultiplier { get; set; } = 2.83465;

    /// <summary>
    /// Converts a value from Millimeter to Point unity.
    /// The method uses the Property MillimiterToPointMultiplier.
    /// The default behavior is multiply the parameter (millimeterValue) by 2.83465 and return it. 
    /// </summary>
    /// <param name="millimeterValue">A value in millimeters (1 meter/100) in double format.</param>
    /// <returns>A double value in points unity.</returns>
    public static double MillimeterToPoint(double millimeterValue)
    {
        return millimeterValue * MillimiterToPointMultiplier;
    }
}
