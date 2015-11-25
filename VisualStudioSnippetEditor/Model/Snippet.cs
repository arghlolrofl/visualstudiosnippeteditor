using System;
using System.Collections.ObjectModel;
using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor.Model
{
  public class Snippet : EntityBase, ISnippet
  {
    private Version _format;
    private ISnippetHeader _header;
    private ObservableCollection<ISnippetLiteral> _literals;
    private ISnippetCode _code;

    #region Properties
    public string Name
    {
      get
      {
        return !String.IsNullOrEmpty(Header.Title) ? Header.Title : "New Snippet";
      }
    }

    public Version Format
    {
      get { return _format; }
      set { _format = value; RaisePropertyChanged(); }
    }

    public ISnippetHeader Header
    {
      get { return _header; }
      set { _header = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<ISnippetLiteral> Literals
    {
      get { return _literals; }
      set { _literals = value; RaisePropertyChanged(); }
    }

    public ISnippetCode Code
    {
      get { return _code; }
      set { _code = value; RaisePropertyChanged(); }
    }

    #endregion

    public Snippet(ISnippetHeader header, ISnippetCode code)
    {
      _header = header;
      _code = code;
      _literals = new ObservableCollection<ISnippetLiteral>();
    }
  }
}
