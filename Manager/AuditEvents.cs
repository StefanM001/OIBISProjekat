using Common;
using System.Reflection;
using System.Resources;

namespace Manager
{
    public enum AuditEventTypes
    {
        AuthenticationSuccess = 0,
        AuthorizationSuccess = 1,
        AuthorizationFailed = 2,
        ProcessCritical = 3
    }
    public class AuditEvents
    {
        private static ResourceManager resourceManager = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (resourceLock)
                {
                    if (resourceManager == null)
                    {
                        resourceManager = new ResourceManager
                            (typeof(AuditEventFile).ToString(),
                            Assembly.GetExecutingAssembly());
                    }
                    return resourceManager;
                }
            }
        }

        public static string AuthenticationSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.AuthenticationSuccess.ToString());
            }
        }

        public static string AuthorizationSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.AuthorizationSuccess.ToString());
            }
        }

        public static string AuthorizationFailed
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.AuthorizationFailed.ToString());
            }
        }

        public static string AuditCritical
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.ProcessCritical.ToString());
            }
        }
    }
}
