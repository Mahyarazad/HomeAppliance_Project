using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AM.Application.Contracts.Role;
using AM.Domain;

namespace AM.Infrastructure
{
    public class RoleRepository : RepositoryBase<int, Role>, IRoleRepository
    {
        private readonly AMContext _amContext;

        public RoleRepository(AMContext amContext) : base(amContext)
        {
            _amContext = amContext;
        }


        public List<RoleViewModel> GetAll()
        {
            return _amContext.Roles.Select(x => new RoleViewModel
            {
                CreationTime = TruncateDateTime.TruncateToDefault(x.CreationTime).ToString(),
                Name = x.Name,
                Id = x.Id
            }).OrderByDescending(x => x.Id).ToList();
        }

        public EditRole GetDetail(int Id)
        {
            return _amContext.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault(x => x.Id == Id);
        }
    }
}