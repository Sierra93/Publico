using System;
using System.Collections.Generic;
using System.Text;
using Publico.Models;
using Publico.Controllers;
using System.Collections;
using Publico.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Publico.Tests {
    [TestClass]
    public class AuthControllerTests {
        [TestMethod]
        public void HashPasswordTest() {
            string hashString = "123456";
            var isHash = HashMD5Service.HashPassword(hashString);
            Assert.IsInstanceOfType(isHash.Result, typeof(String));
        }
    }
}
