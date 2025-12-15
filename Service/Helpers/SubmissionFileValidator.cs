using System.Text.RegularExpressions;

namespace Service.Helpers;

public static class SubmissionFileValidator
{
    private static readonly Regex ValidNameRegex =
        new(@"^[A-Za-z0-9_]+\.(pdf|docx)$", RegexOptions.IgnoreCase);

    public static bool IsValidFileName(string fileName, string groupCode)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return false;

        // no spaces
        if (fileName.Contains(" "))
            return false;

        // no Vietnamese characters
        if (Regex.IsMatch(fileName, @"[àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]",
                RegexOptions.IgnoreCase))
            return false;

        // no group code
        if (!string.IsNullOrEmpty(groupCode) &&
            fileName.Contains(groupCode, StringComparison.OrdinalIgnoreCase))
            return false;

        // extension + format
        return ValidNameRegex.IsMatch(fileName);
    }

    public static bool IsAllowedFolder(string fileType)
    {
        return fileType == "Final Thesis and Reports"
               || fileType == "Reports";
    }
}