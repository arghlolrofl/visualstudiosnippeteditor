using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor.Model
{
  public class SnippetLiteral : EntityBase, ISnippetLiteral
  {
    string _defaultValue;
    string _identifier;
    string _toolTip;

    public string DefaultValue
    {
      get
      {
        return _defaultValue;
      }

      set
      {
        _defaultValue = value;
        RaisePropertyChanged();
      }
    }

    public string Identifier
    {
      get
      {
        return _identifier;
      }

      set
      {
        _identifier = value;
        RaisePropertyChanged();
      }
    }

    public string ToolTip
    {
      get
      {
        return _toolTip;
      }

      set
      {
        _toolTip = value;
        RaisePropertyChanged();
      }
    }
  }
}
