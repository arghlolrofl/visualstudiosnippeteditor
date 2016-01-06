using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Autofac;
using VisualStudioSnippetEditor.Contracts;
using VisualStudioSnippetEditor.Enums;

namespace VisualStudioSnippetEditor.Parser
{
  public class SnippetXmlReader : ISnippetReader
  {
    static XNamespace XmlNamespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet";
    ILifetimeScope _scope;
    FileInfo _targetFile;

    public SnippetXmlReader(ILifetimeScope scope)
    {
      _scope = scope;
    }

    public ISnippet Parse(FileInfo targetFile)
    {
      if (!targetFile.Exists)
        throw new FileNotFoundException("File not found!", targetFile.FullName);

      _targetFile = targetFile;


      ISnippet snippet = _scope.Resolve<ISnippet>();
      XDocument doc = XDocument.Load(targetFile.FullName);
      var format = doc.Root.Element(getXName("CodeSnippet")).Attribute("Format");
      snippet.Format = Version.Parse(format.Value);

      XElement headerNode = doc.Descendants(getXName("Header")).First();
      IEnumerable<XElement> literalNodes = doc.Descendants(getXName("Literal"));
      XElement codeNode = doc.Descendants(getXName("Code")).First();

      parseSnippetHeader(headerNode, snippet.Header);
      parseLiterals(literalNodes, snippet.Literals);
      parseCode(codeNode, snippet.Code);

      return snippet;
    }

    private void parseSnippetHeader(XElement headerNode, ISnippetHeader header)
    {
      XElement titleNode = headerNode.Element(getXName("Title"));
      XElement authorNode = headerNode.Element(getXName("Author"));
      XElement shortcutNode = headerNode.Element(getXName("Shortcut"));
      XElement descriptionNode = headerNode.Element(getXName("Description"));
      XElement typesNode = headerNode.Element(getXName("SnippetTypes"));

      header.Title = titleNode.Value;
      header.Author = authorNode.Value;
      header.Shortcut = shortcutNode.Value;
      header.Description = descriptionNode.Value;
      foreach (var element in typesNode.Elements())
      {
        SnippetType snippetType = (SnippetType)Enum.Parse(typeof(SnippetType), element.Value);
        header.SnippetTypes.Add(snippetType);
      }
    }

    private void parseLiterals(IEnumerable<XElement> literalNodes, ObservableCollection<ISnippetLiteral> literals)
    {
      foreach (XElement literalNode in literalNodes)
      {
        XAttribute editableAttribute = literalNode.Attribute("Editable");
        XElement idNode = literalNode.Element(getXName("ID"));
        XElement tooltipNode = literalNode.Element(getXName("ToolTip"));
        XElement defaultNode = literalNode.Element(getXName("Default"));
        XElement functionNode = literalNode.Element(getXName("Function"));

        ISnippetLiteral literal = _scope.Resolve<ISnippetLiteral>();
        literal.Identifier = idNode.Value;
        literal.DefaultValue = defaultNode?.Value;
        literal.ToolTip = tooltipNode?.Value;
        literal.Function = functionNode?.Value;
        if (editableAttribute != null)
          literal.IsEditable = bool.Parse(editableAttribute?.Value);

        literals.Add(literal);
      }
    }

    private void parseCode(XElement codeNode, ISnippetCode code)
    {
      XAttribute languageAttribute = codeNode.Attribute("Language");
      ProgrammingLanguage lang;
      Enum.TryParse(languageAttribute.Value, out lang);
      if (lang != ProgrammingLanguage.None)
        code.Language = lang;

      code.Content = codeNode.Value;
    }

    private XName getXName(string nodeName)
    { return XmlNamespace + nodeName; }
  }
}
