using System;
using System.Dynamic;
using ETLBox.DataFlow;
using ETLLibrary.Model.Pipeline.Nodes.Destinations;

namespace ETLLibrary.Model.Pipeline.Nodes.Transformations
{
    public abstract class TransformationNode : Node
    {
        public DataFlowTransformation<ExpandoObject, ExpandoObject> DataFlow;
        public Node Parent { get; set; }
        public override void LinkTo(Node node)
        {
            try
            {
                DataFlow.LinkTo(((TransformationNode) node).DataFlow);
            }
            catch (InvalidCastException)
            {
            }

            try
            {
                DataFlow.LinkTo(((DestinationNode) node).DataFlow);
            }
            catch (InvalidCastException)
            {
            }
            
        }
    }
}