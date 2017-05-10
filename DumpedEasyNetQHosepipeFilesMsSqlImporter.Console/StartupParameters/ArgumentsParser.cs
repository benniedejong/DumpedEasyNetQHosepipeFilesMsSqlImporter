namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.StartupParameters
{
    using System.Text.RegularExpressions;

    public sealed class ArgumentsParser
    {
        private readonly Regex argumentRegex = new Regex(@"([a-z])\:(.*)");

        public Arguments Parse(string[] args)
        {
            var arguments = new Arguments();
            foreach (var arg in args)
            {
                var argument = this.ParseArgument(arg);
                arguments.Add(argument);
            }

            return arguments;
        }

        private Argument ParseArgument(string arg)
        {
            var match = this.argumentRegex.Match(arg);
            return match.Success ? new Argument(match.Groups[2].Value, match.Groups[1].Value) : new Argument(arg);
        }
    }
}