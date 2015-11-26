using GalaSoft.MvvmLight;
using VisualStudioSnippetEditor.Enums;

namespace VisualStudioSnippetEditor.Contracts
{
  public class ViewModelInfo
  {
    public ViewKind ViewKind { get; private set; }
    public ViewModelBase ViewModel { get; set; }

    public ViewModelInfo(ViewKind kind)
    {
      ViewKind = kind;
    }
  }
}
