namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.StartupParameters
{
    using System;

    public sealed class ArgumentTryResult
    {
        private bool pass;

        public static ArgumentTryResult Pass()
        {
            return new ArgumentTryResult { pass = true };
        }

        public static ArgumentTryResult Fail()
        {
            return new ArgumentTryResult { pass = false };
        }

        public void FailWith(Action action)
        {
            if (!pass) action();
        }
    }
}