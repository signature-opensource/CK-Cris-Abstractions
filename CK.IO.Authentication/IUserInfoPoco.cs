using CK.Core;
using System;
using System.Collections.Generic;

namespace CK.IO.Authentication;

/// <summary>
/// Exchangeable description of a <see cref="UserInfo"/> with its level.
/// </summary>
public interface IUserInfoPoco : IPoco
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    int UserId { get; set; }

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    string UserName { get; set; }

    /// <summary>
    /// Gets or sets the authentication level of the user.
    /// </summary>
    AuthLevel Level { get; set; }

    /// <summary>
    /// Gets the authentication schemes of the user.
    /// </summary>
    IList<(string Scheme, DateTime LastUsed)> Schemes { get; }

    /// <summary>
    /// Initializes this description from a user information.
    /// </summary>
    /// <param name="info">The user information.</param>
    void InitializeFrom( UserInfo info )
    {
        Throw.CheckNotNullArgument( info );
        UserId = info.UserId;
        UserName = info.UserName;
        Level = info.Level;
        Schemes.Clear();
        foreach( var s in info.Schemes ) Schemes.Add( (s.Name, s.LastUsed) );
    }
}
