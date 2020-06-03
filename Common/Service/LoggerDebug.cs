using Common.Service.Interface;
using System;

namespace Common.Service
{
    public class LoggerDebug : ILoggerService
    {
        public void AddLog(string message, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now} {memberName}");
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now} {message}");
        }
    }
}