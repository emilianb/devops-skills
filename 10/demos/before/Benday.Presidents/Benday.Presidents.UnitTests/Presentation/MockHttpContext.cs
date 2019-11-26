using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Security.Principal;
using Benday.Presidents.Core.Services;
using Benday.Presidents.Core.Models;

namespace Benday.Presidents.UnitTests.Presentation
{
    public class MockHttpContext : HttpContextBase
    {
        
        public MockHttpContext()
        {
            
        }

        public override IPrincipal User
        {
            get;
            set;
        }
    }
}
