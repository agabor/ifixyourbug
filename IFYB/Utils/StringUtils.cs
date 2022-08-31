
namespace IFYB;

public static class StringUtils
{
    public static string? ToHtml(this string text)
    {
        text = text.Replace("\r\n", "<br>");
        text = text.Replace("\n\r", "<br>");
        text = text.Replace("\n", "<br>");
        text = text.Replace("\r", "<br>");
        return text;
    }
}