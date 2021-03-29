using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LocalizationText.Models;
using Microsoft.Extensions.Localization;

namespace LocalizationText.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IStringLocalizer<SharedResource> _localizer;

		// DI でローカライザーを取得します。
		public HomeController(ILogger<HomeController> logger, IStringLocalizer<SharedResource> localizer)
		{
			_logger = logger;
			_localizer = localizer;
		}

		public IActionResult Index()
		{
			// ローカライザーを使用して設定されている言語のリソースファイルからテキストを取得します。
			ViewData["Message"] = _localizer["Goodbye"].Value;

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
