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
doc.Entities.Add(CreateLine(0, 0, 0, 297, 0, 0));
doc.Entities.Add(CreateLine(297, 0, 0, 297, 210, 0));
doc.Entities.Add(CreateLine(297, 210, 0, 0, 210, 0));
doc.Entities.Add(CreateLine(0, 210, 0, 0, 0, 0));

//outras linhas
doc.Entities.Add(CreateLine(0, 0, 0, 150, 10, 0));
doc.Entities.Add(CreateLine(45, 15, 0, 75, 38, 0));
doc.Entities.Add(CreateLine(18, 3, 0, 5, 150, 0));

//write
using (DwgWriter writer = new DwgWriter(cadFilePath, doc))
{
    writer.Write();
}

//read
var docModelSpaceEntities = new List<Entity>();
using (DwgReader reader = new DwgReader(cadFilePath))
{
    CadDocument doc2;
    doc2 = reader.Read();
    docModelSpaceEntities.AddRange(doc2.Entities);
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

foreach(var entity in docModelSpaceEntities)
{
    if(entity is Line line)
    {
        XPoint start = new XPoint(line.StartPoint.X, line.StartPoint.Y);
        XPoint end = new XPoint(line.EndPoint.X, line.EndPoint.Y);
        gfx.DrawLine(XPens.Red, start, end);
    }
}

// Draw a circle with a red pen which is 1.5 point thick.
// var r = width / 5;
// gfx.DrawEllipse(
//         new XPen(XColors.Red, 1.5),
//         XBrushes.White,
//         new XRect(width / 2 - r, height / 2 - r, 2 * r, 2 * r)
//     );

// Create a font.
var font = new XFont("Times New Roman", 20, XFontStyleEx.BoldItalic);

// Draw the text.
gfx.DrawString("Hello, PDFsharp!", font, XBrushes.Black,
    new XRect(0, 0, page.Width.Point, page.Height.Point), XStringFormats.Center);

// Save the document...
//var filename = PdfFileUtility.GetTempPdfFullFileName("samples/HelloWorldSample");
//document.Save(filename);
document.Save(pdfFilePath);
PdfFileUtility.ShowDocument(pdfFilePath);

Line CreateLine(double startX, double startY, double startZ, double endX, double endY, double endZ)
{
    Line line = new Line
    {
        StartPoint = new CSMath.XYZ(startX, startY, startZ),
        EndPoint = new CSMath.XYZ(endX, endY, endZ)
    };
    return line;
}