A Baubit extension for xUnit to develop modular unit tests
# Get Started
MyComponent.cs
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
TestBroker.cs
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
testBroker.json
```json
{
  "type": "Baubit.xUnit.TestBrokerFactory, Baubit.xUnit",
  "parameters": {
    "configuration": {
      "modules": [
        {
          "type": "MyLib.MyModule, MyLib",
          "parameters": {
            "configuration": {
              "myStringProperty": "some string value"
            }
          }
        }
      ]
    }
  }
}
```
Test.cs
```csharp
public class Test : AClassFixture<Fixture<TestBroker>, TestBroker>
{
    public Test(Fixture<TestBroker> fixture, 
                ITestOutputHelper testOutputHelper, 
                IMessageSink diagnosticMessageSink = null) : base(fixture,
                                                                  testOutputHelper,
                                                                  diagnosticMessageSink)
    {
    }

    [Fact]
    public void TestMethod()
    {
        Assert.NotNull(Broker);
        Assert.NotNull(Broker.MyComponent);
        Assert.NotNull(Broker.MyComponent.SomeString);
        // Continue testing MyComponent
    }
}
```
