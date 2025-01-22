namespace WiseMaestroRBAC.Models
{
    public static class UserRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string Viewer = "Viewer";

        public static int GetRoleLevel(string role) => role switch
        {
            SuperAdmin => 4,
            Admin => 3,
            Editor => 2,
            Viewer => 1,
            _ => 0
        };
    }
}
