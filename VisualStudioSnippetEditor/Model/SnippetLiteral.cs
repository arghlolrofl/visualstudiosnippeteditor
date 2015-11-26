using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor.Model
{
  public class SnippetLiteral : EntityBase, ISnippetLiteral
  {
    string _defaultValue;
    string _identifier;
    string _toolTip;
    string _function;
    bool? _isEditable;

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

    public string Function
    {
      get { return _function; }
      set { _function = value; RaisePropertyChanged(); }
    }

    public bool? IsEditable
    {
      get { return _isEditable; }
      set { _isEditable = value; RaisePropertyChanged(); }
    }
  }
}
