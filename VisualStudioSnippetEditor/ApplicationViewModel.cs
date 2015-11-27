using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using VisualStudioSnippetEditor.Contracts;
using VisualStudioSnippetEditor.Enums;
using VisualStudioSnippetEditor.Messages;
using VisualStudioSnippetEditor.ViewModel;

namespace VisualStudioSnippetEditor
{
  public class ApplicationViewModel : GalaSoft.MvvmLight.ViewModelBase, IDisposable
  {
    ILifetimeScope _scope;
    Contracts.ViewModelBase _currentViewModel;
    List<ViewModelInfo> ViewModels;

    public Contracts.ViewModelBase CurrentViewModel
    {
      get { return _currentViewModel; }
      set { _currentViewModel = value; RaisePropertyChanged(); }
    }

    public ApplicationViewModel(ILifetimeScope scope)
    {
      _scope = scope;

      ViewModels = new List<ViewModelInfo>()
      {
        new ViewModelInfo(ViewKind.None) { ViewModel = CurrentViewModel = _scope.Resolve<StartViewModel>() },
        new ViewModelInfo(ViewKind.Edit)
      };

      MessengerInstance.Register<ChangeViewModelMessage>(this, (msg) => changeViewModel(msg));
    }

    private void changeViewModel(ChangeViewModelMessage msg)
    {
      var viewModelInfo = ViewModels.First((vmi) => vmi.ViewKind == msg.ViewKind);

      if (viewModelInfo.ViewModel == null)
      {
        switch (viewModelInfo.ViewKind)
        {
          case ViewKind.None:
            viewModelInfo.ViewModel = _scope.Resolve<StartViewModel>();
            break;
          case ViewKind.Edit:
            viewModelInfo.ViewModel = _scope.Resolve<EditViewModel>();
            (viewModelInfo.ViewModel as EditViewModel).Initialize((ISnippet)msg.Parameter);
            break;
          default:
            break;
        }
      }

      CurrentViewModel = viewModelInfo.ViewModel;
    }

    public void Dispose()
    {
      if (_scope != null)
        _scope.Dispose();
    }
  }
}
