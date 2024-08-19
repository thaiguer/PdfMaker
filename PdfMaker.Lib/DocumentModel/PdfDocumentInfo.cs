using PdfSharp;

namespace PdfMaker.Lib.DocumentModel;

public class PdfDocumentInfo
{
    public PdfDocumentInfo(string title,
                        string subject,
                        string author,
                        string directoryPath,
                        string fileNameWithoutExtension = "",
                        string language = "pt-BR",
                        string extension = ".pdf",
                        PageSize pageSize = PageSize.A3,
                        PageOrientation pageOrientation = PageOrientation.Landscape
        )
    {
        Title = title;
        Subject = subject;
        Author = author;

        if(string.IsNullOrEmpty(fileNameWithoutExtension))
            FileNameWithoutExtension = title;
        else FileNameWithoutExtension = fileNameWithoutExtension;

        DirectoryPath = directoryPath;
        Language = language;
        Extension = extension;
        PageSize = pageSize;
        PageOrientation = pageOrientation;
    }
    
    public string Title { get; private set; }
    public string Subject { get; private set; }
    public string Author { get; private set; }
    public string FileNameWithoutExtension { get; set; }
    public string DirectoryPath { get; set; }
    public string Language { get; private set; }

    private string extension = "";
    public string Extension
    {
        get
        {
            return extension;
        }
        set
        {
            if (value.Length <= 0) extension = ".pdf";
            else if (value[0] != '.') extension = $".{value}";
            else extension = value;
        }
    }

    public PageSize PageSize { get; set; }
    public PageOrientation PageOrientation { get; set; }
    public string FileFullName
    {
        get
        {
            return $"{DirectoryPath}\\{FileNameWithoutExtension}{Extension}";
        }
    }
}
