using CK.Auth;
using CK.Core;
using CK.Cris;
using System.Collections.Generic;

namespace CK.IO.DynamicConfiguration;

/// <summary>
/// Changes a value of the configuration of an application (<see cref="ICommandTargetParty.TargetPartyFullName"/>: a
/// local party of the application) while it runs: a property that its code can change (a <c>[ConfigurationLevel( Run )]</c>
/// property of an App singleton) and that its configuration doesn't restrict. The change is kept: the application starts
/// with it, unless its configured value changed since then (the latest decision wins).
/// </summary>
public interface IConfigurePropertyCommand : ICommand<ICrisBasicCommandResult>, ICommandTargetParty, ICommandAuthCritical
{
    /// <summary>
    /// Gets or sets the path of the property: its section, then its name (':' separated), as in the configuration
    /// ("CrisExecutionHost:ParallelRunnerCount").
    /// </summary>
    string PropertyPath { get; set; }

    /// <summary>
    /// Gets or sets the new value, as a text (invariant culture, as in the configuration): a number ("4"), a boolean
    /// ("true"), the name of an enum value, a duration ("00:00:05")... Null for a null value.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets the value that the sender believes is the current one, as a text (the optimistic concurrency: the
    /// <see cref="IRunPropertyInfo.Value"/> it read). When it differs, the command fails and its message gives the actual
    /// value. It is always compared (null is a value).
    /// </summary>
    string? CurrentValue { get; set; }
}

/// <summary>
/// Lists the properties of an application that can be changed while it runs (<see cref="IConfigurePropertyCommand"/>),
/// with their current value.
/// </summary>
public interface IGetRunPropertiesQCommand : ICommand<IList<IRunPropertyInfo>>, ICommandTargetParty, ICommandAuthCritical
{
}

/// <summary>
/// A property of an application that can be changed while it runs.
/// </summary>
public interface IRunPropertyInfo : IPoco
{
    /// <summary>
    /// Gets or sets the path of the property ("CrisExecutionHost:ParallelRunnerCount").
    /// </summary>
    string Path { get; set; }

    /// <summary>
    /// Gets or sets the name of its type ("Int32", "Boolean", "TimeSpan", the name of an enum...).
    /// </summary>
    string Type { get; set; }

    /// <summary>
    /// Gets the names of the values of an enum (empty for another type).
    /// </summary>
    IList<string> EnumValues { get; }

    /// <summary>
    /// Gets or sets its current value, as a string (invariant culture), null for a null value.
    /// </summary>
    string? Value { get; set; }
}
