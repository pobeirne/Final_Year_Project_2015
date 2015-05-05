using System.Diagnostics;
using LuckyMe.CMS.Common.Models.DTO;
using LuckyMe.CMS.Data;
using LuckyMe.CMS.Data.Repository;
using LuckyMe.CMS.Service.Services;
using LuckyMe.CMS.Service.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LuckyMe.CMS.Service.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService _userservice;
        private static readonly string TestUserId = "16cc730a-6db4-416c-8f8a-b8bad47f34ae";

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
                    " User Claims : {3} "
                    , item.Id
                    , item.Email
                    , item.UserName
                    , item.UserClaims.Count
                    );
            }
        }

        [TestMethod]
        public void GetUserByIdAsync()
        {
            var user = _userservice.GetUserByIdAsync(TestUserId).Result;
            if (user != null)
            {

                Trace.TraceInformation(
                    " \n User ID    : {0} \n," +
                    " User Email : {1} \n," +
                    " User Name  : {2} \n," +
                    " User Claims : {3} \n"
                    , user.Id
                    , user.Email
                    , user.UserName
                    , user.UserClaims.Count
                    );
            }
        }

        [TestMethod]
        public void InsertUserClaimAsync()
        {
            var claim = new UserClaimDto
            {
                ClaimType = "FaceBook_Data",
                ClaimValue = "Test",
                UserId = TestUserId
            };
            var result = _userservice.InsertUserClaimAsync(claim).Result;
            Trace.TraceInformation(" \n Result   : {0} \n,", result);
        }

        [TestMethod]
        public void UpdateUserClaimAsync()
        {
            var claim = new UserClaimDto
            {
                ClaimType = "FaceBook_Data",
                ClaimValue = "Test1",
                UserId = TestUserId
            };
            var result = _userservice.UpdateUserClaimAsync(claim).Result;
            Trace.TraceInformation(" \n Result   : {0} \n,", result);
        }

        [TestMethod]
        public void DeleteUserClaimAsync()
        {
            var claim = new UserClaimDto
            {
                ClaimType = "FaceBook_Data",
                ClaimValue = "Test1",
                UserId = TestUserId
            };
            var result = _userservice.DeleteUserClaimAsync(claim).Result;
            Trace.TraceInformation(" \n Result   : {0} \n,", result);
        }


        [TestMethod]
        public void DeleteUserAsync()
        {
            //var user = _userservice.GetUserByIdAsync(_testUserId).Result;
            //if (user != null)
            //{
            //    var result = _userservice.DeleteUserAsync(user).Result;
            //    if (result)
            //    {
            //        Trace.TraceInformation(" \n Result   : {0} \n,", true);
            //    }
            //}
        }

    }
}
