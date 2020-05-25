namespace Fssp.Service.Interface
{
    public interface IServiceFio
    {
        bool CheckFio(string fio);
        string[] GetFio(string fio);
    }
}