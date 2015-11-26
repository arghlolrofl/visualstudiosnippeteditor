using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using VisualStudioSnippetEditor.Contracts;
using VisualStudioSnippetEditor.Enums;
using VisualStudioSnippetEditor.Messages;

namespace VisualStudioSnippetEditor.ViewModel
{
  public class StartViewModel : ViewModelBase
  {
    const string WindowTitleText = "Code Snippets";

    private ILifetimeScope _scope;
    private ObservableCollection<ISnippet> _snippets;

    private RelayCommand<ISnippet> _editSnippetCommand;
    public RelayCommand<ISnippet> EditSnippetCommand
    {
      get { return _editSnippetCommand; }
      set { _editSnippetCommand = value; RaisePropertyChanged(); }
    }


    #region Properties

    public string WindowTitle
    {
      get { return WindowTitleText; }
    }

    public ObservableCollection<ISnippet> Snippets
    {
      get { return _snippets; }
      set { _snippets = value; RaisePropertyChanged(); }
    }

    #endregion

    public StartViewModel(ILifetimeScope scope)
    {
      MessengerInstance.Register<ApplicationMessage>(this, HandleNotification);

      _scope = scope;
      Snippets = new ObservableCollection<ISnippet>();

      EditSnippetCommand = new RelayCommand<ISnippet>(LaunchSnippetEditor);
    }

    #region Private Methods

    private void HandleNotification(ApplicationMessage message)
    {
      switch (message.NotificationKind)
      {
        case NotificationKind.Initialized:
          scanForSnippets();
          break;
        default:
          break;
      }
    }

    private void scanForSnippets()
    {
      IList<DirectoryInfo> snippetFolders = new List<DirectoryInfo>();

      // Get valid folders to scan for from the config
      foreach (var path in Properties.Settings.Default.SnippetFolders)
      {
        DirectoryInfo snippetDirectory = new DirectoryInfo(path);
        if (!snippetDirectory.Exists)
          throw new DirectoryNotFoundException("Directory not found: " + snippetDirectory.FullName);

        snippetFolders.Add(snippetDirectory);
      }

      // Create and start scan task
      Task<IList<ISnippet>> scanTask = new Task<IList<ISnippet>>(() => scanAllFolders(_scope.BeginLifetimeScope(), snippetFolders));
      scanTask.ContinueWith((t) =>
      {
        // After scanTask has finished, we add the result to the UI property and sort the list
        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
          Snippets.AddRange(t.Result);
          Snippets.BubbleSortBySnippetName();
        }), DispatcherPriority.DataBind);
      });
      scanTask.Start();
      scanTask.Wait();
    }

    private IList<ISnippet> scanAllFolders(ILifetimeScope scope, IList<DirectoryInfo> snippetFolders)
    {
      Task<IList<ISnippet>>[] scanTasks;

      IList<ISnippet> snippets = new List<ISnippet>();

      scanTasks = new Task<IList<ISnippet>>[snippetFolders.Count];
      for (int i = 0; i < scanTasks.Length; i++)
      {
        var folder = snippetFolders[i];
        var reader = scope.Resolve<ISnippetReader>();
        scanTasks[i] = new Task<IList<ISnippet>>(() => scanFolder(folder, reader), TaskCreationOptions.AttachedToParent);
        scanTasks[i].Start();
      }

      Task<IList<ISnippet>>.WaitAll(scanTasks);
      foreach (var scanTask in scanTasks)
      {
        if (scanTask.Result != null)
          snippets.AddRange(scanTask.Result);
      }

      return snippets;
    }

    private IList<ISnippet> scanFolder(DirectoryInfo directory, ISnippetReader snippetReader)
    {
      try
      {
        IList<ISnippet> snippets = new List<ISnippet>();
        var snippetsInFolder = directory.GetFiles("*.snippet", SearchOption.TopDirectoryOnly);
        foreach (var snippetInFolder in snippetsInFolder)
        {
          ISnippet snippet = snippetReader.Parse(snippetInFolder);
          snippets.Add(snippet);
        }

        return snippets;
      }
      catch (Exception)
      {
        return null;
      }
    }

    private void LaunchSnippetEditor(ISnippet snippet)
    {
      MessengerInstance.Send(new ChangeViewModelMessage() { ViewKind = ViewKind.Edit, Parameter = snippet });
    }

    #endregion
  }
}
