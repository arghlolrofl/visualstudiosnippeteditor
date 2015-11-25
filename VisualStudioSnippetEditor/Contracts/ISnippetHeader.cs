using System.Collections.Generic;

namespace VisualStudioSnippetEditor.Contracts
{
  public interface ISnippetHeader
  {
    string Title { get; set; }
    string Shortcut { get; set; }
    string Author { get; set; }
    string Description { get; set; }
    List<SnippetType> SnippetTypes { get; set; }
  }
}
