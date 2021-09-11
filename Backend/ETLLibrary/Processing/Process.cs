using System;
using System.Threading;
using ETLLibrary.Enums;
using ETLLibrary.Model.Pipeline;

namespace ETLLibrary.Processing
{
    public class Process
    {
        private string _username;
        private Pipeline _pipeline;
        public Thread MyThread;
        public Status Status { get; set; }

        public Process(string username, Pipeline pipeline)
        {
            _pipeline = pipeline;
            _username = username;
        }

        public void Run()
        {
            try
            {
                Status = Status.Running;
                _pipeline.Run();
                Status = Status.Finished;
            }
            catch (Exception e)
            {
                Status = Status.Failed;
                throw new Exception(e.Message);
            }
        }

        public void Start()
        {
            var threadStart = new ThreadStart(this.Run);
            MyThread = new Thread(threadStart);
            MyThread.Start();
        }
    }
}