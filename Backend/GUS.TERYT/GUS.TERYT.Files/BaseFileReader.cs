namespace GUS.TERYT.Files;

public abstract class BaseFileReader : IDisposable
{
    protected readonly StreamReader streamReader;
    private bool disposed = false;


    protected BaseFileReader(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(filePath);
        }

        using var fileStream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite,
            bufferSize: 4096,
            useAsync: true);
        this.streamReader = new StreamReader(fileStream);
    }

    protected BaseFileReader(StreamReader streamReader)
    {
        this.streamReader = streamReader;
    }


    public virtual async IAsyncEnumerable<string> ReadRawAsync()
    {
        // Skip first line
        await streamReader.ReadLineAsync();
        while (await streamReader.ReadLineAsync() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            yield return line;
        }
    }

    ~BaseFileReader()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;

        if (disposing)
        {
            streamReader?.Dispose();
        }
        disposed = true;
    }
}