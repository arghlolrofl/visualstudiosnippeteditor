using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using VisualStudioSnippetEditor.Contracts;

namespace VisualStudioSnippetEditor
{
  public static class ObservableCollectionExtension
  {
    public static void BubbleSortBySnippetName(this IList<ISnippet> list)
    {
      Debug.WriteLine("Sorting ISnippet list with elements: " + list.Count);

      if (list.Count <= 1)
        return;

      for (int i = list.Count - 1; i >= 0; i--)
      {
        for (int j = 1; j <= i; j++)
        {
          ISnippet o1 = list[j - 1];
          ISnippet o2 = list[j];
          if (((IComparable)o1.Name).CompareTo(o2.Name) > 0)
          {
            list.Remove(o1);
            list.Insert(j, o1);
          }
        }
      }
    }

    public static void BubbleSort(this IList list)
    {
      Debug.WriteLine("Sorting list with elements: " + list.Count);

      for (int i = list.Count - 1; i >= 0; i--)
      {
        for (int j = 1; j <= i; j++)
        {
          object o1 = list[j - 1];
          object o2 = list[j];
          if (((IComparable)o1).CompareTo(o2) > 0)
          {
            list.Remove(o1);
            list.Insert(j, o1);
          }
        }
      }
    }
  }
}
