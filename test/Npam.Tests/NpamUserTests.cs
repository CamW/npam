using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;

namespace Npam.Tests
{
    public class NpamUserTests
    {
        [Fact]
        public void TestAuthForGoodUser() 
        {
            Assert.True(NpamUser.Authenticate(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameGood, NpamTestsCommon.TestPassword));
        }

        [Fact]
        public void TestAuthFailForBadUser() 
        {
            Assert.False(NpamUser.Authenticate(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameBad, NpamTestsCommon.TestPassword));
        }

        [Fact]
        public void TestGroupsForGoodUser() 
        {
            var groups = NpamUser.GetGroups(NpamTestsCommon.TestUsernameGood);
            foreach(var theGroup in groups) {
                Assert.Equal(NpamTestsCommon.TestUsernameGood, theGroup.GroupName);
            }
        }

        [Fact]
        public void TestGroupsFailForBadUser() 
        {
            Assert.Equal(0, NpamUser.GetGroups(NpamTestsCommon.TestUsernameBad).ToList().Count);
        }

        [Fact]
        public void TestAuthForGoodUserThreaded() 
        {
            const int TotalAuthCalls = 5000;
            Console.WriteLine("Running {0} threaded auth tests...", TotalAuthCalls);
            var complete = 0;
            var sw = new Stopwatch();
            sw.Start();
            for(var i = 0; i < (TotalAuthCalls / 2); i++) {
               var thread1 = new Thread(() => { Assert.True(NpamUser.Authenticate(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameGood, NpamTestsCommon.TestPassword)); Interlocked.Increment(ref complete); });
               var thread2 = new Thread(() => { Assert.False(NpamUser.Authenticate(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameBad, NpamTestsCommon.TestPassword)); Interlocked.Increment(ref complete); });
               thread1.Start();
               thread2.Start();
            }
            while(complete < TotalAuthCalls) Thread.Sleep(1);
            sw.Stop();
            Console.WriteLine("Completed {0} threaded auth tests in {1} seconds.", complete, sw.Elapsed.TotalSeconds);
            Thread.Sleep(2000);
            Assert.Equal(TotalAuthCalls, complete);
        }
    }
}
