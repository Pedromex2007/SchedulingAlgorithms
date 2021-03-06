using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class SJF: Scheduler {
        readonly List<Process> processSorter = new List<Process>();
        public override void BeginSequence() {
            SortProcesses();
            RunProcesses();
            CalculateTimes();
        }
        public override void AddProcess(Process process) {
            processSorter.Add(process);
        }
        /// <summary>
        /// Sort the list of processes in accordance to the shortest bursts being first.
        /// </summary>
        private void SortProcesses() {
            //processSorter.Sort();
 
            foreach (var proc in readyQueue) {
                processSorter.Add(proc);
            }
            readyQueue.Clear();

            processSorter.Sort(delegate (Process c1, Process c2) { return c1.burst_times[c1.ArrPos].CompareTo(c2.burst_times[c2.ArrPos]); });
            foreach (var proc in processSorter) {
                readyQueue.Enqueue(proc);
                Console.WriteLine(proc.ArrPos + " : " + proc.burst_times[proc.ArrPos]);
            }
            processSorter.Clear();
        }
        /// <summary>
        /// Begin the simulation proper and activate each process.
        /// </summary>
        private void RunProcesses() {

            while (true) {

                SortProcesses();
                TotalTime++;
                if (activeProcess == null) {
                    if (readyQueue.Count > 0) {
                        activeProcess = readyQueue.Dequeue();
                    }
                    if (ioQueue.Count <= 0 && readyQueue.Count <= 0 && activeProcess == null) {
                        break;
                    }
                }
                RunBurstCycle();
                RunIOCycle();
                Console.WriteLine("Total time: " + TotalTime);
                //Console.WriteLine("Current Pos: " + activeProcess.arrPos);
            }
        }

        private void RunBurstCycle() {
            if (activeProcess == null) {
                Console.WriteLine("No active process.");
                timeWithoutProcess++;
                return;
            }
            if (activeProcess.firstBurst) {
                activeProcess.ResponseTime = TotalTime;
                activeProcess.firstBurst = false;
            }
            Console.WriteLine("RUNNING CPU, PROCESS" + activeProcess.processID + " : " + activeProcess.burst_times[activeProcess.ArrPos]);
            activeProcess.RunBurst();
            if (activeProcess.IsBurstComplete()) {
                Console.WriteLine("\nProcess complete, transferring to IO queue.");
                if (activeProcess.IsInFinalBurst()) {
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

        private void RunIOCycle() {
            if (ioQueue.Count <= 0) return;
            List<Process> processesToRemove = new List<Process>();

            //Console.WriteLine("IO Time:" + ioQueue.Peek().io_times[ioQueue.Peek().ArrPos]);
            foreach (Process process in ioQueue) {
                Console.WriteLine("IO Time:" + process.io_times[process.ArrPos]);
                process.RunIO();
                if (process.IsIOComplete()) {
                    process.ArrPos++;
                    Console.WriteLine("IO Complete, moving to temporary list to sort.");
                    AddProcess(process);
                    //readyQueue.Enqueue(process);
                    //ioQueue.Remove(process);
                    processesToRemove.Add(process);
                }
            }

            foreach (var process in processesToRemove) {
                ioQueue.Remove(process);
            }
        }
    }
}
