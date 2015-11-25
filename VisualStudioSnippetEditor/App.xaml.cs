﻿using System.Reflection;
using System.Windows;
using Autofac;
using GalaSoft.MvvmLight;
using VisualStudioSnippetEditor.Contracts;
using VisualStudioSnippetEditor.Model;
using VisualStudioSnippetEditor.Parser;

namespace VisualStudioSnippetEditor
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private static IContainer container;
    public static IContainer Container
    {
      get
      {
        if (container == null)
          Bootstrap();

        return container;
      }

      private set
      {
        container = value;
      }
    }



    private static void Bootstrap()
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      ContainerBuilder builder = new ContainerBuilder();

      // Views
      builder.RegisterType<ApplicationWindow>().SingleInstance();

      // ViewModels
      builder.RegisterAssemblyTypes(assembly)
        .Where((t) => t.IsSubclassOf(typeof(ViewModelBase)))
        .SingleInstance();

      // Entities
      builder.RegisterType<Snippet>().As<ISnippet>();
      builder.RegisterType<SnippetHeader>().As<ISnippetHeader>();
      builder.RegisterType<SnippetLiteral>().As<ISnippetLiteral>();
      builder.RegisterType<SnippetCode>().As<ISnippetCode>();

      // Others
      builder.RegisterType<SnippetXmlReader>().As<ISnippetReader>();

      container = builder.Build();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      ApplicationWindow window = Container.BeginLifetimeScope().Resolve<ApplicationWindow>();
      window.Show();
    }
  }
}