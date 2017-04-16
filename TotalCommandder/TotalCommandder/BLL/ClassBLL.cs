using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;

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

        #region file processing

        public void showDrive(DriveInfo drive, ListView lvMain)
        {
            string name = ((string.IsNullOrEmpty(drive.VolumeLabel)) ? "Local Drive" : drive.VolumeLabel) + " (" + drive.Name + ")";

            lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeIcon(drive.Name).ToBitmap());

            ListViewItem item = new ListViewItem(name, lvMain.LargeImageList.Images.Count - 1);

            item.Tag = drive.Name;

            item.Name = drive.Name;

            lvMain.Items.Add(item);
        }

        public void showDirectory(DirectoryInfo directoryInfo, ListView lvMain)
        {
            lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeFolderIcon().ToBitmap());

            ListViewItem item = new ListViewItem(directoryInfo.Name, lvMain.LargeImageList.Images.Count - 1);

            item.Tag = directoryInfo.FullName;

            item.Name = directoryInfo.Name;

            lvMain.Items.Add(item);
        }

        public void showFile(FileInfo fileInfo, ListView lvMain)
        {
            lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeIconFromExtension(fileInfo.FullName).ToBitmap());

            ListViewItem item = new ListViewItem(fileInfo.Name, lvMain.LargeImageList.Images.Count - 1);

            item.Tag = fileInfo.FullName;

            item.Name = fileInfo.Name;

            lvMain.Items.Add(item);
        }

        public bool copyDirectory(string sourcePath, string destinationPath)
        {
            if (File.Exists(sourcePath))
            {
                FileInfo f = new FileInfo(sourcePath);
                f.CopyTo(destinationPath + @"\" + f.Name);
                return true;
            }

            if (!Directory.Exists(destinationPath))//Nếu destinationPath không có folder đó thì tạo folder mới
            {
                return false;
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
                return copyDirectory(System.IO.Path.Combine(sourcePath, d.Name), System.IO.Path.Combine(destinationPath, d.Name));
            }
            return true;
        }

        public bool deleteSendToRecycleBin(List<string> listPath)
        {
            try
            {
                if (listPath.Count > 0)
                {
                    foreach (string path in listPath)
                    {
                        if (Directory.Exists(path))
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(path, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
                        else
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(path, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
                    }

                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public bool deletePermanently(List<string> listPath)
        {
            try
            {
                if (listPath.Count > 0)
                {
                    foreach (string path in listPath)
                    {
                        if (Directory.Exists(path))
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(path, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
                        else
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(path);
                    }
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        #endregion

        #region packing

        /*
        *listPath: Danh sách các folder|File cần đóng gói(The Folder path or File path need pack)
        *zipFilePath: Đường dẫn đích của file sau khi đóng gói(The destination Path of the pack file)
        */
        public bool packFile(List<string> listPath, string zipFilePath)
        {
            //Tạo một thư mục tạm
            DirectoryInfo temp = Directory.CreateDirectory("temp");

            //Copy tất cả các file trong listPath qua cho thư mục tạm
            foreach (string path in listPath)
                BLL.ClassBLL.Instances.copyDirectory(path, temp.FullName);

            List<string> listPathTemp = new List<string>();//Danh sách tạm để làm đối số cho hàm xóa thư mục tạm

            using (ZipFile zip = new ZipFile())
            {
                try
                {
                    //add thư mục tạm đó vào để đóng gói
                    zip.AddDirectory(temp.FullName);
                    zip.Save(zipFilePath);
                    zip.Dispose();

                    //Xóa thư mục tạm đi
                    listPathTemp.Add(temp.FullName);
                    BLL.ClassBLL.Instances.deletePermanently(listPathTemp);

                    return true;
                }
                catch (Exception ex)
                { }
            }

            //Xóa thư mục tạm
            listPathTemp.Add(temp.FullName);
            BLL.ClassBLL.Instances.deletePermanently(listPathTemp);
            return false;
        }

        /*
         * path: đường dẫn của file cần giải nén(The path of the file need unpack)
         * newPath: đường dẫn lưu folder sau khi giải nén(The path of folder after finish unpack)
         */
        public bool unpackFile(string path, string newPath)
        {
            try
            {
                ZipFile zip = ZipFile.Read(path);

                Directory.CreateDirectory(newPath);

                zip.ExtractAll(newPath, ExtractExistingFileAction.OverwriteSilently);

                zip.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                if (Directory.Exists(newPath))
                    deletePermanently(new List<string>() { newPath });
            }

            return false;
        }
        #endregion


    }
}
