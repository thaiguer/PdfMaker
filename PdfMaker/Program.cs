using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using ACadSharp.IO;
using ACadSharp.Entities;
using ACadSharp;
using PdfSharp.Snippets.Font;

Console.WriteLine("Hello, World!");

string cadFilePath = "c:\\dev\\thecadfile.dwg";
string pdfFilePath = "c:\\dev\\thecadfile.pdf";

//Create a new document
CadDocument doc = new CadDocument();

//folha A4
doc.Entities.Add(CreateLine(0, 0, 297, 0));
doc.Entities.Add(CreateLine(297, 0, 297, 210));
doc.Entities.Add(CreateLine(297, 210, 0, 210));
doc.Entities.Add(CreateLine(0, 210, 0, 0));

//outras linhas
doc.Entities.Add(CreateLine(0, 0, 150, 10));
doc.Entities.Add(CreateLine(45, 15, 75, 38));
doc.Entities.Add(CreateLine(18, 3, 5, 150));

//texto
doc.Entities.Add(CreateText("Hello from this great thing", 5, 15));
doc.Entities.Add(CreateText("Hello again", 45, 80));
doc.Entities.Add(CreateText("texting stuff, what a great moment", 100, 150));

//write
using (DwgWriter writer = new DwgWriter(cadFilePath, doc))
{
    writer.Write();
}

//read
var docEntities = new List<Entity>();
using (DwgReader reader = new DwgReader(cadFilePath))
{
    CadDocument doc0;
    doc0 = reader.Read();
    docEntities.AddRange(doc0.Entities);
}

// Create a new PDF document.
var document = new PdfDocument();
document.Info.Title = "Created with PDFsharp";
document.Info.Subject = "Just a simple Hello-World program.";

// Create an empty page in this document.
var page = document.AddPage();
page.Size = PdfSharp.PageSize.A3;
page.Orientation = PdfSharp.PageOrientation.Landscape;

// Get an XGraphics object for drawing on this page.
var gfx = XGraphics.FromPdfPage(page, XGraphicsUnit.Millimeter);

// Draw two lines with a red default pen.
var width = page.Width.Point;
var height = page.Height.Point;

//gfx.DrawLine(XPens.Red, 0, 0, width, height);
//gfx.DrawLine(XPens.Red, width, 0, 0, height);

foreach(var entity in docEntities)
{
    if(entity is Line line)
    {
        XPoint start = new XPoint(line.StartPoint.X, line.StartPoint.Y);
        XPoint end = new XPoint(line.EndPoint.X, line.EndPoint.Y);
        gfx.DrawLine(XPens.Red, start, end);
        continue;
    }

    if(entity is TextEntity textEntity)
    {
        var font0 = new XFont("Times New Roman", textEntity.Height, XFontStyleEx.BoldItalic);
        gfx.DrawString(
                textEntity.Value,
                font0,
                XBrushes.Black,
                new XRect(textEntity.AlignmentPoint.X, textEntity.AlignmentPoint.Y, page.Width.Point, textEntity.Height),
                XStringFormats.Center
            );
        continue;
    }
}

// Draw a circle with a red pen which is 1.5 point thick.
// var r = width / 5;
// gfx.DrawEllipse(
//         new XPen(XColors.Red, 1.5),
//         XBrushes.White,
//         new XRect(width / 2 - r, height / 2 - r, 2 * r, 2 * r)
//     );

// Draw the text.
var font = new XFont("Times New Roman", 20, XFontStyleEx.BoldItalic);
gfx.DrawString("Hello, PDFsharp!", font, XBrushes.Black,
   new XRect(0, 0, page.Width.Point, page.Height.Point), XStringFormats.Center);

// Save the document...
//var filename = PdfFileUtility.GetTempPdfFullFileName("samples/HelloWorldSample");
//document.Save(filename);
document.Save(pdfFilePath);
PdfFileUtility.ShowDocument(pdfFilePath);

Line CreateLine(double startX, double startY, double endX, double endY)
{
    Line line = new Line
    {
        StartPoint = new CSMath.XYZ(startX, startY, 0),
        EndPoint = new CSMath.XYZ(endX, endY, 0)
    };
    return line;
}

TextEntity CreateText(string value, double startX, double startY)
{
    CSMath.XYZ insertPoint = new CSMath.XYZ(startX, startY, 0);
    ACadSharp.Entities.TextEntity text = new TextEntity
    {
        Height = 2,
        Value = value,
        InsertPoint = insertPoint,
        AlignmentPoint = insertPoint,
        HorizontalAlignment = ACadSharp.Entities.TextHorizontalAlignment.Center,
        VerticalAlignment = ACadSharp.Entities.TextVerticalAlignmentType.Middle
    };
    return text;
}