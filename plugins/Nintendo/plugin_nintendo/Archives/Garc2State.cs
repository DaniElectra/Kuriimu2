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
    class Garc2State : IArchiveState, ILoadFiles, ISaveFiles, IReplaceFiles
    {
        private readonly GARC2 _garc2;

        public IList<IArchiveFileInfo> Files { get; private set; }

        public bool ContentChanged => IsChanged();

        public Garc2State()
        {
            _garc2 = new GARC2();
        }

        public async Task Load(IFileSystem fileSystem, UPath filePath, LoadContext loadContext)
        {
            var fileStream = await fileSystem.OpenFileAsync(filePath);
            Files = _garc2.Load(fileStream);
        }

        public Task Save(IFileSystem fileSystem, UPath savePath, SaveContext saveContext)
        {
            var output = fileSystem.OpenFile(savePath, FileMode.Create);
            _garc2.Save(output, Files);

            return Task.CompletedTask;
        }

        public void ReplaceFile(IArchiveFileInfo afi, Stream fileData)
        {
            afi.SetFileData(fileData);
        }

        private bool IsChanged()
        {
            return Files.Any(x => x.ContentChanged);
        }
    }
}
