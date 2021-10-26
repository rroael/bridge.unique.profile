using System.Collections.Generic;
using System.Threading.Tasks;
using Bridge.Unique.Profile.Domain.Models;

namespace Bridge.Unique.Profile.Domain.Business.Contracts
{
    public interface IClientUserBusiness
    {
        Task Create(ClientUser request, List<string> apiCodes);
    }
}