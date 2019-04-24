using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7Dll
{
    [Serializable]
    public enum FileStatus
    {
        Changed,
        Create,
        Delete,
        Rename,
        None
    }
    [Serializable]
    public class File
    {
        private string path = "";
        private FileStatus[] statuses;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public FileStatus Statuses
        {
            get { return statuses[statuses.Length - 1]; }
            set
            {
                Array.Resize(ref statuses, statuses.Length + 1);
                statuses[statuses.Length - 1] = value;
            }
        }

        public File(string path_)
        {
            path = path_;
            statuses = new FileStatus[] { FileStatus.None };
        }

        public File(string path_, bool createOn)
        {
            path = path_;
            //if (createOn)
            //    statuses = new FileStatus[] { FileStatus.Create };
            //else
            statuses = new FileStatus[] { FileStatus.None };
            Statuses = FileStatus.Create;
        }
    }

    [Serializable]
    public class Files
    {
        File[] files;

        public Files(string path)
        {
            var filesPath = Directory.GetFiles(path);

            files = new File[filesPath.Length];
            for (int i = 0; i < filesPath.Length; i++)
            {
                files[i] = new File(filesPath[i]);
            }
        }

        public void NewStatus(string path, FileStatus fileStatus, string newPath = "")
        {
            File file = null;
            if (!FindFile(path, out file)) return;

            file.Statuses = fileStatus;

            if (fileStatus == FileStatus.Rename)
                file.Path = newPath;
        }

        public void CreateFile(string path)
        {
            Array.Resize(ref files, files.Length + 1);
            files[files.Length - 1] = new File(path, true);
        }

        bool FindFile(string path, out File file)
        {
            file = null;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Path == path && files[i].Statuses != FileStatus.Delete)
                {
                    file = files[i];
                    return true;
                }
            }
            return false;
        }

        public File[] GetFiles
        {
            get { return files; }
        }
    }
}
