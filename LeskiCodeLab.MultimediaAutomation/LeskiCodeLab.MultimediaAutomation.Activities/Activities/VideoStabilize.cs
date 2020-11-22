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
    [LocalizedDisplayName(nameof(Resources.VideoStabilize_DisplayName))]
    [LocalizedDescription(nameof(Resources.VideoStabilize_Description))]
    public class VideoStabilize : ContinuableAsyncCodeActivity
    {
        #region Properties

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("FFMPEG Path")]
        public InArgument<string> ffmpegPath { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Input File")]
        public InArgument<String> InputFile { get; set; }

        [Category("Input")]
        [DisplayName("Output Folder")]
        public InArgument<String> OutputFolder { get; set; }

        [DefaultValue("mov")]
        [Category("Input")]
        [DisplayName("Output Container")]
        public InArgument<String> OutputContainer { get; set; } = "mov";

        [DefaultValue("-vf vidstabdetect=shakiness=10:accuracy=15 -f null -")]
        [Category("Input")]
        [DisplayName("Pass 1 Command")]
        public InArgument<String> Pass1Command { get; set; } = "-vf vidstabdetect=shakiness=10:accuracy=15 -f null -";

        [DefaultValue("-vf vidstabtransform=zoom=5:smoothing=30 -vcodec prores_ks -profile:v 3")]
        [Category("Input")]
        [DisplayName("Pass 2 Command")]
        public InArgument<String> Pass2Command { get; set; } = "-vf vidstabtransform=zoom=5:smoothing=30 -vcodec prores_ks -profile:v 3";

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

        public VideoStabilize()
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
            var inputFile = InputFile.Get(context);
            var outputFolder = OutputFolder.Get(context);
            var pass1Command = Pass1Command.Get(context);
            var pass2Command = Pass2Command.Get(context);
            var outputContainer = OutputContainer.Get(context);
            var debuggingMode = DebuggingMode.Get(context);


            var startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = FFMPEGDirectory;

            string inputContainer = inputFile.Substring(inputFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            string fileNameWithoutExtensions = inputFile.Replace(inputContainer, "");
            var fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");


            var uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-i " + '"' + inputFile + '"' + " " + pass1Command;

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

            startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = FFMPEGDirectory;

            inputContainer = inputFile.Substring(inputFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            fileNameWithoutExtensions = inputFile.Replace(inputContainer, "");
            fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");


            uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-i " + '"' + inputFile + '"' + " " + pass2Command + " " + '"' + outputFolder + @"\" + uniqueId + "." + outputContainer + '"';

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
            var FFMPEGPath = '"'+@"C:\\Program Files (x86)\\ffmpeg-4.1-win-32\\ffmpeg.exe"+'"';
            var outputFolder = @"D:\Scratchdisk";
            var outputContainer = "mov";
            var command = "-vcodec prores_ks -profile:v 3";
            var FFMPEGDirectory = FFMPEGPath.Substring(0, FFMPEGPath.LastIndexOf('\\'));
           // FFMPEGPath = '"' + FFMPEGPath + '"';

            var inputFile = @"D:\Scratchdisk\Chicken.mp4";
            var pass1Command = "-vf vidstabdetect=shakiness=10:accuracy=15 -f null -";
            var pass2Command = "-vf vidstabtransform=zoom=5:smoothing=30 -vcodec prores_ks -profile:v 3";



            var startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = FFMPEGDirectory;

            string inputContainer = inputFile.Substring(inputFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            string fileNameWithoutExtensions = inputFile.Replace(inputContainer, "");
            var fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");


            var uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-i " + '"' + inputFile + '"' + " " + pass1Command;

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

            startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = FFMPEGDirectory;

            inputContainer = inputFile.Substring(inputFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            fileNameWithoutExtensions = inputFile.Replace(inputContainer, "");
            fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");


            uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-i " + '"' + inputFile + '"' + " " + pass2Command + " " + '"' + outputFolder + @"\" + uniqueId + "." + outputContainer + '"';

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

