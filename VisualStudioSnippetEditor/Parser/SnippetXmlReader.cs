using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Autofac;
using VisualStudioSnippetEditor.Contracts;

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

      XElement headerNode = doc.Descendants(getXName("Header")).First();

      parseSnippetHeader(headerNode, snippet.Header);

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

    private XName getXName(string nodeName)
    { return XmlNamespace + nodeName; }
  }
}
