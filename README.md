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

#### 3. Create a Test Broker

A **test broker** is a custom class that provides access to all testable components registered in one or more modules. This enables validating component behavior in isolation.

```csharp
[EmbeddedJsonSources("MyLib.Test;testBroker.json")]
public class TestBroker : ITestBroker
{
    public MyComponent MyComponent { get; set; }

    public TestBroker(MyComponent myComponent)
    {
        MyComponent = myComponent;
    }
}
```

An `[EmbeddedJsonSources]` attribute MUST be defined for the TestBroker. This allows the Baubit.xUnit framework to load modules relevant for testing

#### 4. Configure Embedded Resource

Make sure `testBroker.json` is marked as an embedded resource in your test project. Example content:

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
public class MyComponentTests : AClassFixture<Fixture<TestBroker>, TestBroker>
{
    public MyComponentTests(Fixture<TestBroker> fixture,
                            ITestOutputHelper testOutputHelper,
                            IMessageSink diagnosticMessageSink = null)
        : base(fixture, testOutputHelper, diagnosticMessageSink)
    {
    }

    [Fact]
    public void MyComponent_Should_Not_Be_Null()
    {
        Assert.NotNull(Broker);
        Assert.NotNull(Broker.MyComponent);
        Assert.NotNull(Broker.MyComponent.SomeString);
    }
}
```

The test class uses `Fixture<TestBroker>` to bootstrap the module and expose configured services. The `Broker` property provides access to the test broker instance, through which you can access and validate components, behaviors, and configurations.

## Resources

- [Samples](https://github.com/pnagoorkar/Baubit.xUnit/tree/master/Samples)
- [Baubit Framework](https://github.com/pnagoorkar/Baubit)
- [xUnit.net Documentation](https://xunit.net/docs/)
- [xUnit.net Samples](https://github.com/xunit/samples.xunit)

## Contributing

Contributions are welcome! Fork the repo and submit a pull request.

## License

This project is licensed under the Apache 2.0 License.
