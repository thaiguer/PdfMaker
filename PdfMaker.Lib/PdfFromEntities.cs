using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfMaker.Lib.DocumentModel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using PdfSharp.Snippets.Font;
using ACadSharp.Entities;
using ACadSharp.IO;
using System.Reflection.Metadata;
using PdfMaker.Lib.EntitiesHandle;
using PdfMaker.Lib.CadModel;

namespace PdfMaker.Lib;

public class PdfFromEntities
{
    public PdfDocumentInfo PdfDocumentInfo { get; set; }
    public List<Entity> CadEntities { get; set; } = new();
    public List<PdfEntity> PdfEntities { get; set; } = new();
    public PdfDocument PdfDocument { get; set; } = null!;

    public PdfFromEntities(PdfDocumentInfo documentInfo)
    {
        PdfDocumentInfo = documentInfo;
    }

    /// <summary>
    /// Creates the document. Create a PDF document with one page.
    /// Uses the information from PdfDocumentInfo and CadEntities.
    /// The objects in the PDF are drawn based on the position from the entities in millimiters (mm).
    /// If the entities are positioned out of the boundaries of the PDF page they will not be shown.
    /// </summary>
    /// <returns>Success.</returns>
    public bool Create()
    {
        try
        {
            var document = new PdfDocument();
            document.Info.Title = PdfDocumentInfo.Title;
            document.Info.Subject = PdfDocumentInfo.Subject;
            document.Info.Author = PdfDocumentInfo.Author;

            var page = document.AddPage();
            page.Size = PdfDocumentInfo.PageSize;
            page.Orientation = PdfDocumentInfo.PageOrientation;
            
            var xGraphics = XGraphics.FromPdfPage(page, XGraphicsUnit.Millimeter);
            EntitiesHandler entitiesHandler = new EntitiesHandler(xGraphics);
            
            //just for test
            //XPen xPen = new XPen(XColors.BlanchedAlmond, Convert.MillimeterToPoint(0.5));
            //xGraphics.DrawLine(xPen, 0, 0, 150, 150);
            //xGraphics.DrawLine(xPen, 12, 500, 150, 150);
            //just for test

            foreach (var entity in CadEntities)
            {
                PdfEntity pdfEntity = new PdfEntity(entity);
                entitiesHandler.InsertEntity(pdfEntity);
                PdfEntities.Add(pdfEntity);
            }

            foreach(var pdfEntity in PdfEntities)
            {
                if(!pdfEntity.Drawn)
                {
                    Console.WriteLine($"{pdfEntity.CadEntity.GetType()} não incluído no PDF.");
                }
            }

            document.Save(PdfDocumentInfo.FileFullName);
        }
        catch
        {
            return false;
        }
        return true;
    }
}