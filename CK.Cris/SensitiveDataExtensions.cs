using CK.Core;

namespace CK.Cris;

/// <summary>
/// Extends the Cris Pocos with the handling of their <see cref="SensitiveAttribute"/> values.
/// </summary>
public static class SensitiveDataExtensions
{
    /// <summary>
    /// The value that replaces a sensitive value in a redacted copy.
    /// </summary>
    public const string Redacted = "[Redacted]";

    /// <summary>
    /// Rewrites the <see cref="SensitiveAttribute"/> string properties of this Poco and of its nested Cris Pocos.
    /// See <see cref="ICrisPocoModel.TransformSensitiveStrings(ICrisPoco, SensitiveStringTransformer)"/>.
    /// </summary>
    /// <param name="poco">This Poco.</param>
    /// <param name="transformer">The transformation.</param>
    /// <returns>True on success, false as soon as the transformer returned null.</returns>
    public static bool TransformSensitiveStrings( this ICrisPoco poco, SensitiveStringTransformer transformer )
    {
        Throw.CheckNotNullArgument( poco );
        Throw.CheckNotNullArgument( transformer );
        return poco.CrisPocoModel.TransformSensitiveStrings( poco, transformer );
    }

    /// <summary>
    /// Gets the string to log for this Poco: its JSON, with its sensitive values redacted when it may hold some
    /// (<see cref="ICrisPocoModel.HasSensitiveData"/>). This Poco is never modified (a redacted copy is logged).
    /// </summary>
    /// <param name="poco">This Poco.</param>
    /// <returns>The loggable string.</returns>
    public static string ToLogString( this ICrisPoco poco )
    {
        Throw.CheckNotNullArgument( poco );
        if( !poco.CrisPocoModel.HasSensitiveData ) return poco.ToString()!;
        var copy = Copy( poco );
        if( copy == null || !copy.TransformSensitiveStrings( static ( o, n, v ) => Redacted ) )
        {
            return $"{poco.CrisPocoModel.PocoName} (sensitive content not logged)";
        }
        return copy.ToString()!;
    }

    // Logging never fails: the deep copy throws only when an object property holds a type that is not a registered Poco type.
    static ICrisPoco? Copy( ICrisPoco poco )
    {
        try
        {
            return poco.Clone();
        }
        catch( System.Exception ex )
        {
            ActivityMonitor.StaticLogger.Warn( $"Unable to copy '{poco.CrisPocoModel.PocoName}' for a redacted log.", ex );
            return null;
        }
    }
}
