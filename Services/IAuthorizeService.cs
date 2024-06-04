namespace trans_api.Services
{
    public interface IAuthorizeService // Define IAuthorizeService interface
    {
        // Method to check if the role has required permission for admin
        bool HasRequiredPermissionAdmin(string role);

        // Method to check if the role has required permission for admin and agent
        bool HasRequiredPermissionAdminAndAgent(string role);
    }
}
