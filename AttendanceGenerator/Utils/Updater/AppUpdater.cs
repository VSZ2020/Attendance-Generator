﻿using System;

namespace AttendanceGenerator.Utils.Updater
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
            this.CurrentVersion = curVersion;
        }
    }
}
