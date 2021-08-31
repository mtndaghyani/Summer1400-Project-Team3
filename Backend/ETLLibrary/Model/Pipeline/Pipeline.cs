
using System;
using System.Collections.Generic;
using ETLLibrary.Model.Pipeline.Nodes;
using ETLLibrary.Model.Pipeline.Nodes.Transformations;

namespace ETLLibrary.Model.Pipeline
{
    public class Pipeline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        private List<Node> _nodes = new();
    
        public void Run()
        {
            throw new NotImplementedException("should run the whole pipeline");
        }
    }
}