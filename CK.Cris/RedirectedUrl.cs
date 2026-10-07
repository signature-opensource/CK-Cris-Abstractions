namespace CK.Cris;

/// <summary>
/// Command result that is an URL to navigate to (to start a remote login for instance).
/// <para>
/// This is data: an endpoint returns it like any other result (an HTTP endpoint answers JSON, it never emits a redirection).
/// The client navigates to the <see cref="Url"/>: in the current window when <see cref="IsInline"/> is true, in a popup
/// window otherwise.
/// </para>
/// </summary>
/// <param name="Url">The URL to navigate to.</param>
/// <param name="IsInline">Whether the navigation must happen in the current window (or in a popup window).</param>
public record struct RedirectedUrl( string Url, bool IsInline );
