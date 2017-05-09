using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCommandder.GUI
{
    class MyView:System.Windows.Forms.ListView
    {
        public void ShowFromPath(string path)//Hiển thị các thư mục và các file hiện tại, hàm trả về đường dẫn mới
        {
            try
            {
                this.LargeImageList.Images.Clear();
                this.SmallImageList.Images.Clear();
                this.Clear();

                if (path.Equals("This PC"))
                {
                    if (this.View == System.Windows.Forms.View.Details)
                    {
                        this.Columns.Add("Name", 200);
                        this.Columns.Add("Type", 100);
                        this.Columns.Add("Total Size", 100);
                        this.Columns.Add("Free Space", 100);
                    }
                    ShowDrives();
                    return;
                }

                if (this.View == System.Windows.Forms.View.Details)
                {
                    this.Columns.Add("Name", 200);
                    this.Columns.Add("Date modified", 150);
                    this.Columns.Add("Type", 150);
                    this.Columns.Add("Size", 150);
                }

                ShowDirectories(path);

                ShowFiles(path);
            }
            catch (Exception ex) { }
        }

        private void ShowDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
                BLL.ClassBLL.Instances.showDrive(drive, this);
        }

        private void ShowDirectories(string path)
        {
            DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();
            foreach (DirectoryInfo direc in directories)
            {
                if ((direc.Attributes | FileAttributes.Hidden) != direc.Attributes)//Chỉ hiển thị những file không ẩn
                    BLL.ClassBLL.Instances.showDirectory(direc, this);
            }
        }

        private void ShowFiles(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();

            foreach (FileInfo file in files)
            {
                if ((file.Attributes | FileAttributes.Hidden) != file.Attributes)//Chỉ hiển thị những file không ẩn
                    BLL.ClassBLL.Instances.showFile(file, this);
            }
        }
    }
}
