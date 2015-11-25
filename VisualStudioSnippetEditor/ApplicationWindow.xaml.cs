using System.Windows;

namespace VisualStudioSnippetEditor
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class ApplicationWindow : Window
  {
    public ApplicationWindow(ApplicationViewModel viewModel)
    {
      InitializeComponent();
      DataContext = viewModel;
    }
  }
}
