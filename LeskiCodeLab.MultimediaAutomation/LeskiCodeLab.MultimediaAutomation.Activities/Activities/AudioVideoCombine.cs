using System;
using System.Activities;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using LeskiCodeLab.MultimediaAutomation.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace LeskiCodeLab.MultimediaAutomation.Activities
{
    [LocalizedDisplayName(nameof(Resources.AudioVideoCombine_DisplayName))]
    [LocalizedDescription(nameof(Resources.AudioVideoCombine_Description))]
    public class AudioVideoCombine : ContinuableAsyncCodeActivity
    {
        #region Properties

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("FFMPEG Path")]
        public InArgument<string> ffmpegPath { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Video File")]
        public InArgument<String> VideoFile { get; set; }

        [Category("Input")]
        [DisplayName("Output Folder")]
        public InArgument<String> OutputFolder { get; set; }

        [DefaultValue("mov")]
        [Category("Input")]
        [DisplayName("Output Container")]
        public InArgument<String> OutputContainer { get; set; } = "mov";

        [Category("Input")]
        [DisplayName("Audio File")]
        public InArgument<String> AudioFile { get; set; }

        [DefaultValue("-vcodec prores_ks -profile:v 0")]
        [Category("Input")]
        [DisplayName("Command")]
        public InArgument<String> Command { get; set; } = "-vcodec prores_ks -profile:v 3";

        [DefaultValue(false)]
        [Category("Input")]
        [DisplayName("Debugging Mode")]
        public InArgument<bool> DebuggingMode { get; set; } = false;

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        #endregion


        #region Constructors

        public AudioVideoCombine()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            var FFMPEGPath = ffmpegPath.Get(context);
            var FFMPEGDirectory = FFMPEGPath.Substring(0, FFMPEGPath.LastIndexOf('\\'));
            FFMPEGPath = '"' + FFMPEGPath + '"';
            var videoFile = VideoFile.Get(context);
            var outputFolder = OutputFolder.Get(context);
            var outputContainer = OutputContainer.Get(context);
            var command = Command.Get(context);
            var audioFile = AudioFile.Get(context);
            var debuggingMode = DebuggingMode.Get(context);

            var startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = FFMPEGDirectory;

            /*
            string inputContainer = videoFile.Substring(videoFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }*/

            /*
            string fileNameWithoutExtensions = videoFile.Replace(inputContainer, "");
            var fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");*/


            var uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-i " + '"' + videoFile + '"' + " " + "-i " + '"' + audioFile + '"' + " -c copy -map 0:v -map 1:a -shortest " + '"' + outputFolder + @"\" + uniqueId + "." + outputContainer + '"'; // DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

            if (debuggingMode)
            {

                var processn = new Process();
                processn.StartInfo = startInfo;
                processn.EnableRaisingEvents = true;
                processn.StartInfo.FileName = "CMD.EXE";
                processn.StartInfo.Arguments = "/K " + '"' + @FFMPEGPath + " " + startInfo.Arguments + '"';
                processn.Start();
                processn.WaitForExit();

            }
            else
            {
                var processn = Process.Start(startInfo);
                processn.EnableRaisingEvents = true;

                processn.WaitForExit();
            }

            // Outputs
            return (ctx) => {
            };
        }

        public void Execute()
        {
            var FFMPEGPath = '"'+"C:\\Program Files (x86)\\ffmpeg-4.1-win-32\\ffmpeg.exe"+'"';
            var videoFile = @"D:\Scratchdisk\chicken.mp4";
            var outputFolder = @"D:\Scratchdisk";
            var audioFile = @"C:\Users\Hope\Downloads\Pegboard Nerds feat. Elizaveta - Hero.mp3";
            var outputContainer = "mov";
            var command = "-vcodec prores_ks -profile:v 3";
            var FFMPEGDirectory = FFMPEGPath.Substring(0, FFMPEGPath.LastIndexOf('\\'));

            var startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = FFMPEGDirectory;

            string inputContainer = videoFile.Substring(videoFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            string fileNameWithoutExtensions = videoFile.Replace(inputContainer, "");
            var fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");


            var uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-i " + '"' + videoFile + '"' + " " + "-i " + '"' + audioFile + '"' + " -c copy -map 0:v -map 1:a -shortest " + '"' + outputFolder + @"\" + uniqueId + "." + outputContainer + '"'; // DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

            if (true)
            {

                var processn = new Process();
                processn.StartInfo = startInfo;
                processn.EnableRaisingEvents = true;
                processn.StartInfo.FileName = "CMD.EXE";
                processn.StartInfo.Arguments = "/K " + '"' + @FFMPEGPath + " " + startInfo.Arguments + '"';
                processn.Start();
                processn.WaitForExit();

            }
            else
            {
                var processn = Process.Start(startInfo);
                processn.EnableRaisingEvents = true;

                processn.WaitForExit();
            }


        }

        #endregion
    }
}

