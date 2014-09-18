namespace MasterChef.Web.Providers
{
    public interface IImageUploadProvider
    {
        string UploadImage(string sourceUrl, string fileName);
    }
}