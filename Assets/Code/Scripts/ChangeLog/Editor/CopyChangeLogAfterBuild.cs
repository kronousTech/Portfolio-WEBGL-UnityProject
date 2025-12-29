using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace KronosTech.ChangeLog
{
    public class CopyChangeLogAfterBuild : IPostprocessBuildWithReport
    {
        private readonly string m_fileName = "CHANGELOG.md";

        public int callbackOrder => 1;

        #region IPostprocessBuildWithReport
        public void OnPostprocessBuild(BuildReport report)
        {
            var sourceFilePath = Path.Combine(
                Directory.GetParent(Application.dataPath).FullName,
                m_fileName
            );
            var destinationFilePath = Path.Combine(report.summary.outputPath, m_fileName);

            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destinationFilePath, true);

                Debug.Log($"{nameof(CopyChangeLogAfterBuild)}.cs: " +
                    $"Copied file to build folder: {destinationFilePath}");
            }
            else
            {
                Debug.LogError($"{nameof(CopyChangeLogAfterBuild)}.cs: " +
                    $"Source file not found: {sourceFilePath}");
            }
        }
        #endregion
    }
}