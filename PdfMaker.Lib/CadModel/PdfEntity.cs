using ACadSharp.Entities;

namespace PdfMaker.Lib.CadModel
{
    internal class PdfEntity
    {
        public Entity CadEntity { get; set; }
        public bool IsInsertedInPDF { get; set; } = false;

        public PdfEntity(Entity cadEntity)
        {
            CadEntity = cadEntity;
        }
    }
}