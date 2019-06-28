using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace GlobalTurnkey.Models.config
{
    public class ApplicationConfig
    {

        public static ApplicationConfig getInstanceBasedOnSysProp() {
            String configParamStr = Properties.Settings.Default.globalTurnkeySdkConfig;
            ApplicationConfig config;
            if (configParamStr == "production")
            {
                config = ProductionConfig.getInstance();
            }
            else {
                config = TestConfig.getInstance();
            }

            return config;
        }

        public ApplicationConfig() {

        }

        

    }
}