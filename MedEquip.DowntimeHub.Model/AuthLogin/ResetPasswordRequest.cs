namespace MedEquip.DowntimeHub.Model.AuthLogin
{
    public class ResetPasswordRequest
    {
        public string ResetToken { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
