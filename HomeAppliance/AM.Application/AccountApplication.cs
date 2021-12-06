using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework;
using _0_Framework.Application;
using AM.Application.Contracts;
using AM.Domain;
using AM.Infrastructure;

namespace AM.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
            IFileUploader fileUploader)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public OperationResult Create(CreateAccount command)
        {
            var result = new OperationResult();
            if (_accountRepository.Exist(x => x.Email == command.Email) ||
                _accountRepository.Exist(x => x.UserId == command.UserId))
                return result.Failed(ApplicationMessage.RecordExists);
            else
            {
                var profilePicture =
                    _fileUploader.Uploader(command.ProfilePicture, "\\ProfilePicture\\", command.UserId);

                var password = _passwordHasher.Hash(command.Password);
                var account = new Account(
                    command.FullName,
                    command.UserId,
                    command.Email,
                    command.PhoneNumber,
                    password,
                    profilePicture,
                    1);
                _accountRepository.Create(account);
                _accountRepository.SaveChanges();
                return result.Succeeded();
            }
        }

        public OperationResult Edit(EditAccount command)
        {
            var result = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (_accountRepository.Exist(x => x.Email == command.Email) ||
                _accountRepository.Exist(x => x.UserId == command.UserId) ||
                _accountRepository.Exist(x => x.PhoneNumber == command.PhoneNumber) &&
                _accountRepository.Exist(x => x.Id != command.Id))
            {
                var profilePicture = _fileUploader.Uploader(command.ProfilePicture, "ProfilePicture", command.FullName);
                account.Edit(command.FullName, command.UserId, command.Email, command.PhoneNumber,
                    profilePicture, command.RoleId);
                _accountRepository.SaveChanges();
                return result.Succeeded();
            }

            return result.Failed(ApplicationMessage.RecordExists);
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var result = new OperationResult();
            if (command.Password == command.RePassword)
            {
                var password = _passwordHasher.Hash(command.Password);
                var target = _accountRepository.Get(command.Id);
                target.ChangePassword(password);
                _accountRepository.SaveChanges();
                return result.Succeeded();
            }
            else
            {
                return result.Failed(ApplicationMessage.PasswordsAreNotMatched);
            }
        }

        public EditAccount GetDetail(long Id)
        {
            return _accountRepository.GetDetail(Id);

        }

        public ChangePassword getDetailforChangePassword(long Id)
        {
            return _accountRepository.getDetailforChangePassword(Id);
        }
    }
}
