namespace Npam.Interop{

    ///<summary>
    /// The PAM Message Styles.
    ///</summary>
    public enum MessageStyle {
        
        PAM_PROMPT_ECHO_OFF = 1,
        PAM_PROMPT_ECHO_ON = 2,
        PAM_ERROR_MSG = 3,
        PAM_TEXT_INFO = 4,

        /* Linux-PAM specific types */
        
        ///<summary>
        /// Yes/No/Maybe conditionals
        ///</summary>
        PAM_RADIO_TYPE = 5,

        ///<summary>
        /// This is for server client non-human interaction.. these are NOT
        /// part of the X/Open PAM specification.
        ///</summary>
	    PAM_BINARY_PROMPT  = 7,
    }
}