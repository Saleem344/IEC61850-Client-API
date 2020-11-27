using System;
using System.Collections.Generic;

namespace Models
{
    public class Readmultiple_Groups
    {
        public string IpAddress { get; set; }
        public Int16 Port { get; set; }
        public string DeviceName { get; set; }
        public string[] VarAddress { get; set; }
    }
    public class Readsingle_Group
    {
        public string IpAddress { get; set; }
        public Int16 Port { get; set; }
        public string FC { get; set; }
        public string VarAddress { get; set; }
    }

    public class Read_Value
    {
        public string IpAddress { get; set; }
        public Int16 Port { get; set; }
        public string FC { get; set; }
        public string Datatype { get; set; }
        public string VarAddress { get; set; }
    }

    public class Read_Multiplevalues
    {
        public string IpAddress { get; set; }
        public Int16 Port { get; set; }
        public string[] VarAddress { get; set; }
    }
    public class Read_Dataset
    {
        public string IpAddress { get; set; }
        public Int16 Port { get; set; }
        public string VarAddress { get; set; }
    }
    public class Read_Result
    {
        public string Address { get; set; }
        public dynamic Value { get; set; }
        public Int16 Quality { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }

    public class Value_Result
    {
        public string Address { get; set; }
        public dynamic Value { get; set; }
    }


    public class Dataset_Result
    {
        public string Address { get; set; }
        public dynamic Value { get; set; }
    }
    public class WriteCMD
    {
        public string IpAddress { get; set; }
        public Int16 Port { get; set; }
        public bool Operate { get; set; }
        public string VarAddress { get; set; }
    }

    public class WriteValue
    {
        public string IpAddress { get; set; }
        public Int16 Port { get; set; }
        public string VarAddress { get; set; }
        public string FC { get; set; }
        public dynamic NewValue { get; set; }
    }

    public class ResultMultipleModel
    {
        public List<Read_Result> data { get; set; }
        public bool error { get; set; }
        public string errormessage { get; set; }

    }
    public class ResultSingleModel
    {
        public Read_Result data { get; set; }
        public bool error { get; set; }
        public string errormessage { get; set; }

    }

    public class ResultValueModel
    {
        public Value_Result data { get; set; }
        public bool error { get; set; }
        public string errormessage { get; set; }

    }
    public class ResultDatasetModel
    {
        public List<Dataset_Result> data { get; set; }
        public bool error { get; set; }
        public string errormessage { get; set; }

    }

    public class Heartbeat
    {
        public bool Status { get; set; }
    }
    public class Logs
    {
        public ResultMultipleModel Message { get; set; }
    }
}