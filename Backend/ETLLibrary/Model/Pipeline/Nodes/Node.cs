
using System.Dynamic;
using ETLBox.DataFlow;

namespace ETLLibrary.Model.Pipeline.Nodes
{
    public abstract class Node
    {
        public int Id { get; set; }
        protected string Name;
    }
}