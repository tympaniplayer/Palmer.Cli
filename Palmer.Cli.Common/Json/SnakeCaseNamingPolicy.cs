using System.Text.Json;
using Palmer.Cli.Extensions;

namespace Palmer.Cli.Json
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToSnakeCase();
        }
    }
}