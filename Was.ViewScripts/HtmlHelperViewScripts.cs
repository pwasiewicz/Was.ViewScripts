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

        public static IScriptBlockOptions BeginStyles(this HtmlHelper helper)
        {
            return new ScriptBlock((WebViewPage)helper.ViewDataContainer);
        }

        public static MvcHtmlString PageScripts(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(string.Join(Environment.NewLine, ScriptBlock.Scripts.Select(s => s.Value)));
        }

        public static MvcHtmlString PageStyles(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(string.Join(Environment.NewLine, StyleBlock.Styles.Select(s => s.Value)));
        }
    }
}
