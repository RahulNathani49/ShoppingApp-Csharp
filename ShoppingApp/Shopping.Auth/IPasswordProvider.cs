using System;

namespace Utility.Authentication
{
    /// <summary>
    /// This interface should be implemented by a view that contains a password box.
    /// Then an interface reference to the view can be given to the corresponding
    /// view model, so that the view model can access the password, clear the password,
    /// and know when the password changes, without depending on the view directly.
    /// This provides a workaround for the fact that, for security reasons, data binding
    /// cannot be used with passwords in password boxes.
    /// </summary>
    public interface IPasswordProvider
    {
        event Action PasswordChanged;

        string Password { get; }
        bool HasPassword { get; }

        void Clear();
    }
}
