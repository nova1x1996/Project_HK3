using PayPal.Api;

namespace Project.Helper
 {
     public static class HelperPayPal

     {
         private static string _clientId = "AY9QEn6YbHDWeivGOdkpZw_4iWCmTFZnbn7ixfIwaSG-eE4T1FW2LwWJkaJIA_rYmAMbiHVIwfpQ_1e4";
         private static string _secretKey = "ECwdw92rp7s3uSPDhDMqGwPcqo8dv4Ydy2FHLzsHC5eSNEK79Q2Qw5fqF6jmILUdCOrPtSGUnm89kAt-";
         private static string Mode = "sb-pjm9425080818@business.example.com";

         public static APIContext GetAPIContext()
         {
             var config = new Dictionary<string, string>
     {
         {"mode", "sandbox"}, // hoặc "live" nếu bạn sử dụng môi trường sản phẩm
         {"clientId", _clientId},
         {"clientSecret", _secretKey}
     };
             return new APIContext(new OAuthTokenCredential(config).GetAccessToken());
         }

     }
 }