using Microsoft.AspNetCore.Mvc;
using Models;
using tag_functions;
using MongoDBWebAPI.Models;
using MongoDBWebAPI.Services;
using System;

namespace iec61850
{
    //[Produces("application/json")]
    public class Read : Controller
    {
        private readonly DbService _subSvc;

        public Read(DbService dbService)
        {
            _subSvc = dbService;
        }
        
        [HttpPost]
        [Route("write/cmd")]
        public IActionResult write([FromBody] WriteCMD rawdata)
        {
            string IpAddress = rawdata.IpAddress;
            int Port = rawdata.Port;
            bool Operate = rawdata.Operate;
            string CMDAddress = rawdata.VarAddress;
            Read_Write readwrite = new Read_Write(_subSvc);
            ResultValueModel data = readwrite.WriteCMD(IpAddress, Port, Operate, CMDAddress);
            return Json(data);
        }
        [HttpPost]
        [Route("write/value")]
        public IActionResult writevalue([FromBody] WriteValue rawdata)
        {
            string IpAddress = rawdata.IpAddress;
            int Port = rawdata.Port;
            string VarAddress = rawdata.VarAddress;
            string FC = rawdata.FC;
            dynamic NewValue = rawdata.NewValue;
            Read_Write readwrite = new Read_Write(_subSvc);
            ResultValueModel data = readwrite.WriteValue(IpAddress, Port, VarAddress, FC, NewValue);
            return Json(data);
        }
        [HttpGet]
        [Route("heartbeat")]
        public IActionResult heartbeat()
        {
            Heartbeat heartbeatdata = new Heartbeat(){Status = true};
            return Json(heartbeatdata);

        }
        [HttpGet]
        [Route("read/multiplegroups")]
        public IActionResult readmultiple([FromBody] Readmultiple_Groups rawdata)
        {
            string IpAddress = rawdata.IpAddress;
            int Port = rawdata.Port;
            string DeviceName = rawdata.DeviceName;
            string[] VarAddress = rawdata.VarAddress;
            Read_Write readwrite = new Read_Write(_subSvc);
            ResultMultipleModel data = readwrite.ReadMultipleGroups(IpAddress, Port, DeviceName, VarAddress);
            return Json(data);

        }
        [HttpGet]
        [Route("read/singlegroup")]
        public IActionResult readsingle([FromBody] Readsingle_Group rawdata)
        {
            string IpAddress = rawdata.IpAddress;
            int Port = rawdata.Port;
            string FC = rawdata.FC;
            string VarAddress = rawdata.VarAddress;
            Read_Write readwrite = new Read_Write(_subSvc);
            ResultSingleModel data = readwrite.ReadSingleGroup(IpAddress, Port, FC, VarAddress);
            return Json(data);

        }
        [HttpGet]
        [Route("read/singlevariable")]
        public IActionResult readvalue([FromBody] Read_Value rawdata)
        {
            string IpAddress = rawdata.IpAddress;
            int Port = rawdata.Port;
            string FC = rawdata.FC;
            string Datatype = rawdata.Datatype;
            string VarAddress = rawdata.VarAddress;
            Read_Write readwrite = new Read_Write(_subSvc);
            ResultValueModel data = readwrite.ReadVariableValue(IpAddress, Port, Datatype, FC, VarAddress);
            return Json(data);

        }

        [HttpGet]
        [Route("read/dataset")]
        public IActionResult readdataset([FromBody] Read_Dataset rawdata)
        {
            string IpAddress = rawdata.IpAddress;
            int Port = rawdata.Port;
            string VarAddress = rawdata.VarAddress;
            Read_Write readwrite = new Read_Write(_subSvc);
            ResultDatasetModel data = readwrite.ReadDataset(IpAddress, Port, VarAddress);
            return Json(data);

        }

        [HttpGet]
        [Route("read/readrcb")]
        public IActionResult readrcb([FromBody] Read_Dataset rawdata)
        {
            string IpAddress = rawdata.IpAddress;
            int Port = rawdata.Port;
            string VarAddress = rawdata.VarAddress;
            Read_Write readwrite = new Read_Write(_subSvc);
            ResultDatasetModel data = readwrite.ReadDataset(IpAddress, Port, VarAddress);
            return Json(data);

        }

        [HttpGet]
        [Route("read/multiplevariable")]
        public IActionResult readdataset([FromBody] Read_Multiplevalues rawdata)
        {
            string IpAddress = rawdata.IpAddress;
            int Port = rawdata.Port;
            var VarAddress = rawdata.VarAddress;
            Read_Write readwrite = new Read_Write(_subSvc);
            ResultDatasetModel data = readwrite.ReadMultipleVariableValues(IpAddress, Port, VarAddress);
            return Json(data);

        }
    }
}

