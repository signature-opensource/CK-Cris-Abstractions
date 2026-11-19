# CK.IO.DynamicConfiguration

The commands of the dynamic configuration of an application: the values that its code can change while it runs (the
`[ConfigurationLevel( Run )]` properties of its singletons, see CK.Cris.DynamicConfiguration that handles them).

- `IGetRunPropertiesQCommand`: the properties of an application that can be changed, with their current value.
- `IConfigurePropertyCommand`: changes one of them. The values are texts (invariant culture, as in the configuration):
  a command is relayed between parties, its values must be exchangeable. Its `CurrentValue` is the optimistic
  concurrency: when the actual value differs, the command fails and its message gives it.

Both are party commands (`ICommandTargetParty`: the target is a local party of the application) at the Critical level
(`ICommandAuthCritical`).

```csharp
var c = pocos.Create<IConfigurePropertyCommand>( c =>
{
    c.TargetPartyFullName = "Sample/Auth3/$Auth3/#Dev";
    c.PropertyPath = "CrisExecutionHost:ParallelRunnerCount";
    c.CurrentValue = "2";
    c.Value = "4";
} );
```
