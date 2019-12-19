﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace Transaction_Statistical.UControl
{
    public partial class UC_ExportCus : UserControl
    {

        public UC_ExportCus()
        {
            InitializeComponent();
            LoadTemplate();
        }
        public void LoadTemplate()
        {
            try
            {

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = UC_Menu_Startup.Template;
                ckbl_Forms.DataSource = bindingSource;
                ckbl_Forms.DisplayMember = "Value";
                UC_Menu_Startup.Template.ToList().ForEach(x => { ckbl_Forms.SetItemChecked(x.Key, true); });

                //lblPercent.Hide();
                //prgExport.Hide();
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            //prgExport.Show();
            //lblPercent.Show();
            btn_Export.Enabled = false;
            try
            {

                Dictionary<int, string> TemplateChoosen = new Dictionary<int, string>();
                foreach (var item in ckbl_Forms.CheckedItems)
                {
                    var row = (KeyValuePair<int, string>)(item);
                    TemplateChoosen.Add(row.Key, row.Value);
                }
                prgExport.Maximum = TemplateChoosen.Count();
                prgExport.Value = 0;
                prgExport.Step = 1;
                var w = System.Diagnostics.Stopwatch.StartNew();
                w.Start();
                if (InitParametar.ReadTrans.Export(txt_Destination.Text, TemplateChoosen, prgExport, lblPercent))
                {
                    w.Stop();
                    MessageBox.Show("Export successfully. (Execute : " + (w.ElapsedMilliseconds).ToString() + " ms)", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (chb_Open.Checked)
                        new Thread(() => UC_Explorer.OpenFile(InitParametar.ReadTrans.FileExport, false)).Start();
                    (this.Parent as Form).Close();
                }
            }
            catch { }
            btn_Export.Enabled = true;
        }

        private void chb_Open_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
