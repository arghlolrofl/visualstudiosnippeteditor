using System;
using System.Collections.ObjectModel;

namespace VisualStudioSnippetEditor.Contracts
{
  public interface ISnippet
  {
    string Name { get; }

    Version Format { get; set; }

    ISnippetHeader Header { get; set; }

    ObservableCollection<ISnippetLiteral> Literals { get; set; }

    ISnippetCode Code { get; set; }
  }
}
