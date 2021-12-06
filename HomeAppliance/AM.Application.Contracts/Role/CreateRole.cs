namespace AM.Application.Contracts.Role
{
    public class CreateRole
    {
        public string Name { get; set; }
    }

    public class EditRole : CreateRole
    {
        public int Id { get; set; }
    }

    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreationTime { get; set; }
    }
}