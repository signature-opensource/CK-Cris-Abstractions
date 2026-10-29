using CK.Cris;
using System.Collections.Generic;

namespace CK.IO.Authentication;

/// <summary>
/// Gets the available login schemes ("Basic", "Weak", the remote providers, etc.) so that a login
/// user interface shows only what exists. This is not a session command: anybody can send it.
/// It is handled by the Authentication Center: where it runs is a deployment detail (a party whose Authentication
/// Center is remote dispatches it).
/// <para>
/// Each scheme tells whether it can elevate an actor to the Critical level (its <see cref="ILoginSchemeInfo.CriticalLifetime"/>).
/// </para>
/// </summary>
public interface IGetLoginSchemesQCommand : ICommand<IList<ILoginSchemeInfo>>
{
}
