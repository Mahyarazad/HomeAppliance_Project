﻿using System.Collections.Generic;
using _0_Framework;
using _0_Framework.Application;
using AM.Application.Contracts.Role;
using AM.Domain;
using AM.Infrastructure;

namespace AM.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var result = new OperationResult();
            if (_roleRepository.Exist(x => x.Name == command.Name))
                return result.Failed(ApplicationMessage.RecordExists);
            var role = new Role(command.Name);
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return result.Succeeded();

        }

        public OperationResult Edit(EditRole command)
        {
            var result = new OperationResult();
            var role = _roleRepository.Get(command.Id);
            if (_roleRepository.Exist(x => x.Id != command.Id) && command.Name == role.Name)
                return result.Failed(ApplicationMessage.RecordExists);
            role.Edit(command.Name);
            _roleRepository.SaveChanges();
            return result.Succeeded();
        }

        public List<RoleViewModel> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public EditRole GetRole(int Id)
        {
            return _roleRepository.GetDetail(Id);
        }
    }
}