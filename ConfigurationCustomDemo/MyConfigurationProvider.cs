using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Timers;

namespace ConfigurationCustomDemo
{
    class MyConfigurationProvider : ConfigurationProvider
    {
        readonly Timer timer;

        public MyConfigurationProvider()
        {
            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Load(true);
        }

        private void Load(bool reload)
        {
            this.Data["lastTime"] = DateTime.Now.ToString();
            if (reload)
            {
                base.OnReload();
            }
        }

        public override void Load()
        {
            Load(false);
        }
    }
}
