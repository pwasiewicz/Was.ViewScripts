namespace Was.ViewScripts.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    internal class ScriptBlock : IKeyedScriptBlockOptions
    {
        private const string ScriptsKey = "was-viewscripts-scripts";

        public string Key { get; private set; }
        public bool IsUnique { get; private set; }

        public static List<ReadyScriptBlock> PageScripts
        {
            get
            {
                if (HttpContext.Current.Items[ScriptsKey] == null)
                    HttpContext.Current.Items[ScriptsKey] = new List<ReadyScriptBlock>();
                return (List<ReadyScriptBlock>)HttpContext.Current.Items[ScriptsKey];
            }
        }

        private readonly WebViewPage _webPageBase;

        public ScriptBlock(WebViewPage webPageBase)
        {
            this._webPageBase = webPageBase;
            this._webPageBase.OutputStack.Push(new StringWriter());
        }

        public void Dispose()
        {
            var blockvalue = ((StringWriter) this._webPageBase.OutputStack.Pop()).ToString();

            if (this.IsUnique)
            {
                if (PageScripts.Any(sc => sc.Key == this.Key))
                {
                    return;
                }
            }

            PageScripts.Add(new ReadyScriptBlock
            {
                Value = blockvalue,
                Key = this.Key
            });
        }
        

        public IKeyedScriptBlockOptions Unique(bool unique)
        {
            this.IsUnique = unique;
            return this;
        }

        public IKeyedScriptBlockOptions Keyed(string key)
        {
            this.Key = key;
            return this;
        }
    }
}
