using GalaSoft.MvvmLight;
using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor.ViewModel
{
  public class EditViewModel : ViewModelBase
  {
    private ISnippet _snippet;
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
