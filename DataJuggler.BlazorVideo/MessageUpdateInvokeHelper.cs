

#region using statements

using System;
using Microsoft.JSInterop;

#endregion

namespace DataJuggler.BlazorVideo
{

    #region class MessageUpdateInvokeHelper
    /// <summary>
    /// This class is used to receive messages from JavaScript
    /// </summary>
    public class MessageUpdateInvokeHelper
    {
        
        #region Private Variables
        private Action action;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'MessageUpdateInvokeHelper' object.
        /// </summary>
        public MessageUpdateInvokeHelper(Action action)
        {
            this.action = action;
        }
        #endregion
        
        #region Methods
            
            #region UpdateMessageCaller()
            /// <summary>
            /// method Update Message Caller
            /// </summary>
            [JSInvokable("{DataJuggler.BlazorVideo}")]
            public void UpdateMessageCaller()
            {
                action.Invoke();
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
