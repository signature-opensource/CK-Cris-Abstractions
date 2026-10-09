using CK.Cris;
using System.Collections.Generic;

namespace CK.IO.Authentication;

/// <summary>
/// Command part of the login commands: free key/value data that come with a login (an invitation token for instance).
/// They become the <c>CK.Core.LoginContext.UserData</c> that the login validators, the account creation and the account
/// binding services read.
/// <para>
/// <b>They come from the client: they are untrusted.</b> Whoever uses them verifies them (an invitation token checked
/// against the database for instance). The number of entries and their total size are limited.
/// </para>
/// <para>
/// A remote login keeps the data of its start (<see cref="IStartLoginCommand"/>) on the authentication center until its
/// callback: the <see cref="IRemoteLoginCallbackCommand"/> doesn't carry them, the client cannot change them on the way.
/// </para>
/// </summary>
public interface ICommandLoginUserData : ICommandPart
{
    /// <summary>
    /// Gets free key/value data that come with the login.
    /// </summary>
    IDictionary<string, string> UserData { get; }
}
