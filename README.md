# Baubit.xUnit

[![NuGet](https://img.shields.io/nuget/v/Baubit.xUnit.svg)](https://www.nuget.org/packages/Baubit.xUnit)

**Baubit.xUnit** is an extension for [Baubit](https://github.com/pnagoorkar/Baubit) that integrates with [xUnit.net](https://xunit.net/), facilitating the development of modular unit tests for .NET applications. This integration allows for structured and maintainable testing of individual components within a modular architecture.

## Features

- **Modular Testing:** Enables unit testing of individual modules within a Baubit-based application.
- **Seamless xUnit Integration:** Leverages xUnit's [Fixtures framework](https://xunit.net/docs/shared-context) to inject testable components into the test classes.
- **Configuration Flexibility:** Supports configuration-driven test setups - allowing scenario design using configuration files.

## Getting Started

### Installation

To install **Baubit.xUnit**, add the NuGet package to your test project:

```bash
dotnet add package Baubit.xUnit
```

### Usage

Below is a step-by-step guide to setting up and using **Baubit.xUnit** in your test project.

#### 1. Define Your Component

```csharp
public class MyComponent
{
    public string SomeString { get; set; }

    public MyComponent(string someStr)
    {
        SomeString = someStr;
    }
}
```
#### 2. [Add it](https://github.com/pnagoorkar/Baubit?tab=readme-ov-file#-defining-a-module) to the applications IoC container using a Baubit module.

#### 3. Create a Test Context

A **test context** is a custom class that provides access to all testable components registered in one or more modules. This enables validating component behavior in isolation.

```csharp
[Source(EmbeddedJsonResources = ["MyLib.Test;context.json"])]
public class Context : IContext
{
    public MyComponent MyComponent { get; set; }

    public Context(MyComponent myComponent)
    {
        MyComponent = myComponent;
    }
}
```

A `[Source]` attribute MUST be defined for the Context. This allows the Baubit.xUnit framework to load modules relevant for testing

#### 4. Configure Embedded Resource

Make sure `context.json` is marked as an embedded resource in your test project. Example content:

```json
{
  "modules": [
    {
      "type": "MyLib.MyModule, MyLib",
      "configuration": {
        "myStringProperty": "some string value"
      }
    }
  ]
}
```

#### 5. Write the Unit Test

```csharp
public class MyComponentTests : AClassFixture<Context>
{
    public MyComponentTests(Fixture<Context> fixture,
                            ITestOutputHelper testOutputHelper,
                            IMessageSink diagnosticMessageSink = null)
        : base(fixture, testOutputHelper, diagnosticMessageSink)
    {
    }

    [Fact]
    public void MyComponent_Should_Not_Be_Null()
    {
        Assert.NotNull(Context);
        Assert.NotNull(Context.MyComponent);
        Assert.NotNull(Context.MyComponent.SomeString);
    }
}
```

The test class uses `Fixture<Context>` to bootstrap the module and expose configured services. The `Context` property provides access to the test context instance, through which you can access and validate components, behaviors, and configurations.

## Scenario Variance
**Baubit.xUnit** supports scenario variance to test multiple scenarios using the same test method
```cs
public class MyScenario : IScenario<Context>
{
    public string ScenarioSpecificData { get; }

    public Result Run(Context context)
    {
        context.MyComponent.DoSomething(ScenarioSpecificData);
        return Result.OkIf(context.MyComponent.State == States.MySpecificState, new Error("Invalid component state after doing something"));
    }

    public Result Run() => throw new NotImplementedException();

    public Task<Result> RunAsync(Context context) => throw new NotImplementedException();

    public Task<Result> RunAsync() => throw new NotImplementedException();
}
```

```cs
public class MyComponentTests : AClassFixture<Context>
{
    public MyComponentTests(Fixture<Context> fixture,
                            ITestOutputHelper testOutputHelper,
                            IMessageSink diagnosticMessageSink = null)
        : base(fixture, testOutputHelper, diagnosticMessageSink)
    {
    }

    [Theory]
    [InlineData("MyLib.Test;Scenarios.scenario1.json")]
    [InlineData("MyLib.Test;Scenarios.scenario2.json")]
    [InlineData("MyLib.Test;Scenarios.scenario3.json")]
    public void MyComponent_ShouldBeInMySpecificStateAfterDoingSomething()
    {
        var result = ExecuteScenario<MyScenario>(embeddedJsonResource);
        var reasons = new List<IReason>();
        result.UnwrapReasons(reasons);
        var reasonsString = string.Join(Environment.NewLine, reasons);
        Assert.True(result.IsSuccess, reasonsString);
    }
}
```
You assertions will just not check if the scenario was successful, but also tell you the exact reasons behind the failure (as long as your components are capturing reasons in the call stack).

Sample scenario json files
```json
//scenario1.json
{
    "scenarioSpecificData": "<specific data 1>"
}
```
```json
//scenario2.json
{
    "scenarioSpecificData": "<specific data 2>"
}
```
```json
//scenario3.json
{
    "scenarioSpecificData": "<specific data 3>"
}
```

## Resources

- [Samples](https://github.com/pnagoorkar/Baubit.xUnit/tree/master/Samples)
- [Baubit Framework](https://github.com/pnagoorkar/Baubit)
- [xUnit.net Documentation](https://xunit.net/docs/)
- [xUnit.net Samples](https://github.com/xunit/samples.xunit)

## Contributing

Contributions are welcome! Fork the repo and submit a pull request.

## License

This project is licensed under the Apache 2.0 License.
