﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using Transaction_Statistical.Class;

namespace Transaction_Statistical
{
    public partial class UC_Menu_Startup : UserControl
    {
        public static Dictionary<int, string> Template = new Dictionary<int, string>()
        {
            {(int)TemplateHelper.TEMPLATE.CanQuyTheoCouterTrenMay,"Cân Quỹ Theo Counter Trên Máy" },
            {(int)TemplateHelper.TEMPLATE.BaoCaoGiaoDichTaiChinh,"GD Tài Chính" },
            {(int)TemplateHelper.TEMPLATE.BaoCaoGiaoDichTaiChinhKhongThanhCong,"GD Tài Chính Không Thành Công" },
            {(int)TemplateHelper.TEMPLATE.BaoCaoGiaoDichTaiChinhBatThuong,"GD Tài Chính Bất Thường" },
            {(int)TemplateHelper.TEMPLATE.BaoCaoHoatDongBatThuong,"BC HD Bất Thường" },
            {(int)TemplateHelper.TEMPLATE.BaoCaoHoatDongBatThuongTheoChuKy,"BC HD Bất Thường Theo Chu Kỳ" },
        };
        string[] TypeStart = { "DAILY", "ONCE", "WEEKLY", "MONTHLY" };
        string[] Months = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC", "*" };
        string[] Days = { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };

        string TaskName = "AutoRunTransaction_";
        UC_Explorer uc_Explorer;
        SQLiteHelper sqlite;
        string forms;
        Process process;
        public UC_Menu_Startup()
        {
            InitializeComponent2();
            uc_Explorer = new UC_Explorer();
            sqlite = new SQLiteHelper();
            process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "SCHTASKS";
            LoadTemplate();
            LoadTask();
        }
        public void LoadTemplate()
        {
            try
            {
                cbo_TypeStart.Items.AddRange(TypeStart);
                cbo_Week.Items.AddRange(Days);
                cbo_Month.Items.AddRange(Months);
                DataTable cfg_vendor = InitParametar.sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "60", "Parent_ID", "54");
                foreach (DataRow R in cfg_vendor.Rows)
                {
                    ComboBoxItem cb = new ComboBoxItem();
                    cb.Text = R["Field"].ToString();
                    cb.Value = R["ID"].ToString();
                    cbo_LstTemplate.Items.Add(cb);
                    if (cb.Value.Equals(InitParametar.ReadTrans.TemplateTransactionID.ToString())) cbo_LstTemplate.SelectedItem = cb;
                }
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = UC_Menu_Startup.Template;
                ckbl_Forms.DataSource = bindingSource;
                ckbl_Forms.DisplayMember = "Value";
                Template.ToList().ForEach(x => { ckbl_Forms.SetItemChecked(x.Key, true); });
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
        }
        private void LoadTask()
        {
            DataTable cfg_data = sqlite.GetTableDataWithColumnName("CfgData", "Type_ID", "511");
            int n = 0;
            dataGridView_lsPermissions.Rows.Clear();
            foreach (DataRow rowData in cfg_data.Rows)
            {
                DataGridViewRow row = new DataGridViewRow();
                string[] description = rowData["Data"].ToString().Split('|');
                row.CreateCells(dataGridView_lsPermissions);
                row.Cells[0].Value = n; n++;
                row.Cells[1].Value = rowData["Field"];
                row.Cells[2].Value = CheckTaskExist(rowData["Field"].ToString());
                row.Cells[3].Value = description[0].Split(';')[0];
                row.Cells[5].Value = string.Format("Template: {0}, source: {1}, destination: {2}, forms: {3} ", description[1], description[2], description[3], description[4]);
                row.Tag = rowData;
                dataGridView_lsPermissions.Rows.Add(row);
            }
        }
        private void txt_MouseDown(object sender, MouseEventArgs e)
        {
            uc_Explorer.ShowFromControl(this, sender as Control);
        }
        private void txt_HH_Validating(object sender, CancelEventArgs e)
        {
            TextBox box = sender as TextBox;
            string pattern = "[0-1][0-9]:[0-6][0-9]";

            if (box != null)
            {
                if (!Regex.IsMatch(box.Text, pattern, RegexOptions.CultureInvariant))
                {
                    MessageBox.Show("Not a valid time format ('HH:mm').");
                    e.Cancel = true;
                    box.Select(0, box.Text.Length);
                }
            }
        }
        private bool CheckTaskExist(string taskName)
        {
            string fileExe = Process.GetCurrentProcess().MainModule.FileName + " Auto";
            process.StartInfo.Arguments = string.Format("/query /TN \"{0}\"", TaskName + taskName);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //* Read the output (or the error)
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();

            if (output.Contains(taskName) || err.Contains(taskName)) return true;
            return false;
        }
        private bool CreateTask(string taskName)
        {
            try
            {
                string fileExe = Process.GetCurrentProcess().MainModule.FileName + " \"" + taskName + "\"";
                if (cbo_TypeStart.SelectedItem.Equals(TypeStart[0]) || cbo_TypeStart.SelectedItem.Equals(TypeStart[1]))
                    process.StartInfo.Arguments = string.Format("/Create /RU SYSTEM /SC {0} /TN \"{1}\" /TR \"{2}\" /ST {3} /F", cbo_TypeStart.Text, TaskName + taskName, fileExe, txt_HH.Text);
                else if (cbo_TypeStart.SelectedItem.Equals(TypeStart[2]))
                    process.StartInfo.Arguments = string.Format("/Create /RU SYSTEM /SC {0} /TN \"{1}\" /TR \"{2}\" /ST {3} /D \"{4}\" /F", cbo_TypeStart.Text, TaskName + taskName, fileExe, txt_HH.Text, cbo_Week.Text.Replace(",", " "));
                else
                    process.StartInfo.Arguments = string.Format("/Create /RU SYSTEM /SC {0} /TN \"{1}\" /TR \"{2}\" /ST {3} /M \"{4}\" /D {5} /F", cbo_TypeStart.Text, TaskName + taskName, fileExe, txt_HH.Text, cbo_Month.Text.Replace(",", " "), Nud_Day.Value);

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();
                //* Read the output (or the error)
                string output = process.StandardOutput.ReadToEnd();
                if (output.Contains("successfully been created")) return true;
                string err = process.StandardError.ReadToEnd();
                MessageBox.Show(output + Environment.NewLine + err, "Creates scheduled task", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }
        private bool RemoveTask(string taskName)
        {
            process.StartInfo.Arguments = string.Format("/Delete /TN \"{0}\" /F", TaskName + taskName);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //* Read the output (or the error)
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            if (output.Contains("ERROR:") || err.Contains("ERROR:"))
                return false;
            return true;
        }
        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (dataGridView_lsPermissions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please, select Task name to remove!", "Remove Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataRow row = dataGridView_lsPermissions.SelectedRows[0].Tag as DataRow;
            RemoveTask(row["Field"].ToString());
            if (sqlite.DeleteEntry("CfgData", "ID", row["ID"].ToString()))
            {
                MessageBox.Show("Remove task successful", "Remove Task", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTask();
                return;
            }
            MessageBox.Show("Remmove task unsuccessful", "Remove Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            EntryList entr = new EntryList();
            entr.ColumnName.Add("Field");
            entr.Content.Add(txt_TaskName.Text);
            if (string.IsNullOrEmpty(txt_TaskName.Text) || string.IsNullOrEmpty(txt_Source.Text) || string.IsNullOrEmpty(txt_Destination.Text) || string.IsNullOrEmpty(cbo_LstTemplate.Text))
            {
                MessageBox.Show("Fields not empty.", "Add data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sqlite.CheckExistEntry("CfgData", entr))
            {
                MessageBox.Show("Task name existed.", "Add data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CreateTask(txt_TaskName.Text))
            {
                string data = string.Format("{0};{1};{2};{3}", txt_HH.Text, cbo_TypeStart.Text, cbo_TypeStart.SelectedItem.Equals(TypeStart[2]) ? cbo_Week.Text : cbo_Month.Text, Nud_Day.Value);
                data += "|" + (cbo_LstTemplate.SelectedItem as ComboBoxItem).Value + "|" + txt_Source.Text + "|" + txt_Destination.Text + "|" + forms;
                //foreach (var item in ckbl_Forms.CheckedItems) data += item.ToString() + ";"; data = data.TrimEnd(';');

                Dictionary<int, string> TemplateChoosen = new Dictionary<int, string>();
                foreach (var item in ckbl_Forms.CheckedItems)
                {
                    var row = (KeyValuePair<int, string>)(item);
                    TemplateChoosen.Add(row.Key, row.Value);
                }
                data += string.Join(";", TemplateChoosen);
                entr.ColumnName.Add("Type_ID");
                entr.Content.Add("511");
                entr.ColumnName.Add("Data");
                entr.Content.Add(data);
                if (sqlite.CreateEntry("CfgData", entr))
                {
                    MessageBox.Show("Add Task successful", "Add Task", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTask();
                    return;
                }
            }
            MessageBox.Show("Add Task unsuccessful", "Add Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataRow row = dataGridView_lsPermissions.SelectedRows[0].Tag as DataRow;
            string data = txt_HH.Text + "|" + (cbo_LstTemplate.SelectedItem as ComboBoxItem).Value + "|" + txt_Source.Text + "|" + txt_Destination.Text + "|" + forms;
            //foreach (var item in ckbl_Forms.CheckedItems) data += item.ToString() + ";"; data = data.TrimEnd(';');

            Dictionary<int, string> TemplateChoosen = new Dictionary<int, string>();
            foreach (var item in ckbl_Forms.CheckedItems)
            {
                var rows = (KeyValuePair<int, string>)(item);
                TemplateChoosen.Add(rows.Key, rows.Value);
            }
            data += string.Join(";", TemplateChoosen);
            if (sqlite.Update1Entry("CfgData", "Data", data, "ID", row["ID"].ToString()))
            {
                MessageBox.Show("Save Task  successful", "Save Task", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTask();
                return;
            }
            MessageBox.Show("Save task unsuccessful", "Save Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void txt_TaskName_TextChanged(object sender, EventArgs e)
        {
            btn_Save.Enabled = false;
            btn_Add.Enabled = true;
            btn_Remove.Enabled = false;
        }

        private void dataGridView_lsPermissions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView_lsPermissions.SelectedRows.Count == 0) return;
                txt_TaskName.Text = (dataGridView_lsPermissions.SelectedRows[0].Tag as DataRow)["Field"].ToString();
                string[] field = (dataGridView_lsPermissions.SelectedRows[0].Tag as DataRow)["Data"].ToString().Split('|');
                if (field.Length != 0)
                {
                    txt_HH.Text = field[0].Split(';')[0];
                    cbo_TypeStart.SelectedItem = field[0].Split(';')[1];
                    if (cbo_TypeStart.SelectedItem.Equals(TypeStart[2])) cbo_Week.Text = field[0].Split(';')[2];
                    else if (cbo_TypeStart.SelectedItem.Equals(TypeStart[3])) { cbo_Month.Text = field[0].Split(';')[2]; Nud_Day.Value = (decimal)int.Parse(field[0].Split(';')[3]); }

                    foreach (ComboBoxItem cb in cbo_LstTemplate.Items) if (cb.Text.Equals(field[1])) cbo_LstTemplate.SelectedItem = cb;
                    txt_Source.Text = field[2];
                    txt_Destination.Text = field[3];
                    forms = field[3];
                }
                btn_Add.Enabled = false;
                btn_Save.Enabled = true;
                btn_Remove.Enabled = true;
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name); ;
            }
        }

        private void dataGridView_lsPermissions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_lsPermissions.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            {
                if ((bool)dataGridView_lsPermissions.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue)
                    CreateTask(dataGridView_lsPermissions.Rows[e.RowIndex].Cells[1].Value.ToString());
                else
                    RemoveTask(dataGridView_lsPermissions.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            else if (dataGridView_lsPermissions.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell)
            {
                if ((bool)dataGridView_lsPermissions.Rows[e.RowIndex].Cells[2].EditedFormattedValue)
                {
                    //run
                    process.StartInfo.Arguments = string.Format(" /Run /TN \"{0}\"", TaskName + dataGridView_lsPermissions.Rows[e.RowIndex].Cells[1].Value);
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string err = process.StandardError.ReadToEnd();
                    MessageBox.Show(output + Environment.NewLine + err, "Run Task");
                }
                else
                    MessageBox.Show("Task not Active", "Run Task");
            }
        }

        private void UC_Menu_Startup_Load(object sender, EventArgs e)
        {

        }

        private void cbo_TypeStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_TypeStart.Text.Equals(TypeStart[0]) || cbo_TypeStart.Text.Equals(TypeStart[1]))
            {
                pnl_Week.Visible = false;
                pnl_Month.Visible = false;
            }
            else if (cbo_TypeStart.Text.Equals(TypeStart[2]))
            {
                pnl_Week.Visible = true;
                pnl_Month.Visible = false;
            }
            else
            {
                pnl_Week.Visible = false;
                pnl_Month.Visible = true;
            }
        }
    }
}
