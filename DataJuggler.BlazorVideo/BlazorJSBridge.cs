

#region using statements

using DataJuggler.UltimateHelper;
using Microsoft.JSInterop;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.BlazorVideo
{

    #region class BlazorJSBridge
    /// <summary>
    /// This method is used to communicate with the BlazorJSInterop file
    /// </summary>
    public class BlazorJSBridge
    {
        
        #region Private Variables
        #endregion
        
        #region Methods
            
            #region GetCurrentTime(IJSRuntime jsRuntime)
            /// <summary>
            /// method Get Current Time
            /// </summary>
            public async static Task<int> GetCurrentTime(IJSRuntime jsRuntime)
            {
                // set the value
                int currentTime = 0;
                
                try
                {
                    // set the value
                    var videoTime = await jsRuntime.InvokeAsync<double>("BlazorJSFunctions.GetCurrentTime");
                    
                    // return the value cast as an int
                    currentTime = (int) videoTime;
                }
                catch (System.Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("GetCurrentTime", "BlazorJSBridge", error);
                }
                
                // return value
                return currentTime;
            }
            #endregion

            #region GetVideoDuration(IJSRuntime jsRuntime)
            /// <summary>
            /// method Gets Video Length
            /// </summary>
            public async static Task<double> GetVideoDuration(IJSRuntime jsRuntime)
            {
                // set the value
                double videoDuration = 0;
                
                try
                {
                    // set the value
                    videoDuration = await jsRuntime.InvokeAsync<double>("BlazorJSFunctions.GetVideoDuration");
                }
                catch (System.Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("GetVideoDuration", "BlazorJSBridge", error);
                }
                
                // return value
                return videoDuration;
            }
            #endregion

            #region IsVideoPlaying(IJSRuntime jsRuntime)
            /// <summary>
            /// method Gets Video Length
            /// </summary>
            public async static Task<int> IsVideoPlaying(IJSRuntime jsRuntime)
            {
                // set the value
                int isVideoPlaying = 0;
                
                try
                {
                    // set the value
                    isVideoPlaying = await jsRuntime.InvokeAsync<int>("BlazorJSFunctions.IsVideoPlaying");
                }
                catch (System.Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("IsVideoPlaying", "BlazorJSBridge", error);
                }
                
                // return value
                return isVideoPlaying;
            }
            #endregion
            
            #region OpenFullScreen(IJSRuntime jsRuntime)
            /// <summary>
            /// method Open Full Screen
            /// </summary>
            public async static Task<int> OpenFullScreen(IJSRuntime jsRuntime)
            {
                // set the value
                int action = 0;
                
                try
                {
                    await jsRuntime.InvokeAsync<int>("BlazorJSFunctions.OpenFullScreen");
                    
                    // return true
                    action = 1;
                }
                catch
                {
                    
                }
                
                // return value
                return action;
            }
            #endregion
            
            #region PlayOrPause(IJSRuntime jsRuntime)
            /// <summary>
            /// method Play Or Pause
            /// </summary>
            public async static Task<int> PlayOrPause(IJSRuntime jsRuntime)
            {
                // set the value
                int action = 0;
                
                try
                {
                    // set the value
                    action = await jsRuntime.InvokeAsync<int>("BlazorJSFunctions.PlayOrPause");
                    
                    // return true
                    action = 1;
                }
                catch
                {
                    
                }
                
                // return value
                return action;
            }
            #endregion
            
            #region Prompt(IJSRuntime jsRuntime, string message)
            /// <summary>
            /// method Prompt
            /// </summary>
            public static ValueTask<string> Prompt(IJSRuntime jsRuntime, string message)
            {
                // Implemented in exampleJsInterop.js
                return jsRuntime.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
            }
            #endregion
            
            #region TimeMonitor(IJSRuntime jsRuntime)
            /// <summary>
            /// This method Time Monitor
            /// </summary>
            public static async Task<int> TimeMonitor(IJSRuntime jsRuntime)
            {
                 // set the value
                int action = 0;
                
                try
                {
                    await jsRuntime.InvokeAsync<int>("BlazorJSFunctions.OnTimedUpdate");
                    
                    // return true
                    action = 1;
                }
                catch
                {
                    
                }
                
                // return value
                return action;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
