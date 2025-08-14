using System;
using System.Collections.Generic;

namespace TinyUrl.Models;

public partial class Tinyurldetail
{
    public int Id { get; set; }

    public string OriginalUrl { get; set; } = null!;

    public string UrlCode { get; set; } = null!;

    public bool? IsActive { get; set; }
}
