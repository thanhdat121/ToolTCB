﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;

namespace Transaction_Statistical
{
    public partial class UC_Explorer : UserControl
    {
        public static Image FolderImage;
        public static Image HardDiskImage;
        public static Image MyComputerImage;
        public static Image InternetImage;
        public static TreeNodeX Node_DisksDirectories;
        public static Dictionary<string, Image> ListExtensionIcon;
        private static Control textBoxShow;
        bool runningShowExplorer = false;
        bool showExplorer = false;
        public UC_Explorer(Control _textBoxShow)
        {
            InitializeComponent();
            init();
            textBoxShow = _textBoxShow;
        }
       
        public UC_Explorer()
        {
            InitializeComponent();
            init();
            this.tre_Explorer.MouseLeave += new System.EventHandler(uc_Explorer_MouseLeave);
        }
        public void ShowFromControl(Control _Parent, Control _textBoxShow)
        {
            if (!_Parent.Controls.Contains(this))
            {
                this.Size = new Size(0, 0);
                _Parent.Controls.Add(this);
                this.BringToFront();
                this.SendToBack();
                _Parent.Controls.SetChildIndex(this, 0);
            }

            if (showExplorer && _textBoxShow.Equals(textBoxShow)) return;
            textBoxShow = _textBoxShow;
            Point pt = textBoxShow.Parent.PointToScreen(textBoxShow.Location);
            this.Location = new Point(_Parent.PointToClient(pt).X, _Parent.PointToClient(pt).Y + textBoxShow.Height + 2);
            if (showExplorer)
                this.Size = new System.Drawing.Size(_textBoxShow.Width, this.Size.Height);
            else
            {
                this.Size = new System.Drawing.Size(_textBoxShow.Width, 0);
                SlideExplorerShow(null, null);
            }
            this.SelectPath(textBoxShow.Text);
        }
        private void uc_Explorer_MouseLeave(object sender, EventArgs e)
        {
            SlideExplorerShow(sender, e);
        }
        private void tre_Explorer_MouseHover(object sender, EventArgs e)
        {
            showExplorer = true;
        }
        private void txt_Path_MouseEnter(object sender, EventArgs e)
        {
            if (!showExplorer) SlideExplorerShow(sender, e);
        }
        public void SlideExplorerShow(object sender, EventArgs e)
        {
            if (showExplorer) showExplorer = false; else showExplorer = true;
            if (runningShowExplorer) return;
            runningShowExplorer = true;
            while (runningShowExplorer)
            {
                if (showExplorer)
                {
                    this.Height += 5;
                    if (this.Height >= 500)                  
                        break;
                }
                else
                {
                    this.Height -= 3;
                    if (this.Height == 0) break;
                }
                this.Update();
            }
            runningShowExplorer = false;
        }

        public void SelectPath(string path)
        {
            try
            {
                if (!(File.Exists(path) || Directory.Exists(path))) return;
                tre_Explorer.Select();
                if (tre_Explorer.SelectedNode != null)
                {
                    if (tre_Explorer.SelectedNode.Tag is DirectoryInfo) if ((tre_Explorer.SelectedNode.Tag as DirectoryInfo).FullName.Equals(path)) return;
                    if (tre_Explorer.SelectedNode.Tag is FileInfo) if ((tre_Explorer.SelectedNode.Tag as FileInfo).FullName.Equals(path)) return;
                }
                TreeNodeCollection nodeRoots = tre_Explorer.Nodes[0].Nodes;
                foreach (string name in path.Split('\\'))
                {
                    if (nodeRoots.ContainsKey(name.ToUpper()))
                    {
                        tre_Explorer.SelectedNode = nodeRoots[name.ToUpper()];
                        nodeRoots = nodeRoots[name.ToUpper()].Nodes;
                        tre_Explorer.SelectedNode.EnsureVisible();
                        continue;
                    }
                    foreach (TreeNode node in nodeRoots)
                    {
                        tre_Explorer.SelectedNode = node;
                        if (node.Nodes.ContainsKey(name.ToUpper()))
                        {
                            tre_Explorer.SelectedNode = node.Nodes[name.ToUpper()];
                            nodeRoots = node.Nodes[name.ToUpper()].Nodes;
                            break;
                        }
                    }
                }
            }catch(Exception ex)
            {

            }
        }
        private void init()
        {
            //image-icon extention
            FolderImage = IconHelper.ShellIcon.GetSmallFolderIcon().ToBitmap();
            HardDiskImage = IconHelper.ShellIcon.GetSmallIconFromExtension(@".").ToBitmap();
            MyComputerImage = Properties.Resources.Computer.ToBitmap();
            InternetImage = Properties.Resources.Internet.ToBitmap();
            LoadFullDiskDirectory();
            tre_Explorer.Nodes.Add(Node_DisksDirectories);
        }
        public void LoadFullDiskDirectory()
        {
            Node_DisksDirectories = new TreeNodeX(Environment.MachineName);
            Node_DisksDirectories.ImageKey = Node_DisksDirectories.SelectedImageKey = "Computer";
            GetDrivesToNode(Node_DisksDirectories);
            Node_DisksDirectories.Expand();
        }
        public void GetDrivesToNode(TreeNodeX nodeRoot)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
           TreeNode driveItem;
            foreach (DriveInfo drive in drives)
            {

                driveItem = nodeRoot.Nodes.Add(drive.Name.TrimEnd('\\').ToUpper(), drive.DriveType.ToString() + @" (" + drive.Name + @")");
                driveItem.Tag = drive;
                driveItem.ImageKey = driveItem.SelectedImageKey = drive.DriveType.ToString();

                DirectoryInfo info = new DirectoryInfo(drive.Name);               
                if (info.Exists)
                {
                    driveItem.Text = drive.VolumeLabel + @" (" + drive.Name + @")";
                    GetDirectoriesOfDriveToNode(drive, driveItem);                    
                }
            }
        }
        public void GetDirectoriesOfDriveToNode(DriveInfo driveInfo, TreeNode nodeDrive)
        {
            DirectoryInfo info = new DirectoryInfo(driveInfo.Name);
            GetAllDirectoriesFilesOfDirectoryToNode(info, nodeDrive,  2);
        }
        public bool GetAllDirectoriesFilesOfDirectoryToNode(DirectoryInfo rootPath, TreeNode NodeRoot, int nloop)
        {
            if (nloop > 0)
            {
                string imgFile;
                nloop--;
                try
                {
                    // System.Security.AccessControl.DirectorySecurity AC = rootPath.GetAccessControl(System.Security.AccessControl.AccessControlSections.Access);  
                    foreach (DirectoryInfo subDir in rootPath.GetDirectories())
                    {
                        if (NodeRoot.Nodes.ContainsKey(subDir.Name.ToUpper())) continue;
                        TreeNode aNode = NodeRoot.Nodes.Add(subDir.Name.ToUpper(), subDir.Name);
                        aNode.Tag = subDir;
                        aNode.ImageKey = IconHelper.ImageUltility.CreateBitmapAttributes(subDir.Attributes, "Folder_Close", ref imageList);
                        aNode.SelectedImageKey = IconHelper.ImageUltility.CreateBitmapAttributes(subDir.Attributes, "Folder_Open", ref imageList);

                        if (nloop > 0) GetAllDirectoriesFilesOfDirectoryToNode(subDir, aNode, nloop);
                    }
                    foreach (FileInfo file in rootPath.GetFiles())
                    {
                        if (NodeRoot.Nodes.ContainsKey(file.Name.ToUpper())) continue;
                        TreeNode aNode = NodeRoot.Nodes.Add(file.Name.ToUpper(), file.Name);
                        aNode.Tag = file;
                        imgFile = file.Extension;

                        if (imgFile.ToUpper().Equals(".LNK") || imgFile.ToUpper().Equals(".URL") || imgFile.ToUpper().Equals(".EXE"))
                        {
                            Bitmap icon = Icon.ExtractAssociatedIcon(file.FullName).ToBitmap();
                            if (icon.Height > 16 || icon.Width > 16) icon = IconHelper.ImageUltility.ResizeImage(icon, 18, 18);
                            imageList.Images.Add(file.Name, icon);
                            imgFile = file.Name;
                        }
                        else
                        if (string.IsNullOrEmpty(imgFile)) imgFile = "NewFile";
                        else
                            if (!imageList.Images.ContainsKey(imgFile))
                        {
                            imageList.Images.Add(imgFile, IconHelper.ShellIcon.GetSmallIconFromExtension(file.Extension).ToBitmap());

                        }
                        aNode.ImageKey = aNode.SelectedImageKey = IconHelper.ImageUltility.CreateBitmapAttributes(file.Attributes, imgFile, ref imageList);
                        
                    }
                    return true;
                }
                catch(Exception ex)
                {
                    //  NodeRoot.CheckBoxVisible = false;
                    //DevComponents.DotNetBar.ElementStyle styleDeny = new DevComponents.DotNetBar.ElementStyle();
                    //styleDeny.TextColor = Color.Red;
                   // NodeRoot.Style = styleDeny;
                    //NodeRoot.Image = IconHelper.ImageUltility.AddIconLockToBitmap(new Bitmap(NodeRoot.Image), IconHelper.ImageUltility.ResizeImage(Properties.Resources.Lock.ToBitmap(), 10, 10), IconHelper.ImageUltility.PositionIcon.BottomRight);
                }
            }
            return false;
        }

        private void tre_Explorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if ((tre_Explorer.SelectedNode).Tag is DirectoryInfo)
                {
                    GetAllDirectoriesFilesOfDirectoryToNode((tre_Explorer.SelectedNode).Tag as DirectoryInfo, tre_Explorer.SelectedNode, 1);
                }
                if (tre_Explorer.SelectedNode.Tag is DirectoryInfo) textBoxShow.Text = (tre_Explorer.SelectedNode.Tag as DirectoryInfo).FullName;
                if (tre_Explorer.SelectedNode.Tag is FileInfo) textBoxShow.Text = (tre_Explorer.SelectedNode.Tag as FileInfo).FullName;

            }
            catch (Exception ex)
            {
            }
        }

        private void tre_Explorer_MouseLeave(object sender, EventArgs e)
        {
       //     control.Text = string.Empty;
        }
        public static void OpenFile(string path, bool deleteAfterClose)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                Process process = new Process();
                process.StartInfo.FileName = file.FullName;
                process.Start();
                if (deleteAfterClose)
                {
                    bool inProcess = true;
                    while (inProcess)
                    {
                        process.Refresh();
                        System.Threading.Thread.Sleep(10);
                        if (process.HasExited)
                        {
                            inProcess = false;
                        }
                    }
                    process.WaitForExit(100);
                    File.Delete(file.FullName);
                    Directory.Delete(file.DirectoryName, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Open File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tre_Explorer_DoubleClick(object sender, EventArgs e)
        {
            if (tre_Explorer.SelectedNode.Tag is FileInfo)
            {
                string path = (tre_Explorer.SelectedNode.Tag as FileInfo).FullName;
                Thread th_file = new Thread(() => OpenFile(path, false));
                th_file.Start();
            }
        }

        
    }  
 

}
