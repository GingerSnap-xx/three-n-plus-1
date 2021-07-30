using System.Collections.Generic;

namespace three_n_plus_one
{
    public class Result {
        public long Subject {get;set;}
        public long Peak {get;set;}
        public long Steps {get;set;}
        public List<long> Hailstones {get;set;} = new List<long>();
    }
}