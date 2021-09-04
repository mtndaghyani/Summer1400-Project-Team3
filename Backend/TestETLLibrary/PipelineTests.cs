using System;
using System.Collections.Generic;
using ETLLibrary.Model.Pipeline;
using ETLLibrary.Model.Pipeline.Nodes.Destinations.Csv;
using ETLLibrary.Model.Pipeline.Nodes.Sources;
using ETLLibrary.Model.Pipeline.Nodes.Sources.Csv;
using ETLLibrary.Model.Pipeline.Nodes.Transformations;
using ETLLibrary.Model.Pipeline.Nodes.Transformations.Aggregations;
using Xunit;

namespace TestETLLibrary
{
    public class PipelineTests
    {
        [Fact]
        public void Test1() // just for my local testing
        {
            Pipeline pipeline = new Pipeline(1 , "sample");
            CsvSource csvSource = new CsvSource(1, "source from sample csv", "demo.csv");
            AggregationNode aggregationNode =
                new AggregationNode(2, "simple sum", AggregationType.Sum, "Period", new List<string>(){} );
            CsvDestination csvDestination = new CsvDestination(3, "sample destination", "modified.csv");
            pipeline.AddNode(csvSource);
            pipeline.AddNode(csvDestination);
            pipeline.AddNode(aggregationNode);
            pipeline.LinkNodes(1 , 2);
            pipeline.LinkNodes(2 , 3);
            pipeline.Run();
        }
    }
}