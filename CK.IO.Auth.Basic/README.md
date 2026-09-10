# Basic authentication commands

Log in with a user name and a password, refresh, log out. Three commands, and a serializable view of
the authentication state they produce.

> ℹ️ Read [CK.IO.Auth](../CK.IO.Auth/README.md) for the authentication parts. None of the commands
> here uses one, and the reason is in the last section.

## The three commands.

```csharp
public interface IBasicLoginCommand : ICommand<IAuthenticationResult>
{
    string UserName { get; set; }
    string Password { get; set; }
    bool ImpersonateActualUser { get; set; }
    TimeSpan? ExpiresTimeSpan { get; set; }
    TimeSpan? CriticalExpiresTimeSpan { get; set; }
}
```

[`ILogoutCommand`](ILogoutCommand.cs) is empty and has no result - there is nothing to say beyond
"forget the current authentication".
[`IRefreshAuthenticationCommand`](IRefreshAuthenticationCommand.cs) has one property, `CallBackend`,
which asks for the authentication to be re-validated against its source rather than simply re-read.

Two things about the lifetimes are stated in the declaration and worth respecting. They are
**requests**: *"there is no guaranty that this duration will be applied (the server can restrict
this)"*. And they are two, not one - the critical lifetime is requested separately from the plain
one, so a client can ask for a short critical window and a long session, or the reverse.

`ImpersonateActualUser` is the login-time form of impersonation: on success the new user impersonates
whoever was already logged in, rather than replacing them.

## The result is a token plus a flattened authentication info.

```csharp
public interface IAuthenticationResult : IStandardResultPart
{
    IPocoAuthenticationInfo Info { get; }
    string? Token { get; set; }
}
```

It extends `IStandardResultPart`, so a failed login is a successful *command* with `Success` false and
the user messages explaining why - not an exception, and not an `ICrisResultError`.

[`IPocoAuthenticationInfo`](IPocoAuthenticationInfo.cs) is the part that carries the design decision.
It is a Poco mirror of `IAuthenticationInfo`, property for property: the three users, the
impersonation flag, the level, both expiry dates, the device identifier. The three users are the
subtlety - `User`, `UnsafeUser` and `ActualUser` are separate values, and they differ exactly when the
authentication has expired or an impersonation is in effect.

`InitializeFrom` is a default interface method on both this and
[`IPocoUserInfo`](IPocoUserInfo.cs), so filling the mirror from the real object is one call and lives
with the declaration rather than in a mapper somewhere:

```csharp
void InitializeFrom( IAuthenticationInfo info )
{
    Throw.CheckNotNullArgument( info );
    User.InitializeFrom( info.User );
    ActualUser.InitializeFrom( info.ActualUser );
    UnsafeUser.InitializeFrom( info.UnsafeUser );
    IsImpersonated = info.IsImpersonated;
    Level = info.Level;
    Expires = info.Expires;
    CriticalExpires = info.CriticalExpires;
    DeviceId = info.DeviceId;
}
```

The mirror exists because `IAuthenticationInfo` cannot cross a boundary - a Poco can be serialized,
projected to TypeScript, and put in a command result; a service-side interface cannot.
`IPocoUserInfo.Schemes` shows the cost of the exercise: `IList<(string Scheme, DateTime LastUsed)>`,
a list of value tuples, because that is the Poco-expressible shape of the original.

## No command here declares an authentication part, on purpose.

`IBasicLoginCommand` is the command that *establishes* the authentication, so it cannot require one -
it is the archetypal unauthenticated command. `ILogoutCommand` and `IRefreshAuthenticationCommand`
operate on the ambient authentication rather than on an actor identifier carried by the caller, so
they have nothing to declare either.

That is why this package references [CK.IO.Auth](../CK.IO.Auth/README.md) without using a single one
of its eleven interfaces. What the results actually need - `IAuthenticationInfo`, `IUserInfo` and
`AuthLevel` - is declared further upstream and simply arrives along the chain.

Everything here is in the `CK.Auth` namespace - the project sets `RootNamespace` to it, like
`CK.IO.Auth` does.

## Requires.

- `CK.IO.Auth`, and nothing else. Note that this package uses none of its parts - the section above
  says why.
