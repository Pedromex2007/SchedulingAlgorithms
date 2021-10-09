using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class MLFQ : Scheduler {
        RR queue1 = new RR(5);
        RR queue2 = new RR(10);
        FCFS queue3 = new FCFS();

        public MLFQ() {
            queue1.MLFQUsed = true;
            queue2.MLFQUsed = true;
            queue3.MLFQUsed = true;
        }

        public override void BeginSequence() {
            foreach (Process proc in readyQueue) {
                ProcessAdjust(proc);
            }
            readyQueue.Clear();
            RunProcesses();
        }

        /// <summary>
        /// Adjust which queue specified process should go under.
        /// This will be determined by its internal *rank*.
        /// </summary>
        /// <param name="processToDowngrade">Process to adjust.</param>
        public void ProcessAdjust(Process processToDowngrade) {
            switch (processToDowngrade.rank) {
                case 1:
                    queue1.AddProcess(processToDowngrade);
                    break;
                case 2:
                    queue2.AddProcess(processToDowngrade);
                    break;
                case 3:
                    queue3.AddProcess(processToDowngrade);
                    break;
                default:
                    queue3.AddProcess(processToDowngrade);
                    break;
            }
        }

        public void RunProcesses() {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Beginning Queue 1");
            Console.ResetColor();
            queue1.BeginSequence();

            foreach (var proc in queue1.transferList) {
                ProcessAdjust(proc);
            }

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Beginning Queue 2");
            Console.ResetColor();
            queue2.TotalTime = queue1.TotalTime;
            queue2.timeWithoutProcess = queue1.timeWithoutProcess;
            queue2.BeginSequence();

            foreach (var proc in queue2.transferList) {
                ProcessAdjust(proc);
            }

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Beginning Queue 3");
            Console.ResetColor();

            queue3.TotalTime = queue2.TotalTime;
            queue3.timeWithoutProcess = queue2.timeWithoutProcess;
            queue3.BeginSequence();

            foreach (var proc in queue1.waitingQueue) {
                this.waitingQueue.Enqueue(proc);
            }
            foreach (var proc in queue2.waitingQueue) {
                this.waitingQueue.Enqueue(proc);
            }
            foreach (var proc in queue3.waitingQueue) {
                this.waitingQueue.Enqueue(proc);
            }

            this.TotalTime = queue3.TotalTime;
            this.timeWithoutProcess = queue3.timeWithoutProcess;

            CalculateTimes();
        }
    }
}
