A Baubit extension for xUnit to develop modular unit tests
# Get Started
testBroker.json
```json
{
  "type": "Baubit.xUnit.TestBrokerFactory, Baubit.xUnit",
  "parameters": {
    "configuration": {

    }
  }
}
```
TestBroker.cs
```csharp
[EmbeddedJsonSources("MyProject;MyProject.testBroker.json")]
public class TestBroker : ITestBroker
{
  public string SomeStringProperty { get; set;}
}
```
Test.cs
```csharp
public class Test : AClassFixture<Fixture<TestBroker>, TestBroker>
{
    public Test(Fixture<TestBroker> fixture, ITestOutputHelper testOutputHelper, IMessageSink diagnosticMessageSink = null) : base(fixture, testOutputHelper, diagnosticMessageSink)
    {
    }

    [Fact]
    public void MyTestMethod()
    {
        var str = this.Broker.SomeStringProperty;
        // test str
    }
}
```
