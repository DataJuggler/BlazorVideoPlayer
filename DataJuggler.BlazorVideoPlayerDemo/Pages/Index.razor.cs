

#region using statements

using DataJuggler.BlazorVideo;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataJuggler.Blazor.Components;
using DataJuggler.Blazor.Components.Interfaces;
using System.Data;
using DataJuggler.UltimateHelper;
using System.Text;

#endregion

namespace DataJuggler.BlazorVideoPlayerDemo.Pages
{

    #region class Index
    /// <summary>
    /// This class is the Code Behind for the Index page.
    /// </summary>
    public partial class Index : IBlazorComponentParent
    {

        #region Private Variables
        private List<IBlazorComponent> children;
        private BlazorVideoPlayer videoPlayer;
        private string displayTime;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of an Index object
        /// </summary>
        public Index()
        {
            // Create a new collection of 'IBlazorComponent' objects.
            Children = new List<IBlazorComponent>();
        }
        #endregion

        #region Methods

            #region FindChildByName(string name)
            /// <summary>
            /// method returns the Child By Name
            /// </summary>
            public IBlazorComponent FindChildByName(string name)
            {
                // initial value
                IBlazorComponent child = null;

                

                // return value
                return child;
            }
            #endregion

            #region GetCurrentTime()
            /// <summary>
            /// This method returns the Current Time
            /// </summary>
            public async void GetCurrentTime()
            {
                // initial value
                int currentTime = 0;

                // if the value for HasVideoPlayer is true
                if (HasVideoPlayer)
                {
                    // get the current time
                    currentTime = await VideoPlayer.GetCurrentTime();

                    // now set the display time
                    SetDisplayTime(currentTime);
                }
            }
            #endregion
            
            #region ReceiveData(Message message)
            /// <summary>
            /// method returns the Data
            /// </summary>
            public void ReceiveData(Message message)
            {
                
            }
            #endregion
            
            #region Refresh()
            /// <summary>
            /// method Refreshes the UI
            /// </summary>
            public void Refresh()
            {
                // Update the UI
                InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
            #endregion
            
            #region Register(IBlazorComponent component)
            /// <summary>
            /// method returns the
            /// </summary>
            public void Register(IBlazorComponent component)
            {
                // If the component object exists
                if (NullHelper.Exists(component))
                {
                    // if this is the VideoPlayer in case other components end up on this page
                    if (component is BlazorVideoPlayer)
                    {
                        // Store the VideoPlayer
                        this.VideoPlayer = component as BlazorVideoPlayer;
                    }

                    // Add this component
                    this.Children.Add(component);

                    // Create a new instance of a 'NamedParameter' object.
                    NamedParameter namedParameter = new NamedParameter();

                    // Set the Name
                    namedParameter.Name = "JSRuntime";
                    namedParameter.Value = JSRuntime;

                    // Create a message
                    DataJuggler.Blazor.Components.Message message = new DataJuggler.Blazor.Components.Message();

                    // add the parameter
                    message.Parameters.Add(namedParameter);

                    // Send the message
                    component.ReceiveData(message);
                }
            }
            #endregion

            #region SetDisplayTime(int currentTime)
            /// <summary>
            /// This method Set Display Time
            /// </summary>
            public void SetDisplayTime(int currentTime)
            {
                // Create a new instance of a 'StringBuilder' object.
               StringBuilder sb = new StringBuilder();

                // locals
                int hours = (int) Math.Floor((double) currentTime / 3600);
                int minutes = (int) Math.Floor((double) currentTime / 60);
                int seconds = currentTime % 60;

                // only include hours if it is set
                if (hours > 0)
                {
                    // Append the hours and colon
                    sb.Append(hours);
                    sb.Append(":");

                    // reduce the minutes
                    minutes -= (hours * 60);

                    // if 1:09 minutes for example
                    if (minutes < 10)
                    {
                        // Append a preceding 0
                        sb.Append("0");
                    }
                }

                // Append the minutes and colon
                sb.Append(minutes);
                sb.Append(":");
                sb.Append(seconds);

                // if the video is 25:05 for example
                if (seconds < 10)
                {
                    // Append a preceding 0
                    sb.Append("0");
                }

                // Set the DisplayTime
                DisplayTime = sb.ToString();

                // Refresh the UI
                StateHasChanged();
            }
            #endregion
            
        #endregion

        #region Properties

            #region Children
            /// <summary>
            /// This property gets or sets the value for 'Children'.
            /// </summary>
            public List<IBlazorComponent> Children
            {
                get { return children; }
                set { children = value; }
            }
            #endregion
            
            #region DisplayTime
            /// <summary>
            /// This property gets or sets the value for 'DisplayTime'.
            /// </summary>
            public string DisplayTime
            {
                get { return displayTime; }
                set 
                {
                    // set the value
                    displayTime = value;

                    // for debugging only. Testing if JavaScript setting a value will update this.
                    // I suspect not, but all I can do is test to find out.
                    int temp = NumericHelper.ParseInteger(displayTime, 0, -1);

                    // if set
                    if (temp > 10)
                    {
                        // break point only
                        int x = 0;
                    }
                }
            }
            #endregion
            
            #region HasChildren
            /// <summary>
            /// This property returns true if this object has a 'Children'.
            /// </summary>
            public bool HasChildren
            {
                get
                {
                    // initial value
                    bool hasChildren = (this.Children != null);
                    
                    // return value
                    return hasChildren;
                }
            }
            #endregion
            
            #region HasVideoPlayer
            /// <summary>
            /// This property returns true if this object has a 'VideoPlayer'.
            /// </summary>
            public bool HasVideoPlayer
            {
                get
                {
                    // initial value
                    bool hasVideoPlayer = (this.VideoPlayer != null);
                    
                    // return value
                    return hasVideoPlayer;
                }
            }
            #endregion
            
            #region VideoPlayer
            /// <summary>
            /// This property gets or sets the value for 'VideoPlayer'.
            /// </summary>
            public BlazorVideoPlayer VideoPlayer
            {
                get { return videoPlayer; }
                set { videoPlayer = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
