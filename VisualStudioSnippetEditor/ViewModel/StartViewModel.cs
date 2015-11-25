using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using GalaSoft.MvvmLight;
using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor.ViewModel
{
  public class StartViewModel : ViewModelBase
  {
    const string WindowTitleText = "Code Snippets";

    private ILifetimeScope _scope;
    private ObservableCollection<ISnippet> _snippets;
    private ISnippetReader _snippetReader;

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

    public StartViewModel(ILifetimeScope scope, ISnippetReader reader)
    {
      _snippetReader = reader;
      _scope = scope;
      Snippets = new ObservableCollection<ISnippet>();

      scanForSnippets();

    }

    #region Private Methods

    private void scanForSnippets()
    {
      List<Task> tasks = new List<Task>();

      foreach (var snippetFolder in Properties.Settings.Default.SnippetFolders)
      {
        Task task = Task.Run(() =>
        {
          Debug.WriteLine("Starting task ...");
          scanSnippetFolder(snippetFolder);
          Debug.WriteLine("Finished task ...");
        });
        tasks.Add(task);
      }

      Task finalTask = Task.Factory.ContinueWhenAll(
        tasks.ToArray(), (resultTasks) =>
        {
          Debug.WriteLine("Starting finalTask ...");
          Debug.WriteLine("    Element count: " + Snippets.Count);
          Application.Current.Dispatcher.BeginInvoke(
            new Action(() => Snippets.BubbleSortBySnippetName()), DispatcherPriority.DataBind);
          Debug.WriteLine("Finished finalTask ...");
        }
      );
      Debug.WriteLine("Waiting for finalTask ...");
      finalTask.Wait();
      //Task waitTask = Task.WhenAll(tasks.ToArray());      
      //waitTask.Wait();
      //waitTask.ContinueWith((task) => Snippets.BubbleSortBySnippetName());
    }

    private async void scanSnippetFolder(string snippetFolder)
    {
      Debug.WriteLine("Scanning snippet folder: " + snippetFolder);

      try
      {
        DirectoryInfo directory = new DirectoryInfo(snippetFolder);
        if (!directory.Exists)
          throw new DirectoryNotFoundException("Directory not found: " + directory.FullName);

        var snippetsInFolder = directory.GetFiles("*.snippet", SearchOption.TopDirectoryOnly);
        foreach (var snippetInFolder in snippetsInFolder)
        {
          ISnippet snippet = _snippetReader.Parse(snippetInFolder);

          await Application.Current.Dispatcher.BeginInvoke(
            new Action(() => Snippets.Add(snippet)), DispatcherPriority.Normal);
        }

        Debug.WriteLine("Task finished ...");
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error while scanning snippet folders: " + ex.Message);
      }
    }

    #endregion
  }
}
