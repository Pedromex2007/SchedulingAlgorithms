using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class Process {
        public int processID;
        //public int arrivalTime = 0;

        private readonly int totalIOandBurst = 0;
        private int waitTime = 0;

        public bool firstBurst = true;

        //Correct wait time will be received upon being called for.
        public int WaitTime {
            get {
                waitTime = turnAroundTime - totalIOandBurst;
                return waitTime;
            }
        }

        public int rank = 1; //Used for the MLFQ to identify which queue it should go into.
        private int completeTime;
        /// <summary>
        /// Sets complete time. Since arrival time is always zero, turn around time for this process will always be equal to completion time.
        /// </summary>
        public int CompleteTime { 
            get {
                return completeTime;
            }
            set {
                completeTime = value;
                turnAroundTime = completeTime;
            }
        }
        public int ResponseTime { get; set; }
        public int turnAroundTime = 0;

        private int arrPos = 0;

        /// <summary>
        /// Getter/setter to the current position this process is in.
        /// </summary>
        public int ArrPos { 
            get {
                return arrPos;
            }
            set {
                arrPos = value;
                if(arrPos > burst_times.Length) {
                    arrPos = burst_times.Length;
                }
            }
        }

        //Our burst and IO times.
        public int[] burst_times;
        public int[] io_times;

        /// <summary>
        /// Process constructor. Sets all the nesscary parameter upon instantiation.
        /// </summary>
        /// <param name="id">The ID of this process. Two processes should NOT share the same ID.</param>
        /// <param name="bursts">Array of bursts this process has.</param>
        /// <param name="ios">Array of IOs this process has.</param>
        public Process(int id, int[] bursts, int[] ios) {
            processID = id;
            burst_times = bursts;
            io_times = ios;

            foreach (var item in burst_times) {
                totalIOandBurst += item;
            }
            foreach (var item in io_times) {
                totalIOandBurst += item;
            }
        }
        /// <summary>
        /// Check if this process is on its final burst in the array.
        /// </summary>
        /// <returns></returns>
        public bool IsInFinalBurst() {
            Console.WriteLine("Final burst is: " + burst_times[burst_times.Length-1]);
            if (arrPos >= burst_times.Length-1) {
                return true;
            } else return false;
        }
        /// <summary>
        /// Check to see if this process has completed a burst.
        /// </summary>
        /// <returns></returns>
        public bool IsBurstComplete() {
            if (burst_times[arrPos] == -1) {
                return true;
            } else return false;
        }
        /// <summary>
        /// Check to see if this process has completed its active IO burst.
        /// </summary>
        /// <returns></returns>
        public bool IsIOComplete() {
            try {
                if (io_times[arrPos] == -1) {
                    return true;
                } else return false;
            }
            catch (Exception) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Array size has been exceeded.");
                Console.ResetColor();
                return true;
            }
        }

        /// <summary>
        /// Run a single burst.
        /// </summary>
        public void RunBurst() {
            try {
                burst_times[arrPos]--;
                if (burst_times[arrPos] <= 0) {
                    burst_times[arrPos] = -1;
                }
            }
            catch (Exception) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Position exceeds burst time length.");
                Console.WriteLine("Burst time length: " + burst_times.Length);
                Console.WriteLine("Array position: " + arrPos);
                Console.ResetColor();
                throw;
            }
        }
        /// <summary>
        /// Run a single IO burst.
        /// </summary>
        public void RunIO() {
            try {
                io_times[arrPos]--;
                if (io_times[arrPos] <= 0) {
                    io_times[arrPos] = -1;
                }
            }
            catch (Exception) {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Position exceeds IO time length.");
                Console.WriteLine("IO time length: " + io_times.Length);
                Console.WriteLine("Array position: " + arrPos);
                Console.ResetColor();
                throw;
            }

        }
    }
}
