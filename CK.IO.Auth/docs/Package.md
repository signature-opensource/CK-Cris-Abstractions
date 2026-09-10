Authentication parts for Cris commands and events: mix one in and the command carries an actor
identifier that the receiving endpoint checks against the ambient authentication info.

Three levels, each one a marker that says what a handler may assume - unsafe (the identifier is
whatever the last authentication said, expired or not), normal (a valid authentication), critical (a
recent one) - plus device identifier and impersonation as independent additions rather than steps.

Each comes in two shapes: a part usable on commands and events alike, and a command-only part. This
package declares the contract and contains no executable code; enforcing the level is done elsewhere.
