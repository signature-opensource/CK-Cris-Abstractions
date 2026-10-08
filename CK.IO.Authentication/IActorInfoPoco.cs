using CK.Core;
using System;
using System.Collections.Generic;

namespace CK.IO.Authentication;

/// <summary>
/// Exchangeable description of a <see cref="ActorInfo"/> with its level.
/// </summary>
public interface IActorInfoPoco : IPoco
{
    /// <summary>
    /// Gets or sets the actor identifier.
    /// </summary>
    int ActorId { get; set; }

    /// <summary>
    /// Gets or sets the actor name.
    /// </summary>
    string ActorName { get; set; }

    /// <summary>
    /// Gets or sets the authentication level of the actor.
    /// </summary>
    AuthLevel Level { get; set; }

    /// <summary>
    /// Gets the authentication schemes of the actor.
    /// </summary>
    IList<(string Scheme, DateTime LastUsed)> Schemes { get; }

    /// <summary>
    /// Initializes this description from an actor information.
    /// </summary>
    /// <param name="info">The actor information.</param>
    void InitializeFrom( ActorInfo info )
    {
        Throw.CheckNotNullArgument( info );
        ActorId = info.ActorId;
        ActorName = info.ActorName;
        Level = info.Level;
        Schemes.Clear();
        foreach( var s in info.Schemes ) Schemes.Add( (s.Name, s.LastUsed) );
    }
}
