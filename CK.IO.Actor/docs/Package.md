The Cris command contracts for users and groups: create, rename, destroy, check a name's availability,
read a profile, and add, remove or clear group memberships.

Contracts and their argument checks only - there is no database here. Every command declares the
authentication level it needs, and a single real object carries the incoming validators that reject
an empty user name or an identifier below the reserved range, with a message code per rule.

What executes them is a separate concern: this package is what a client references to send them and
what a server references to handle them.
