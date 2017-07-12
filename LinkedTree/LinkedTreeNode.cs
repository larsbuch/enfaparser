using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedTree
{
    public class LinkedTreeNode
    {
        private LinkedTreeNode _root;
        private LinkedTreeNode _parent;
        private List<LinkedTreeNode> _children; // TODO Ambiguity problem
        private LinkedTreeNode _firstOnSameLevel;
        private LinkedTreeNode _next; // TODO Ambiguity problem
        private LinkedTreeNode _previous;

        private string _nonTerminal;
        private List<char> _matched;
        private SourceData _sourceData;
        private int _level;

        public LinkedTreeNode(string nonTerminal, SourceData sourceData) : this(null, nonTerminal, new List<char>(), sourceData)
        { }

        public LinkedTreeNode(LinkedTreeNode root, string nonTerminal, List<char> matched, SourceData sourceData)
        {
            _root = root == null ? this : root;
            _nonTerminal = nonTerminal;
            _matched = matched;
            _sourceData = sourceData;
            _children = new List<LinkedTreeNode>(); // TODO Ambiguity problem
        }

        public LinkedTreeNode Root
        {
            get
            {
                return _root;
            }
        }

        public LinkedTreeNode Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        public List<LinkedTreeNode> Children // TODO Ambiguity problem
        {
            get
            {
                return _children;
            }
        }

        public LinkedTreeNode Next
        {
            get
            {
                return _next;
            }
            set
            {
                _next = value;
            }
        }

        public LinkedTreeNode Previous
        {
            get
            {
                return _previous;
            }
            set
            {
                _previous = value;
            }
        }

        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        public LinkedTreeNode FirstOnSameLevel
        {
            get
            {
                return _firstOnSameLevel;
            }
            set
            {
                _firstOnSameLevel = value;
            }
        }

        public LinkedTreeNode AddChild(string nonTerminal, List<char> matched, SourceData sourceData)
        {
            LinkedTreeNode child = new LinkedTreeNode(Root, nonTerminal, matched, sourceData);
            Children.Add(child); // TODO Ambiguity problem
            child.Parent = this;
            child.Level = Level + 1;
            child.FirstOnSameLevel = child;
            return child;
        }

        public LinkedTreeNode AddNext(string nonTerminal, List<char> matched, SourceData sourceData)
        {
            LinkedTreeNode next = new LinkedTreeNode(Root, nonTerminal, matched, sourceData);
            Next = next; // TODO Ambiguity problem
            next.Previous = this;
            next.Parent = Parent;
            next.Level = Level;
            next.FirstOnSameLevel = FirstOnSameLevel;
            return next;
        }
    }
}
