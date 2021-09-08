using System;
using System.Collections.Generic;
using System.Linq;
using ETLLibrary.Model.Pipeline.Nodes.Destinations;
using ETLLibrary.Model.Pipeline.Nodes.Destinations.Csv;
using ETLLibrary.Model.Pipeline.Nodes.Sources;
using ETLLibrary.Model.Pipeline.Nodes.Sources.Csv;
using ETLLibrary.Model.Pipeline.Nodes.Transformations;
using ETLLibrary.Model.Pipeline.Nodes.Transformations.Aggregations;
using ETLLibrary.Model.Pipeline.Nodes.Transformations.Joins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ETLLibrary.Model.Pipeline
{
    public class PipelineConvertor
    {
        public static Pipeline GetPipelineFromJson(string jsonString)
        {
            Pipeline pipeline = new Pipeline(1 , "");
            JObject jObject = JsonConvert.DeserializeObject<JObject>(jsonString);
            JArray nodes = (JArray) jObject["nodes"];
            CreatePipelineNodes(nodes , pipeline);
            JArray edges = (JArray) jObject["edges"];
            CreateEdges(edges, pipeline);
            return pipeline;
        }

        private static void CreatePipelineNodes(JArray nodes, Pipeline pipeline)
        {
            int nodesSize = nodes.Count;
            for (int i = 0; i < nodesSize; ++i)
            {
                JToken node = nodes[i];
                AddNode(node , pipeline);
            }
        }

        private static void AddNode(JToken node, Pipeline pipeline)
        {
            string id = node["id"].ToString();
            string type = node["type"].ToString();
            if (type == "filter")
            {
                CreateFilterNode(node , pipeline);
            }
            if (type == "join")
            {
                CreateJoinNode(node , pipeline);
            }
            if (type == "aggregate")
            {
                CreateAggregationNode(node , pipeline);
            }
            if (type == "common" && id == "source")
            {
                CreateSourceNode(node , pipeline);
            }
            if (type == "common" && id == "destination")
            {
                CreateDestinationNode(node , pipeline);
            }
        }

        private static void CreateFilterNode(JToken node, Pipeline pipeline)
        {
            
        }
        private static void CreateJoinNode(JToken node, Pipeline pipeline)
        {
            throw new NotImplementedException();
        }
        private static void CreateAggregationNode(JToken node, Pipeline pipeline)
        {
            AggregationType aggregationType = AggregationType.Sum;
            if (node["data"]["operation"].ToString() == "sum")
                aggregationType = AggregationType.Sum;
            else if (node["data"]["operation"].ToString() == "count")
                aggregationType = AggregationType.Count;
            else if (node["data"]["operation"].ToString() == "min")
                aggregationType = AggregationType.Min;
            else if (node["data"]["operation"].ToString() == "max")
                aggregationType = AggregationType.Max;
            else if (node["data"]["operation"].ToString() == "average")
                aggregationType = AggregationType.Average;
            else
                throw new NotImplementedException();
            TransformationNode transformationNode = new AggregationNode(node["id"].ToString() , "" , aggregationType , node["data"]["column"].ToString() , node["data"]["outputName"].ToString() , node["data"]["groupColumns"].ToObject<List<string>>());
            pipeline.AddNode(transformationNode);
        }
        private static void CreateSourceNode(JToken node, Pipeline pipeline)
        {
            SourceNode sourceNode = new CsvSource(node["id"].ToString() , "" , "");
            pipeline.AddNode(sourceNode);
        }
        private static void CreateDestinationNode(JToken node, Pipeline pipeline)
        {
            DestinationNode destinationNode = new CsvDestination(node["id"].ToString() , "" , "");
            pipeline.AddNode(destinationNode);
        }
        
        private static void CreateEdges(JArray edges, Pipeline pipeline)
        {
            
        }
        
    }
}