using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    abstract class Scheduler {
        protected Process activeProcess;
        public int timeWithoutProcess = 0;
        public int totalTime = 0;

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


        protected void CalculateTimes() {
            float CPUUtil = (float)timeWithoutProcess / totalTime;
            CPUUtil = 1 - CPUUtil;
            Console.WriteLine("CPU Utilization: " + CPUUtil);

            Console.WriteLine("Tr times");
            foreach (var process in waitingQueue) {
                Console.WriteLine(process.processID + " : " + process.turnAroundTime);
            }
            Console.WriteLine("Wt times");
            foreach (var process in waitingQueue) {
                //process.waitTime--;
                Console.WriteLine(process.processID + " : " + process.WaitTime);
            }
        }

    }
}
