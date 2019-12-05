﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Transaction_Statistical.UControl
{
    partial class UC_Transaction
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        bool runningShowMenu = false;
        bool showMenu = false;

        bool runningShowExplorer = false;
        bool showExplorer = false;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
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
                    //uc_Explorer.Location = new Point(uc_Explorer.Location.X + 3, uc_Explorer.Location.Y);
                    //if (uc_Explorer.Location.X >= 0) break;
                    uc_Explorer.Height += 5;
                    if (uc_Explorer.Height >= 500)
                    {
                        uc_Explorer.SelectPath(txt_Path.Text);
                        break;
                    }
                }
                else
                {
                    uc_Explorer.Height -= 3;
                    if (uc_Explorer.Height == 0) break;
                    //uc_Explorer.Location = new Point(uc_Explorer.Location.X - 3, uc_Explorer.Location.Y);
                    //if (uc_Explorer.Location.X <= -uc_Explorer.Width) break;
                }
                this.Update();
            }
            runningShowExplorer = false;
        }
        public void SlideMenuShow()
        {
            if (showMenu) showMenu = false; else showMenu = true;
            if (runningShowMenu) return;
            runningShowMenu = true;
            while (runningShowMenu)
            {
                if (showMenu)
                {
                    uc_Menu.Location = new Point(uc_Menu.Location.X + 3, uc_Menu.Location.Y);
                    if (uc_Menu.Location.X >= 0) break;
                }
                else
                {
                    uc_Menu.Location = new Point(uc_Menu.Location.X - 3, uc_Menu.Location.Y);
                    if (uc_Menu.Location.X <= -uc_Menu.Width) break;
                }
                this.Update();
            }
            runningShowMenu = false;
        }
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {          
            this.uc_Menu = new Transaction_Statistical.UControl.UC_Menu();
            this.txt_Path = new System.Windows.Forms.TextBox();
            this.gpBox_Actions = new System.Windows.Forms.GroupBox();
            this.pl_Actions = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer_Main = new System.Windows.Forms.SplitContainer();
            this.btn_Menu = new Transaction_Statistical.AddOn.ButtonMenu();
            this.btn_Read = new Transaction_Statistical.AddOn.ButtonZ();
            this.tre_LstTrans = new System.Windows.Forms.TreeView();
            this.cb_FullTime = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.gpBox_Actions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).BeginInit();
            this.splitContainer_Main.Panel1.SuspendLayout();
            this.splitContainer_Main.Panel2.SuspendLayout();
            this.splitContainer_Main.SuspendLayout();
            this.SuspendLayout();           
            // 
            // uc_Menu
            // 
            this.uc_Menu.BackColor = System.Drawing.Color.Transparent;
            this.uc_Menu.Location = new System.Drawing.Point(-10000, 100);
            this.uc_Menu.Name = "uc_Menu";
            this.uc_Menu.Size = new System.Drawing.Size(1066, 568);
            this.uc_Menu.TabIndex = 0;
            // 
            // txt_Path
            // 
            this.txt_Path.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txt_Path.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txt_Path.BackColor = System.Drawing.Color.DimGray;
            this.txt_Path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Path.ForeColor = System.Drawing.Color.White;
            this.txt_Path.Location = new System.Drawing.Point(21, 189);
            this.txt_Path.Name = "txt_Path";
            this.txt_Path.Size = new System.Drawing.Size(645, 22);
            this.txt_Path.TabIndex = 1;
            this.txt_Path.Text = @"E:\Project\NPS\Document\99147001_EJF\99147001_EJF\New folder";
            this.txt_Path.MouseClick +=new MouseEventHandler(txt_Path_MouseEnter);
            //this.txt_Path.MouseLeave += new System.EventHandler(txt_Path_MouseLeave);
            //this.txt_Path.MouseEnter += new System.EventHandler(txt_Path_MouseEnter);
            // 
            // uc_Explorer
            // 
            this.uc_Explorer = new UC_Explorer(txt_Path);
            this.uc_Explorer.BackColor = System.Drawing.Color.Transparent;
            this.uc_Explorer.Location = new Point(this.txt_Path.Location.X, this.txt_Path.Location.Y + this.txt_Path.Height + 5);
            this.uc_Explorer.Name = "uc_Explorer";
            this.uc_Explorer.Size = new System.Drawing.Size(this.txt_Path.Width, 0);
            this.uc_Explorer.TabIndex = 1;
            this.uc_Explorer.tre_Explorer.MouseLeave += new System.EventHandler(uc_Explorer_Leave);
            // 
            // gpBox_Actions
            // 
            this.gpBox_Actions.Controls.Add(this.pl_Actions);
            this.gpBox_Actions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gpBox_Actions.ForeColor = System.Drawing.Color.White;
            this.gpBox_Actions.Location = new System.Drawing.Point(21, 61);
            this.gpBox_Actions.Name = "gpBox_Actions";
            this.gpBox_Actions.Size = new System.Drawing.Size(645, 112);
            this.gpBox_Actions.TabIndex = 2;
            this.gpBox_Actions.TabStop = false;
            this.gpBox_Actions.Text = "Actions";
            // 
            // pl_Actions
            // 
            this.pl_Actions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_Actions.Location = new System.Drawing.Point(3, 18);
            this.pl_Actions.Name = "pl_Actions";
            this.pl_Actions.Size = new System.Drawing.Size(639, 91);
            this.pl_Actions.TabIndex = 0;
            // 
            // splitContainer_Main
            // 
            this.splitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer_Main.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_Main.Name = "splitContainer_Main";
            // 
            // splitContainer_Main.Panel1
            // 
            this.splitContainer_Main.Panel1.Controls.Add(this.btn_Menu);
            this.splitContainer_Main.Panel1.Controls.Add(this.btn_Read);
            this.splitContainer_Main.Panel1.Controls.Add(this.tre_LstTrans);
            this.splitContainer_Main.Panel1.Controls.Add(this.cb_FullTime);
            this.splitContainer_Main.Panel1.Controls.Add(this.label2);
            this.splitContainer_Main.Panel1.Controls.Add(this.dateTimePicker_End);
            this.splitContainer_Main.Panel1.Controls.Add(this.label1);
            this.splitContainer_Main.Panel1.Controls.Add(this.dateTimePicker_Start);
            this.splitContainer_Main.Panel1.Controls.Add(this.txt_Path);
            this.splitContainer_Main.Panel1.Controls.Add(this.gpBox_Actions);
            // 
            // splitContainer_Main.Panel2
            // 
            this.splitContainer_Main.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer_Main.Size = new System.Drawing.Size(1324, 639);
            this.splitContainer_Main.SplitterDistance = 560;
            this.splitContainer_Main.TabIndex = 3;
            // 
            // btn_Menu
            // 
            this.btn_Menu.BackColor = System.Drawing.Color.Transparent;
            this.btn_Menu.Color4point = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(240)))));
            this.btn_Menu.Color4pointDown = System.Drawing.Color.DeepSkyBlue;
            this.btn_Menu.Color4pointHover = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(215)))));
            this.btn_Menu.Location = new System.Drawing.Point(6, 7);
            this.btn_Menu.Name = "btn_Menu";
            this.btn_Menu.Size = new System.Drawing.Size(39, 39);
            this.btn_Menu.TabIndex = 1;
            this.btn_Menu.OnMouseDownHandler += new Transaction_Statistical.AddOn.ButtonMenu.MouseDownHandler(this.btn_Menu_OnMouseDownHandler);
            // 
            // btn_Read
            // 
            this.btn_Read.BorderLeft = false;
            this.btn_Read.BZBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(120)))), ((int)(((byte)(240)))));
            this.btn_Read.DisplayText = "Read";
            this.btn_Read.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Read.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Read.ForeColor = System.Drawing.Color.White;
            this.btn_Read.Location = new System.Drawing.Point(17, 224);
            this.btn_Read.MouseClickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.btn_Read.MouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(140)))));
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.NotchangeAfterMouseUP = false;
            this.btn_Read.Size = new System.Drawing.Size(649, 28);
            this.btn_Read.TabIndex = 9;
            this.btn_Read.Text = "Read";
            this.btn_Read.TextLocation_X = 210;
            this.btn_Read.TextLocation_Y = -3;
            this.btn_Read.UseVisualStyleBackColor = true;
            this.btn_Read.Click += new System.EventHandler(this.bt_Read_Click);
            // 
            // tre_LstTrans
            // 
            this.tre_LstTrans.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left )));
            this.tre_LstTrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.tre_LstTrans.ForeColor = System.Drawing.Color.White;
            this.tre_LstTrans.Location = new System.Drawing.Point(17, 269);
            this.tre_LstTrans.Name = "tre_LstTrans";
            this.tre_LstTrans.Size = new System.Drawing.Size(649, 355);
            this.tre_LstTrans.TabIndex = 8;
            this.tre_LstTrans.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tre_LstTrans_AfterSelect);
            // 
            // cb_FullTime
            // 
            this.cb_FullTime.AutoSize = true;
            this.cb_FullTime.ForeColor = System.Drawing.Color.White;
            this.cb_FullTime.Location = new System.Drawing.Point(575, 30);
            this.cb_FullTime.Name = "cb_FullTime";
            this.cb_FullTime.Size = new System.Drawing.Size(82, 21);
            this.cb_FullTime.TabIndex = 7;
            this.cb_FullTime.Text = "Full time";
            this.cb_FullTime.UseVisualStyleBackColor = true;
            this.cb_FullTime.CheckedChanged += new System.EventHandler(this.cb_FullTime_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(325, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "To";
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.CustomFormat = "HH:mm dd/mm/yy";
            this.dateTimePicker_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_End.Location = new System.Drawing.Point(359, 29);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(195, 22);
            this.dateTimePicker_End.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(70, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "From";
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.CustomFormat = "HH:mm dd/mm/yy";
            this.dateTimePicker_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_Start.Location = new System.Drawing.Point(116, 29);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(195, 22);
            this.dateTimePicker_Start.TabIndex = 3;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(18, 33);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(614, 355);
            this.propertyGrid1.TabIndex = 0;
            // 
            // UC_Transaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.Controls.Add(this.uc_Explorer);
            this.Controls.Add(this.uc_Menu);
            this.Controls.Add(this.splitContainer_Main);
            this.Name = "UC_Transaction";
            this.Size = new System.Drawing.Size(1324, 639);
            this.gpBox_Actions.ResumeLayout(false);
            this.splitContainer_Main.Panel1.ResumeLayout(false);
            this.splitContainer_Main.Panel1.PerformLayout();
            this.splitContainer_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Main)).EndInit();
            this.splitContainer_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txt_Path;
        private System.Windows.Forms.GroupBox gpBox_Actions;
        private System.Windows.Forms.FlowLayoutPanel pl_Actions;
        private System.Windows.Forms.SplitContainer splitContainer_Main;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.CheckBox cb_FullTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tre_LstTrans;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private AddOn.ButtonZ btn_Read;
        private AddOn.ButtonMenu btn_Menu;
        public UC_Menu uc_Menu;
        public UC_Explorer uc_Explorer;
    }
}
