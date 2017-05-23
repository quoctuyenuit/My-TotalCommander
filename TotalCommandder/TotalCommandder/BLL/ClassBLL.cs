using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;
using System.Drawing;
using System.Net.Mail;
using System.Net;

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
            string text;
            try
            {
                text = ((string.IsNullOrEmpty(drive.VolumeLabel)) ? "Local Drive" : drive.VolumeLabel) + " (" + drive.Name + ")";
            }
            catch (Exception ex)
            {
                text = drive.Name;
            }

            lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeIcon(drive.Name).ToBitmap());
            lvMain.SmallImageList.Images.Add(BLL.ShellIcon.GetLargeIcon(drive.Name).ToBitmap());

            ListViewItem item;

            if (lvMain.View == View.Tile)
            {
                item = new ListViewItem();
            }
            else
            {
                string[] strItem = new string[4];
                strItem[0] = drive.Name;
                strItem[1] = drive.GetType().Name;
                try
                {
                    strItem[2] = Math.Round(drive.TotalSize / (Math.Pow(2, 30))).ToString() + " GB";
                    strItem[3] = Math.Round(drive.TotalFreeSpace / (Math.Pow(2, 30))).ToString() + " GB";
                }
                catch(Exception ex)
                {
                    strItem[2] = strItem[3] = "";//Xử lý trường hợp gặp ổ đĩa ảo không có các trường dữ liệu Size
                }

                item = new ListViewItem(strItem);
            }

            item.Text = text;

            item.Name = drive.Name;

            item.ImageIndex = lvMain.LargeImageList.Images.Count - 1;

            item.Tag = drive.Name;

            lvMain.Items.Add(item);
        }

        public void showDirectory(DirectoryInfo directoryInfo, ListView lvMain)
        {
            lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeFolderIcon().ToBitmap());
            lvMain.SmallImageList.Images.Add(BLL.ShellIcon.GetLargeFolderIcon().ToBitmap());
            
            ListViewItem item= new ListViewItem(directoryInfo.Name);

            item.Name = directoryInfo.Name;
            
            item.ImageIndex = lvMain.LargeImageList.Images.Count - 1;
            
            item.Tag = directoryInfo.FullName;

            if (lvMain.View == View.Details)
            {
                string[] strItem = new string[2];
                strItem[0] = directoryInfo.CreationTime.ToShortDateString() + " " + directoryInfo.CreationTime.ToLongTimeString();
                strItem[1] = directoryInfo.GetType().Name;

                item.SubItems.AddRange(strItem);
            }
            

            lvMain.Items.Add(item);
        }

        private Bitmap readImage(string path, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);

            Image image = Image.FromFile(path);

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(image, new Rectangle(0, 0, width, height));

            image.Dispose();

            return bmp;
        }

        public void showFile(FileInfo fileInfo, ListView lvMain)
        {
            string[] ImageExtension = new string[] { ".bmp", ".ico", ".gif", ".jpeg", ".jpg", ".jfif",".png", ".tif", ".tiff", ".wmf", ".emf" };


            ListViewItem item =  new ListViewItem();

            item.Name = fileInfo.Name;

            if (fileInfo.Name.Contains('.'))
                item.Text = fileInfo.Name.Remove(fileInfo.Name.LastIndexOf('.'));
            else
                item.Text = fileInfo.Name;

            item.Tag = fileInfo.FullName;

            if (lvMain.View == View.Tile || lvMain.View == View.LargeIcon)
            {
                if (ImageExtension.Contains(fileInfo.Extension.ToLower()))
                {
                    lvMain.LargeImageList.Images.Add(readImage(fileInfo.FullName,lvMain.LargeImageList.ImageSize.Width, lvMain.LargeImageList.ImageSize.Height ));
                }
                else
                    lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeIconFromExtension(fileInfo.FullName).ToBitmap());

            }
            else
            {
                lvMain.SmallImageList.Images.Add(BLL.ShellIcon.GetLargeIconFromExtension(fileInfo.FullName).ToBitmap());

                lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeIconFromExtension(fileInfo.FullName).ToBitmap());
            }

            if (lvMain.View == View.Details)
            {
                double fileSize = fileInfo.Length;

                string[] Unit = new string[] { " KB", " MB", " GB", " TB" };
                int u = 0;
                do
                {
                    fileSize = fileSize / (Math.Pow(2, 10 * (u + 1)));
                } while (fileSize > 1000 && u < 4);

                fileSize = Math.Round(fileSize, 2);

                string[] strItem = new string[3];
                strItem[0] = fileInfo.CreationTime.ToShortDateString() + " " + fileInfo.CreationTime.ToLongTimeString();
                strItem[1] = fileInfo.GetType().Name;
                strItem[2] = fileSize.ToString() + Unit[u];

                item.SubItems.AddRange(strItem);

            }

            item.ImageIndex = lvMain.LargeImageList.Images.Count - 1;

            lvMain.Items.Add(item);
        }

        //Hiển thị danh sách các item vào listView
        public void showDirectoryAndFiles(List<string> listPath, ListView lvMain)//Hiển thị các thư mục và các file hiện tại, hàm trả về đường dẫn mới
        {
            try
            {
                lvMain.LargeImageList.Images.Clear();
                lvMain.SmallImageList.Images.Clear();
                lvMain.Clear();

                if (lvMain.View == View.Details)
                {
                    lvMain.Columns.Add("Name", 200);
                    lvMain.Columns.Add("Date modified", 150);
                    lvMain.Columns.Add("Type", 150);
                    lvMain.Columns.Add("Size", 150);
                }

                foreach (string item in listPath)
                {
                    if (Directory.Exists(item))
                        showDirectory(Microsoft.VisualBasic.FileIO.FileSystem.GetDirectoryInfo(item), lvMain);
                    else
                        showFile(Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(item), lvMain);
                }

            }
            catch (Exception ex) { }
        }

        public bool copyAction(string sourcePath, string destinationPath, Microsoft.VisualBasic.FileIO.UIOption UI = Microsoft.VisualBasic.FileIO.UIOption.AllDialogs)
        {
            try
            {
                destinationPath = Path.Combine(destinationPath, Path.Combine(destinationPath, sourcePath.Substring(sourcePath.LastIndexOf('\\') + 1)));

                if (Directory.Exists(sourcePath))
                {
                    Directory.CreateDirectory(destinationPath);

                    Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourcePath, destinationPath, UI, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                }
                else
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(sourcePath, destinationPath, UI, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                return true;
            }
            catch (Exception ex) { }

            return false;

        }

        public bool moveAction(string sourcePath, string destinationPath, Microsoft.VisualBasic.FileIO.UIOption UI = Microsoft.VisualBasic.FileIO.UIOption.AllDialogs)
        {
            try
            {
                destinationPath = Path.Combine(destinationPath, Path.Combine(destinationPath, sourcePath.Substring(sourcePath.LastIndexOf('\\') + 1)));

                if (Directory.Exists(sourcePath))
                    Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(sourcePath, destinationPath, UI, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                else
                    Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(sourcePath, destinationPath, UI, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                return true;
            }
            catch (Exception ex) { }

            return false;

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
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(path, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                        else
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(path, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                    }

                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public bool deletePermanently(List<string> listPath, Microsoft.VisualBasic.FileIO.UIOption UI)
        {
            try
            {
                if (listPath.Count > 0)
                {
                    foreach (string path in listPath)
                    {
                        if (Directory.Exists(path))
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(path, UI, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                        else
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(path, UI, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                    }
                    return true;
                }
            }
            catch (Exception ex) { }

            return false;
        }

        public List<string> findAction(string path, string name)
        {
            try
            {
                List<string> result = new List<string>();

                foreach (string folder in Microsoft.VisualBasic.FileIO.FileSystem.GetDirectories(path))
                {
                    DirectoryInfo direcryInfo = Microsoft.VisualBasic.FileIO.FileSystem.GetDirectoryInfo(folder);

                    if (((direcryInfo.Attributes | FileAttributes.Hidden) == direcryInfo.Attributes) || ((direcryInfo.Attributes | FileAttributes.Temporary) == direcryInfo.Attributes))
                        continue;

                    result.AddRange(findAction(folder, name));

                    string nameFile = direcryInfo.Name;

                    if (nameFile.ToLower().Contains(name.ToLower()))
                        result.Add(folder);
                }

                foreach (string file in Microsoft.VisualBasic.FileIO.FileSystem.GetFiles(path))
                {

                    FileInfo fileInfo = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(file);

                    if (((fileInfo.Attributes | FileAttributes.Hidden) == fileInfo.Attributes) || ((fileInfo.Attributes | FileAttributes.Temporary) == fileInfo.Attributes))
                        continue;

                    string nameFile = fileInfo.Name;

                    if (nameFile.Contains('.'))
                        nameFile = nameFile.Remove(nameFile.LastIndexOf('.'));

                    if (nameFile.ToLower().Contains(name.ToLower()))
                        result.Add(file);
                }

                return result;
            }
            catch (Exception ex) { }
            return new List<string>();
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
                BLL.ClassBLL.Instances.copyAction(path, temp.FullName, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs);

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
                    BLL.ClassBLL.Instances.deletePermanently(listPathTemp, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs);

                    return true;
                }
                catch (Exception ex)
                { }
            }

            //Xóa thư mục tạm
            listPathTemp.Add(temp.FullName);
            BLL.ClassBLL.Instances.deletePermanently(listPathTemp, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs);
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
                    deletePermanently(new List<string>() { newPath }, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs);
            }

            return false;
        }
        #endregion

        #region DetailView

        public void showViewDetail(ListView lvMain, string currentPath)
        {
            lvMain.Clear();
            lvMain.LargeImageList.Images.Clear();
            lvMain.SmallImageList.Images.Clear();
            lvMain.StateImageList.Images.Clear();

            if (currentPath.Equals("This PC"))
            {

            }
            else
            {
                //Add Folders
                string[] folders = Directory.GetDirectories(currentPath);

                lvMain.LargeImageList.Images.Add("FolderIcon", BLL.ShellIcon.GetLargeFolderIcon().ToBitmap());

                foreach (string folder in folders)
                {
                    DirectoryInfo info = new DirectoryInfo(folder);
                    if ((info.Attributes | FileAttributes.Hidden) == info.Attributes || ((info.Attributes | FileAttributes.Temporary) == info.Attributes))
                        continue;
                    string Name = info.Name;
                    string Date = info.CreationTime.ToShortDateString();
                    string Type = info.GetType().Name;
                    string[] strItem = new string[3];
                    strItem[0] = Name;
                    strItem[1] = Date;
                    strItem[2] = Type;

                    ListViewItem item = new ListViewItem(strItem);
                    item.ImageKey = "FolderIcon";
                    item.Name = Name;
                    item.Tag = folder;

                    lvMain.Items.Add(item);
                }

                string[] files = Directory.GetFiles(currentPath);

                foreach (string file in files)
                {

                    FileInfo info = new FileInfo(file);
                    if (lvMain.LargeImageList.Images[info.Name] == null)
                        lvMain.LargeImageList.Images.Add(info.Name, BLL.ShellIcon.GetLargeIcon(info.FullName).ToBitmap());

                    if ((info.Attributes | FileAttributes.Hidden) == info.Attributes || ((info.Attributes | FileAttributes.Temporary) == info.Attributes))
                        continue;

                    string Name = info.Name;
                    string Date = info.CreationTime.ToShortDateString();
                    string Type = info.GetType().Name;
                    double Size = info.Length;

                    string[] Unit = new string[] { "KB", "MB", "GB", "TB" };
                    int u = 0;
                    do
                    {
                        Size = Size / (Math.Pow(2, 10 * (u + 1)));
                    } while (Size > 1000 && u < 4);

                    Size = Math.Round(Size, 2);

                    string[] strItem = new string[4];
                    strItem[0] = Name;
                    strItem[1] = Date;
                    strItem[2] = Type;
                    strItem[3] = Size.ToString();

                    ListViewItem item = new ListViewItem(strItem);
                    item.ImageKey = info.Name;

                    if (info.Name.Contains('.'))
                        item.Text = info.Name.Remove(info.Name.LastIndexOf('.'));

                    item.Tag = info.FullName;

                    item.Name = info.Name;

                    lvMain.Items.Add(item);
                }
            }

            lvMain.Columns.Add("Name", 200);
            lvMain.Columns.Add("Date", 100);
            lvMain.Columns.Add("Type", 100);
            lvMain.Columns.Add("Size", 100);

            lvMain.View = View.Details;
        }

        #endregion

        #region Mail

        public bool sendMail(string mailFrom, string password, string mailTo, string subject, string cc, List<string> attachFile, string bodyMail)
        {
            try
            {

                SmtpClient mailclient = new SmtpClient("smtp.gmail.com", 587);

                mailclient.EnableSsl = true;

                mailclient.Credentials = new NetworkCredential(mailFrom, password);

                MailMessage message = new MailMessage(mailFrom, mailTo);

                message.Subject = subject;

                message.Body = bodyMail;

                foreach (string item in attachFile)
                    message.Attachments.Add(new Attachment(item));

                if (!string.IsNullOrEmpty(cc))
                    message.CC.Add(new MailAddress(cc));

                mailclient.Send(message);

                MessageBox.Show("New password was sent to " + mailTo, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        #endregion

    }
}
