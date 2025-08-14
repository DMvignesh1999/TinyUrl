using Microsoft.AspNetCore.Mvc;
using System;
using TinyUrl.Models;
using TinyUrl.Services;
using TinyUrlDetail.Services;

namespace TinyUrl.Controllers
{
    public class TinyUrlController: Controller
    {
        private readonly ITinyUrlService tinyUrlService;
        public TinyUrlController(ITinyUrlService tinyUrlService) {
            this.tinyUrlService = tinyUrlService;
        }

        [HttpGet]
        [Route("/tinyurlpage")]
        public IActionResult TinyUrlPage(string? urlCode)
        {
            var tinyUrlDetails = tinyUrlService.GetTinyUrlDetails();
            List<Tinyurldetail> tinyurlDetailsToDisplay = new List<Tinyurldetail>();
            if (tinyUrlDetails != null)
            {
                tinyurlDetailsToDisplay = tinyUrlDetails;
            }
            var model = new TinyUrlModel(urlCode, tinyurlDetailsToDisplay);
            return View(model);
        }

        [HttpPost]
        [Route("/tinyurlpage/add")]
        public IActionResult AddTinyUrl(string url)
        {
            var urlCode = tinyUrlService.AddTinyUrlInDB(url);
            return Json(new { redirectUrl = Url.Action("TinyUrlPage", "TinyUrl", new { urlCode = urlCode }) });
        }

        [HttpGet]
        [Route("/redirectoriginalurl")]
        public IActionResult RedirectOriginalUrl(string? urlCode)
        {
            var originalUrl = tinyUrlService.RedirectToOriginalPage(urlCode);
            return Redirect(originalUrl);
        }

        [HttpPost]
        [Route("/tinyurlpage/delete")]
        public IActionResult DeleteTinyUrl(string urlCode)
        {
            var isDeleted = tinyUrlService.DeleteTinyUrlInDB(urlCode);
            if (isDeleted)
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
