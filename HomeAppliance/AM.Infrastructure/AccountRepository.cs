using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AM.Application.Contracts;
using AM.Domain;
using Microsoft.EntityFrameworkCore;

namespace AM.Infrastructure
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AMContext _amContext;

        public AccountRepository(AMContext amContext) : base(amContext)
        {
            _amContext = amContext;
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _amContext.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                ProfilePicture = x.ProfilePicture,
                Role = x.RoleId,
                UserId = x.UserId,
                CreationTime = TruncateDateTime.TruncateToDefault(x.CreationTime).ToString()
            });
            if (!string.IsNullOrEmpty(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            if (!string.IsNullOrEmpty(searchModel.FullName))
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));
            if (!string.IsNullOrEmpty(searchModel.PhoneNumber))
                query = query.Where(x => x.PhoneNumber.Contains(searchModel.PhoneNumber));
            if (!string.IsNullOrEmpty(searchModel.UserId))
                query = query.Where(x => x.UserId.Contains(searchModel.UserId));
            // if (searchModel.Role != null)
            //     query = query.Where(x => x.Role == searchModel.Role);
            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditAccount GetDetail(long Id)
        {
            return _amContext.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                RoleId = x.RoleId,
                UserId = x.UserId,
                PictureString = x.ProfilePicture
            }).FirstOrDefault(x => x.Id == Id);
        }

        public ChangePassword getDetailforChangePassword(long Id)
        {
            return _amContext.Accounts.Select(x => new ChangePassword
            {
                Email = x.Email,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == Id);
        }
    }
}
