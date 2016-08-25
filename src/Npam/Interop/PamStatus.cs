namespace Npam.Interop{

    ///<summary>
    /// The PAM return values.
    ///</summary>
    public enum PamStatus {
        
        /// <summary>
        /// Successful function return.
        /// </summary>
        PAM_SUCCESS = 0,

        /// <summary>
        /// dlopen() failure when dynamically
        /// loading a service module.
        /// </summary>
        PAM_OPEN_ERR = 1,
        
        /// <summary>
        /// Symbol not found.
        /// </summary>
        PAM_SYMBOL_ERR = 2,

        /// <summary>
        /// Error in service module.
        /// </summary>
        PAM_SERVICE_ERR = 3,

        /// <summary>
        /// System error.
        /// </summary>
        PAM_SYSTEM_ERR = 4,

        /// <summary>
        /// Memory buffer error.
        /// </summary>
        PAM_BUF_ERR = 5,

        /// <summary>
        /// Permission denied.
        /// </summary>
        PAM_PERM_DENIED = 6,

        /// <summary>
        /// Authentication failure.
        /// </summary>
        PAM_AUTH_ERR = 7,

        /// <summary>
        /// Can not access authentication data
        /// due to insufficient credentials.
        /// </summary>
        PAM_CRED_INSUFFICIENT = 8,

        /// <summary>
        /// Underlying authentication service
        /// can not retrieve authentication
        /// information.
        /// </summary>
        PAM_AUTHINFO_UNAVAIL = 9,  

        /// <summary>
        /// User not known to the underlying authenticaiton module.
        /// </summary>
        PAM_USER_UNKNOWN = 10,

        /// <summary>
        /// An authentication service has
        /// maintained a retry count which has
        /// been reached.  No further retries
        /// should be attempted.
        /// </summary>
        PAM_MAXTRIES = 11,     

        /// <summary>
        /// New authentication token required.
        /// This is normally returned if the
        /// machine security policies require
        /// that the password should be changed
        /// beccause the password is NULL or it
        /// has aged.
        /// </summary>
        PAM_NEW_AUTHTOK_REQD = 12,

        /// <summary>
        /// User account has expired.
        /// </summary>
        PAM_ACCT_EXPIRED = 13,

        /// <summary>
        /// Can not make/remove an entry for
        /// the specified session.
        /// </summary>
        PAM_SESSION_ERR = 14,

        /// <summary>
        /// Underlying authentication service
        /// can not retrieve user credentials
        /// unavailable.
        /// </summary>
        PAM_CRED_UNAVAIL = 15,

        /// <summary>
        /// User credentials expired
        /// </summary>
        PAM_CRED_EXPIRED = 16,

        /// <summary>
        /// Failure setting user credentials.
        /// </summary>
        PAM_CRED_ERR = 17,

        /// <summary>
        /// No module specific data is present.
        /// </summary>
        PAM_NO_MODULE_DATA = 18,

        /// <summary>
        /// Conversation error.
        /// </summary>
        PAM_CONV_ERR = 19,

        /// <summary>
        /// Authentication token manipulation error.
        /// </summary>
        PAM_AUTHTOK_ERR = 20,

        /// <summary>
        /// Authentication information cannot be recovered.
        /// </summary>
        PAM_AUTHTOK_RECOVERY_ERR = 21,

        /// <summary>
        /// Authentication token lock busy.
        /// </summary>
        PAM_AUTHTOK_LOCK_BUSY = 22,

        /// <summary>
        /// Authentication token aging disabled.
        /// </summary>
        PAM_AUTHTOK_DISABLE_AGING = 23,

        /// <summary>
        /// Preliminary check by password service.
        /// </summary>
        PAM_TRY_AGAIN = 24,

        /// <summary>
        /// Ignore underlying account module
        /// regardless of whether the control
        /// flag is required, optional, or sufficient.
        /// </summary>
        PAM_IGNORE = 25,
        /// <summary>
        /// Critical error (?module fail now request).
        /// </summary>
        PAM_ABORT = 26,

        /// <summary>
        /// User's authentication token has expired.
        /// </summary>
        PAM_AUTHTOK_EXPIRED = 27, 

        /// <summary>
        /// Module is not known.
        /// </summary>
        PAM_MODULE_UNKNOWN = 28,

        /// <summary>
        /// Bad item passed to pam_*_item().
        /// </summary>
        PAM_BAD_ITEM = 29,

        /// <summary>
        ///Conversation function is event driven
        ///and data is not available yet.
        /// </summary>
        PAM_CONV_AGAIN = 30,

        /// <summary>
        /// Please call this function again to
        /// complete authentication stack. Before
        /// calling again, verify that conversation
        /// is completed.
        /// </summary>
        PAM_INCOMPLETE = 31,
    }
}