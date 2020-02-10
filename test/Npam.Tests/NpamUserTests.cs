using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Npam.Tests
{
    public class NpamUserTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NpamUserTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestAuthForGoodUser()
        {
            Assert.True(NpamUser.Authenticate(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameGood,
                NpamTestsCommon.TestPassword));
        }

        [Fact]
        public void TestAuthFailForBadUser()
        {
            Assert.False(NpamUser.Authenticate(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameBad,
                NpamTestsCommon.TestPassword));
        }

        [Fact]
        public void TestGroupsForGoodUser()
        {
            var groups = NpamUser.GetGroups(NpamTestsCommon.TestUsernameGood);
            foreach (var theGroup in groups)
            {
                Assert.Equal(NpamTestsCommon.TestUsernameGood, theGroup.GroupName);
            }
        }

        [Fact]
        public void TestGroupsFailForBadUser()
        {
            Assert.Empty(NpamUser.GetGroups(NpamTestsCommon.TestUsernameBad).ToList());
        }

        [Fact]
        public void TestAuthForGoodUserThreaded()
        {
            const int totalAuthCalls = 5000;
            _testOutputHelper.WriteLine("Running {0} threaded auth tests...", totalAuthCalls);
            var complete = 0;
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < (totalAuthCalls / 2); i++)
            {
                var thread1 = new Thread(() =>
                {
                    Assert.True(NpamUser.Authenticate(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameGood,
                        NpamTestsCommon.TestPassword));
                    Interlocked.Increment(ref complete);
                });
                var thread2 = new Thread(() =>
                {
                    Assert.False(NpamUser.Authenticate(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameBad,
                        NpamTestsCommon.TestPassword));
                    Interlocked.Increment(ref complete);
                });
                thread1.Start();
                thread2.Start();
            }

            while (complete < totalAuthCalls) Thread.Sleep(1);
            sw.Stop();
            _testOutputHelper.WriteLine("Completed {0} threaded auth tests in {1} seconds.", complete, sw.Elapsed.TotalSeconds);
            Thread.Sleep(2000);
            Assert.Equal(totalAuthCalls, complete);
        }
    }
}