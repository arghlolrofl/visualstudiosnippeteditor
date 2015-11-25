namespace VisualStudioSnippetEditor.Contracts
{
  public interface ISnippetCode
  {
    ProgrammingLanguage Language { get; set; }
    string Content { get; set; }
  }
}
