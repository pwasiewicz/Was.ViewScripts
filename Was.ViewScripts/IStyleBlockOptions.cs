namespace Was.ViewScripts
{
    using System;

    public interface IStyleBlockOptions : IDisposable
    {
        IKeyedStyleBlockOptions Keyed(string key);
    }

    public interface IKeyedStyleBlockOptions : IStyleBlockOptions
    {
        IKeyedStyleBlockOptions Unique(bool unique = true);
    }
}
