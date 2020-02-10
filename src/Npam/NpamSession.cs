using System;
using System.Collections.Generic;
using Npam.Interop;

namespace Npam
{
    public class NpamSession : IDisposable
    {
        private IntPtr _pamHandle = IntPtr.Zero;
        private readonly string _serviceName;
        private readonly string _user;
        private readonly IntPtr _appData;

        public delegate IEnumerable<PamResponse> ConvCallbackDelegate(IEnumerable<PamMessage> messages, IntPtr appData);

        private readonly ConvCallbackDelegate _conversationCallback;
        private bool _endCalled = false;


        //Must be tracked and passed in when calling pam_end;
        private PamStatus _lastReturnedValue = PamStatus.PamSuccess;
        private readonly object _pamCallLock = new object();

        public NpamSession(string serviceName, string user, ConvCallbackDelegate conversationCallback, IntPtr appData)
        {
            this._serviceName = serviceName;
            this._conversationCallback = conversationCallback;
            this._user = user;
            this._appData = appData;
        }

        ///<summary>
        /// Initializes the session. Must be called first.
        /// http://linux.die.net/man/3/pam_start
        ///</summary>
        public PamStatus Start()
        {
            var conversation = new PamConv();
            conversation.ConversationCallback = HandlePamConversation;
            conversation.AppData = _appData;
            lock (this._pamCallLock)
            {
                if (this._pamHandle != IntPtr.Zero)
                {
                    throw new InvalidOperationException(
                        "Start may not be called multiple times for the same PamSession!");
                }

                this._lastReturnedValue = Pam.pam_start(this._serviceName, _user, conversation, ref _pamHandle);
                return this._lastReturnedValue;
            }
        }

        ///<summary>
        /// Authenticates the user against PAM. Only ensures that the username and password are correct. Call AccountManagement to check that the account is in good standing.
        /// http://linux.die.net/man/3/pam_authenticate
        ///</summary>
        public PamStatus Authenticate(int flags)
        {
            lock (this._pamCallLock)
            {
                EnsureSessionAlive();
                this._lastReturnedValue = Pam.pam_authenticate(this._pamHandle, flags);
                return this._lastReturnedValue;
            }
        }

        ///<summary>
        /// Ensures that the account is in good standing - not locked out expired, etc.
        /// http://linux.die.net/man/3/pam_acct_mgmt
        ///</summary>
        public PamStatus AccountManagement(int flags)
        {
            lock (this._pamCallLock)
            {
                EnsureSessionAlive();
                this._lastReturnedValue = Pam.pam_acct_mgmt(this._pamHandle, flags);
                return this._lastReturnedValue;
            }
        }

        ///<summary>
        /// Releases the PAM handle held as part of this session.
        /// http://linux.die.net/man/3/pam_end
        ///</summary>
        public PamStatus End()
        {
            lock (this._pamCallLock)
            {
                if (this._pamHandle == IntPtr.Zero) return PamStatus.PamSuccess;
                if (this._endCalled) return this._lastReturnedValue;
                this._lastReturnedValue = Pam.pam_end(this._pamHandle, this._lastReturnedValue);
                this._endCalled = true;
                return this._lastReturnedValue;
            }
        }

        private void EnsureSessionAlive()
        {
            if (this._endCalled)
                throw new InvalidOperationException("Pam session has been destroyed and may not be interacted with.");
        }

        private PamStatus HandlePamConversation(int messageCount, IntPtr messageArrayPtr, ref IntPtr responseArrayPtr,
            IntPtr appDataPtr)
        {
            if (messageCount <= 0) return PamStatus.PamConvErr;
            var messages = MarshalUtils.MarshalPtrPtrStructIn<PamMessage>(messageCount, messageArrayPtr);


            List<PamResponse> responses;
            try
            {
                responses = new List<PamResponse>(this._conversationCallback(messages, appDataPtr));
            }
            catch
            {
                return PamStatus.PamConvErr;
            }

            responseArrayPtr = messageCount == 1
                ? MarshalUtils.MarshalPtrStructOut(responses[0])
                : MarshalUtils.MarshalPtrPtrStructOut(responses);

            return PamStatus.PamSuccess;
        }

        public void Dispose()
        {
            End();
        }

        ~NpamSession()
        {
            End();
        }
    }
}