using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class MLFQ : Scheduler {
        RR queue1 = new RR();
        RR queue2 = new RR();
        FCFS queue3 = new FCFS();
    }
}
