namespace PdfMaker.Lib.Styles;

public class IndexColor
{
    public byte Index { get; set; }
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte Alpha { get; set; } = 255;

    public IndexColor(byte index, byte r, byte g, byte b)
    {
        Index = index;
        R = r;
        G = g;
        B = b;
    }
}