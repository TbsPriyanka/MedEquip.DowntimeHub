namespace MedEquip.DowntimeHub.Model.User
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string Department { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? Password { get; set; }
        public int ActionBy { get; set; }
    }
}
