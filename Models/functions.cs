using System;
using System.Collections.Generic;
using IEC61850.Client;
using IEC61850.Common;
using Models;
using common_functions;
using MongoDBWebAPI.Services;
using MongoDBWebAPI.Models;

namespace tag_functions
{
    public class Read_Write
    {
        private readonly DbService _subSvc;

        public Read_Write(DbService dbService)
        {
            _subSvc = dbService;
        }
        public ResultMultipleModel ReadMultipleGroups(string hostip, Int32 port, string logicaldevicename, string[] vargroup)
        {   //instance
            IedConnection con = new IedConnection();

            try
            {   //connection
                con.Connect(hostip, port);
                //list variable group
                var variable_group = new List<string>();
                
                foreach (string address in vargroup)
                {
                    variable_group.Add(address);

                }
                //mms connecton
                MmsConnection mmsConnection = con.GetMmsConnection();
                //read data
                MmsValue mmsresult = mmsConnection.ReadMultipleVariables(logicaldevicename, variable_group); 
                //connection close
                con.Abort();
                //result list
                data_extract dataExtract = new data_extract();
                dynamic ValueTuple = null;
                Int16 Quality;
                DateTimeOffset Timestamp;
                Read_Result listresult = null;
                
                List<Read_Result> Variableset = new List<Read_Result>();
                int i = 0;
                
                foreach (MmsValue address in mmsresult)
                {
                                                           
                    if(address.Size() == 4)
                    {
                        ValueTuple = dataExtract.ExtractValue(address.GetElement(0));
                        Quality = dataExtract.ExtractValue(address.GetElement(2));
                        Timestamp = dataExtract.ExtractValue(address.GetElement(3));
                        
                    }
                    else 
                    {
                        ValueTuple = dataExtract.ExtractValue(address.GetElement(0));
                        Quality = dataExtract.ExtractValue(address.GetElement(1));
                        Timestamp = dataExtract.ExtractValue(address.GetElement(2));
                    }

                    listresult = new Read_Result
                    {
                        Address = variable_group[i],
                        Value = ValueTuple,
                        Quality = Quality,
                        Timestamp = Timestamp

                    };
                    Variableset.Add(listresult);

                    
                    i = i + 1;
                    
                }
                
                ResultMultipleModel result = new ResultMultipleModel()
                {
                    data = Variableset,
                    error = false,
                    errormessage = null
                };
                //destroy instance
                con.Dispose();
                //result
                return result;
            }
            catch (IedConnectionException e)
            {
                ResultMultipleModel result = new ResultMultipleModel()
                {
                    data = null,
                    error = true,
                    errormessage = e.Message.ToString()
                };

                //insert logs into db
                Submission dbinsert = new Submission
                {
                    CreatedAt = DateTime.Now,
                    Content = e.Message.ToString()
                };
                _subSvc.Create(dbinsert);

                //destroy instance
                con.Dispose();
                //error result
                return result;
            }

        }

        public ResultSingleModel ReadSingleGroup(string hostip, Int32 port, string FC, string vargroup)
        {   //instance
            IedConnection con = new IedConnection();

            try
            {   //connection
                con.Connect(hostip, port);
                //exctract fc
                data_extract dataExtract = new data_extract();
                var FunctionCode = dataExtract.ExtractFC(FC);
                //read group
                MmsValue mmsresult = con.ReadValue(vargroup, FunctionCode);
                //con close
                con.Abort();

                //result list
                dynamic ValueTuple = mmsresult;
                Read_Result listresult = null;
                Int16 Quality;
                DateTimeOffset Timestamp;
                List<dynamic> valueArray = new List<dynamic>();
                
                if (mmsresult.GetType() == MmsType.MMS_STRUCTURE)
                {
                    if(mmsresult.Size() == 4)
                    {
                        ValueTuple = dataExtract.ExtractValue(mmsresult.GetElement(0));
                        Quality = dataExtract.ExtractValue(mmsresult.GetElement(2));
                        Timestamp = dataExtract.ExtractValue(mmsresult.GetElement(3));
                    }
                    else
                    {
                        ValueTuple = dataExtract.ExtractValue(mmsresult.GetElement(0));
                        Quality = dataExtract.ExtractValue(mmsresult.GetElement(1));
                        Timestamp = dataExtract.ExtractValue(mmsresult.GetElement(2));
                    }

                    listresult = new Read_Result
                    {
                        Address = vargroup,
                        Value = ValueTuple,
                        Quality = Quality,
                        Timestamp = Timestamp


                    };
                }
                ResultSingleModel result = new ResultSingleModel()
                {
                    data = listresult,
                    error = false,
                    errormessage = null
                };
                //destroy instance
                con.Dispose();
                //result
                return result;
            }
            catch (IedConnectionException e)
            {
                ResultSingleModel result = new ResultSingleModel()
                {
                    data = null,
                    error = true,
                    errormessage = e.Message.ToString()
                };
                //insert logs into db
                Submission dbinsert = new Submission
                {
                    CreatedAt = DateTime.Now,
                    Content = e.Message.ToString()
                };
                _subSvc.Create(dbinsert);
                //destroy instance
                con.Dispose();
                //error result
                return result;
            }

        }
        public ResultValueModel ReadVariableValue(string hostip, Int32 port, string datatype, string FC, string valueaddress)
        {   //instance
            IedConnection con = new IedConnection();

            try
            {   //connection
                con.Connect(hostip, port);
                //extract fc
                data_extract dataExtract = new data_extract();
                var FunctionCode = dataExtract.ExtractFC(FC);
                dynamic mmsresult = null;
                //validate datatype
                if (datatype == "BOOLEAN")
                    mmsresult = con.ReadBooleanValue(valueaddress, FunctionCode);
                else if (datatype == "FLOAT")
                    mmsresult = con.ReadFloatValue(valueaddress, FunctionCode);
                else if (datatype == "INT")
                    mmsresult = con.ReadIntegerValue(valueaddress, FunctionCode);
                else if (datatype == "STRING")
                    mmsresult = con.ReadStringValue(valueaddress, FunctionCode);
                else if (datatype == "BITSTRING")
                    mmsresult = con.ReadBitStringValue(valueaddress, FunctionCode);

                //con close
                con.Abort();
                //extract value
                dynamic ValueTuple = null;
                ValueTuple = dataExtract.ExtractValue(mmsresult);
                //result
                Value_Result listresult = new Value_Result
                {
                    Address = valueaddress,
                    Value = ValueTuple,
                };
                ResultValueModel result = new ResultValueModel
                {
                    data = listresult,
                    error = false,
                    errormessage = null
                };
                //destroy instance
                con.Dispose();
                //result
                return result;
            }
            catch (IedConnectionException e)
            {
                ResultValueModel result = new ResultValueModel()
                {
                    data = null,
                    error = true,
                    errormessage = e.Message.ToString()
                };
                //insert logs into db
                Submission dbinsert = new Submission
                {
                    CreatedAt = DateTime.Now,
                    Content = e.Message.ToString()
                };
                _subSvc.Create(dbinsert);
                //destroy result
                con.Dispose();
                //result
                return result;
            }

        }
        public ResultDatasetModel ReadMultipleVariableValues(string hostip, Int32 port, string[] valueaddress)
        {   //instance
            IedConnection con = new IedConnection();

            try
            {   //connection
                con.Connect(hostip, port);
                //dataset elements
                List<string> dataSetElements = new List<string>();

                foreach(string address in valueaddress)
                {
                    dataSetElements.Add(address);
                }
                string LDname = dataSetElements[0].Split("/")[0];
                string LNname = dataSetElements[0].Split("/")[1].Split(".")[0];
                string DSname = "ReadValues";
                string dataSetName = LDname+"/"+LNname+"."+DSname;

                //create dataset
                con.CreateDataSet(dataSetName,dataSetElements);
                //read data set
                DataSet mmsresult = con.ReadDataSetValues(dataSetName,null);

                List<string> DataSetDirectory = con.GetDataSetDirectory(dataSetName);
                if(DataSetDirectory != null)
                    //delete dataset
                    con.DeleteDataSet(dataSetName);


                //con close
                con.Abort();

                //get data set values
                List<Dataset_Result> valuelist = new List<Dataset_Result>();
                data_extract dataExtract = new data_extract();
                int i = 0;
                foreach(var value in mmsresult.GetValues())
                {
                    var ValueTuple = dataExtract.ExtractValue(value);
                    
                    valuelist.Add(new Dataset_Result
                    {
                        Address = DataSetDirectory[i],
                        Value = ValueTuple
                    });
                    i+=1;
                }
                ResultDatasetModel result = new ResultDatasetModel
                {
                    data = valuelist,
                    error = false,
                    errormessage = null
                };
                //destroy instance
                con.Dispose();
                //result
                return result;
            }
            catch (IedConnectionException e)
            {
                ResultDatasetModel result = new ResultDatasetModel()
                {
                    data = null,
                    error = true,
                    errormessage = e.Message.ToString()
                };
                //insert logs into db
                Submission dbinsert = new Submission
                {
                    CreatedAt = DateTime.Now,
                    Content = e.Message.ToString()
                };
                _subSvc.Create(dbinsert);

                //destroy result
                con.Dispose();
                //result
                return result;
            }

        }

        public ResultDatasetModel ReadDataset(string hostip, Int32 port, string dataaddress)
        {   //instance
            IedConnection con = new IedConnection();

            try
            {   //connection
                con.Connect(hostip, port);
                //read data set
                DataSet mmsresult = con.ReadDataSetValues(dataaddress,null);

                //get data set values
                List<Dataset_Result> valuelist = new List<Dataset_Result>();
                data_extract dataExtract = new data_extract();
                List<string> DataSetDirectory = con.GetDataSetDirectory(dataaddress);
                int i = 0;
                foreach(MmsValue value in mmsresult.GetValues())
                {
                    var ValueTuple = dataExtract.ExtractValue(value);
                    valuelist.Add(new Dataset_Result
                    {
                        Address = DataSetDirectory[i],
                        Value = ValueTuple
                    });
                    i+=1;
                
                }
                //con close
                con.Abort();
                
                ResultDatasetModel result = new ResultDatasetModel
                {
                    data = valuelist,
                    error = false,
                    errormessage = null
                };
                //destroy instance
                con.Dispose();
                //result
                return result;
            }
            catch (IedConnectionException e)
            {
                ResultDatasetModel result = new ResultDatasetModel()
                {
                    data = null,
                    error = true,
                    errormessage = e.Message.ToString()
                };
                //insert logs into db
                Submission dbinsert = new Submission
                {
                    CreatedAt = DateTime.Now,
                    Content = e.Message.ToString()
                };
                _subSvc.Create(dbinsert);
                //destroy result
                con.Dispose();
                //result
                return result;
            }
            

        }

        public ResultDatasetModel ReadRCB(string hostip, Int32 port, string dataaddress)
        {   //instance
            IedConnection con = new IedConnection();

            try
            {   //connection
                con.Connect(hostip, port);
                //read data set
                // create a new data set

				List<string> dataSetElements = new List<string>();

				dataSetElements.Add("simpleIOGenericIO/GGIO1.AnIn1.mag.f[MX]");
				dataSetElements.Add("simpleIOGenericIO/GGIO1.AnIn2.mag.f[MX]");
				dataSetElements.Add("simpleIOGenericIO/GGIO1.AnIn3.mag.f[MX]");
				dataSetElements.Add("simpleIOGenericIO/GGIO1.AnIn4.mag.f[MX]");

				// permanent (domain specific) data set
				//string dataSetReference = "simpleIOGenericIO/LLN0.ds1";

				// temporary (association specific) data set
				string dataSetReference = "@ss";

                // Note: this function will throw an exception when a data set with the same name already exists
				con.CreateDataSet(dataSetReference, dataSetElements);

				// reconfigure existing RCB with new data set

				string rcbReference = "simpleIOGenericIO/LLN0.RP.EventsRCB01";

				ReportControlBlock rcb = con.GetReportControlBlock(rcbReference);

				rcb.GetRCBValues();

				// note: the second parameter is not required!
				rcb.InstallReportHandler(reportHandler, rcb);

				string rcbDataSetReference = dataSetReference.Replace('.', '$');

				rcb.SetDataSetReference(rcbDataSetReference);
				rcb.SetTrgOps(TriggerOptions.DATA_CHANGED | TriggerOptions.INTEGRITY);				
				rcb.SetIntgPd(5000);
				rcb.SetRptEna(true);

				rcb.SetRCBValues();

                // Console.WriteLine(mmsresult.GetRCBValues());
                List<Dataset_Result> valuelist = new List<Dataset_Result>();
                data_extract dataExtract = new data_extract();

                //con close
                con.Abort();
                
                ResultDatasetModel result = new ResultDatasetModel
                {
                    data = valuelist,
                    error = false,
                    errormessage = null
                };
                //destroy instance
                con.Dispose();
                //result
                return result;
            }
            catch (IedConnectionException e)
            {
                ResultDatasetModel result = new ResultDatasetModel()
                {
                    data = null,
                    error = true,
                    errormessage = e.Message.ToString()
                };
                //insert logs into db
                Submission dbinsert = new Submission
                {
                    CreatedAt = DateTime.Now,
                    Content = e.Message.ToString()
                };
                _subSvc.Create(dbinsert);
                //destroy result
                con.Dispose();
                //result
                return result;
            }
        }
        public ResultValueModel WriteValue(string hostip, Int32 port, string varaddress, string FC, dynamic newvalue)
        {   //instance
            IedConnection con = new IedConnection();

            try
            {   //connection
                con.Connect(hostip, port);
                //extract fc
                data_extract dataExtract = new data_extract();
                var FunctionCode = dataExtract.ExtractFC(FC);
                var Value = dataExtract.ExtractValue(newvalue);
                dynamic mmsresult = null;
                //validate datatype
                mmsresult =  con.WriteValue(varaddress, FunctionCode, new MmsValue (Value));
                //con close
                con.Abort();
                //result
                Value_Result listresult = new Value_Result
                {
                    Address = varaddress,
                    Value = mmsresult,
                };
                ResultValueModel result = new ResultValueModel
                {
                    data = listresult,
                    error = false,
                    errormessage = null
                };
                //destroy instance
                con.Dispose();
                //result
                return result;
            }
            catch (IedConnectionException e)
            {
                ResultValueModel result = new ResultValueModel()
                {
                    data = null,
                    error = true,
                    errormessage = e.Message.ToString()
                };
                //insert logs into db
                Submission dbinsert = new Submission
                {
                    CreatedAt = DateTime.Now,
                    Content = e.Message.ToString()
                };
                _subSvc.Create(dbinsert);
                //destroy result
                con.Dispose();
                //result
                return result;
            }

        }
        public ResultValueModel WriteCMD(string hostip, Int32 port, bool operate, string cmdaddress)
        {   //instance
            IedConnection con = new IedConnection();

            try
            {   //connection
                con.Connect(hostip, port);
                //create control object
                ControlObject control = con.CreateControlObject(cmdaddress);
                ControlModel controlModel = control.GetControlModel();
                //result variable
                dynamic ValueTuple = null;

                //validate control model type
                if (controlModel == ControlModel.DIRECT_ENHANCED)
                {
                    control.SetCommandTerminationHandler(commandTerminationHandler, null);
                    ValueTuple = control.Operate(operate);
                }
                else if (controlModel == ControlModel.DIRECT_NORMAL)
                {
                    control.SetCommandTerminationHandler(commandTerminationHandler, null);
                    ValueTuple = control.Operate(operate);
                }
                else if (controlModel == ControlModel.SBO_ENHANCED)
                {
                    //set handler
                    control.SetCommandTerminationHandler(commandTerminationHandler, null);
                    //set SynchroCheck
                    control.SetSynchroCheck(true);
                    //set InterlockCheck
                    control.SetInterlockCheck(true);
                    //validate  cmd select
                    if (control.SelectWithValue(true))
                    {
                        //operate  cmd 
                        ValueTuple = control.Operate(operate);
                    }
                    else if (control.SelectWithValue(false))
                    {
                        //operate  cmd 
                        ValueTuple = control.Operate(operate);
                    }
                    else if(control.Select())
                    {
                        //operate  cmd 
                        ValueTuple = control.Operate(operate);
                    }
                    else
                    {
                        ValueTuple = "SBO cmd not selected!";
                        //insert logs into db
                        Submission dbinsert = new Submission
                        {
                            CreatedAt = DateTime.Now,
                            Content = ValueTuple
                        };
                        _subSvc.Create(dbinsert);
                    }

                }
                else
                {
                    ValueTuple = "CMD type is not DIRECT_ENHANCED or DIRECT_NORMAL or SBO_ENHANCED kindly Check!";
                    //insert logs into db
                    Submission dbinsert = new Submission
                    {
                        CreatedAt = DateTime.Now,
                        Content = ValueTuple
                    };
                    _subSvc.Create(dbinsert);
                }

                //con close
                con.Abort();

                //result
                Value_Result listresult = new Value_Result
                {
                    Address = cmdaddress,
                    Value = ValueTuple,
                };
                ResultValueModel result = new ResultValueModel
                {
                    data = listresult,
                    error = false,
                    errormessage = null
                };
                //destroy instance
                con.Dispose();
                //result
                return result;
            }
            catch (IedConnectionException e)
            {
                ResultValueModel result = new ResultValueModel()
                {
                    data = null,
                    error = true,
                    errormessage = e.Message.ToString()
                };
                //insert logs into db
                Submission dbinsert = new Submission
                {
                    CreatedAt = DateTime.Now,
                    Content = e.Message.ToString()
                };
                _subSvc.Create(dbinsert);
                //destroy result
                con.Dispose();
                //result
                return result;
            }

        }
        private static void commandTerminationHandler(Object parameter, ControlObject control)
        {
            LastApplError lastApplError = control.GetLastApplError();
            Console.WriteLine("HANDLER CALLED! " + lastApplError.addCause);
        }

        private static void reportHandler (Report report, object parameter)
		{
			Console.WriteLine ("Received report:\n----------------");

			if (report.HasTimestamp ())
				Console.WriteLine ("  timestamp: " + MmsValue.MsTimeToDateTimeOffset (report.GetTimestamp ()).ToString ());

			MmsValue values = report.GetDataSetValues ();

			Console.WriteLine ("  report dataset contains " + values.Size () + " elements");

			for (int i = 0; i < values.Size(); i++) {
				if (report.GetReasonForInclusion(i) != ReasonForInclusion.REASON_NOT_INCLUDED) {
					Console.WriteLine("    element " + i + " included for reason " + report.GetReasonForInclusion(i).ToString() + " " + values.GetElement(i));
				}
			}
		}
    }

}
