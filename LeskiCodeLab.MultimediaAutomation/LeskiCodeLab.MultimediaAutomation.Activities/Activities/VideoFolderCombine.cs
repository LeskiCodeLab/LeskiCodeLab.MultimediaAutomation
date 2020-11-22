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
    [LocalizedDisplayName(nameof(Resources.VideoFolderCombine_DisplayName))]
    [LocalizedDescription(nameof(Resources.VideoFolderCombine_Description))]
    public class VideoFolderCombine : ContinuableAsyncCodeActivity
    {
        #region Properties

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("FFMPEG Path")]
        public InArgument<string> ffmpegPath { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Input Text File")]
        public InArgument<String> InputTextFile { get; set; }

        [Category("Input")]
        [DisplayName("Output Folder")]
        public InArgument<String> OutputFolder { get; set; }

        [DefaultValue("mov")]
        [Category("Input")]
        [DisplayName("Output Container")]
        public InArgument<String> OutputContainer { get; set; } = "mov";

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

        public VideoFolderCombine()
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

            var inputTextFile = InputTextFile.Get(context);
            var outputFolder = OutputFolder.Get(context);
            var outputContainer = OutputContainer.Get(context);
            var command = Command.Get(context);
            var debuggingMode = DebuggingMode.Get(context);


            var startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = FFMPEGDirectory;

            string inputContainer = inputTextFile.Substring(inputTextFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            string fileNameWithoutExtensions = inputTextFile.Replace(inputContainer, "");
            var fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");


            var uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-f concat -safe 0 -i " + '"' + inputTextFile + '"' + " " + "-c copy " + outputFolder + @"\" + "combined_" + uniqueId + "." + outputContainer + '"';//DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

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
            var FFMPEGPath = '"' + @"C:\\Program Files (x86)\\ffmpeg-4.1-win-32\\ffmpeg.exe" + '"';
            var FFMPEGDirectory = FFMPEGPath.Substring(0, FFMPEGPath.LastIndexOf('\\'));
            var outputFolder = @"D:\Scratchdisk";
            var outputContainer = "mov";
            var inputTextFile = @"D:\Scratchdisk\VideoList.txt";



            var startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = FFMPEGDirectory;

            string inputContainer = inputTextFile.Substring(inputTextFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            string fileNameWithoutExtensions = inputTextFile.Replace(inputContainer, "");
            var fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");


            var uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-f concat -safe 0 -i " + '"' + inputTextFile + '"' + " " + "-c copy " + outputFolder + @"\" + "combined_" + uniqueId + "." + outputContainer + '"';//DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

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

