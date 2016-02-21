namespace Was.ViewScripts.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    internal class ScriptBlock : Block<ReadyScriptBlock>, IKeyedScriptBlockOptions
    {
        private const string ContextKey = "was-viewscripts-scripts";

        
        public ScriptBlock(WebViewPage webPageBase)
            : base(webPageBase)
        {
        }

        public static List<ReadyScriptBlock> Scripts => Blocks(ContextKey);

        public void Dispose()
        {
            var blockvalue = ((StringWriter) this.WebPageBase.OutputStack.Pop()).ToString();

            if (this.IsUnique)
            {
                if (Scripts.Any(sc => sc.Key == this.Key))
                {
                    return;
                }
            }

            Scripts.Add(new ReadyScriptBlock
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
