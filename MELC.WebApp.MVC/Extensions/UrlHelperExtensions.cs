using MELC.WebApp.MVC.Dtos;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MELC.WebApp.MVC.Extensions
{
    public static class UrlHelperExtensions
    {
        private static BundleConfigDto BundleConfig => ReadConfig();
        public static HtmlString Script(this IUrlHelper helper, string bundleName)
        {
            var output = "";

            var htmlTags = BundleConfig.Js.FirstOrDefault(x => x.Name == bundleName);

            foreach (var item in htmlTags.InputFiles)
            {
                output += $@"<script type='text/javascript' src='{helper.Content(item)}'></script>";
            }

            return new HtmlString(output);
        }



        private static BundleConfigDto ReadConfig()
        {
            var jsonString = File.ReadAllText("bundle.config.json");
            var bundleConfig = JsonConvert.DeserializeObject<BundleConfigDto>(jsonString);
            return bundleConfig ?? throw new InvalidOperationException("Bundle config não encontrado");
        }
    }
}
