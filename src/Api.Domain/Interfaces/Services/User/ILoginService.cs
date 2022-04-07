using System;
using System.Threading.Tasks;
using Api.Domain.DataTransfer;

namespace Api.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<Object> FindByLogin(LoginRequests user);
    }
}
