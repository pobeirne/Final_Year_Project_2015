using System.Diagnostics;
using LuckyMe.CMS.Data;
using LuckyMe.CMS.Data.Repository;
using LuckyMe.CMS.Entity.DTO;
using LuckyMe.CMS.Service.Services;
using LuckyMe.CMS.Service.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LuckyMe.CMS.Service.Tests
{
    [TestClass]
    public class UserAccountTest
    {
        private IUserService _userservice;

        [TestInitialize]
        public void Initialise()
        {
            _userservice = new UserService(new UserRepository(new LuckyMeCMSEntityContext()));
        }

        [TestMethod]
        public void GetAllUsersAsync()
        {
            var list = _userservice.GetAllUsersAsync().Result;

            foreach (var item in list)
            {
                Trace.TraceInformation(
                    " User ID    : {0} ," +
                    " User Email : {1} ," +
                    " User Name  : {2} ," +
                    " User Providers : {3} "
                    , item.Id
                    , item.Email
                    , item.UserName
                    , item.UserProviders.Count
                    );
            }
        }

        [TestMethod]
        public void GetUser()
        {
            var user = _userservice.GetUserById("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            Trace.TraceInformation(
                " \n User ID    : {0} \n," +
                " User Email : {1} \n," +
                " User Name  : {2} \n," +
                " User Providers : {3} \n"
                , user.Id
                , user.Email
                , user.UserName
                , user.UserProviders.Count
                );
        }

        [TestMethod]
        public void AddProviderToUser()
        {
            var provider = new UserProviderDTO()
            {
                LoginProvider = "FaceBook_Ser",
                ProviderKey = "Test",
                UserId = "16cc730a-6db4-416c-8f8a-b8bad47f34ae"
            };
            _userservice.InsertUserExternalLoginEntry(provider);
        }
    }
}
