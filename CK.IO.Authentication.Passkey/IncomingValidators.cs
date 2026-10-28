using CK.Core;
using CK.Cris;

namespace CK.IO.Authentication.Passkey;

/// <summary>
/// Validates the passkey commands when they enter the System: the endpoint that receives them refuses them at its
/// entry, and the authentication center validates the data of its passkey operations again.
/// </summary>
public class IncomingValidators : IRealObject
{
    /// <summary>
    /// The maximal length of a browser response: 16 KB.
    /// </summary>
    public const int MaxResponseLength = 16 * 1024;

    /// <summary>
    /// The maximal length of a passkey name: 128.
    /// </summary>
    public const int MaxNameLength = 128;

    /// <summary>
    /// Limits the size of a login response.
    /// </summary>
    /// <param name="collector">The message collector.</param>
    /// <param name="command">The command.</param>
    [IncomingValidator]
    public virtual void ValidatePasskeyLoginCommand( UserMessageCollector collector, IPasskeyLoginCommand command ) => CheckResponse( collector, command.Response );

    /// <summary>
    /// Limits the size of a registration response.
    /// </summary>
    /// <param name="collector">The message collector.</param>
    /// <param name="command">The command.</param>
    [IncomingValidator]
    public virtual void ValidatePasskeyRegistrationCommand( UserMessageCollector collector, IPasskeyRegistrationCommand command ) => CheckResponse( collector, command.Response );

    /// <summary>
    /// Checks the name of a new passkey.
    /// </summary>
    /// <param name="collector">The message collector.</param>
    /// <param name="command">The command.</param>
    [IncomingValidator]
    public virtual void ValidateStartPasskeyRegistrationCommand( UserMessageCollector collector, IStartPasskeyRegistrationCommand command )
    {
        if( command.Name != null && (string.IsNullOrWhiteSpace( command.Name ) || command.Name.Length > MaxNameLength) )
        {
            collector.Error( $"The passkey name must not be blank and must have at most {MaxNameLength} characters.", "Authentication.InvalidPasskeyName" );
        }
    }

    static void CheckResponse( UserMessageCollector collector, string response )
    {
        if( string.IsNullOrEmpty( response ) || response.Length > MaxResponseLength )
        {
            collector.Error( $"The passkey response must not be empty and must have at most {MaxResponseLength} characters.", "Authentication.InvalidPasskeyResponse" );
        }
    }
}
