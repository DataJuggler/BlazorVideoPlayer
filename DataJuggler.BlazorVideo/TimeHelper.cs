using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DataJuggler.UltimateHelper;

namespace DataJuggler.BlazorVideo
{
    public class TimeHelper
    {

        #region Methods

            #region FormatDisplayTime(double currentTime)
            /// <summary>
            /// This method formats the seconds into Display Time
            /// </summary>
            public static string FormatDisplayTime(double currentTime)
            {
                // initial value
                string displayTime = "";
                
                try
                {
                    // Create a new instance of a 'StringBuilder' object.
                    StringBuilder sb = new StringBuilder();

                    // locals
                    int hours = (int) Math.Floor(currentTime / 3600);
                    int minutes = (int) Math.Floor(currentTime / 60);
                    int seconds = (int) Math.Floor(currentTime % 60);

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
                    displayTime = sb.ToString();
                }
                catch (Exception error)
                {
                    // for debugging only for now
                    DebugHelper.WriteDebugError("FormatDisplayTime", "TimeHelper.cs", error);
                }

                // return value
                return displayTime;
            }
            #endregion
    }
    #endregion

}
