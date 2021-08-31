namespace ETLLibrary.Model.Pipeline.Nodes.Destinations
{
    public abstract class DestinationNode : Node
    {
        public Node Parent { get; set; }
    }
}