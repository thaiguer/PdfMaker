using ACadSharp.Entities;

namespace PdfMaker.Lib.CadModel
{
    public class PdfEntity
    {
        public Entity CadEntity { get; set; }
        public bool Drawn { get; set; } = false;

        public PdfEntity(Entity cadEntity)
        {
            CadEntity = cadEntity;
        }
    }
}