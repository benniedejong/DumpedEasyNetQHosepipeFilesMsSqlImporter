namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.StartupParameters
{
    using System;
    using System.Collections.Generic;

    public sealed class Arguments
    {
        private readonly IList<Argument> arguments = new List<Argument>();

        private readonly IDictionary<string, Argument> keys = new Dictionary<string, Argument>();

        public void Add(Argument argument)
        {
            this.arguments.Add(argument);
            if (argument.HasKey)
            {
                this.keys.Add(argument.Key, argument);
            }
        }

        public ArgumentTryResult At(int position, Action<Argument> argumentAction)
        {
            if (position < 0 || position >= this.arguments.Count)
            {
                return ArgumentTryResult.Fail();
            }

            var argument = this.arguments[position];
            argumentAction(argument);
            return ArgumentTryResult.Pass();
        }

        public ArgumentTryResult WithKey(string key, Action<Argument> argumentAction)
        {
            if (!this.keys.ContainsKey(key))
            {
                return ArgumentTryResult.Fail();
            }

            var argument = this.keys[key];
            argumentAction(argument);
            return ArgumentTryResult.Pass();
        }

        public ArgumentTryResult WithTypedKeyOptional<T>(string key, Action<Argument> argumentAction) where T : IConvertible
        {
            if (!this.keys.ContainsKey(key))
            {
                return ArgumentTryResult.Pass();
            }

            try
            {
                Convert.ChangeType(this.keys[key].Value, typeof(T));
            }
            catch (InvalidCastException)
            {
                return ArgumentTryResult.Fail();
            }

            var argument = this.keys[key];
            argumentAction(argument);
            return ArgumentTryResult.Pass();
        }

        public ArgumentTryResult At(int position, string command, Action argumentAction)
        {
            if (position < 0 || position >= this.arguments.Count)
            {
                return ArgumentTryResult.Fail();
            }

            var argument = this.arguments[position];
            if (argument.Value != command)
            {
                return ArgumentTryResult.Fail();
            }

            argumentAction();
            return ArgumentTryResult.Pass();
        }
    }
}