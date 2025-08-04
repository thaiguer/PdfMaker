using ACadSharp.Entities;

namespace PdfMaker.Lib.EntitiesHandle;

public partial class EntitiesHandler
{
    void DrawHatch(Hatch hatch)
    {
        if (hatch.IsInvisible) return;

        var paths = hatch.Paths;
        foreach(var path in paths)
        {
            foreach(var entity in path.Entities)
            {
                
            }
        }
    }
}