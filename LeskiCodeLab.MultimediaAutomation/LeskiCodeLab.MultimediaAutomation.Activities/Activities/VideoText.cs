using System;
using System.Activities;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LeskiCodeLab.MultimediaAutomation.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace LeskiCodeLab.MultimediaAutomation.Activities
{
    [LocalizedDisplayName(nameof(Resources.VideoText_DisplayName))]
    [LocalizedDescription(nameof(Resources.VideoText_Description))]
    public class VideoText : ContinuableAsyncCodeActivity
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

        [Category("Input")]
        [DisplayName("Font File")]
        public InArgument<String> FontFile { get; set; }

        [Category("Input")]
        [DisplayName("Text")]
        public InArgument<String> Text { get; set; }

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

        public VideoText()
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
            var outputContainer = OutputContainer.Get(context);
            var command = Command.Get(context);
            var fontFile = FontFile.Get(context);
            var debuggingMode = DebuggingMode.Get(context);
            var text = Text.Get(context);


            string tempPath = Path.GetTempPath();
            var fontFileName = fontFile.Substring(fontFile.LastIndexOf(@"\"));
            fontFileName = fontFileName.Replace(@"\", "");

            String fontFilePath = Path.Combine(tempPath, fontFileName);

            if (!System.IO.File.Exists(fontFilePath))
            {
                System.IO.File.Copy(fontFile, fontFilePath, true);
            }

            var startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = tempPath;

            string inputContainer = inputFile.Substring(inputFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            string fileNameWithoutExtensions = inputFile.Replace(inputContainer, "");
            var fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");

            var uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-i " + '"' + inputFile + '"' + " " + "-vf drawtext=enable='between(t,2,8)':\"fontfile = " + fontFileName + '"' + ":text=\"" + text + "\":fontcolor=white:fontsize=124:x=(w-text_w)/2:y=(h-text_h)/2 " + command + " " + '"' + outputFolder + @"\" + uniqueId + "." + outputContainer + '"';

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

            var inputFile = @"D:\Scratchdisk\Chicken.mp4";
            var fontFile = @"D:\Scratchdisk\comicate.ttf";
            var text = "Hello World!";


            string tempPath = Path.GetTempPath();
            var fontFileName = fontFile.Substring(fontFile.LastIndexOf(@"\"));
            fontFileName = fontFileName.Replace(@"\", "");

            String fontFilePath = Path.Combine(tempPath, fontFileName);

            if (!System.IO.File.Exists(fontFilePath))
            {
                System.IO.File.Copy(fontFile, fontFilePath, true);
            }

            var startInfo = new ProcessStartInfo(FFMPEGPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.WorkingDirectory = tempPath;

            string inputContainer = inputFile.Substring(inputFile.LastIndexOf('.'));
            if (outputContainer == "")
            {
                outputContainer = inputContainer;
            }

            string fileNameWithoutExtensions = inputFile.Replace(inputContainer, "");
            var fileName = fileNameWithoutExtensions.Substring(fileNameWithoutExtensions.LastIndexOf(@"\"));
            fileName = fileName.Replace(@"\", "");

            var uniqueId = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            startInfo.Arguments = "-i " + '"' + inputFile + '"' + " " + "-vf drawtext=enable='between(t,2,8)':\"fontfile = " + fontFileName + '"' + ":text=\"" + text + "\":fontcolor=white:fontsize=124:x=(w-text_w)/2:y=(h-text_h)/2 " + command + " " + '"' + outputFolder + @"\" + uniqueId + "." + outputContainer + '"';

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

