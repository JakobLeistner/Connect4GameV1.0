using RestAPIClient.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIClient
{
    public class AuthProvider
    {
        private readonly RestApiClient restApiClient;

        public AuthProvider(RestApiClient restApiClient)
        {
            this.restApiClient = restApiClient;
        }

        public Session AktSession { get; set; }
        public UserReturnModel AktUser { get; set; }
        public async Task<bool> IsSessionValid()
        {
            if (AktSession == null) {
                return false;
            }
            return await restApiClient.Post<bool>("User/IsSessionValid", AktSession);
        }

        public async Task<LoginResultModel> Login(LoginModel data)
        {
            var result = await restApiClient.Post<LoginResultModel>("User", data);
            if (result.Success)
            {
                AktSession = result.UserModel.Session;
                AktUser = result.UserModel;
            }
            return result;
        }

        public async Task<UserReturnModel> GetUserData(int id)
        {
            return await restApiClient.Get<UserReturnModel>($"GetUser?id={id}");
        }
    }
}
