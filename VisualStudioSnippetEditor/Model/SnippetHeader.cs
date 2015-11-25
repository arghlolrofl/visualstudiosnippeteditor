using System.Collections.Generic;
using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor.Model
{
  public class SnippetHeader : EntityBase, ISnippetHeader
  {
    string _author;
    string _shortcut;
    string _title;
    string _description;
    List<SnippetType> _snippetTypes;


    public string Author
    {
      get
      {
        return _author;
      }

      set
      {
        _author = value;
        RaisePropertyChanged();
      }
    }

    public string Shortcut
    {
      get
      {
        return _shortcut;
      }

      set
      {
        _shortcut = value;
        RaisePropertyChanged();
      }
    }

    public string Description
    {
      get { return _description; }
      set { _description = value; RaisePropertyChanged(); }
    }

    public List<SnippetType> SnippetTypes
    {
      get
      {
        return _snippetTypes;
      }

      set
      {
        _snippetTypes = value;
        RaisePropertyChanged();
      }
    }

    public string Title
    {
      get
      {
        return _title;
      }

      set
      {
        _title = value;
        RaisePropertyChanged();
      }
    }

    public SnippetHeader()
    {
      SnippetTypes = new List<SnippetType>();
    }
  }
}
