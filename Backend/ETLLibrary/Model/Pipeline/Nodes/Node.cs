
namespace ETLLibrary.Model.Pipeline.Nodes
{
    public abstract class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public abstract void Execute();
        
    }
}