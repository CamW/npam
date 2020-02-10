using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Npam.Interop;
using Xunit;

namespace Npam.Tests
{
    public class NpamSessionTests
    {
        private static IEnumerable<PamResponse> ConvHandler(IEnumerable<PamMessage> messages, IntPtr appData)
        {
            return messages.Select(message => new PamResponse(NpamTestsCommon.TestPassword));
        }


        [Fact]
        public void TestAuthForGoodUser()
        {
            var start = DateTime.Now.Ticks;
            var appData = Marshal.AllocHGlobal(Marshal.SizeOf<long>());
            Marshal.WriteInt64(appData, 0, start);

            NpamSession session = null;
            using (session = new NpamSession(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameGood, ConvHandler,
                appData))
            {
                Assert.Equal(PamStatus.PamSuccess, session.Start());
                Assert.Throws<InvalidOperationException>(() => { session.Start(); });
                Assert.Equal(PamStatus.PamSuccess, session.Authenticate(0));
                Assert.Equal(PamStatus.PamSuccess, session.AccountManagement(0));
            }

            Assert.Throws<InvalidOperationException>(() => { session.AccountManagement(0); });
        }

        [Fact]
        public void TestFailForBadUser()
        {
            var start = DateTime.Now.Ticks;
            var appData = Marshal.AllocHGlobal(Marshal.SizeOf<long>());
            Marshal.WriteInt64(appData, 0, start);

            NpamSession session = null;
            using (session = new NpamSession(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameBad, ConvHandler,
                appData))
            {
                Assert.Equal(PamStatus.PamSuccess, session.Start());
                Assert.Throws<InvalidOperationException>(() => { session.Start(); });
                Assert.Equal(PamStatus.PamAuthErr, session.Authenticate(0));
                Assert.Equal(PamStatus.PamAuthErr, session.AccountManagement(0));
            }

            Assert.Throws<InvalidOperationException>(() => { session.AccountManagement(0); });
        }
    }
}