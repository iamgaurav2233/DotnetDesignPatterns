using System;
using System.ComponentModel.Design.Serialization;
using Microsoft.VisualBasic;
namespace DesignPatterns
{
    class Program
    {
        public class Node
        {
            public List<Node?> List { get; set; } = Enumerable.Repeat<Node?>(null, 26).ToList();
            public int Length { get; set; } = 0;
            public bool IsPresent { get; set; } = false;

        }
        class Trie
        {
            private Node RootNode;
            public Trie()
            {
                RootNode = new Node();
            }
            public void InsertString(string s)
            {
                Node? temp = RootNode;
                for (int i = 0; i < s.Length; i++)
                {
                    int charPos = (int)s[i] - 97;
                    temp.List[charPos] ??= new Node();
                    temp = temp.List[charPos];
                    temp!.Length++;
                }
                temp.IsPresent = true;
            }
            public void DeleteString(string s)
            {
                Node? temp = RootNode;
                for (int i = 0; i < s.Length; i++)
                {
                    int charPos = (int)s[i] - 97;
                    if (temp.List[charPos] == null)
                    {
                        throw new ArgumentException("This string does not exist");
                    }
                    temp = temp.List[charPos];
                    if (temp?.Length > 0)
                    {
                        temp.Length--;
                    }
                    else
                    {
                        throw new ArgumentException("This string does not exist");
                    }
                }
                if (temp.IsPresent)
                {
                    temp.IsPresent = false;
                }
                else
                {
                    throw new ArgumentException("This string does not exist");
                }
            }
            public bool IsPresent(string s)
            {
                Node? temp = RootNode;
                for (int i = 0; i < s.Length; i++)
                {
                    int charPos = (int)s[i] - 97;
                    if (temp?.List[charPos] == null)
                    {
                        return false;
                    }
                    temp = temp.List[charPos];
                }
                return temp!.IsPresent;
            }
        }
        static void Main(string[] args)
        {
            Trie trie = new Trie();
            trie.InsertString("abc");
            trie.InsertString("cde");
            Console.WriteLine(trie.IsPresent("abc"));
            Console.WriteLine(trie.IsPresent("abd"));
            trie.DeleteString("abc");
            trie.DeleteString("aaa");
            Console.WriteLine(trie.IsPresent("abc"));
        }
    }
}

