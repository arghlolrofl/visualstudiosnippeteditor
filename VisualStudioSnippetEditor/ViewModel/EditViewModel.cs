using System;
using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor.ViewModel
{
  public class EditViewModel : Contracts.ViewModelBase
  {
    const string WindowTitleText = "Edit Snippet - {0}";
    private ISnippet _snippet;

    public override string WindowTitle
    {
      get { return String.Format(WindowTitleText, Snippet.Name); }
    }

    public ISnippet Snippet
    {
      get { return _snippet; }
      set { _snippet = value; RaisePropertyChanged(); }
    }


    public void Initialize(ISnippet snippet)
    {
      Snippet = snippet;
    }
  }
}
