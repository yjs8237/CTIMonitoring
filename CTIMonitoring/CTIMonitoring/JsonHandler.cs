using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using VO;
namespace JSON
{
    class JsonHandler
    {
           
        private string extension;
        private DataContractJsonSerializer jsonSer;
        private MemoryStream stream1;
        private MonitoringVO monVO;

        public JsonHandler()
        {
            monVO = new MonitoringVO();
        }

        public void setType(string type)
        {
            monVO.setType(type);    
        }
        

        public void setCmd(string cmd)
        {
            monVO.setCmd(cmd);
        }

        public string getJsonData()
        {
            stream1 = new MemoryStream();
            jsonSer = new DataContractJsonSerializer(typeof(MonitoringVO));
            jsonSer.WriteObject(stream1, monVO);

            stream1.Position = 0;
            StreamReader reader = new StreamReader(stream1);

            string returnData = reader.ReadToEnd();

            stream1.Close();
            reader.Close();

            return returnData;
        }

        public MonitoringVO recvJson(string jsonData)
        {
            MonitoringVO monitoring = new MonitoringVO();
            stream1 = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            //jsonSer = new DataContractJsonSerializer(phonePad.GetType());
            jsonSer = new DataContractJsonSerializer(typeof(MonitoringVO));
            monitoring = (MonitoringVO)jsonSer.ReadObject(stream1);

            stream1.Close();

            return monitoring;
        }
    }

}
