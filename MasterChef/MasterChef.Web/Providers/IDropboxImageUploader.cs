namespace MasterChef.Web.Providers
{
    public interface IDropboxImageUploader
    {
        string UploadImageToDropbox(string url, string fileName);
    }
}