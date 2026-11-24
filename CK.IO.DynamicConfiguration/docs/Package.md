The commands of the dynamic configuration of an application: an operator lists and changes the values that its code can
change while it runs (the `[ConfigurationLevel( Run )]` properties of its singletons).

`IGetRunPropertiesQCommand` lists these values with their current value, and `IConfigurePropertyCommand` changes one of
them (`PropertyPath`, `Value` and `CurrentValue` as texts, the current value being the optimistic concurrency). Both are
party commands at the Critical level. Contract only: CK.Cris.DynamicConfiguration handles them.
