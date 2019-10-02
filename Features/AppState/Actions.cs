using BlazorState;

namespace MyApp.Features.AppState
{
    public class ListAddressBookAction : IAction
    {
        public string Search { get; set; }
    }
    public class LoginAction : IAction
    {
        public Celin.AIS.AuthResponse AuthResponse { get; set; }
    }
    public class LogoutAction : IAction { }
}
