namespace Notificator.Repository;

public interface IHomeRepository
{
    //public List<StreamerInfoRS> getStreamerInfo();

    public List<Dictionary<string, dynamic>> ReqAuth();
    
}