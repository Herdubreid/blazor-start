using BlazorState;
using MediatR;
using MyApp.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Features.AppState
{
    public partial class AppState
    {
        public class ListAddressBookHandler : ActionHandler<ListAddressBookAction>
        {
            AppState State => Store.GetState<AppState>();
            E1Service E1Service { get; }
            public override async Task<Unit> Handle(ListAddressBookAction aAction, CancellationToken aCancellationToken)
            {
                try
                {
                    var keywords = aAction.Search.Split(" ");
                    var rq = new E1.F0101.Request(keywords);
                    var rs = await E1Service.RequestAsync<E1.F0101.Response>(rq);
                    State.AddressBookList = rs.fs_DATABROWSE_F0101.data.gridData.rowset;
                    State.ErrorMsg = string.Empty;
                }
                catch (Exception e)
                {
                    State.ErrorMsg = e.Message;
                }
                return Unit.Value;
            }
            public ListAddressBookHandler(IStore store, E1Service e1Service) : base(store)
            {
                E1Service = e1Service;
            }
        }
        public class LoginHander : ActionHandler<LoginAction>
        {
            AppState State => Store.GetState<AppState>();
            public override Task<Unit> Handle(LoginAction aAction, CancellationToken aCancellationToken)
            {
                State.AuthResponse = aAction.AuthResponse;
                State.LoginTime = DateTime.Now;
                EventHandler handler = State.Changed;
                handler?.Invoke(State, null);
                return Unit.Task;
            }
            public LoginHander(IStore store) : base(store) { }
        }
        public class LogoutHandler : ActionHandler<LogoutAction>
        {
            AppState State => Store.GetState<AppState>();
            E1Service E1Service { get; }
            public override async Task<Unit> Handle(LogoutAction aAction, CancellationToken aCancellationToken)
            {
                await E1Service.Logout();
                State.AuthResponse = null;
                EventHandler handler = State.Changed;
                handler?.Invoke(State, null);
                return Unit.Value;
            }
            public LogoutHandler(IStore store, E1Service e1Service) : base(store)
            {
                E1Service = e1Service;
                E1Service.AuthResponse = State.AuthResponse;
            }
        }
    }
}
