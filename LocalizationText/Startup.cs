using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LocalizationText
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// このメソッドはランタイムによって呼び出されます。 このメソッドを使用して、コンテナーにサービスを追加します。
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			// Resx ファイルのあるフォルダを指定する。
			// コントローラー側でしか使用しないならこちらでもよい。AddViewLocalization を使用するならこちらはなくても動作する。
			//services.AddLocalization(options => options.ResourcesPath = "Resources");

			services.AddMvc()
				// ローカライズに必要。Resx ファイルのフォルダパスを指定
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; });
		}

		// このメソッドはランタイムによって呼び出されます。 このメソッドを使用して、HTTP要求パイプラインを構成します。
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// デフォルトのHSTS値は30日です。 運用シナリオではこれを変更することができます。https://aka.ms/aspnetcore-hsts を参照してください。
				app.UseHsts();
			}

			// 標準の機能で切り替えたい言語を定義します。
			var supportedCultures = new[]
			{
				new CultureInfo("ja"),
				new CultureInfo("en"),
				new CultureInfo("es"),
			};

			// 標準の言語切り替え機能を有効にします。対応しているのは「クエリ文字列」「Cookie」「Accept-Language HTTP ヘッダー」です。
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("ja"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
									name: "default",
									pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
