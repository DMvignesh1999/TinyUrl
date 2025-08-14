using System.Collections.Generic;

namespace TinyUrl.Models
{
    public class TinyUrlModel
    {
        public TinyUrlModel(string? urlCode, List<Tinyurldetail>? tinyurlDetails)
        {
            UrlCode = urlCode;
            TinyUrlDetails = tinyurlDetails;
        }

        public string? UrlCode { get; set; } = string.Empty;
        public List<Tinyurldetail>? TinyUrlDetails { get; set; }
    }
}
