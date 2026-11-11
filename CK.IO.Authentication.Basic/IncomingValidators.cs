using CK.Core;
using CK.Cris;

namespace CK.IO.Authentication.Basic;

/// <summary>
/// Validates the basic login command when it enters the System: unbounded credentials would be a memory and work
/// exhaustion vector (an anonymous caller makes the authentication center look the name up and hash the password).
/// </summary>
public class IncomingValidators : IRealObject
{
    /// <summary>
    /// The maximal length of a user name: 256.
    /// </summary>
    public const int MaxUserNameLength = 256;

    /// <summary>
    /// The maximal length of a password: 1024.
    /// </summary>
    public const int MaxPasswordLength = 1024;

    /// <summary>
    /// Limits the size of the user name and of the password.
    /// </summary>
    /// <param name="collector">The message collector.</param>
    /// <param name="command">The command.</param>
    [IncomingValidator]
    public virtual void ValidateBasicLoginCommand( UserMessageCollector collector, IBasicLoginCommand command )
    {
        if( command.UserName != null && command.UserName.Length > MaxUserNameLength )
        {
            collector.Error( $"The user name must have at most {MaxUserNameLength} characters.", "Authentication.UserNameTooLong" );
        }
        if( command.Password != null && command.Password.Length > MaxPasswordLength )
        {
            collector.Error( $"The password must have at most {MaxPasswordLength} characters.", "Authentication.PasswordTooLong" );
        }
    }
}
