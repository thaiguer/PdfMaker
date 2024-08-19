using ACadSharp.Entities;
using Microsoft.VisualBasic;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PdfMaker.Lib.EntitiesHandle
{
    internal class EntitiesHandler
    {
        public void InsertEntity(Entity entity, XGraphics xGraphics)
        {
            xGraphics.DrawLine(XPens.Red, 0, 0, 150, 150);
            xGraphics.DrawLine(XPens.Red, 12, 500, 150, 150);

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
        //}

        //void DrawLine()
        //{
        //    XPoint start = new XPoint(line.StartPoint.X, line.StartPoint.Y);
        //    XPoint end = new XPoint(line.EndPoint.X, line.EndPoint.Y);
        //    var gfx = XGraphics.FromPdfPage(page, XGraphicsUnit.Millimeter);
        //    gfx.DrawLine(XPens.Red, start, end);
        //}
    }
}
