

#region using statements

using System;
using System.Collections.Generic;
using System.Text;
using DataJuggler.Blazor.Components;
using DataJuggler.Blazor.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DataJuggler.UltimateHelper;
using Microsoft.JSInterop;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.BlazorVideo
{

    #region class BlazorVideoPlayer : IBlazorComponent
    /// <summary>
    /// This class is used to play videos and provide information on what button was clicked, 
    /// so video play time can be determined.
    /// </summary>
    public partial class BlazorVideoPlayer : IBlazorComponent
    {

        #region Private Variables
        private string name;
        private IBlazorComponentParent parent;
        private IJSRuntime jSRunTime;
        private bool playing;
        private string graphStyle;
        private string controlsList;
        private string contextMenuInfo;
        private bool allowDownloads;
        private bool allowContextMenu;
        private string displayTime;
        private string videoDuration;
        private bool labelHooked;
        private bool showControlsContainer;
        private double graphHeight;
        private string graphHeightStyle;
        private string graphBackgroundStyle;
        private bool isFullScreen;
        private string videlUrl;
        private MessageUpdateInvokeHelper messageUpdateInvokeHelper;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a BlazorVideoPlayer object.
        /// </summary>
        public BlazorVideoPlayer()
        {
            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

            #region FullScreenChanged()
            /// <summary>
            /// This method is called From JavaScript when the full screen event changes
            /// </summary>
            [JSInvokable]
            public void FullScreenChanged()
            {
                        
            }
            #endregion
            
            #region GetCurrentTime()
            /// <summary>
            /// This method returns the Current Time
            /// </summary>
            public async Task<int> GetCurrentTime()
            {
                // initial value
                int currentTime = 0;

                // if the value for HasJSRunTime is true
                if (HasJSRunTime) 
                {
                    // get the currentTime
                    currentTime = await BlazorJSBridge.GetCurrentTime(this.JSRunTime);
                }
                
                // return value
                return currentTime;
            }
            #endregion
            
            #region GraphClicked()
            /// <summary>
            /// This method is called when the Graph is Clicked
            /// </summary>
            public void GraphClicked()
            {
                // to do: Figure out the current time clicked
                // to do: Set current time in video
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Defaults
                AllowDownloads = false;
                AllowContextMenu = false;
                GraphHeight = .8;

                // Default Display Time until it is set by the video since the Duration is displayed first
                displayTime = "0:00";

                // Default to true
                ShowControlsContainer = true;
            }
            #endregion
            
            #region OpenFullScreen()
            /// <summary>
            /// method Open Full Screen
            /// </summary>
            public async void OpenFullScreen()
            {
                // Verify the JSRunTime is available else this won't work.
                if (HasJSRunTime)
                {
                    await BlazorJSBridge.OpenFullScreen(jsRuntime: this.JSRunTime);
                }
            }
            #endregion
            
            #region PlayOrPause()
            /// <summary>
            /// method Play Or Pause
            /// </summary>
            public async void PlayOrPause()
            {
                // local
                double duration = 0;
                
                // if the value for HasJSRunTime is true
                if (HasJSRunTime)
                {
                    await BlazorJSBridge.PlayOrPause(jsRuntime: this.JSRunTime);

                    // Toggle
                    Playing = !Playing;

                    // if we are playing but the Label is not hooked up
                    if (Playing) 
                    {
                        // if the Label has not been hooked yet
                        if (!LabelHooked)
                        {
                            await BlazorJSBridge.TimeMonitor(this.JSRunTime);

                            // get the duration of the video
                            duration = await BlazorJSBridge.GetVideoDuration(this.JSRunTime);

                            // Format the display time
                            VideoDuration = "/" + TimeHelper.FormatDisplayTime(duration);

                            // The Label has been hooked up
                            LabelHooked = true;
                        }
                    }

                    // Ensure UI Updates
                    StateHasChanged();
                }
            }
            #endregion
        
            #region ReceiveData(Message message)
            /// <summary>
            /// This method is used to receive messages from the parent page
            /// </summary>
            /// <param name="message"></param>
            public void ReceiveData(Message message)
            {
                // If the message object exists
                if (NullHelper.Exists(message))
                {
                    // if there are one or more parameters
                    if (ListHelper.HasOneOrMoreItems(message.Parameters))
                    {
                        // if this is the JSRuntime param we need
                        if (message.Parameters[0].Name == "JSRuntime")
                        {
                            // Store the JSRuntime
                            this.JSRunTime = message.Parameters[0].Value as IJSRuntime;
                        }
                    }
                }
            }
            #endregion
        
        #endregion

        #region Properties

            #region AllowContextMenu
            /// <summary>
            /// This property gets or sets the value for 'AllowContextMenu'.
            /// </summary>
            [Parameter]
            public bool AllowContextMenu
            {
                get { return allowContextMenu; }
                set 
                {
                    // set the value
                    allowContextMenu = value;

                    // if the value for allowContextMenu is true
                    if (allowContextMenu)
                    {
                        // Erase
                        ContextMenuInfo = "return true;";
                    }
                    else
                    {
                        // This is the default value
                        ContextMenuInfo = "return false;";
                    }
                }
            }
            #endregion
            
            #region AllowDownloads
            /// <summary>
            /// This property gets or sets the value for 'AllowDownloads'.
            /// </summary>
            [Parameter]
            public bool AllowDownloads
            {
                get { return allowDownloads; }
                set
                {
                    // set the value
                    allowDownloads = value;

                    // if the value for allowDownloads is true
                    if (allowDownloads)
                    {
                        // Use blank, I think this will still allow the download
                        ControlsList = "";
                    }
                    else
                    {
                        // Use blank, I think this will still allow the download
                        ControlsList = "nodownload";
                    }
                }
            }
            #endregion
            
            #region ContextMenuInfo
            /// <summary>
            /// This property gets or sets the value for 'ContextMenuInfo'.
            /// </summary>
            public string ContextMenuInfo
            {
                get { return contextMenuInfo; }
                set { contextMenuInfo = value; }
            }
            #endregion
            
            #region ControlsList
            /// <summary>
            /// This property gets or sets the value for 'ControlsList'.
            /// </summary>
            public string ControlsList
            {
                get { return controlsList; }
                set { controlsList = value; }
            }
            #endregion
            
            #region DisplayTime
            /// <summary>
            /// This property gets or sets the value for 'DisplayTime'.
            /// </summary>
            public string DisplayTime
            {
                get { return displayTime; }
                set { displayTime = value; }
            }
            #endregion
            
            #region GraphBackgroundStyle
            /// <summary>
            /// This property gets or sets the value for 'GraphBackgroundStyle'.
            /// </summary>
            public string GraphBackgroundStyle
            {
                get { return graphBackgroundStyle; }
                set { graphBackgroundStyle = value; }
            }
            #endregion
            
            #region GraphHeight
            /// <summary>
            /// This property gets or sets the value for 'GraphHeight'.
            /// </summary>
            public double GraphHeight
            {
                get { return graphHeight; }
                set 
                {
                    // set the value
                    graphHeight = value; 

                    // set the string value
                    graphHeightStyle = graphHeight.ToString() + "vh";
                }
            }
            #endregion
            
            #region GraphHeightStyle
            /// <summary>
            /// This property gets or sets the value for 'GraphHeightStyle'.
            /// </summary>
            public string GraphHeightStyle
            {
                get { return graphHeightStyle; }
                set { graphHeightStyle = value; }
            }
            #endregion
            
            #region GraphStyle
            /// <summary>
            /// This property gets or sets the value for 'GraphStyle'.
            /// </summary>
            public string GraphStyle
            {
                get { return graphStyle; }
                set { graphStyle = value; }
            }
            #endregion
            
            #region HasJSRunTime
            /// <summary>
            /// This property returns true if this object has a 'JSRunTime'.
            /// </summary>
            public bool HasJSRunTime
            {
                get
                {
                    // initial value
                    bool hasJSRunTime = (this.JSRunTime != null);
                    
                    // return value
                    return hasJSRunTime;
                }
            }
            #endregion
            
            #region HasParent
            /// <summary>
            /// This property returns true if this object has a 'Parent'.
            /// </summary>
            public bool HasParent
            {
                get
                {
                    // initial value
                    bool hasParent = (this.Parent != null);
                    
                    // return value
                    return hasParent;
                }
            }
            #endregion
            
            #region IsFullScreen
            /// <summary>
            /// This property gets or sets the value for 'IsFullScreen'.
            /// </summary>
            public bool IsFullScreen
            {
                get { return isFullScreen; }
                set { isFullScreen = value; }
            }
            #endregion
            
            #region JSRunTime
            /// <summary>
            /// This property gets or sets the value for 'JSRunTime'.
            /// </summary>
            public IJSRuntime JSRunTime
            {
                get { return jSRunTime; }
                set { jSRunTime = value; }
            }
            #endregion
            
            #region LabelHooked
            /// <summary>
            /// This property gets or sets the value for 'LabelHooked'.
            /// </summary>
            public bool LabelHooked
            {
                get { return labelHooked; }
                set { labelHooked = value; }
            }
            #endregion
            
            #region Name
            /// <summary>
            /// This property gets or sets the value for 'Name'.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            #endregion
            
            #region Parent
            /// <summary>
            /// This property gets or sets the value for 'Parent'.
            /// </summary>
            [Parameter]
            public IBlazorComponentParent Parent
            {
                get { return parent; }
                set 
                { 
                    parent = value;

                    // if the value for HasParent is true
                    if (HasParent)
                    {
                        // Register with the parent
                        Parent.Register(this);
                    }
                }
            }
            #endregion

            #region Playing
            /// <summary>
            /// This property gets or sets the value for 'Playing'.
            /// </summary>
            public bool Playing
            {
                get { return playing; }
                set { playing = value; }
            }
            #endregion
            
            #region ShowControlsContainer
            /// <summary>
            /// This property gets or sets the value for 'ShowControlsContainer'.
            /// </summary>
            [Parameter]
            public bool ShowControlsContainer
            {
                get { return showControlsContainer; }
                set { showControlsContainer = value; }
            }
            #endregion
            
            #region VideoUrl
            /// <summary>
            /// This property gets or sets the value for 'VidelUrl'.
            /// </summary>
            [Parameter]
            public string VideoUrl
            {
                get { return videlUrl; }
                set { videlUrl = value; }
            }
            #endregion
            
            #region VideoDuration
            /// <summary>
            /// This property gets or sets the value for 'VideoDuration'.
            /// </summary>
            public string VideoDuration
            {
                get { return videoDuration; }
                set { videoDuration = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
