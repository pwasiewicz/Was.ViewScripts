namespace Was.ViewScripts
{
    using System;

    public interface IScriptBlockOptions : IDisposable
    {
        IKeyedScriptBlockOptions Keyed(string key);
    }

    public interface IKeyedScriptBlockOptions : IScriptBlockOptions
    {
        IKeyedScriptBlockOptions  Unique(bool unique = true);
    }
}
