namespace Infrastructure.Scripts;

public static class SqlLoader
{
    public static string Load(string relativePath)
    {
        var fullPath = Path.Combine(AppContext.BaseDirectory, "Scripts", relativePath);
        return File.ReadAllText(fullPath);
    }
}