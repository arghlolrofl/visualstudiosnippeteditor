namespace VisualStudioSnippetEditor.Contracts
{
  public enum SnippetType
  {
    /// <summary>
    /// Simple self expanding snippet
    /// </summary>
    Expansion,

    /// <summary>
    /// Snippet surrounding other code
    /// </summary>
    SurroundsWith,

    /// <summary>
    /// Refactoring Snippet
    /// </summary>
    Refactoring
  }
}
