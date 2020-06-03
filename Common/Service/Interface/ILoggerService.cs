using System.Runtime.CompilerServices;

namespace Common.Service.Interface
{
    public interface ILoggerService
    {
        void AddLog(string message, [CallerMemberName] string memberName = "");
    }
}