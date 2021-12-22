using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        #region Public Fields

        public bool IsItemExcluded { get; set; }
        public bool IsSearchStopped { get; set; }

        #endregion

        #region Private Fields

        private readonly string _startPath;
        private readonly Func<FileSystemInfo, bool> _filter;

        #endregion

        #region EventHandlers

        public event EventHandler<EventArgs> Start;
        public event EventHandler<EventArgs> Finish;
        public event EventHandler<ItemFindedEventArgs> FileFinded;
        public event EventHandler<ItemFindedEventArgs> DirectoryFinded;
        public event EventHandler<ItemFindedEventArgs> FilteredFileFinded;
        public event EventHandler<ItemFindedEventArgs> FilteredDirectoryFinded;

        #endregion

        #region Constructors

        public FileSystemVisitor(string path)
        {
            _startPath = path;
        }

        public FileSystemVisitor(string path, Func<FileSystemInfo, bool> filter)
        {
            _startPath = path;
            _filter = filter;
        }

        #endregion

        #region Public Methods

        public IEnumerator GetEnumerator()
        {
            OnStart();
            foreach (var file in GetFileSystemInfo(_startPath))
            {
                if (IsSearchStopped)
                {
                    yield break;
                }

                CallFileFindedEventHandler(file);

                if (FindedFileVerified(file))
                {
                    CallFilteredFileFindedEventHandler(file);
                    yield return file;
                }
            }
            OnFinish();
        }

        #endregion

        #region Protected Methods

        protected virtual void OnStart()
        {
            Start?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnFinish()
        {
            Finish?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnFileFinded(ItemFindedEventArgs args)
        {
            FileFinded?.Invoke(this, args);
        }

        protected virtual void OnDirectoryFinded(ItemFindedEventArgs args)
        {
            DirectoryFinded?.Invoke(this, args);
        }

        protected virtual void OnFilteredFileFinded(ItemFindedEventArgs args)
        {
            FilteredFileFinded?.Invoke(this, args);
        }

        protected virtual void OnFilteredDirectoryFinded(ItemFindedEventArgs args)
        {
            FilteredDirectoryFinded?.Invoke(this, args);
        }

        #endregion

        #region Private Methods

        private IEnumerable<FileSystemInfo> GetFileSystemInfo(string path)
        {
            var directory = new DirectoryInfo(path);

            foreach (var file in directory.GetFiles())
            {
                yield return file;
            }

            foreach (var dir in directory.GetDirectories())
            {
                yield return dir;

                foreach (var file in GetFileSystemInfo(dir.FullName))
                {
                    yield return file;
                }
            }
        }

        private bool FindedFileVerified(FileSystemInfo file)
        {
            if (!IsItemExcluded && (_filter is null || _filter(file)))
            {
                return true;
            }
            IsItemExcluded = false;
            return false;
        }

        private void CallFileFindedEventHandler(FileSystemInfo file)
        {
            var type = GetFileType(file);
            var args = GetItemFindedEventArgs(file, type);
            if (type == FileType.File)
                OnFileFinded(args);
            else
                OnDirectoryFinded(args);
        }

        private void CallFilteredFileFindedEventHandler(FileSystemInfo file)
        {
            var type = GetFileType(file);
            var args = GetItemFindedEventArgs(file, type);
            if (type == FileType.File)
                OnFilteredFileFinded(args);
            else
                OnFilteredDirectoryFinded(args);
        }

        private FileType GetFileType(FileSystemInfo file)
        {
            return file.GetType() == typeof(FileInfo) ? FileType.File : FileType.Directory;
        }

        private ItemFindedEventArgs GetItemFindedEventArgs(FileSystemInfo file, FileType type)
        {
            return new ItemFindedEventArgs(file.FullName, type);
        }

        #endregion
    }
}
