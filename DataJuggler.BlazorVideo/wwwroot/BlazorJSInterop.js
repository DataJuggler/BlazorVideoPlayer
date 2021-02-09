// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.BlazorJSFunctions =
{
    GetCurrentTime: function()
    {
        // inititial value
        var returnValue = 0;

        // get the videoPlayer
        var videoPlayer = document.getElementById("videoPlayer"); 

        try
        {  
            // set the returnVaalue
            returnValue = videoPlayer.currentTime;
        }
        catch (err)
        {
            // return a negative value
            returnValue = -1;
        }

        // return the returnValue
        return returnValue;
    },
    IsVideoPlaying: function()
    {
        // inititial value
        var returnValue = 0;

        try
        {
            // get the videoPlayer
            var videoPlayer = document.getElementById("videoPlayer");

            // is the video currently playing
            var isVideoPlaying = video => !!(videoPlayer.currentTime > 0 && !videoPlayer.paused && !videoPlayer.ended && videoPlayer.readyState > 2);

            if (isVideoPlaying)
            {
                // set the returnVaalue
                returnValue = 1;
            }
            else
            {
                // set the returnVaalue
                returnValue = 0;
            }
        }
        catch (err) {
            // return a negative value
            returnValue = -1;
        }

        // return the returnValue
        return returnValue;
    },
    GetVideoDuration: function ()
    {
        // inititial value
        var returnValue = 0;

        // get the videoPlayer
        var videoPlayer = document.getElementById("videoPlayer");

        try
        {
            // set the returnVaalue
            returnValue = videoPlayer.duration;
        }
        catch (err)
        {
            // return a negative value
            returnValue = -1;
        }

        // return the returnValue
        return returnValue;
    },
    OnTimedUpdate: function()
    {
        // initial value
        var returnValue = 0;

        // get the videoPlayer
        var videoPlayer = document.getElementById("videoPlayer"); 

        videoPlayer.ontimeupdate = function () { hookVideo() };

        // return value
        return returnValue;

        function hookVideo()
        {
            var timestamp = Math.round(videoPlayer.currentTime);

            // get hours minutes and seconds
            var hours = Math.floor(timestamp / 3600);            
            var minutes = Math.floor(timestamp / 60) - (hours * 60);
            var seconds = Math.floor(timestamp % 60);

            // get the formatted time
            var formatted = hours.toString().padStart(2, '0') + ':' + minutes.toString().padStart(2, '0') + ':' + seconds.toString().padStart(2, '0');

            // if only seconds
            if (formatted.startsWith("00:00"))
            {
                // start after the 3 zeros
                formatted = formatted.substring(4);
            }
            else if (formatted.startsWith("00:0"))
            {
                // start after the 3 zeros
                formatted = formatted.substring(4);
            }
            else if (formatted.startsWith("00:"))
            {
                // now replace out any unneeded trailing 0's
                formatted = formatted.substring(3);
            }
            
            // Display the current position of the video in a label element with id="videoTime"
            document.getElementById("videoTime").innerHTML = formatted;
        }
    },
    ShowPrompt: function (message)
    {
        return prompt(message, 'Message From Webpage');
    },
    PlayOrPause: function (text)
    {
        // original value
        var returnValue = 0;

        // get the videoPlayer
        var videoPlayer = document.getElementById("videoPlayer"); 

            try {
                if (videoPlayer.paused) {
                    videoPlayer.play();
                }
                else {
                    videoPlayer.pause()
                }

                // set to 1;
                returnValue = 1;
            }
            catch (err) {
                returnValue = -2;
            }

        // return value
        return returnValue;
    },
    OpenFullScreen: function ()
    {
        var videoPlayer = document.getElementById("videoPlayer"); 

        if (videoPlayer.requestFullscreen)
        {
            videoPlayer.requestFullscreen();
        } else if (videoPlayer.mozRequestFullScreen) { /* Firefox */
            videoPlayer.mozRequestFullScreen();
        } else if (videoPlayer.webkitRequestFullscreen) { /* Chrome, Safari and Opera */
            videoPlayer.webkitRequestFullscreen();
        } else if (videoPlayer.msRequestFullscreen) { /* IE/Edge */
            videoPlayer.msRequestFullscreen();
        }

        // subscribe to the full screen change event
        DetectFullScreenChange();
    },
    DetectFullScreenChange: function ()
    {
        alert('Test');

        // get the videoPlayer
        var videoPlayer = document.getElementById("videoPlayer");

        videoPlayer.bind('webkitfullscreenchange mozfullscreenchange fullscreenchange', function (e) {
            var state = document.fullScreen || document.mozFullScreen || document.webkitIsFullScreen;
            var event = state ? 'FullscreenOn' : 'FullscreenOff';

            // notify the message caller
            // updateMessageCallerJS();
            alert('Fullscreen: ' + event);
        });
    }
};



