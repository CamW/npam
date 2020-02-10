using System;
using System.Collections.Generic;
using Npam.Interop;

namespace Npam
{
    public static class NpamUser
    {
        private const int AuthenticateFlags = 0;
        private const int AccountManagementFlags = 0;

        public static bool Authenticate(string serviceName, string user, string password)
        {
            //Initialize
            var lastReturnedValue = PamStatus.PamSuccess;
            var pamHandle = IntPtr.Zero;
            var conversation = new PamConv();
            var conversationHandler = new ConversationHandler(password);
            conversation.ConversationCallback = conversationHandler.HandlePamConversation;

            try
            {
                //Start
                lastReturnedValue = Pam.pam_start(serviceName, user, conversation, ref pamHandle);
                if (lastReturnedValue != PamStatus.PamSuccess) return false;
                //Authenticate - Verifies username and password
                lastReturnedValue = Pam.pam_authenticate(pamHandle, AuthenticateFlags);
                if (lastReturnedValue != PamStatus.PamSuccess) return false;
                //Account Management - Checks that account is valid, checks account expiration, access restrictions.
                lastReturnedValue = Pam.pam_acct_mgmt(pamHandle, AccountManagementFlags);
                if (lastReturnedValue != PamStatus.PamSuccess) return false;
            }
            finally
            {
                lastReturnedValue = Pam.pam_end(pamHandle, lastReturnedValue);
            }

            return true;
        }

        public static IEnumerable<Group> GetGroups(string user)
        {
            var info = StdLibC.GetPwNamAsAccountInfo(user);
            if (info == null) yield break;
            var numGroups = 0;
            var groupIdArray = new int[numGroups];
            StdLibC.getgrouplist(user, info.GroupID, groupIdArray, ref numGroups);
            groupIdArray = new int[numGroups];
            StdLibC.getgrouplist(user, info.GroupID, groupIdArray, ref numGroups);
            foreach (var groupId in groupIdArray)
                yield return StdLibC.GetGrGidAsGroup(groupId);
        }

        ///<summary>
        /// Gets information on the specified user such as real name, shell and home directory.
        /// http://linux.die.net/man/3/getpwnam
        ///</summary>
        public static AccountInfo GetAccountInfo(string user)
        {
            return StdLibC.GetPwNamAsAccountInfo(user);
        }

        internal class ConversationHandler
        {
            private readonly string _password;

            public ConversationHandler(string password)
            {
                this._password = password;
            }

            public PamStatus HandlePamConversation(int messageCount, IntPtr messageArrayPtr,
                ref IntPtr responseArrayPtr, IntPtr appDataPtr)
            {
                if (messageCount <= 0) return PamStatus.PamConvErr;
                var messages = MarshalUtils.MarshalPtrPtrStructIn<PamMessage>(messageCount, messageArrayPtr);
                if (messageCount == 1)
                {
                    responseArrayPtr = MarshalUtils.MarshalPtrStructOut(new PamResponse(_password));
                }
                else
                {
                    throw new NotSupportedException(
                        "NpamAuthentication does not support PAM modules which require responses to multiple conversational messages. Please use NpamSession instead.");
                }

                return PamStatus.PamSuccess;
            }
        }
    }
}