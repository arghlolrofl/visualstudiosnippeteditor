﻿namespace VisualStudioSnippetEditor.Contracts
{
  public interface ISnippetLiteral
  {
    string Identifier { get; set; }
    string ToolTip { get; set; }
    string DefaultValue { get; set; }
    string Function { get; set; }
    bool? IsEditable { get; set; }
  }
}
