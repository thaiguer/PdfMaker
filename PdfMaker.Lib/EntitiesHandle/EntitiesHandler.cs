using ACadSharp.Entities;
using PdfMaker.Lib.CadModel;
using PdfSharp.Drawing;

namespace PdfMaker.Lib.EntitiesHandle;

public class EntitiesHandler
{
    private XGraphics _xGraphics { get; set; }
    public EntitiesHandler(XGraphics xGraphics)
    {
        _xGraphics = xGraphics;
    }

    public void InsertEntity(PdfEntity entity)
    {
        if (entity.CadEntity is Line line)
        {
            DrawLine(line);
            entity.Drawn = true;
            return;
        }

        //TODO: add other entity types
    }

    void DrawLine(Line line)
    {
        XPen xPen = new XPen(XColors.Black, Convert.MillimeterToPoint(10));
        XPoint start = new XPoint(line.StartPoint.X, line.StartPoint.Y);
        XPoint end = new XPoint(line.EndPoint.X, line.EndPoint.Y);
        _xGraphics.DrawLine(XPens.Red, start, end);

        Console.WriteLine(line.Color.Index);
    }

    //void DrawEntities()
    //{
    //    foreach (var entity in docEntities)
    //    {
    //        if (entity is Line line)
    //        {
    //            DrawLine();
    //            continue;
    //        }

    //        if (entity is TextEntity textEntity)
    //        {
    //            gfx.DrawString(
    //                    textEntity.Value,
    //                    font0,
    //                    XBrushes.Black,
    //                    new XRect(textEntity.AlignmentPoint.X, textEntity.AlignmentPoint.Y, page.Width.Point, textEntity.Height),
    //                    XStringFormats.Center
    //                );
    //            continue;
    //        }
    //    }

    //// Create a font.
    //var font = new XFont("Times New Roman", 20, XFontStyleEx.BoldItalic);
    //font = new XFont("Arial", 20, XFontStyleEx.Bold);
    //gfx.DrawString("Hello, again!", font, XBrushes.Black, new XPoint(150, 45), XStringFormats.TopLeft);
    //gfx.DrawString("some other text in same position", font, XBrushes.HotPink, new XPoint(150, 45), XStringFormats.TopLeft);

    //// Draw two lines with a red default pen.
    //var width = page.Width.Point;
    //var height = page.Height.Point;
    //gfx.DrawLine(XPens.Red, 0, 0, width, height);
    //gfx.DrawLine(XPens.Red, width, 0, 0, height);

    //// Draw a circle with a red pen which is 1.5 point thick.
    //var r = width / 5;
    //gfx.DrawEllipse(new XPen(XColors.Red, 1.5), XBrushes.White, new XRect(width / 2 - r, height / 2 - r, 2 * r, 2 * r));

    //// Draw the text.
    ////gfx.DrawString("Hello, PDFsharp!", font, XBrushes.Black, new XRect(0, 0, page.Width.Point, page.Height.Point), XStringFormats.Center);
    //gfx.DrawString("Hello, again!", font, XBrushes.Black, new XPoint(0, 0), XStringFormats.TopLeft);
    //}
}