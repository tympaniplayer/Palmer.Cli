namespace Palmer.Cli.Extensions;

public static class CliExtensions {

    public static string ToSnakeCase(this string str)
    {
        return string.Concat(
            str.Select(
                (x, i) => i > 0 && char.IsUpper(x)
                    ? "_" + x
                    : x.ToString()
            )
        ).ToLower();
    }
}