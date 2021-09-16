namespace IdentityServer.Domain.Constants
{
    public static class ApplicationConstants
    {
        public const string DeviceIdParam = "device_id";

        public static class Resources
        {
            public const string MobileApi = "qpay_mobile_api";
            public const string AdminApi = "qpay_admin_api";
            public const string AtmApi = "qpay_atm_api";
        }
        
        public static class Scopes
        {
            public const string MobileApplication = "mobile_application";
            public const string AdminApplication = "admin_application";
            public const string AtmApplication = "atm_application";
        }
    }
}