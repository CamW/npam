using System;
using System.Collections.Generic;
using Npam.Interop;

namespace Npam
{
    public class NpamSession : IDisposable
    { 
        private IntPtr pamHandle = IntPtr.Zero;
        private string serviceName;
        private string user;
        private IntPtr appData = IntPtr.Zero;

        public delegate IEnumerable<PamResponse> ConvCallbackDelegate (IEnumerable<PamMessage> messages, IntPtr appData);
        private ConvCallbackDelegate conversationCallback;
        private bool endCalled = false;


        //Must be tracked and passed in when calling pam_end;
        private PamStatus lastReturnedValue = PamStatus.PAM_SUCCESS;
        private readonly object PamCallLock = new object();

        public NpamSession(string serviceName, string user, ConvCallbackDelegate conversationCallback, IntPtr appData) {
            this.serviceName = serviceName;
            this.conversationCallback = conversationCallback;
            this.user = user;
            this.appData = appData;
        }

        public PamStatus Start() {
            PamConv conversation = new PamConv();
            conversation.ConversationCallback = HandlePamConversation;
            conversation.AppData = appData;
            lock(this.PamCallLock) {
                if (this.pamHandle != IntPtr.Zero) {
                    throw new InvalidOperationException("Start may not be called multiple times for the same PamSession!");
                }
                this.lastReturnedValue = Pam.pam_start(this.serviceName, user, conversation, ref pamHandle);
                return this.lastReturnedValue;
            }
        }

        public PamStatus Authenticate(int flags) {
            lock(this.PamCallLock) {
                EnsureSessionAlive();
                this.lastReturnedValue = Pam.pam_authenticate(this.pamHandle, flags);
                return this.lastReturnedValue;
            }
        }

        public PamStatus AccountManagement(int flags) {
            lock(this.PamCallLock) {
                EnsureSessionAlive();
                this.lastReturnedValue = Pam.pam_acct_mgmt(this.pamHandle, flags);
                return this.lastReturnedValue;
            }
        }

        public PamStatus End() {
            lock(this.PamCallLock) {
                if (this.pamHandle == IntPtr.Zero) return PamStatus.PAM_SUCCESS;
                if (this.endCalled) return this.lastReturnedValue;
                this.lastReturnedValue = Pam.pam_end(this.pamHandle, this.lastReturnedValue);
                this.endCalled = true;
                return this.lastReturnedValue;
            }
        }

        private void EnsureSessionAlive() {
            if (this.endCalled)
                throw new InvalidOperationException("Pam session has been destroyed and may not be interacted with.");
        }

        private PamStatus HandlePamConversation(int messageCount, IntPtr messageArrayPtr, ref IntPtr responseArrayPtr,  IntPtr appDataPtr) {
            if (messageCount <= 0) return PamStatus.PAM_CONV_ERR;
            var messages = MarshalUtils.MarshalPtrPtrStructIn<PamMessage>(messageCount, messageArrayPtr);


            List<PamResponse> responses;
            try {
                responses = new List<PamResponse>(this.conversationCallback(messages, appDataPtr));
            } catch {
                return PamStatus.PAM_CONV_ERR;
            }

            if (messageCount == 1) {
                responseArrayPtr = MarshalUtils.MarshalPtrStructOut<PamResponse>(responses[0]);
            } else {
                responseArrayPtr = MarshalUtils.MarshalPtrPtrStructOut<PamResponse>(responses);
            }

            return PamStatus.PAM_SUCCESS;
        }

        public void Dispose()
        {
            End();
        }

        ~NpamSession() {
            End();
        }
    }
}