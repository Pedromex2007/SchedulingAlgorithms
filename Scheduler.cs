using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    abstract class Scheduler {
        protected Process activeProcess;
        public int timeWithoutProcess = 0;
        public int TotalTime { get; set; }

        protected Queue<Process> readyQueue = new Queue<Process>();
        protected List<Process> ioQueue = new List<Process>();


        public Queue<Process> waitingQueue = new Queue<Process>(); //Processes that completed their bursts entirely.
        public List<Process> transferList = new List<Process>(); //Processes that will be transferred to another scheduler. (used for MLQ)

        /// <summary>
        /// Begin a scheduler's simulation. This will print the waiting time, turn around time, and receive time.
        /// </summary>
        abstract public void BeginSequence();

        /// <summary>
        /// Add process to the scheduler. How this will be done depends on how this method was implemented.
        /// Therefore, ALWAYS use this method to add a process to a scheduler.
        /// </summary>
        /// <param name="process">Process to add to scheduler.</param>
        public virtual void AddProcess(Process process) {
            readyQueue.Enqueue(process);
        }

        /// <summary>
        /// Print the Wt, Tr, and Ttr times for each process, along with the average and CPU utilization for scheduler.
        /// </summary>
        protected void CalculateTimes() {
            float CPUUtil = (float)timeWithoutProcess / TotalTime;
            CPUUtil = 1 - CPUUtil;
            float TtrAvg = 0;
            float WtAvg = 0;
            float TrAvg = 0;
            Console.WriteLine("CPU Utilization: " + CPUUtil);

            Console.WriteLine("Ttr times");
            foreach (var process in waitingQueue) {
                Console.WriteLine(process.processID + " : " + process.turnAroundTime);
                TtrAvg += process.turnAroundTime;

            }
            Console.WriteLine("Wt times");
            foreach (var process in waitingQueue) {
                Console.WriteLine(process.processID + " : " + process.WaitTime);
                WtAvg += process.WaitTime;
            }
            Console.WriteLine("Tr times");
            foreach (var process in waitingQueue) {
                process.ResponseTime--;
                if (process.ResponseTime < 0) process.ResponseTime = 0;
                Console.WriteLine(process.processID + " : " + process.ResponseTime);
                TrAvg += process.ResponseTime;
            }

            Console.WriteLine("Avg Ttr time: " + TtrAvg / waitingQueue.Count);

            Console.WriteLine("Avg Wt times: " + WtAvg / waitingQueue.Count);

            Console.WriteLine("Avg Tr times: " + TrAvg / waitingQueue.Count);
        }

    }
}
