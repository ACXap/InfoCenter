using Common.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common.Service
{
    public static class UserAgentService
    {
        private static List<EntityUserAgent> _collectionUserAgent = Init();

        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36";
        private static readonly object _lock = new object();

        private static List<EntityUserAgent> Init()
        {
            var file = "userAgent.ini";
            if (File.Exists(file))
            {
                try
                {
                    return File.ReadAllLines(file).Select(x =>
                    {
                        return new EntityUserAgent() { UserAgent = x };
                    }).ToList();
                }
                catch { }
            }

            return null;
        }

        public static string GetUserAgent()
        {
            if (_collectionUserAgent == null || !_collectionUserAgent.Any()) return USER_AGENT;

            string userAgent = null;
            lock (_lock)
            {
                var count = _collectionUserAgent.Min(x => x.CountUse);
                var agent = _collectionUserAgent.FirstOrDefault(x => x.CountUse == count);
                agent.CountUse++;
                userAgent = agent.UserAgent;
            }

            return userAgent;
        }
    }
}