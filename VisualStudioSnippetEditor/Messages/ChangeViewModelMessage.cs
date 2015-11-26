using VisualStudioSnippetEditor.Enums;

namespace VisualStudioSnippetEditor.Messages
{
  public class ChangeViewModelMessage
  {
    public ViewKind ViewKind { get; set; }

    public object Parameter { get; set; }
  }
}
