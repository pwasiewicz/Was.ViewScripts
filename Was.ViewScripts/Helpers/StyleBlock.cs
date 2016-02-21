namespace Was.ViewScripts.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    internal class StyleBlock : Block<ReadyStyleBlock>, IKeyedStyleBlockOptions
    {
        private const string ContextKey = "was-viewscripts-styles";

        public StyleBlock(WebViewPage webPageBase) : base(webPageBase)
        {
        }

        public static List<ReadyStyleBlock> Styles => Blocks(ContextKey);

        public void Dispose()
        {
            var blockvalue = ((StringWriter)this.WebPageBase.OutputStack.Pop()).ToString();

            if (this.IsUnique)
            {
                if (Styles.Any(sc => sc.Key == this.Key))
                {
                    return;
                }
            }

            Styles.Add(new ReadyStyleBlock
            {
                Value = blockvalue,
                Key = this.Key
            });
        }

        public IKeyedStyleBlockOptions Keyed(string key)
        {
            this.Key = key;
            return this;
        }

        public IKeyedStyleBlockOptions Unique(bool unique = true)
        {
            this.IsUnique = true;
            return this;
        }
    }
}
