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

                CallEventHandler(file);

                if (FindedFileVerified(file))
                {
                    yield return file;
                }
                else
                {
                    yield break;
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
            return !IsItemExcluded && (_filter is null || _filter(file));
        }

        private void CallEventHandler(FileSystemInfo file)
        {
            var type = file.GetType() == typeof(FileInfo) ? FileType.File : FileType.Directory;
            var args = new ItemFindedEventArgs(file.FullName, type);
            if (type == FileType.File)
                CallFileFindedEventHandler(args);
            else
                CallDirectoryFindedEventHandler(args);
        }

        private void CallFileFindedEventHandler(ItemFindedEventArgs args)
        {
            if (_filter == null)
                OnFileFinded(args);
            else
                OnFilteredFileFinded(args);
        }

        private void CallDirectoryFindedEventHandler(ItemFindedEventArgs args)
        {
            if (_filter == null)
                OnDirectoryFinded(args);
            else
                OnFilteredDirectoryFinded(args);
        }

        #endregion
    }
}
