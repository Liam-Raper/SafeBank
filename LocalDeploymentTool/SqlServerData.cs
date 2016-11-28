using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeploymentTool
{
    public class SqlServerData
    {
        public string ServerName;
        public string InstanceName;
        public string IsClustered;
        public string Version;

        public string SqlVersion()
        {
            var majorVersion = int.Parse(Version.Split('.').First());
            switch (majorVersion)
            {
                case 9:
                    return "SQL Server 2005";
                case 10:
                    return "SQL Server 2008";
                case 11:
                    return "SQL Server 2012";
                case 12:
                    return "SQL Server 2014";
                case 13:
                    return "SQL Server 2016";
                default:
                    return null;
            }
        }

        public string ConnectionName()
        {
            return ServerName + "\\" + InstanceName;
        }

    }
}
