using System.Collections.Generic;
using _0_Framework;
using AM.Application.Contracts.Role;

namespace AM.Domain
{
    public interface IRoleRepository : IRepository<int, Role>
    {
        List<RoleViewModel> GetAll();
        EditRole GetDetail(int Id);

    }
}