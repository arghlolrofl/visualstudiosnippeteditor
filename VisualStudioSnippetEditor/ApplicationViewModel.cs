using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using GalaSoft.MvvmLight;
using VisualStudioSnippetEditor.Messages;
using VisualStudioSnippetEditor.ViewModel;

namespace VisualStudioSnippetEditor
{
  public class ApplicationViewModel : ViewModelBase, IDisposable
  {
    ILifetimeScope _scope;
    Dictionary<string, ViewModelBase> ViewModels;

    public ViewModelBase CurrentViewModel { get; set; }

    public ApplicationViewModel(ILifetimeScope scope)
    {
      _scope = scope;

      ViewModels = new Dictionary<string, ViewModelBase>()
      {
        { nameof(StartViewModel), CurrentViewModel = _scope.Resolve<StartViewModel>() },
        { nameof(AnotherViewModel), null }
      };

      MessengerInstance.Register<ChangeViewModelMessage>(this, changeViewModel);
    }

    private void changeViewModel(ChangeViewModelMessage msg)
    {
      CurrentViewModel = ViewModels.First(vm => vm.Key == msg.ViewModelName).Value;
    }

    public void Dispose()
    {
      if (_scope != null)
        _scope.Dispose();
    }
  }
}
