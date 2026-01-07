namespace MedEquip.DowntimeHub.Common.SqlHelper
{
    public static class StoredProcedure
    {
        public const string User_AddOrUpdate = "User_AddOrUpdate";
        public const string User_GetById = "User_GetById";
        public const string User_List = "User_List";
        public const string User_Delete = "User_Delete";

        public const string User_Login = "User_Login";
        public const string User_ForgotPassword = "User_ForgotPassword";
        public const string User_Logout = "User_Logout";

        public const string Role_AddOrUpdate = "Role_AddOrUpdate";
        public const string Role_GetById = "Role_GetById";
        public const string Role_List = "Role_List";
        public const string Role_Delete = "Role_Delete";
    }
}
