using System;
using GalaSoft.MvvmLight.CommandWpf;
using VisualStudioSnippetEditor.Contracts;
using VisualStudioSnippetEditor.Enums;
using VisualStudioSnippetEditor.Messages;

namespace VisualStudioSnippetEditor.ViewModel
{
  public class EditViewModel : Contracts.ViewModelBase
  {
    const string WindowTitleText = "Edit Snippet - {0}";
    private ISnippet _snippet;

    public RelayCommand LeaveEditModeCommand { get; set; }

    public override string WindowTitle
    {
      get { return String.Format(WindowTitleText, Snippet.Name); }
    }

    public ISnippet Snippet
    {
      get { return _snippet; }
      set { _snippet = value; RaisePropertyChanged(); }
    }

    public EditViewModel()
    {
      LeaveEditModeCommand = new RelayCommand(() =>
      {
        MessengerInstance.Send(new ChangeViewModelMessage() { ViewKind = ViewKind.None });
      });
    }

    public void Initialize(ISnippet snippet)
    {
      Snippet = snippet;
    }
  }
}
