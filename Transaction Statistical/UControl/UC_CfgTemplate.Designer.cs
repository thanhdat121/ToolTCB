﻿namespace Transaction_Statistical
{
    partial class UC_CfgTemplate
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
        private void InitializeComponent2()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_CfgTemplate));
            this.spc_Main = new System.Windows.Forms.SplitContainer();
            this.grp_Keyword = new Mode_GroupBox();
            this.spc_Keyword_Main = new System.Windows.Forms.SplitContainer();
            this.cbo_Keyword_Typelog = new Mode_ComboBox();
            this.btn_Keyword_Resfresh = new Mode_Button();
            this.cbo_Keyword_LstKeyword = new Mode_ComboBox();
            this.spc_Keyword_Level = new System.Windows.Forms.SplitContainer();
            this.spc_Keyword_Pattern = new System.Windows.Forms.SplitContainer();
            this.fctxt_Pattern = new Mode_FastColoredTextBox();
            this.label1 = new Mode_Label();
            this.chk_Keywork_Pattern = new Mode_CheckBox();
            this.btn_Keyword_Add = new Mode_Button();
            this.btn_Keyword_Remove = new Mode_Button();
            this.btn_Keyword_Save = new Mode_Button();
            this.btn_Keyword_Help = new Mode_Button();
            this.spc_Keyword_Test = new System.Windows.Forms.SplitContainer();
            this.fctxt_Test = new Mode_FastColoredTextBox();
            this.btn_RunTest2 = new Mode_Button();
            this.label2 = new Mode_Label();
            this.chk_Keywork_Test = new Mode_CheckBox();
            this.btn_Keyword_Import = new Mode_Button();
            this.btn_Keyword_Run = new Mode_Button();
            this.grp_Transaction = new Mode_GroupBox();
            this.label5 = new Mode_Label();
            this.label4 = new Mode_Label();
            this.label3 = new Mode_Label();
            this.btn_Transaction_Refresh = new Mode_Button();
            this.cbo_Transactions = new Mode_ComboBox();
            this.btn_Transaction_Add = new Mode_Button();
            this.btn_Transaction_Save = new Mode_Button();
            this.btn_Transaction_Remove = new Mode_Button();
            this.cbo_Transaction_Identification = new CheckedComboBox();
            this.cbo_Transaction_UnSuccess = new Transaction_Statistical.CheckedComboBox();
            this.cbo_Transaction_Success = new Transaction_Statistical.CheckedComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).BeginInit();
            this.spc_Main.Panel1.SuspendLayout();
            this.spc_Main.Panel2.SuspendLayout();
            this.spc_Main.SuspendLayout();
            this.grp_Keyword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Keyword_Main)).BeginInit();
            this.spc_Keyword_Main.Panel1.SuspendLayout();
            this.spc_Keyword_Main.Panel2.SuspendLayout();
            this.spc_Keyword_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Keyword_Level)).BeginInit();
            this.spc_Keyword_Level.Panel1.SuspendLayout();
            this.spc_Keyword_Level.Panel2.SuspendLayout();
            this.spc_Keyword_Level.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Keyword_Pattern)).BeginInit();
            this.spc_Keyword_Pattern.Panel1.SuspendLayout();
            this.spc_Keyword_Pattern.Panel2.SuspendLayout();
            this.spc_Keyword_Pattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctxt_Pattern)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Keyword_Test)).BeginInit();
            this.spc_Keyword_Test.Panel1.SuspendLayout();
            this.spc_Keyword_Test.Panel2.SuspendLayout();
            this.spc_Keyword_Test.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctxt_Test)).BeginInit();
            this.grp_Transaction.SuspendLayout();
            this.SuspendLayout();
            // 
            // spc_Main
            // 
            this.spc_Main.BackColor = System.Drawing.Color.Transparent;
            this.spc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Main.Location = new System.Drawing.Point(0, 0);
            this.spc_Main.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spc_Main.Name = "spc_Main";
            // 
            // spc_Main.Panel1
            // 
            this.spc_Main.Panel1.Controls.Add(this.grp_Keyword);
            // 
            // spc_Main.Panel2
            // 
            this.spc_Main.Panel2.Controls.Add(this.grp_Transaction);
            this.spc_Main.Size = new System.Drawing.Size(1500, 779);
            this.spc_Main.SplitterDistance = 808;
            this.spc_Main.TabIndex = 0;
            // 
            // grp_Keyword
            // 
            this.grp_Keyword.BackColor = System.Drawing.Color.Transparent;
            this.grp_Keyword.Controls.Add(this.spc_Keyword_Main);
            this.grp_Keyword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Keyword.Location = new System.Drawing.Point(0, 0);
            this.grp_Keyword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grp_Keyword.Name = "grp_Keyword";
            this.grp_Keyword.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grp_Keyword.Size = new System.Drawing.Size(808, 779);
            this.grp_Keyword.TabIndex = 0;
            this.grp_Keyword.TabStop = false;
            this.grp_Keyword.Text = "Key works";
            this.grp_Keyword.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            // 
            // spc_Keyword_Main
            // 
            this.spc_Keyword_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Keyword_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spc_Keyword_Main.IsSplitterFixed = true;
            this.spc_Keyword_Main.Location = new System.Drawing.Point(3, 17);
            this.spc_Keyword_Main.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spc_Keyword_Main.Name = "spc_Keyword_Main";
            this.spc_Keyword_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_Keyword_Main.Panel1
            // 
            this.spc_Keyword_Main.Panel1.Controls.Add(this.cbo_Keyword_Typelog);
            this.spc_Keyword_Main.Panel1.Controls.Add(this.btn_Keyword_Resfresh);
            this.spc_Keyword_Main.Panel1.Controls.Add(this.cbo_Keyword_LstKeyword);
            // 
            // spc_Keyword_Main.Panel2
            // 
            this.spc_Keyword_Main.Panel2.Controls.Add(this.spc_Keyword_Level);
            this.spc_Keyword_Main.Size = new System.Drawing.Size(802, 760);
            this.spc_Keyword_Main.SplitterDistance = 53;
            this.spc_Keyword_Main.TabIndex = 0;
            // 
            // cbo_Keyword_Typelog
            // 
            this.cbo_Keyword_Typelog.BackColor =InitGUI.Custom.Frm_Background.DisplayColor;
            this.cbo_Keyword_Typelog.ForeColor = this.ForeColor;
            this.cbo_Keyword_Typelog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Keyword_Typelog.FormattingEnabled = true;
            this.cbo_Keyword_Typelog.Location = new System.Drawing.Point(3, 16);
            this.cbo_Keyword_Typelog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_Keyword_Typelog.Name = "cbo_Keyword_Typelog";
            this.cbo_Keyword_Typelog.Size = new System.Drawing.Size(199, 24);
            this.cbo_Keyword_Typelog.Sorted = true;
            this.cbo_Keyword_Typelog.TabIndex = 15;
            this.cbo_Keyword_Typelog.SelectedIndexChanged += new System.EventHandler(this.cbo_Keyword_Typelog_SelectedIndexChanged);
            this.cbo_Keyword_Typelog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_Keyword_Typelog.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            // 
            // btn_Keyword_Resfresh
            // 
            this.btn_Keyword_Resfresh.AutoSize = true;           
            this.btn_Keyword_Resfresh.Location = new System.Drawing.Point(645, 14);
            this.btn_Keyword_Resfresh.Name = "btn_Keyword_Resfresh";
            this.btn_Keyword_Resfresh.Size = new System.Drawing.Size(105, 36);
            this.btn_Keyword_Resfresh.TabIndex = 14;
            this.btn_Keyword_Resfresh.Text = "Resfresh";
            // 
            // cbo_Keyword_LstKeyword
            // 
            this.cbo_Keyword_LstKeyword.BackColor =InitGUI.Custom.Frm_Background.DisplayColor;
            this.cbo_Keyword_LstKeyword.ForeColor = this.ForeColor;
            this.cbo_Keyword_LstKeyword.FormattingEnabled = true;
            this.cbo_Keyword_LstKeyword.Location = new System.Drawing.Point(208, 16);
            this.cbo_Keyword_LstKeyword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_Keyword_LstKeyword.Name = "cbo_Keyword_LstKeyword";
            this.cbo_Keyword_LstKeyword.Size = new System.Drawing.Size(431, 24);
            this.cbo_Keyword_LstKeyword.Sorted = true;
            this.cbo_Keyword_LstKeyword.TabIndex = 13;
            this.cbo_Keyword_LstKeyword.SelectedIndexChanged += new System.EventHandler(this.cbo_Keyword_LstKeyword_SelectedIndexChanged);
            this.cbo_Keyword_LstKeyword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_Keyword_LstKeyword.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            // 
            // spc_Keyword_Level
            // 
            this.spc_Keyword_Level.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Keyword_Level.Location = new System.Drawing.Point(0, 0);
            this.spc_Keyword_Level.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spc_Keyword_Level.Name = "spc_Keyword_Level";
            this.spc_Keyword_Level.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_Keyword_Level.Panel1
            // 
            this.spc_Keyword_Level.Panel1.Controls.Add(this.spc_Keyword_Pattern);
            // 
            // spc_Keyword_Level.Panel2
            // 
            this.spc_Keyword_Level.Panel2.Controls.Add(this.spc_Keyword_Test);
            this.spc_Keyword_Level.Size = new System.Drawing.Size(802, 703);
            this.spc_Keyword_Level.SplitterDistance = 332;
            this.spc_Keyword_Level.TabIndex = 0;
            // 
            // spc_Keyword_Pattern
            // 
            this.spc_Keyword_Pattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Keyword_Pattern.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spc_Keyword_Pattern.IsSplitterFixed = true;
            this.spc_Keyword_Pattern.Location = new System.Drawing.Point(0, 0);
            this.spc_Keyword_Pattern.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spc_Keyword_Pattern.Name = "spc_Keyword_Pattern";
            // 
            // spc_Keyword_Pattern.Panel1
            // 
            this.spc_Keyword_Pattern.Panel1.Controls.Add(this.fctxt_Pattern);
            // 
            // spc_Keyword_Pattern.Panel2
            // 
            this.spc_Keyword_Pattern.Panel2.Controls.Add(this.label1);
            this.spc_Keyword_Pattern.Panel2.Controls.Add(this.chk_Keywork_Pattern);
            this.spc_Keyword_Pattern.Panel2.Controls.Add(this.btn_Keyword_Add);
            this.spc_Keyword_Pattern.Panel2.Controls.Add(this.btn_Keyword_Remove);
            this.spc_Keyword_Pattern.Panel2.Controls.Add(this.btn_Keyword_Save);
            this.spc_Keyword_Pattern.Panel2.Controls.Add(this.btn_Keyword_Help);
            this.spc_Keyword_Pattern.Size = new System.Drawing.Size(802, 332);
            this.spc_Keyword_Pattern.SplitterDistance = 643;
            this.spc_Keyword_Pattern.TabIndex = 0;
            // 
            // fctxt_Pattern
            // 
            this.fctxt_Pattern.BackColor = InitGUI.Custom.Editor_Background.DisplayColor;
            this.fctxt_Pattern.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            this.fctxt_Pattern.AllowSeveralTextStyleDrawing = true;
            this.fctxt_Pattern.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctxt_Pattern.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.fctxt_Pattern.AutoScrollMinSize = new System.Drawing.Size(0, 18);
            this.fctxt_Pattern.BackBrush = null;
            this.fctxt_Pattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fctxt_Pattern.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fctxt_Pattern.CharHeight = 18;
            this.fctxt_Pattern.CharWidth = 10;
            this.fctxt_Pattern.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctxt_Pattern.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctxt_Pattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctxt_Pattern.IsReplaceMode = false;
            this.fctxt_Pattern.Language = FastColoredTextBoxNS.Language.CSharp;
            this.fctxt_Pattern.LeftBracket = '(';
            this.fctxt_Pattern.LeftBracket2 = '{';
            this.fctxt_Pattern.Location = new System.Drawing.Point(0, 0);
            this.fctxt_Pattern.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fctxt_Pattern.Name = "fctxt_Pattern";
            this.fctxt_Pattern.Paddings = new System.Windows.Forms.Padding(0);
            this.fctxt_Pattern.RightBracket = ')';
            this.fctxt_Pattern.RightBracket2 = '}';
            this.fctxt_Pattern.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctxt_Pattern.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctxt_Pattern.ServiceColors")));
            this.fctxt_Pattern.ServiceLinesColor = System.Drawing.Color.DimGray;
            this.fctxt_Pattern.Size = new System.Drawing.Size(643, 332);
            this.fctxt_Pattern.TabIndex = 1;
            this.fctxt_Pattern.WordWrap = true;
            this.fctxt_Pattern.Zoom = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Pattern String";
            // 
            // chk_Keywork_Pattern
            // 
            this.chk_Keywork_Pattern.AutoSize = true;
            this.chk_Keywork_Pattern.Checked = true;
            this.chk_Keywork_Pattern.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Keywork_Pattern.Location = new System.Drawing.Point(28, 224);
            this.chk_Keywork_Pattern.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chk_Keywork_Pattern.Name = "chk_Keywork_Pattern";
            this.chk_Keywork_Pattern.Size = new System.Drawing.Size(102, 21);
            this.chk_Keywork_Pattern.TabIndex = 17;
            this.chk_Keywork_Pattern.Text = "Word Wrap";
            this.chk_Keywork_Pattern.UseVisualStyleBackColor = true;
            this.chk_Keywork_Pattern.CheckedChanged += new System.EventHandler(this.chk_Keywork_Pattern_CheckedChanged);
            // 
            // btn_Keyword_Add
            // 
            this.btn_Keyword_Add.Location = new System.Drawing.Point(29, 80);
            this.btn_Keyword_Add.Name = "btn_Keyword_Add";
            this.btn_Keyword_Add.Size = new System.Drawing.Size(105, 42);
            this.btn_Keyword_Add.TabIndex = 15;
            this.btn_Keyword_Add.Text = "Add";
            this.btn_Keyword_Add.Click += new System.EventHandler(this.btn_Keyword_Add_Click);
            // 
            // btn_Keyword_Remove
            // 
            this.btn_Keyword_Remove.Location = new System.Drawing.Point(28, 128);
            this.btn_Keyword_Remove.Name = "btn_Keyword_Remove";
            this.btn_Keyword_Remove.Size = new System.Drawing.Size(105, 42);
            this.btn_Keyword_Remove.TabIndex = 16;
            this.btn_Keyword_Remove.Text = "Remove";
            this.btn_Keyword_Remove.Click += new System.EventHandler(this.btn_Keyword_Remove_Click);
            // 
            // btn_Keyword_Save
            // 
            this.btn_Keyword_Save.Location = new System.Drawing.Point(29, 32);
            this.btn_Keyword_Save.Name = "btn_Keyword_Save";
            this.btn_Keyword_Save.Size = new System.Drawing.Size(105, 42);
            this.btn_Keyword_Save.TabIndex = 14;
            this.btn_Keyword_Save.Text = "Save";
            this.btn_Keyword_Save.Click += new System.EventHandler(this.btn_Keyword_Save_Click);
            // 
            // btn_Keyword_Help
            // 
            this.btn_Keyword_Help.Location = new System.Drawing.Point(28, 176);
            this.btn_Keyword_Help.Name = "btn_Keyword_Help";
            this.btn_Keyword_Help.Size = new System.Drawing.Size(105, 42);
            this.btn_Keyword_Help.TabIndex = 12;
            this.btn_Keyword_Help.Text = "Help";
            this.btn_Keyword_Help.Click += new System.EventHandler(this.btn_Keyword_Help_Click);
            // 
            // spc_Keyword_Test
            // 
            this.spc_Keyword_Test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Keyword_Test.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spc_Keyword_Test.Location = new System.Drawing.Point(0, 0);
            this.spc_Keyword_Test.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.spc_Keyword_Test.Name = "spc_Keyword_Test";
            // 
            // spc_Keyword_Test.Panel1
            // 
            this.spc_Keyword_Test.Panel1.Controls.Add(this.fctxt_Test);
            // 
            // spc_Keyword_Test.Panel2
            // 
            this.spc_Keyword_Test.Panel2.Controls.Add(this.btn_RunTest2);
            this.spc_Keyword_Test.Panel2.Controls.Add(this.label2);
            this.spc_Keyword_Test.Panel2.Controls.Add(this.chk_Keywork_Test);
            this.spc_Keyword_Test.Panel2.Controls.Add(this.btn_Keyword_Import);
            this.spc_Keyword_Test.Panel2.Controls.Add(this.btn_Keyword_Run);
            this.spc_Keyword_Test.Size = new System.Drawing.Size(802, 367);
            this.spc_Keyword_Test.SplitterDistance = 643;
            this.spc_Keyword_Test.TabIndex = 0;
            // 
            // fctxt_Test
            // 
            this.fctxt_Test.BackColor = InitGUI.Custom.Editor_Background.DisplayColor;
            this.fctxt_Test.ForeColor = InitGUI.Custom.Editor_ForeColor.DisplayColor;
            this.fctxt_Test.AllowSeveralTextStyleDrawing = true;
            this.fctxt_Test.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctxt_Test.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.fctxt_Test.AutoScrollMinSize = new System.Drawing.Size(0, 18);
            this.fctxt_Test.BackBrush = null;
            this.fctxt_Test.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fctxt_Test.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fctxt_Test.CharHeight = 18;
            this.fctxt_Test.CharWidth = 10;
            this.fctxt_Test.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctxt_Test.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctxt_Test.Dock = System.Windows.Forms.DockStyle.Fill;

            this.fctxt_Test.IsReplaceMode = false;
            this.fctxt_Test.Language = FastColoredTextBoxNS.Language.CSharp;
            this.fctxt_Test.LeftBracket = '(';
            this.fctxt_Test.LeftBracket2 = '{';
            this.fctxt_Test.Location = new System.Drawing.Point(0, 0);
            this.fctxt_Test.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fctxt_Test.Name = "fctxt_Test";
            this.fctxt_Test.Paddings = new System.Windows.Forms.Padding(0);
            this.fctxt_Test.RightBracket = ')';
            this.fctxt_Test.RightBracket2 = '}';
            this.fctxt_Test.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctxt_Test.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctxt_Test.ServiceColors")));
            this.fctxt_Test.ServiceLinesColor = System.Drawing.Color.DimGray;
            this.fctxt_Test.Size = new System.Drawing.Size(643, 367);
            this.fctxt_Test.TabIndex = 2;
            this.fctxt_Test.WordWrap = true;
            this.fctxt_Test.Zoom = 100;
            // 
            // btn_RunTest2
            // 
            this.btn_RunTest2.Location = new System.Drawing.Point(28, 87);
            this.btn_RunTest2.Name = "btn_RunTest2";
            this.btn_RunTest2.Size = new System.Drawing.Size(105, 42);
            this.btn_RunTest2.TabIndex = 17;
            this.btn_RunTest2.Text = "Run Test 2";
            this.btn_RunTest2.Click += new System.EventHandler(this.btn_RunTest2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Test String";
            // 
            // chk_Keywork_Test
            // 
            this.chk_Keywork_Test.AutoSize = true;
            this.chk_Keywork_Test.Checked = true;
            this.chk_Keywork_Test.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Keywork_Test.Location = new System.Drawing.Point(28, 204);
            this.chk_Keywork_Test.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chk_Keywork_Test.Name = "chk_Keywork_Test";
            this.chk_Keywork_Test.Size = new System.Drawing.Size(102, 21);
            this.chk_Keywork_Test.TabIndex = 16;
            this.chk_Keywork_Test.Text = "Word Wrap";
            this.chk_Keywork_Test.UseVisualStyleBackColor = true;
            this.chk_Keywork_Test.CheckedChanged += new System.EventHandler(this.chk_Keywork_Test_CheckedChanged);
            // 
            // btn_Keyword_Import
            // 
            this.btn_Keyword_Import.Location = new System.Drawing.Point(28, 146);
            this.btn_Keyword_Import.Name = "btn_Keyword_Import";
            this.btn_Keyword_Import.Size = new System.Drawing.Size(105, 42);
            this.btn_Keyword_Import.TabIndex = 15;
            this.btn_Keyword_Import.Text = "Import";
            this.btn_Keyword_Import.Click += new System.EventHandler(this.btn_Keyword_Import_Click);
            // 
            // btn_Keyword_Run
            // 
            this.btn_Keyword_Run.Location = new System.Drawing.Point(29, 41);
            this.btn_Keyword_Run.Name = "btn_Keyword_Run";
            this.btn_Keyword_Run.Size = new System.Drawing.Size(105, 42);
            this.btn_Keyword_Run.TabIndex = 14;
            this.btn_Keyword_Run.Text = "Run";
            this.btn_Keyword_Run.Click += new System.EventHandler(this.btn_Keyword_Run_Click);
            // 
            // grp_Transaction
            // 
            this.grp_Transaction.Controls.Add(this.cbo_Transaction_Identification);
            this.grp_Transaction.Controls.Add(this.cbo_Transaction_UnSuccess);
            this.grp_Transaction.Controls.Add(this.label4);
            this.grp_Transaction.Controls.Add(this.label3);
            this.grp_Transaction.Controls.Add(this.label5);
            this.grp_Transaction.Controls.Add(this.cbo_Transaction_Success);
            this.grp_Transaction.Controls.Add(this.btn_Transaction_Refresh);
            this.grp_Transaction.Controls.Add(this.cbo_Transactions);
            this.grp_Transaction.Controls.Add(this.btn_Transaction_Add);
            this.grp_Transaction.Controls.Add(this.btn_Transaction_Save);
            this.grp_Transaction.Controls.Add(this.btn_Transaction_Remove);
            this.grp_Transaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Transaction.Location = new System.Drawing.Point(0, 0);
            this.grp_Transaction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grp_Transaction.Name = "grp_Transaction";
            this.grp_Transaction.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grp_Transaction.Size = new System.Drawing.Size(688, 779);
            this.grp_Transaction.TabIndex = 1;
            this.grp_Transaction.TabStop = false;
            this.grp_Transaction.Text = "Transaction defind";
            this.grp_Transaction.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 17);
            this.label5.TabIndex = 42;
            this.label5.Text = "Identification events";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 17);
            this.label4.TabIndex = 42;
            this.label4.Text = "UnSuccess events";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 41;
            this.label3.Text = "Success events";
            // 
            // btn_Transaction_Refresh
            // 
            this.btn_Transaction_Refresh.Location = new System.Drawing.Point(488, 33);
            this.btn_Transaction_Refresh.Name = "btn_Transaction_Refresh";
            this.btn_Transaction_Refresh.Size = new System.Drawing.Size(105, 28);
            this.btn_Transaction_Refresh.TabIndex = 25;
            this.btn_Transaction_Refresh.Text = "Add";
            this.btn_Transaction_Refresh.Click += new System.EventHandler(this.btn_Transaction_Refresh_Click);
            // 
            // cbo_Transactions
            // 
            this.cbo_Transactions.BackColor = InitGUI.Custom.Frm_Background.DisplayColor;
            this.cbo_Transactions.FormattingEnabled = true;
            this.cbo_Transactions.Location = new System.Drawing.Point(24, 34);
            this.cbo_Transactions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbo_Transactions.Name = "cbo_Transactions";
            this.cbo_Transactions.Size = new System.Drawing.Size(447, 24);
            this.cbo_Transactions.Sorted = true;
            this.cbo_Transactions.TabIndex = 18;
            this.cbo_Transactions.SelectedIndexChanged += new System.EventHandler(this.cbo_Transactions_SelectedIndexChanged);
            this.cbo_Transactions.TextChanged += new System.EventHandler(this.cbo_Transactions_TextChanged);
            this.cbo_Transactions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cbo_Transactions_MouseDown);
            this.cbo_Transactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_Transactions.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            // 
            // btn_Transaction_Add
            // 
            this.btn_Transaction_Add.Location = new System.Drawing.Point(180, 208);
            this.btn_Transaction_Add.Name = "btn_Transaction_Add";
            this.btn_Transaction_Add.Size = new System.Drawing.Size(105, 42);
            this.btn_Transaction_Add.TabIndex = 19;
            this.btn_Transaction_Add.Text = "Add";
            this.btn_Transaction_Add.Click += new System.EventHandler(this.btn_Transaction_Add_Click);           
            // 
            // btn_Transaction_Save
            // 
            this.btn_Transaction_Save.Location = new System.Drawing.Point(290, 208);
            this.btn_Transaction_Save.Name = "btn_Transaction_Save";
            this.btn_Transaction_Save.Size = new System.Drawing.Size(105, 42);
            this.btn_Transaction_Save.TabIndex = 20;
            this.btn_Transaction_Save.Text = "Save";
            this.btn_Transaction_Save.Click += new System.EventHandler(this.btn_Transaction_Save_Click);
            // 
            // btn_Transaction_Remove
            // 
            this.btn_Transaction_Remove.Location = new System.Drawing.Point(401, 208);
            this.btn_Transaction_Remove.Name = "btn_Transaction_Remove";
            this.btn_Transaction_Remove.Size = new System.Drawing.Size(105, 42);
            this.btn_Transaction_Remove.TabIndex = 21;
            this.btn_Transaction_Remove.Text = "Remove";
            this.btn_Transaction_Remove.Click += new System.EventHandler(this.btn_Transaction_Remove_Click);
            // 
            // cbo_Transaction_Identification
            // 
            this.cbo_Transaction_Identification.BackColor = InitGUI.Custom.Frm_Background.DisplayColor;
            this.cbo_Transaction_Identification.CheckOnClick = true;
            this.cbo_Transaction_Identification.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbo_Transaction_Identification.DropDownHeight = 1;
            this.cbo_Transaction_Identification.FormattingEnabled = true;
            this.cbo_Transaction_Identification.IntegralHeight = false;
            this.cbo_Transaction_Identification.Location = new System.Drawing.Point(179, 79);
            this.cbo_Transaction_Identification.Name = "cbo_Transaction_UnSuccess";
            this.cbo_Transaction_Identification.Size = new System.Drawing.Size(414, 23);
            this.cbo_Transaction_Identification.TabIndex = 43;
            this.cbo_Transaction_Identification.ValueSeparator = ",";
            this.cbo_Transaction_Identification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_Transaction_Identification.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            // 
            // cbo_Transaction_UnSuccess
            // 
            this.cbo_Transaction_UnSuccess.BackColor = InitGUI.Custom.Frm_Background.DisplayColor;
            this.cbo_Transaction_UnSuccess.CheckOnClick = true;
            this.cbo_Transaction_UnSuccess.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbo_Transaction_UnSuccess.DropDownHeight = 1;
            this.cbo_Transaction_UnSuccess.FormattingEnabled = true;
            this.cbo_Transaction_UnSuccess.IntegralHeight = false;
            this.cbo_Transaction_UnSuccess.Location = new System.Drawing.Point(179, 160);
            this.cbo_Transaction_UnSuccess.Name = "cbo_Transaction_UnSuccess";
            this.cbo_Transaction_UnSuccess.Size = new System.Drawing.Size(414, 23);
            this.cbo_Transaction_UnSuccess.TabIndex = 43;
            this.cbo_Transaction_UnSuccess.ValueSeparator = ",";
            this.cbo_Transaction_UnSuccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_Transaction_UnSuccess.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            // 
            // cbo_Transaction_Success
            // 
            this.cbo_Transaction_Success.BackColor = InitGUI.Custom.Frm_Background.DisplayColor;
            this.cbo_Transaction_Success.CheckOnClick = true;
            this.cbo_Transaction_Success.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbo_Transaction_Success.DropDownHeight = 1;
            this.cbo_Transaction_Success.FormattingEnabled = true;
            this.cbo_Transaction_Success.IntegralHeight = false;
            this.cbo_Transaction_Success.Location = new System.Drawing.Point(179, 122);
            this.cbo_Transaction_Success.Name = "cbo_Transaction_Success";
            this.cbo_Transaction_Success.Size = new System.Drawing.Size(414, 23);
            this.cbo_Transaction_Success.TabIndex = 40;
            this.cbo_Transaction_Success.ValueSeparator = ",";
            this.cbo_Transaction_Success.FlatStyle = System.Windows.Forms.FlatStyle.Flat; 
            this.cbo_Transaction_Success.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            // 
            // UC_CfgTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = InitGUI.Custom.Frm_Background.DisplayColor;
            this.ForeColor = InitGUI.Custom.Frm_ForeColor.DisplayColor;
            this.Controls.Add(this.spc_Main);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UC_CfgTemplate";
            this.Size = new System.Drawing.Size(1500, 779);
            this.spc_Main.Panel1.ResumeLayout(false);
            this.spc_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).EndInit();
            this.spc_Main.ResumeLayout(false);
            this.grp_Keyword.ResumeLayout(false);
            this.spc_Keyword_Main.Panel1.ResumeLayout(false);
            this.spc_Keyword_Main.Panel1.PerformLayout();
            this.spc_Keyword_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Keyword_Main)).EndInit();
            this.spc_Keyword_Main.ResumeLayout(false);
            this.spc_Keyword_Level.Panel1.ResumeLayout(false);
            this.spc_Keyword_Level.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Keyword_Level)).EndInit();
            this.spc_Keyword_Level.ResumeLayout(false);
            this.spc_Keyword_Pattern.Panel1.ResumeLayout(false);
            this.spc_Keyword_Pattern.Panel2.ResumeLayout(false);
            this.spc_Keyword_Pattern.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Keyword_Pattern)).EndInit();
            this.spc_Keyword_Pattern.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fctxt_Pattern)).EndInit();
            this.spc_Keyword_Test.Panel1.ResumeLayout(false);
            this.spc_Keyword_Test.Panel2.ResumeLayout(false);
            this.spc_Keyword_Test.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Keyword_Test)).EndInit();
            this.spc_Keyword_Test.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fctxt_Test)).EndInit();
            this.grp_Transaction.ResumeLayout(false);
            this.grp_Transaction.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer spc_Main;
        private System.Windows.Forms.SplitContainer spc_Keyword_Main;
        private Mode_Button btn_Transaction_Remove;
        private Mode_Button btn_Transaction_Save;
        private Mode_Button btn_Transaction_Add;
        private Mode_ComboBox cbo_Transactions;
        private Mode_GroupBox grp_Keyword;
        private Mode_GroupBox grp_Transaction;
        private System.Windows.Forms.SplitContainer spc_Keyword_Level;
        private System.Windows.Forms.SplitContainer spc_Keyword_Pattern;
        private System.Windows.Forms.SplitContainer spc_Keyword_Test;
        private Mode_Label label1;
        private Mode_CheckBox chk_Keywork_Pattern;
        private Mode_Button btn_Keyword_Add;
        private Mode_Button btn_Keyword_Remove;
        private Mode_Button btn_Keyword_Save;
        private Mode_Button btn_Keyword_Help;
        private Mode_Label label2;
        private Mode_CheckBox chk_Keywork_Test;
        private Mode_Button btn_Keyword_Import;
        private Mode_Button btn_Keyword_Run;
        private Mode_FastColoredTextBox fctxt_Pattern;
        private Mode_FastColoredTextBox fctxt_Test;
        private Mode_Button btn_Keyword_Resfresh;
        private Mode_ComboBox cbo_Keyword_LstKeyword;
        private Mode_Button btn_Transaction_Refresh;
        private Mode_ComboBox cbo_Keyword_Typelog;
        private Mode_Button btn_RunTest2;
        private CheckedComboBox cbo_Transaction_UnSuccess;
        private CheckedComboBox cbo_Transaction_Identification;
        private Mode_Label label4;
        private Mode_Label label3;
        private Mode_Label label5;
        private CheckedComboBox cbo_Transaction_Success;
    }
}
