namespace Xrm.Domain.ModelBase
{
    public interface INotify
    {
        void OnPropertyChanged(string propertyName);
    }
}
