using CommandDotNet.TestTools;
using CommandDotNet.TestTools.Scenarios;
using FluentAssertions;
using System;
using Xunit;
using CommandDotNet;

namespace Palmer.Cli.Tests;

public sealed class ProgramTests
{
    [Fact]
    public void GivenNoArgs_Program_PrintsHello()
    {
        var result = new AppRunner<Program>().RunInMem(string.Empty);

        result.ExitCode.Should().Be(0);
        result.Console.Out.ToString().Should().Contain("Hello, Nate");
    }
}