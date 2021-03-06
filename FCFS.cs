using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class FCFS : Scheduler {
        public bool MLFQUsed = false;
        public override void BeginSequence() {
            RunProcesses();
            CalculateTimes();
        }
        /// <summary>
        /// Begin the simulation proper and activate each process.
        /// </summary>
        public void RunProcesses() {
            while (true) {
                //if (readyQueue.Count <= 0 && activeProcess == null) {
                //    break;
                //}
                TotalTime++;
                if (activeProcess == null) {
                    if (readyQueue.Count > 0) {
                        activeProcess = readyQueue.Dequeue();
                    }  
                    if(ioQueue.Count <= 0 && readyQueue.Count <= 0 && activeProcess == null) {
                        break;
                    }
                }
                RunBurstCycle();
                RunIOCycle();
                Console.WriteLine("Total time: " + TotalTime);
                //Console.WriteLine("Current Pos: " + activeProcess.arrPos);
            }
            
        }
        /// <summary>
        /// Begin a CPU burst cycle.
        /// </summary>
        private void RunBurstCycle() {
            if (activeProcess == null) {
                Console.WriteLine("No active process.");
                timeWithoutProcess++;
                return;
            }
            if(activeProcess.firstBurst) {
                activeProcess.ResponseTime = TotalTime;
                activeProcess.firstBurst = false;
            }
            Console.WriteLine("RUNNING CPU, PROCESS" + activeProcess.processID + " : " + activeProcess.burst_times[activeProcess.ArrPos]);
            activeProcess.RunBurst();
            if(activeProcess.IsBurstComplete()) {
                Console.WriteLine("\nProcess complete, transferring to IO queue.");
                if(activeProcess.IsInFinalBurst()) {
                    //Do NOT put it in IO queue, the process is done completely.
                    waitingQueue.Enqueue(activeProcess);
                    activeProcess.CompleteTime = TotalTime;
                    Console.WriteLine("Process has finished final burst and has been placed into waiting queue.");
                    //Console.WriteLine("Finished last burst at: " + totalTime);
                } else {
                    ioQueue.Add(activeProcess);
                }
                activeProcess = null;
            }
        }
        /// <summary>
        /// Begin an IO burst cycle.
        /// </summary>
        private void RunIOCycle() {
            if (ioQueue.Count <= 0) return;
            List<Process> processesToRemove = new List<Process>();

            //Console.WriteLine("IO Time:" + ioQueue.Peek().io_times[ioQueue.Peek().ArrPos]);
            foreach (Process process in ioQueue) {
                Console.WriteLine("IO Time:" + process.io_times[process.ArrPos]);
                process.RunIO();
                if (process.IsIOComplete()) {
                    process.ArrPos++;
                    Console.WriteLine("IO Complete, moving to ready queue.");
                    readyQueue.Enqueue(process);
                    processesToRemove.Add(process);
                }
            }

            foreach (var process in processesToRemove) {
                ioQueue.Remove(process);
            }
        }
    }
}
