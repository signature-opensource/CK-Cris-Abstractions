using CK.Core;

namespace CK.Cris;

/// <summary>
/// Validates the <see cref="ICommandTargetParty"/> of a received command: its target is required. (A party command
/// sent in-process is routed before its handling validation: its router refuses an empty target.)
/// </summary>
public sealed class CommandTargetPartyValidator : ISingletonAutoService
{
    /// <summary>
    /// Refuses an empty <see cref="ICommandTargetParty.TargetPartyFullName"/>.
    /// </summary>
    /// <param name="validator">The message collector.</param>
    /// <param name="part">The part to validate.</param>
    [IncomingValidator]
    public void CheckTarget( UserMessageCollector validator, ICommandTargetParty part )
    {
        if( string.IsNullOrWhiteSpace( part.TargetPartyFullName ) )
        {
            validator.Error( $"The target party of '{part.CrisPocoModel.PocoName}' is required.", "Cris.MissingTargetParty" );
        }
    }
}
