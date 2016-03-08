using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using EyouSoft.Services.BackgroundServices;
using Microsoft.Practices.Unity;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration;

namespace EyouSoft.SmsWeb
{
    public class Global : HttpApplication
    {
        /// <summary>
        /// 启动 background services
        /// </summary>
        private void LaunchBackgroundServices()
        {
            if (!System.IO.File.Exists(Toolkit.Utils.GetMapPath("/Config/BackgroundServices.txt"))) return;

            var container = (IUnityContainer)Application["container"];

            var backgroundServicesExecutor = (BackgroundServicesExecutor)Application["backgroundServicesExecutor"];

            if (backgroundServicesExecutor == null)
            {
                backgroundServicesExecutor = new BackgroundServicesExecutor(container, BackgroundServicesItem.短信中心);

                Application.Add("backgroundServicesExecutor", backgroundServicesExecutor);

                backgroundServicesExecutor.Start();
            }
        }

        /// <summary>
        /// register controller factory
        /// </summary>
        private void registerControllerFactory()
        {
            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection section = null;
            System.Configuration.Configuration config = null;
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();

            map.ExeConfigFilename = EyouSoft.Toolkit.Utils.GetMapPath("/Config/IDAL.Configuration.xml");
            config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            section = (UnityConfigurationSection)config.GetSection("unity");
            section.Containers.Default.Configure(container);
            Application.Add("container", container);
        }


        protected void Application_Start(object sender, EventArgs e)
        {
            registerControllerFactory();
            LaunchBackgroundServices();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            var backgroundServicesExecutor = (BackgroundServicesExecutor)Application["backgroundServicesExecutor"];

            if (backgroundServicesExecutor != null)
            {
                backgroundServicesExecutor.Stop();
            }
        }
    }
}