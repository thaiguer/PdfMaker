using ACadSharp.Entities;
using PdfSharp.Drawing;

namespace PdfMaker.Lib.EntitiesHandle;

public class CircleHandler
{
    private XGraphics _xGraphics; 
    public CircleHandler(XGraphics xGraphics)
    {
        _xGraphics = xGraphics;
    }
    public void DrawCircle(Circle circle)
    {
        if (circle.IsInvisible) return;

        XPen xPen = new XPen(
            Styles.CadIndexColors.GetXColorFromIndex((byte)circle.Color.Index),
            Convert.MillimeterToPoint(((double)circle.LineWeight)));

        XPoint xLocation = new XPoint(circle.Center.X, 297 - circle.Center.Y);
        XSize xSixe = new XSize(circle.Radius, circle.Radius);
        XRect xLimits = new XRect(xLocation, xSixe);
        _xGraphics.DrawEllipse(xPen, xLimits);
    }   
}