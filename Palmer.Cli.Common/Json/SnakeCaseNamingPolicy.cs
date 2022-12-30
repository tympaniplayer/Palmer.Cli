using System.Text.Json;
using Palmer.Cli.Common.Extensions;

namespace Palmer.Cli.Common.Json
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}