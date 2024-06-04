namespace trans_api.Services
{
    public class AuthorizeService: IAuthorizeService
    {
        public bool HasRequiredPermissionAdmin(string role)
        {
            // Implement your logic to check if the user has the required permissions
            return role == "Admin";
        }

        public bool HasRequiredPermissionAdminAndAgent(string role)
        {
            // Implement your logic to check if the user has the required permissions
            return role == "Admin" || role == "Agent";
        }
    }
}
