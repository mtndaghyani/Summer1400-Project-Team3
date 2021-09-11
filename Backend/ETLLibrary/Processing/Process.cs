using System.Threading;
using ETLLibrary.Enums;

namespace ETLLibrary.Processing
{
    public class Process
    {
        private string _username;
        private Thread _thread;
        public Status Status { get; set; }
    }
}