using System;
using System.Collections.Generic;
using System.Text;
using CTIMonitoring;


namespace CTIMonitoringTEST
{
    class UseDll : Monitoring
    {
        private Form1 form;

        public UseDll(Form1 form)
        {
            this.form = form;
        }
        public override void GetEventOnMon1(string data)
        {
            form.setData(data);
        }

        public override void GetEventOnMon2(string data)
        {
            throw new NotImplementedException();
        }

        public override void GetEventOnMon3(string data)
        {
            throw new NotImplementedException();
        }

        public override void GetEventOnMon4(string data)
        {
            throw new NotImplementedException();
        }
    }
}
