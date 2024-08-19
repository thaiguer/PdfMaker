using ACadSharp.Entities;
using ACadSharp.IO;
using ACadSharp;

namespace PdfMaker.Lib.CadModel;

public class Reader
{
    /// <summary>
    /// Read entities from the ModelSpace.
    /// </summary>
    /// <param name="cadFilePath">.dwg or .dxf file full name</param>
    /// <returns></returns>
    public List<Entity> ReadFromModelSpace(string cadFilePath)
    {
        FileInfo fileInfo = new FileInfo(cadFilePath);

        if (fileInfo.Extension.ToLower() == ".dwg")
        {
            using (DwgReader reader = new DwgReader(cadFilePath))
            {
                return GetEntitiesFromDwgModelSpace(reader);
            }
        }

        if (fileInfo.Extension.ToLower() == ".dxf")
        {
            using (DxfReader reader = new DxfReader(cadFilePath))
            {
                return GetEntitiesFromDxfModelSpace(reader);
            }
        }

        return new List<Entity>();
    }

    private List<Entity> GetEntitiesFromDwgModelSpace(DwgReader reader)
    {
        CadDocument document = reader.Read();
        return GetEntitiesFromDocumentModelSpace(document);
    }

    private List<Entity> GetEntitiesFromDxfModelSpace(DxfReader reader)
    {
        CadDocument document = reader.Read();
        return GetEntitiesFromDocumentModelSpace(document);
    }

    private List<Entity> GetEntitiesFromDocumentModelSpace(CadDocument document)
    {
        var entities = new List<Entity>();
        foreach (var entity in document.ModelSpace.Entities)
        {
            entities.Add(entity);
        }
        return entities;
    }
}