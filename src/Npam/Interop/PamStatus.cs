namespace Npam.Interop
{
    ///<summary>
    /// The PAM return values.
    ///</summary>
    public enum PamStatus
    {
        /// <summary>
        /// Successful function return.
        /// </summary>
        PamSuccess = 0,

        /// <summary>
        /// dlopen() failure when dynamically
        /// loading a service module.
        /// </summary>
        PamOpenErr = 1,

        /// <summary>
        /// Symbol not found.
        /// </summary>
        PamSymbolErr = 2,

        /// <summary>
        /// Error in service module.
        /// </summary>
        PamServiceErr = 3,

        /// <summary>
        /// System error.
        /// </summary>
        PamSystemErr = 4,

        /// <summary>
        /// Memory buffer error.
        /// </summary>
        PamBufErr = 5,

        /// <summary>
        /// Permission denied.
        /// </summary>
        PamPermDenied = 6,

        /// <summary>
        /// Authentication failure.
        /// </summary>
        PamAuthErr = 7,

        /// <summary>
        /// Can not access authentication data
        /// due to insufficient credentials.
        /// </summary>
        PamCredInsufficient = 8,

        /// <summary>
        /// Underlying authentication service
        /// can not retrieve authentication
        /// information.
        /// </summary>
        PamAuthInfoUnavailable = 9,

        /// <summary>
        /// User not known to the underlying authentication module.
        /// </summary>
        PamUserUnknown = 10,

        /// <summary>
        /// An authentication service has
        /// maintained a retry count which has
        /// been reached.  No further retries
        /// should be attempted.
        /// </summary>
        PamMaxTries = 11,

        /// <summary>
        /// New authentication token required.
        /// This is normally returned if the
        /// machine security policies require
        /// that the password should be changed
        /// because the password is NULL or it
        /// has aged.
        /// </summary>
        PamNewAuthTokenRequired = 12,

        /// <summary>
        /// User account has expired.
        /// </summary>
        PamAcctExpired = 13,

        /// <summary>
        /// Can not make/remove an entry for
        /// the specified session.
        /// </summary>
        PamSessionErr = 14,

        /// <summary>
        /// Underlying authentication service
        /// can not retrieve user credentials
        /// unavailable.
        /// </summary>
        PamCredUnavailable = 15,

        /// <summary>
        /// User credentials expired
        /// </summary>
        PamCredExpired = 16,

        /// <summary>
        /// Failure setting user credentials.
        /// </summary>
        PamCredErr = 17,

        /// <summary>
        /// No module specific data is present.
        /// </summary>
        PamNoModuleData = 18,

        /// <summary>
        /// Conversation error.
        /// </summary>
        PamConvErr = 19,

        /// <summary>
        /// Authentication token manipulation error.
        /// </summary>
        PamAuthTokenErr = 20,

        /// <summary>
        /// Authentication information cannot be recovered.
        /// </summary>
        PamAuthTokenRecoveryErr = 21,

        /// <summary>
        /// Authentication token lock busy.
        /// </summary>
        PamAuthTokenLockBusy = 22,

        /// <summary>
        /// Authentication token aging disabled.
        /// </summary>
        PamAuthTokenDisableAging = 23,

        /// <summary>
        /// Preliminary check by password service.
        /// </summary>
        PamTryAgain = 24,

        /// <summary>
        /// Ignore underlying account module
        /// regardless of whether the control
        /// flag is required, optional, or sufficient.
        /// </summary>
        PamIgnore = 25,

        /// <summary>
        /// Critical error (?module fail now request).
        /// </summary>
        PamAbort = 26,

        /// <summary>
        /// User's authentication token has expired.
        /// </summary>
        PamAuthTokenExpired = 27,

        /// <summary>
        /// Module is not known.
        /// </summary>
        PamModuleUnknown = 28,

        /// <summary>
        /// Bad item passed to pam_*_item().
        /// </summary>
        PamBadItem = 29,

        /// <summary>
        ///Conversation function is event driven
        ///and data is not available yet.
        /// </summary>
        PamConvAgain = 30,

        /// <summary>
        /// Please call this function again to
        /// complete authentication stack. Before
        /// calling again, verify that conversation
        /// is completed.
        /// </summary>
        PamIncomplete = 31,
    }
}