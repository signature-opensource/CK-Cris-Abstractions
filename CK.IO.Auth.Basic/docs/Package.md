The three commands of a user name and password authentication: `IBasicLoginCommand`, `IRefreshAuthenticationCommand`
and `ILogoutCommand`. The login result, `IAuthenticationResult`, carries the authentication token and `IPocoAuthenticationInfo`:
a flattened, serializable view of the authentication info that can cross a process boundary or reach a TypeScript front.
