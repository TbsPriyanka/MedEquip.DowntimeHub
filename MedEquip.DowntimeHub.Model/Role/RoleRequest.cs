namespace MedEquip.DowntimeHub.Model.Role
{
    public class RoleRequest
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
