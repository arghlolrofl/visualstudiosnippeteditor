using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisualStudioSnippetEditor.Contracts;
using VisualStudioSnippetEditor.Enums;
using VisualStudioSnippetEditor.Parser;

namespace VisualStudioSnippetEditor.Test
{
  [TestClass]
  public class ISnippetReaderFixture
  {
    const string SampleSnippetPath = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC#\Snippets\1033\Visual C#\ctor.snippet";

    [TestMethod]
    public void VerifyParseHeaderIsWorkingTest()
    {
      // Arrange
      ISnippetReader reader = new SnippetXmlReader(App.Container.BeginLifetimeScope());
      FileInfo snippetFile = new FileInfo(SampleSnippetPath);

      // Act
      ISnippet snippet = reader.Parse(snippetFile);

      // Assert
      Assert.AreEqual("Microsoft Corporation", snippet.Header.Author);
      Assert.AreEqual("ctor", snippet.Header.Title);
      Assert.AreEqual("ctor", snippet.Header.Shortcut);
      Assert.AreEqual("Code snippet for constructor", snippet.Header.Description);
      Assert.AreEqual(1, snippet.Header.SnippetTypes.Count);
      Assert.AreEqual(SnippetType.Expansion, snippet.Header.SnippetTypes.First());
    }

    [TestMethod]
    public void VerifyParseLiteralsIsWorkingTest()
    {
      // Arrange
      ISnippetReader reader = new SnippetXmlReader(App.Container.BeginLifetimeScope());
      FileInfo snippetFile = new FileInfo(SampleSnippetPath);

      // Act
      ISnippet snippet = reader.Parse(snippetFile);

      // Assert
      Assert.AreEqual(1, snippet.Literals.Count);
      Assert.AreEqual("classname", snippet.Literals.First().Identifier);
      Assert.AreEqual("Class name", snippet.Literals.First().ToolTip);
      Assert.AreEqual("ClassName()", snippet.Literals.First().Function);
      Assert.AreEqual("ClassNamePlaceholder", snippet.Literals.First().DefaultValue);
      Assert.IsFalse(snippet.Literals.First().IsEditable.Value);
    }

    [TestMethod]
    public void VerifyParseCodeIsWorkingTest()
    {
      // Arrange
      ISnippetReader reader = new SnippetXmlReader(App.Container.BeginLifetimeScope());
      FileInfo snippetFile = new FileInfo(SampleSnippetPath);

      // Act
      ISnippet snippet = reader.Parse(snippetFile);
      string[] lines = snippet.Code.Content.Split('\n');

      // Assert
      Assert.AreEqual(ProgrammingLanguage.csharp, snippet.Language);
      Assert.AreEqual(4, lines.Length);
      Assert.AreEqual("public $classname$ ()", lines[0]);
      Assert.AreEqual("\t{", lines[1]);
      Assert.AreEqual("\t\t$end$", lines[2]);
      Assert.AreEqual("\t}", lines[3]);
    }
  }
}
