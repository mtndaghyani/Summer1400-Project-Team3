using System;
using System.Collections.Generic;
using ETLLibrary.Model.Pipeline.Nodes;

namespace ETLLibrary.Model.Pipeline
{
    public class Pipeline
    {
        private int _id;
        private string _name;
        private List<Node> _nodes;

        public void Run()
        {
            throw new NotImplementedException("should run the whole pipeline");
        }
    }
}