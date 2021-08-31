
using System.Dynamic;
using ETLBox.DataFlow;

namespace ETLLibrary.Model.Pipeline.Nodes
{
    public abstract class Node
    {
        protected DataFlowSource<ExpandoObject> DataFlow;
        public int Id { get; set; }
        public string Name { get; set; }
    }
}