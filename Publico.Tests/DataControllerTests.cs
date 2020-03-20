using Microsoft.VisualStudio.TestTools.UnitTesting;
using Publico.Models;
using Publico.Controllers;
using System.Collections;
using System.Collections.Generic;

namespace Publico.Tests {
    [TestClass]
    public class DataControllerTests {
        [TestMethod]
        public void AddFriendTest() {
            var oData = new { 
                Login = "Tom",
                Password = "123"
            };
            var oFriends = new List<IEnumerable>();
            Assert.IsNotNull(oData.Login);
            Assert.IsNotNull(oData.Password);
            oFriends.Add(oData.Login);
            Assert.IsNotNull(oFriends);
        }
    }
}