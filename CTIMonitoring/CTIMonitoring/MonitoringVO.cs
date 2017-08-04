using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;
namespace VO
{
    [DataContract]
    class MonitoringVO
    {
        public MonitoringVO()
        {
            
        }
        [DataMember]
        private string type;
        [DataMember]
        private string cmd;
        [DataMember]
        private string ret;
        [DataMember]
        private string strResult;


        public void setStrResult(string result)
        {
            this.strResult = result;
        }
        public string getStrResult()
        {
            return this.strResult;
        }

        public void setCmd(string cmd)
        {
            this.cmd = cmd;
        }
        public string getCmd()
        {
            return this.cmd;
        }

        public void setType(string type)
        {
            this.type = type;
        }
        public string getType()
        {
            return this.type;
        }
      

        public void setRet(string ret)
        {
            this.ret = ret;
        }
        public string getRet()
        {
            return this.ret;
        }


    }
}
