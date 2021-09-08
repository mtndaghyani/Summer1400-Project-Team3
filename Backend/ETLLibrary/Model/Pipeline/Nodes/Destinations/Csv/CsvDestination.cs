using System.Dynamic;
using ETLBox.DataFlow.Connectors;

namespace ETLLibrary.Model.Pipeline.Nodes.Destinations.Csv
{
    public class CsvDestination : DestinationNode
    {
        private string _fileLocation;

        public CsvDestination(string id, string name, string fileLocation)
        {
            Id = id;
            Name = name;
            _fileLocation = fileLocation;
            CreateDataFlow();
        }

        private void CreateDataFlow()
        {
            CsvDestination<ExpandoObject> csvDestination = new CsvDestination<ExpandoObject>(_fileLocation);
            DataFlow = csvDestination;
        }
    }
}