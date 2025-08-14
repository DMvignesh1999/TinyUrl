using System;
using TinyUrl.Models;
using TinyUrl.Services;

namespace TinyUrlDetail.Services
{
    public class TinyUrlService : ITinyUrlService
    {
        private readonly AppDbContext appDbContext;
        public TinyUrlService(AppDbContext appDbContext) {        
            this.appDbContext = appDbContext;
        }

        public string AddTinyUrlInDB(string url) {
            if (string.IsNullOrEmpty(url)) {
                return string.Empty;
            }
            var random = new Random();
            int urlCode = random.Next(100000, 1000000);
            using (var context = new AppDbContext())
            {
                var newUrlDetails = new Tinyurldetail
                {
                    OriginalUrl = url,
                    UrlCode = urlCode.ToString(),
                    IsActive = true
                };

                context.Tinyurldetails.Add(newUrlDetails);
                context.SaveChanges();
            }

            return urlCode.ToString();
        }

        public string RedirectToOriginalPage(string urlCode)
        {
            if (string.IsNullOrEmpty(urlCode))
            {
                return string.Empty;
            }

            var originalUrl = (from tinyUrlDetail in appDbContext.Tinyurldetails
                              where urlCode == tinyUrlDetail.UrlCode && tinyUrlDetail.IsActive == true
                              select tinyUrlDetail.OriginalUrl).FirstOrDefault();

            if (string.IsNullOrEmpty(originalUrl.ToString()))
            {
                return string.Empty;
            }

            return originalUrl.ToString();
        }

        public bool DeleteTinyUrlInDB(string urlCode)
        {
            if (string.IsNullOrEmpty(urlCode))
            {
                return false;
            }

            var urlDetails = appDbContext.Tinyurldetails.FirstOrDefault(x => x.UrlCode == urlCode);

            if (urlDetails != null)
            {
                appDbContext.Tinyurldetails.Remove(urlDetails);
                appDbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public List<Tinyurldetail> GetTinyUrlDetails()
        {
            var tinyUrlDetails = (from tinyUrlDetail in appDbContext.Tinyurldetails
                               where tinyUrlDetail.IsActive == true
                               select tinyUrlDetail).Take(5).ToList();

            if (tinyUrlDetails.Count() == 0)
            {
                return null;
            }

            return tinyUrlDetails;
        }
    }
}
