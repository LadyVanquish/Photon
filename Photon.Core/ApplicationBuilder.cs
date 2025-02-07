using System.Reflection;
using Photon.Window;

namespace Photon;

public sealed class ApplicationBuilder
{
    private Type? _applicationType;
    private string _title = GetDefaultTitleName();
    private Rectangle _positionAndSize = new(-1, -1, 1280, 720);

    private static string? GetAssemblyTitle(Assembly? assembly)
    {
        if (assembly is null)
        {
            return null;
        }

        AssemblyTitleAttribute? attribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
        if (attribute is null)
        {
            return null;
        }

        return attribute.Title;
    }

    private static string GetDefaultTitleName()
    {
        string? assemblyTitle = GetAssemblyTitle(Assembly.GetEntryAssembly());
        if (!string.IsNullOrEmpty(assemblyTitle))
        {
            return assemblyTitle!;
        }

        return "Photon";
    }

    public ApplicationBuilder UseApplication<T>(Action<T>? application = null) where T : Application
    {
        _applicationType = typeof(T);
        return this;
    }

    public ApplicationBuilder UseTitle(string title)
    {
        _title = title;
        return this;
    }

    public ApplicationBuilder UsePosition(int x, int y)
    {
        _positionAndSize.X = x;
        _positionAndSize.Y = y;
        return this;
    }

    public ApplicationBuilder UsePosition(Point position)
    {
        _positionAndSize.Location = position;
        return this;
    }

    public ApplicationBuilder UseSize(int width, int height)
    {
        _positionAndSize.Width = width;
        _positionAndSize.Height = height;
        return this;
    }

    public ApplicationBuilder UseSize(Size size)
    {
        _positionAndSize.Size = size;
        return this;
    }

    public Application Build()
    {
        if (_applicationType is null)
        {
            throw new NotImplementedException();
        }
        Application application = (Activator.CreateInstance(_applicationType, _title, _positionAndSize) as Application)!;
        return application;
    }
}
