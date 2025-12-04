using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace KronosTech.BuildPostProcessing
{
    public class FixWebGLMouseBug : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform == BuildTarget.WebGL)
            {
                string outputPath = report.summary.outputPath;

                ModifyFilesInDirectory(outputPath);
            }
        }

        private void ModifyFilesInDirectory(string directoryPath)
        {
            var gzippedFiles = Directory.GetFiles(directoryPath, "*.js.gz", SearchOption.AllDirectories);

            if (gzippedFiles.Length > 0)
            {
                foreach (var filePath in gzippedFiles)
                {
                    ModifyAndCompressFile(filePath, true);
                }
            }
            else
            {
                var jsFiles = Directory.GetFiles(directoryPath, "*.js", SearchOption.AllDirectories);
                foreach (var filePath in jsFiles)
                {
                    if (!filePath.EndsWith(".js.gz"))
                    {
                        ModifyAndCompressFile(filePath, false);
                    }
                }
            }
        }

        private void ModifyAndCompressFile(string filePath, bool isGzipped)
        {
            string tempFilePath = filePath;

            if (isGzipped)
            {
                tempFilePath = Path.ChangeExtension(filePath, ".tmp.js");

                var decompress = new ProcessStartInfo("gzip", $"-d -k -c \"{filePath}\" > \"{tempFilePath}\"")
                {
                    UseShellExecute = false
                };

                Process.Start(decompress).WaitForExit();
            }

            string content = File.ReadAllText(tempFilePath);
            string wrapper = "(function(){var orig=Element.prototype.requestPointerLock;var pending=false;Element.prototype.requestPointerLock=function(opts){if(pending){return;}pending=true;var p=orig.call(this,opts);try{if(p&&typeof p.finally==='function'){p.finally(function(){pending=false;});}else{pending=false;}}catch(e){pending=false;}if(p&&typeof p.catch==='function'){p.catch(function(err){console.log(err);});}return p;};})();\n";

            if (content.Contains(".loader.js"))
            {
                int idx = content.IndexOf(".loader.js", StringComparison.InvariantCulture);
                if (idx >= 0)
                {
                    int scriptOpen = content.LastIndexOf("<script", idx, StringComparison.InvariantCulture);
                    if (scriptOpen >= 0)
                    {
                        content = content.Insert(scriptOpen, "<script>" + wrapper + "</script>\n");
                    }
                }
            }

            if (filePath != null && filePath.EndsWith(".loader.js", StringComparison.InvariantCultureIgnoreCase))
            {
                content = wrapper + content;
            }

            content = content.Replace(
                "requestPointerLock()",
                "requestPointerLock({unadjustedMovement:true})"
            );

            File.WriteAllText(tempFilePath, content);

            if (isGzipped)
            {
                File.Delete(filePath);

                var startInfo = new ProcessStartInfo("gzip", $"-c \"{tempFilePath}\" > \"{filePath}\"")
                {
                    UseShellExecute = false
                };

                Process.Start(startInfo).WaitForExit();

                File.Delete(tempFilePath);
            }
        }
    }
}