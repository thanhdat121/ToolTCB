﻿namespace Transaction_Statistical.UControl
{
    partial class UC_Menu_Overview
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.cbo_LstTemplate = new System.Windows.Forms.ComboBox();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Template";
            // 
            // btn_Edit
            // 
            this.btn_Edit.AutoSize = true;
            this.btn_Edit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_Edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btn_Edit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.btn_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Edit.ForeColor = System.Drawing.Color.White;
            this.btn_Edit.Location = new System.Drawing.Point(225, 6);
            this.btn_Edit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(79, 25);
            this.btn_Edit.TabIndex = 15;
            this.btn_Edit.Text = "Edit";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // cbo_LstTemplate
            // 
            this.cbo_LstTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.cbo_LstTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_LstTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_LstTemplate.ForeColor = System.Drawing.Color.White;
            this.cbo_LstTemplate.FormattingEnabled = true;
            this.cbo_LstTemplate.Location = new System.Drawing.Point(57, 7);
            this.cbo_LstTemplate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbo_LstTemplate.Name = "cbo_LstTemplate";
            this.cbo_LstTemplate.Size = new System.Drawing.Size(164, 21);
            this.cbo_LstTemplate.TabIndex = 16;
            // 
            // btn_Apply
            // 
            this.btn_Apply.AutoSize = true;
            this.btn_Apply.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_Apply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btn_Apply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.btn_Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Apply.ForeColor = System.Drawing.Color.White;
            this.btn_Apply.Location = new System.Drawing.Point(308, 6);
            this.btn_Apply.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(79, 25);
            this.btn_Apply.TabIndex = 17;
            this.btn_Apply.Text = "Apply";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // UC_Menu_Overview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.cbo_LstTemplate);
            this.Controls.Add(this.btn_Edit);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UC_Menu_Overview";
            this.Size = new System.Drawing.Size(747, 474);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Edit;
        private System.Windows.Forms.ComboBox cbo_LstTemplate;
        private System.Windows.Forms.Button btn_Apply;
    }
}
