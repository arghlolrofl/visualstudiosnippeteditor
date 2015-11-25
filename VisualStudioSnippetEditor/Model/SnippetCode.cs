using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor.Model
{
  public class SnippetCode : EntityBase, ISnippetCode
  {
    ProgrammingLanguage _language;
    string _content;

    public string Content
    {
      get
      {
        return _content;
      }

      set
      {
        _content = value;
        RaisePropertyChanged();
      }
    }

    public ProgrammingLanguage Language
    {
      get
      {
        return _language;
      }

      set
      {
        _language = value;
        RaisePropertyChanged();
      }
    }
  }
}
