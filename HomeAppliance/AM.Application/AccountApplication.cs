using System.Collections.Generic;
using System.Linq;
using _0_Framework;
using _0_Framework.Application;
using AM.Application.Contracts.Account;
using AM.Domain;
using Microsoft.VisualBasic;

namespace AM.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IAutenticateHelper _autenticateHelper;
        private readonly IRoleRepository _roleRepository;
        public AccountApplication(IAccountRepository accountRepository,
            IPasswordHasher passwordHasher,
            IAutenticateHelper authenticateHelper,
            IFileUploader fileUploader,
            IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _autenticateHelper = authenticateHelper;
            _roleRepository = roleRepository;
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public OperationResult Register(RegisterAccount command)
        {
            var result = new OperationResult();
            if (_accountRepository.Exist(x => x.Email == command.Email) ||
                _accountRepository.Exist(x => x.UserId == command.UserId))
                return result.Failed(ApplicationMessage.RecordExists);

            var profilePicture =
                _fileUploader.Uploader(command.ProfilePicture, "\\ProfilePicture\\", command.UserId);
            if (command.ProfilePicture == null)
                profilePicture = "DefaultProfile.png";
            var password = _passwordHasher.Hash(command.Password);
            var account = new Account(
                command.FullName,
                command.UserId,
                command.Email,
                command.PhoneNumber,
                password,
                profilePicture,
                command.RoleId);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return result.Succeeded();
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

        public OperationResult Login(EditAccount command)
        {
            var result = new OperationResult();
            var account = _accountRepository.GetDetailByUser(command.UserId);
            if (account == null)
                return result.Failed(ApplicationMessage.UserNotExists);

            var (verified, needsUpgrade) = _passwordHasher.Check(account.Password, command.Password);
            if (!verified)
                return result.Failed(ApplicationMessage.WrongPassword);

            var permissions = _roleRepository.GetDetail(account.RoleId)
                .MappedPermissions
                .Select(x => x.Code)
                .ToList();

            var authModel = new AuthViewModel(account.Id, account.UserId, account.FullName,
                account.RoleId.ToString(), account.PictureString, permissions);
            _autenticateHelper.Login(authModel);
            return result.Succeeded();
        }

        public OperationResult RegisterUser(RegisterUser command)
        {
            var result = new OperationResult();
            if (command.Password != command.ConfirmPassword)
                return result.Failed(ApplicationMessage.PasswordsAreNotMatched);
            if (_accountRepository.Exist(x => x.Email == command.Email) ||
                    _accountRepository.Exist(x => x.UserId == command.UserId))
                return result.Failed(ApplicationMessage.RecordExists);

            var password = _passwordHasher.Hash(command.Password);
            var account = new Account(
                command.FullName,
                command.UserId,
                command.Email,
                command.PhoneNumber,
                password,
                "DefaultProfile.png",
                int.Parse(AuthorizationRoles.User));
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return result.Succeeded();

        }

        public void Logout()
        {
            _autenticateHelper.Logout();
        }
    }
}
