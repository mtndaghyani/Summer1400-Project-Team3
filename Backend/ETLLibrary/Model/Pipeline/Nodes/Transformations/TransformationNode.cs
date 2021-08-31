namespace ETLLibrary.Model.Pipeline.Nodes.Transformations
{
    public abstract class TransformationNode : Node
    {
        public Node Parent { get; set; }
    }
}