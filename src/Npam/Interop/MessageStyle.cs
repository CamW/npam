namespace Npam.Interop
{
    ///<summary>
    /// The PAM Message Styles.
    ///</summary>
    public enum MessageStyle
    {
        PamPromptEchoOff = 1,
        PamPromptEchoOn = 2,
        PamErrorMsg = 3,
        PamTextInfo = 4,

        /* Linux-PAM specific types */

        ///<summary>
        /// Yes/No/Maybe conditionals
        ///</summary>
        PamRadioType = 5,

        ///<summary>
        /// This is for server client non-human interaction.. these are NOT
        /// part of the X/Open PAM specification.
        ///</summary>
        PamBinaryPrompt = 7,
    }
}