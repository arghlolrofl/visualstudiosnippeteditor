using System.IO;

namespace VisualStudioSnippetEditor.Contracts
{
  public interface ISnippetReader
  {
    ISnippet Parse(FileInfo targetFile);
  }
}
