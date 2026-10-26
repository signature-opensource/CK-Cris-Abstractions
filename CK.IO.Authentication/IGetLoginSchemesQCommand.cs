using CK.Auth;
using CK.Cris;
using System.Collections.Generic;

namespace CK.IO.Authentication;

/// <summary>
/// Gets the available login schemes ("Basic", "Weak", the remote providers, etc.) so that a login
/// user interface shows only what exists. This is not a session command: anybody can send it.
/// It is executed by the Authentication Center (<see cref="ICommandAuthCenter"/>): a front relays it.
/// </summary>
public interface IGetLoginSchemesQCommand : ICommand<IList<string>>, ICommandAuthCenter
{
}
