using VisualStudioSnippetEditor.Enums;

namespace VisualStudioSnippetEditor.Messages
{
  public class ApplicationMessage
  {
    public NotificationKind NotificationKind { get; set; }

    public ApplicationMessage(NotificationKind kind)
    {
      NotificationKind = kind;
    }
  }
}
