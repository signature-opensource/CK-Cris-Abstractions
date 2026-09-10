The three commands of a user name and password authentication: log in, refresh, log out.

The login command carries the credentials, whether to impersonate the currently logged in user, and
the requested lifetimes - both plain and critical - with no guarantee the server honours them.

Its result is the authentication token plus a flattened, serializable view of the authentication
info: the three users (current, unsafe, actual), the level, both expiration dates and the device
identifier. That flattening is the point - it is what crosses a process boundary or reaches a
TypeScript front, where the service-side authentication objects cannot go.
