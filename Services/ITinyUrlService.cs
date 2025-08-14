using TinyUrl.Models;

namespace TinyUrl.Services
{
    public interface ITinyUrlService
    {
        string AddTinyUrlInDB(string url);

        string RedirectToOriginalPage(string urlCode);

        bool DeleteTinyUrlInDB(string urlCode);

        List<Tinyurldetail> GetTinyUrlDetails();
    }
}
