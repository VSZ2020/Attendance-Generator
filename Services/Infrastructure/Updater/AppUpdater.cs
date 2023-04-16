using System;

namespace Services.Infrastructure.Updater
{
    public class AppUpdater
    {
        private string fileInfoUrl;
        public UpdaterFileInfo? updaterFileInfo;
        public Version? NewVersion { get; set; }
        public Version? CurrentVersion { get; set; }

        public AppUpdater(string fileInfoUrl, Version curVersion)
        {
            this.fileInfoUrl = fileInfoUrl;
            CurrentVersion = curVersion;
        }
    }
}
