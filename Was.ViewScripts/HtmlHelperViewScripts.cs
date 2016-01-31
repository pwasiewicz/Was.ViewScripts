namespace Was.ViewScripts
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Helpers;

    public static class HtmlHelperViewScripts
    {
        public static IScriptBlockOptions BeginScripts(this HtmlHelper helper)
        {
            return new ScriptBlock((WebViewPage)helper.ViewDataContainer);
        }

        public static MvcHtmlString PageScripts(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(string.Join(Environment.NewLine, ScriptBlock.PageScripts.Select(s => s.Value)));
        }
    }
}
