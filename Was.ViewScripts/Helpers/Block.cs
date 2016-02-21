namespace Was.ViewScripts.Helpers
{
    using System.IO;
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Web;

    internal abstract class Block<T>
    {
        protected readonly WebViewPage WebPageBase;

        protected Block(WebViewPage webPageBase)
        {
            WebPageBase = webPageBase;
            this.WebPageBase.OutputStack.Push(new StringWriter());
        }


        public string Key { get; protected set; }
        public bool IsUnique { get; protected set; }

        protected static List<T> Blocks(string key)
        {
            if (HttpContext.Current.Items[key] == null)
                HttpContext.Current.Items[key] = new List<T>();
            return (List<T>)HttpContext.Current.Items[key];
        }
    }
}
