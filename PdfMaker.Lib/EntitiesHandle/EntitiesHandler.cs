using ACadSharp.Entities;
using CSMath;
using PdfMaker.Lib.CadModel;
using PdfSharp.Drawing;

namespace PdfMaker.Lib.EntitiesHandle;

public partial class EntitiesHandler
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
        //ACadSharp.Entities.MLine

        if (entity.CadEntity is Point point)
        {
            DrawPoint(point);
            entity.Drawn = true;
            return;
        }

        if (entity.CadEntity is Circle circle)
        {
            var handler = new CircleHandler(_xGraphics);
            handler.DrawCircle(circle);

            entity.Drawn = true;
            return;
        }

        if (entity.CadEntity is TextEntity textEntity)
        {
            DrawTextEntity(textEntity);
            entity.Drawn = true;
            return;
        }

        if (entity.CadEntity is MText mText)
        {
            DrawMText(mText);
            entity.Drawn = true;
            return;
        }

        if (entity.CadEntity is LwPolyline lwPolyLine)
        {
            DrawLwPolyline(lwPolyLine);
            entity.Drawn = true;
            return;
        }

        if (entity.CadEntity is Hatch hatch)
        {
            DrawHatch(hatch);
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

    void DrawMText(MText mText)
    {
        if (mText.IsInvisible) return;

        var textLines = mText.Value.Split("\\P");
        var nextInsertPoint = new XYZ(); //must change the next insert point to a different place

        foreach (var line in textLines)
        {
            var textEntity = new TextEntity();
            textEntity.Value = mText.Value;
            textEntity.InsertPoint = mText.InsertPoint;
            textEntity.Height = mText.Height;
            DrawTextEntity(textEntity);
            nextInsertPoint = new XYZ();
        }
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
}