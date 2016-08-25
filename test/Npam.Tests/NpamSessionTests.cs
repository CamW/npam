using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Npam.Interop;
using Xunit;

namespace Npam.Test
{
    public class NpamSessionTests
    {
        private static IEnumerable<PamResponse> ConvHandler (IEnumerable<PamMessage> messages, IntPtr appData) {
            foreach (PamMessage message in messages)
            {
                yield return new PamResponse(NpamTestsCommon.TestPassword);
            }
        }


        [Fact]
        public void TestAuthForGoodUser() 
        {
            long start = DateTime.Now.Ticks;
            IntPtr appData = Marshal.AllocHGlobal(Marshal.SizeOf<long>());
            Marshal.WriteInt64(appData, 0, start);

            NpamSession session = null; 
            using (session = new NpamSession(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameGood, ConvHandler, appData)) {
                Assert.Equal(PamStatus.PAM_SUCCESS, session.Start());
                Assert.Throws<InvalidOperationException>(() => { session.Start(); });
                Assert.Equal(PamStatus.PAM_SUCCESS, session.Authenticate(0));
                Assert.Equal(PamStatus.PAM_SUCCESS, session.AccountManagement(0));
            }
            Assert.Throws<InvalidOperationException>(() => { session.AccountManagement(0); });
        }

        [Fact]
        public void TestFailForBadUser() 
        {
            long start = DateTime.Now.Ticks;
            IntPtr appData = Marshal.AllocHGlobal(Marshal.SizeOf<long>());
            Marshal.WriteInt64(appData, 0, start);

            NpamSession session = null; 
            using (session = new NpamSession(NpamTestsCommon.TestService, NpamTestsCommon.TestUsernameBad, ConvHandler, appData)) {
                Assert.Equal(PamStatus.PAM_SUCCESS, session.Start());
                Assert.Throws<InvalidOperationException>(() => { session.Start(); });
                Assert.Equal(PamStatus.PAM_AUTH_ERR, session.Authenticate(0));
                Assert.Equal(PamStatus.PAM_AUTH_ERR, session.AccountManagement(0));
            }
            Assert.Throws<InvalidOperationException>(() => { session.AccountManagement(0); });
        }
    }
}
