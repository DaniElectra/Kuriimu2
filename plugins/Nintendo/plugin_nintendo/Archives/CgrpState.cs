﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontract.Interfaces.FileSystem;
using Kontract.Interfaces.Plugins.State;
using Kontract.Interfaces.Plugins.State.Archive;
using Kontract.Interfaces.Progress;
using Kontract.Interfaces.Providers;
using Kontract.Models.Archive;
using Kontract.Models.Context;
using Kontract.Models.IO;

namespace plugin_nintendo.Archives
{
    class CgrpState : IArchiveState, ILoadFiles, ISaveFiles, IReplaceFiles
    {
        private readonly Cgrp _cgrp;

        public IList<ArchiveFileInfo> Files { get; private set; }

        public bool ContentChanged => IsChanged();

        public CgrpState()
        {
            _cgrp = new Cgrp();
        }

        public async void Load(IFileSystem fileSystem, UPath filePath, LoadContext loadContext)
        {
            var fileStream = await fileSystem.OpenFileAsync(filePath);
            Files = _cgrp.Load(fileStream);
        }

        public void Save(IFileSystem fileSystem, UPath savePath, SaveContext saveContext)
        {
            var output = fileSystem.OpenFile(savePath, FileMode.Create);
            _cgrp.Save(output, Files);
        }

        public void ReplaceFile(ArchiveFileInfo afi, Stream fileData)
        {
            afi.SetFileData(fileData);
        }

        private bool IsChanged()
        {
            return Files.Any(x => x.ContentChanged);
        }
    }
}
