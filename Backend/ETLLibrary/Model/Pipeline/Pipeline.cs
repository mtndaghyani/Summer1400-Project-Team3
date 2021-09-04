using System;
using System.Collections.Generic;
using System.Linq;
using ETLBox.DataFlow;
using ETLLibrary.Model.Pipeline.Nodes;
using ETLLibrary.Model.Pipeline.Nodes.Sources;

namespace ETLLibrary.Model.Pipeline
{
    public class Pipeline
    {
        private int _id;
        private string _name;
        private List<Node> _nodes = new ();

        public Pipeline(int id, string name)
        {
            _id = id;
            _name = name;
        }
        public void AddNode(Node node)
        {
            _nodes.Add(node);
        }

        public void LinkNodes(int sourceId, int destinationId)
        {
            _nodes.First(x => x.Id == sourceId).LinkTo(_nodes.First(x => x.Id == destinationId));
        }

        public void Run()
        {
            foreach (var node in _nodes)
            {
                try
                {
                    Network.Execute(((SourceNode) node).DataFlow);
                }
                catch (InvalidCastException)
                {
                    
                }
            }
        }
    }
}