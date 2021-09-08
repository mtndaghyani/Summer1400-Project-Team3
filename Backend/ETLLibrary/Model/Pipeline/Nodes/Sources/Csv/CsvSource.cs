
using System.Dynamic;
using ETLBox.DataFlow.Connectors;

namespace ETLLibrary.Model.Pipeline.Nodes.Sources.Csv
{
    public class CsvSource : SourceNode
    {
        private string _csvLocation;

        public CsvSource(string id, string name, string csvLocation)
        {
            Id = id;
            Name = name;
            _csvLocation = csvLocation;
            CreateDataFlow();
        }

        private void CreateDataFlow()
        {
            CsvSource<ExpandoObject> csvSource = new CsvSource<ExpandoObject>(_csvLocation);
            DataFlow = csvSource;
        }
    }
}