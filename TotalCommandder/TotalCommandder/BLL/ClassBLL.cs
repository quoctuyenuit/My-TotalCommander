using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCommandder.BLL
{
    class ClassBLL
    {
        private static ClassBLL instances;

        public static ClassBLL Instances
        {
            get
            {
                if (instances == null)
                    instances = new ClassBLL();
                return ClassBLL.instances;
            }
            set { ClassBLL.instances = value; }
        }

        public void copyDirectory(string sourcePath, string destinationPath)
        {
            if (File.Exists(sourcePath))
            {
                FileInfo f = new FileInfo(sourcePath);
                f.CopyTo(destinationPath + @"\"+ f.Name);
                return;
            }

            if (!Directory.Exists(destinationPath))//Nếu destinationPath không có folder đó thì tạo folder mới
            {
                Directory.CreateDirectory(destinationPath);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);

            string[] temp = sourcePath.Split('\\');

            Directory.CreateDirectory(Path.Combine(destinationPath, temp.Last()));

            FileInfo[] files = directoryInfo.GetFiles();

            destinationPath = Path.Combine(destinationPath, temp.Last());

            //Copy files in the directory
            foreach (FileInfo f in files)
            {
                f.CopyTo(System.IO.Path.Combine(destinationPath, f.Name));
            }
            //Copy subdirectorys
            DirectoryInfo[] direc = directoryInfo.GetDirectories();
            foreach (DirectoryInfo d in direc)
            {
                copyDirectory(System.IO.Path.Combine(sourcePath, d.Name), System.IO.Path.Combine(destinationPath, d.Name));
            }

        }

    }
}
