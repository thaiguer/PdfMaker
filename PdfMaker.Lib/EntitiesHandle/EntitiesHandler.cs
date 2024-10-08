﻿using ACadSharp.Entities;
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

        //ACadSharp.Entities.Ellipse
        //ACadSharp.Entities.Hatch]
        //ACadSharp.Entities.MLine
        //ACadSharp.Entities.MText

        if (entity.CadEntity is Point point)
        {
            DrawPoint(point);
            entity.Drawn = true;
            return;
        }

        if (entity.CadEntity is Circle circle)
        {
            DrawCircle(circle);
            entity.Drawn = true;
            return;
        }

        if (entity.CadEntity is TextEntity textEntity)
        {
            DrawTextEntity(textEntity);
            entity.Drawn = true;
            return;
        }

        if (entity.CadEntity is LwPolyline lwPolyLine)
        {
            DrawLwPolyline(lwPolyLine);
            entity.Drawn = true;
            return;
        }

        //if (entity.CadEntity is Arc arc)
        //{
        //    DrawArc(arc);
        //    entity.Drawn = true;
        //    return;
        //}

        //if (entity.CadEntity is Insert insert)
        //{
        //    DrawInsert(insert);
        //    entity.Drawn = true;
        //    return;
        //}


        //TODO: add other entity types
    }

    void DrawLwPolyline(LwPolyline lwPolyline)
    {
        if (lwPolyline.IsInvisible) return;

        for(int i = 0; i < lwPolyline.Vertices.Count; i++)
        {
            XPen xPen = new XPen(
            Styles.CadIndexColors.GetXColorFromIndex(10),
            Convert.MillimeterToPoint((double)lwPolyline.Vertices[i].StartWidth));
            
            XPoint start = new XPoint(lwPolyline.Vertices[i].Location.X, 297 - lwPolyline.Vertices[i].Location.Y);

            XPoint end;
            if (i + 1 >= lwPolyline.Vertices.Count)
            {
                if (!lwPolyline.IsClosed) continue;

                end = new XPoint(lwPolyline.Vertices[0].Location.X, 297 - lwPolyline.Vertices[0].Location.Y);
                _xGraphics.DrawLine(xPen, start, end);
            }
            else
            {
                end = new XPoint(lwPolyline.Vertices[i + 1].Location.X, 297 - lwPolyline.Vertices[i + 1].Location.Y);
                _xGraphics.DrawLine(xPen, start, end);
            }
        }
    }

    void DrawLine(Line line)
    {
        if (line.IsInvisible) return;

        XPen xPen = new XPen(
            Styles.CadIndexColors.GetXColorFromIndex((byte)line.Color.Index),
            Convert.MillimeterToPoint(((double)line.LineWeight)));

        XPoint start = new XPoint(line.StartPoint.X, 297 - line.StartPoint.Y);
        XPoint end = new XPoint(line.EndPoint.X, 297 - line.EndPoint.Y);
        _xGraphics.DrawLine(xPen, start, end);
    }

    void DrawTextEntity(TextEntity textEntity)
    {
        string fontName = "Arial";
        if (textEntity.IsInvisible) return;

        XFont xFont = new XFont(fontName, textEntity.Height);
        XPoint position = new XPoint(textEntity.InsertPoint.X, 297 - textEntity.InsertPoint.Y);
        _xGraphics.DrawString(textEntity.Value, xFont, XBrushes.BurlyWood, position);
    }

    void DrawCircle(Circle circle)
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

    void DrawPoint(Point point)
    {
        if (point.IsInvisible) return;

        XPen xPen = new XPen(
            Styles.CadIndexColors.GetXColorFromIndex((byte)point.Color.Index),
            Convert.MillimeterToPoint(((double)point.LineWeight)));

        XPoint xLocation = new XPoint(point.Location.X, 297 - point.Location.Y);
        _xGraphics.DrawLine(xPen, xLocation, xLocation);
    }

    void DrawPolyline(Polyline polyline)
    {
        if (polyline.IsInvisible) return;

        //foreach(var vertice in polyline.Vertices)
        //{
        //    vertice.Location
        //}

        //XPen xPen = new XPen(
        //    Styles.CadIndexColors.GetXColorFromIndex((byte)line.Color.Index),
        //    Convert.MillimeterToPoint(((double)line.LineWeight)));

        //XPoint start = new XPoint(line.StartPoint.X, line.StartPoint.Y);
        //XPoint end = new XPoint(line.EndPoint.X, line.EndPoint.Y);
        //_xGraphics.DrawLine(xPen, start, end);
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