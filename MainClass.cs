﻿using System;

namespace Project1OS {
    static class MainClass {
        public static void Main(String[] args) {

            /*int[] p1bursts = { 6, 4 };
            int[] p1io = { 10 };
            int[] p2bursts = { 9, 6 };
            int[] p2io = { 15 };
            int[] p3bursts = { 3, 2 };
            int[] p3io = { 5 };*/

            int[] p1bursts = { 5, 3, 5, 4, 6, 4, 3, 5 };
            int[] p1io = { 27, 31, 43, 18, 22, 26, 24 };
            int[] p2bursts = { 4, 5, 7, 12, 9, 4, 9, 7, 8 };
            int[] p2io = { 48, 44, 42, 37, 76, 41, 31, 43 };
            int[] p3bursts = { 8, 12, 18, 14, 4, 15, 14, 5, 6 };
            int[] p3io = {  33, 41, 65, 21, 61, 18, 26, 31 };
            int[] p4bursts = { 3, 4, 5, 3, 4, 5, 6, 5, 3 };
            int[] p4io = { 35, 41, 45, 51, 61, 54, 82, 77 };
            int[] p5bursts = { 16, 17, 5, 16, 7, 13, 11, 6, 3, 4 };
            int[] p5io = { 24, 21, 36, 26, 31, 28, 21, 13, 11, };
            int[] p6bursts = { 11, 4, 5, 6, 7, 9, 12, 15, 8 };
            int[] p6io = {  22, 8, 10, 12, 14, 18, 24, 30 };
            int[] p7bursts = { 14, 17,  11, 15, 4, 7, 16, 10 };
            int[] p7io = {  46, 41, 42, 21, 32, 19, 33 };
            int[] p8bursts = { 4, 5, 6, 14, 16, 6 };
            int[] p8io = { 14, 33, 51, 73, 87 };
            Process p1obj = new Process(1, p1bursts, p1io);
            Process p2obj = new Process(2, p2bursts, p2io);
            Process p3obj = new Process(3, p3bursts, p3io);
            Process p4obj = new Process(4, p4bursts, p4io);
            Process p5obj = new Process(5, p5bursts, p5io);
            Process p6obj = new Process(6, p6bursts, p6io);
            Process p7obj = new Process(7, p7bursts, p7io);
            Process p8obj = new Process(8, p8bursts, p8io);
            FCFS firstcomefirstserve = new FCFS();
            firstcomefirstserve.AddProcess(p1obj);
            firstcomefirstserve.AddProcess(p2obj);
            firstcomefirstserve.AddProcess(p3obj);
            firstcomefirstserve.AddProcess(p4obj);
            firstcomefirstserve.AddProcess(p5obj);
            firstcomefirstserve.AddProcess(p6obj);
            firstcomefirstserve.AddProcess(p7obj);
            firstcomefirstserve.AddProcess(p8obj);
            firstcomefirstserve.BeginSequence();
        }
    }
}