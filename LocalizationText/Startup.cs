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

		// ���̃��\�b�h�̓����^�C���ɂ���ČĂяo����܂��B ���̃��\�b�h���g�p���āA�R���e�i�[�ɃT�[�r�X��ǉ����܂��B
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			// Resx �t�@�C���̂���t�H���_���w�肷��B
			// �R���g���[���[���ł����g�p���Ȃ��Ȃ炱����ł��悢�BAddViewLocalization ���g�p����Ȃ炱����͂Ȃ��Ă����삷��B
			//services.AddLocalization(options => options.ResourcesPath = "Resources");

			services.AddMvc()
				// ���[�J���C�Y�ɕK�v�BResx �t�@�C���̃t�H���_�p�X���w��
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; });
		}

		// ���̃��\�b�h�̓����^�C���ɂ���ČĂяo����܂��B ���̃��\�b�h���g�p���āAHTTP�v���p�C�v���C�����\�����܂��B
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// �f�t�H���g��HSTS�l��30���ł��B �^�p�V�i���I�ł͂����ύX���邱�Ƃ��ł��܂��Bhttps://aka.ms/aspnetcore-hsts ���Q�Ƃ��Ă��������B
				app.UseHsts();
			}

			// �W���̋@�\�Ő؂�ւ�����������`���܂��B
			var supportedCultures = new[]
			{
				new CultureInfo("ja"),
				new CultureInfo("en"),
				new CultureInfo("es"),
			};

			// �W���̌���؂�ւ��@�\��L���ɂ��܂��B�Ή����Ă���̂́u�N�G��������v�uCookie�v�uAccept-Language HTTP �w�b�_�[�v�ł��B
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
