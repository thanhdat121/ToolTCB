﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using OfficeOpenXml;
using Transaction_Statistical.Class;

namespace Transaction_Statistical
{
    public class InitParametar
    {
        public static string SAuthor = @"Copyright © 2019, Công ty TNHH Giải pháp và Dịch vụ Nam Phương.";
        public static string STitle = @"Transaction Statistical";
        public static string SComment = @"Hotline: 1900 633 412 \nEmail: np.support @npss.vn\nWeb: http://npss.vn";

        public static string sTest = string.Empty;
        public static SQLiteHelper sqlite;
        public static string PathDirectoryCurrentApp;//=Path.GetDirectoryName(Application.ExecutablePath);
        public static string PathDirectoryCurrentAppConfigData;//=Path.GetDirectoryName(Application.ExecutablePath);
        public static string PathDirectoryUtilities;// = PathDirectoryCurrentApp + "Utilities";

        public static string FolderSystemTrace;
        public static string DatabaseFile;


        //form enu
        public static bool ActiveFormMain;
        public static bool ExitApp = false;
        public static string UsrSupport;
        public static string PwdSupport;
        public static string IpSupport;
        public static string PathUpdateSupport;
        public static string PathUpdateSupportError;


        /// Transaction Template
        public static ReadTransaction ReadTrans;


        //log application
        public static bool WriteApplication = true;
        public static bool AutoRunMode;
        public static void Init()
        {
            try
            {

                // ftp Support
                UsrSupport = "UsrUpdate";
                PwdSupport = "@UsrUpdate@";
                IpSupport = "ftp://bsi.vn";
                PathUpdateSupport = "/UsrUpdate/Analyze";
                PathUpdateSupportError = "/UsrUpdate/Analyze/Error";
                //version

                //VersionAnalyze.Date = DateTime.ParseExact("2013-04-07 16:40:52", "yyyy-MM-dd HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);

                //Init directory and file config
                PathDirectoryCurrentApp = Path.GetDirectoryName(Application.ExecutablePath);
                PathDirectoryUtilities = (PathDirectoryCurrentApp + "\\Utilities").Replace(@"\\", @"\");
                FolderSystemTrace = (PathDirectoryCurrentApp + "\\Trace").Replace(@"\\", @"\");
                PathDirectoryCurrentAppConfigData = PathDirectoryCurrentApp + @"\Config";
                DatabaseFile = PathDirectoryCurrentAppConfigData + "\\DB.s3db";
                sqlite = new SQLiteHelper();
                //   listTransaction = new Dictionary<string, Dictionary<DateTime, Transaction>>();
                ReadTrans = new ReadTransaction();

            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name); ;
            }
        }
        public async static Task<bool> AutoStart(string taskName)
        {
            try
            {
                AutoRunMode = true;
                WriteLogApplication(string.Format("{0:HH:mm:ss fff} Class: Auto Task", DateTime.Now), true, false);
                WriteLogApplication("   => Task name: " + taskName, false, false);

                DataRow rowTask = sqlite.GetRowDataWith2ColumnName("CfgData", "Field", taskName, "Type_ID", "511");
                string[] data = rowTask["Data"].ToString().Split('|');
                WriteLogApplication("   => Task data: " + rowTask["Data"].ToString(), false, false);
                ReadTrans.TemplateTransactionID = data[1];
                WriteLogApplication("   => Template id: " + data[1], false, false);
                if (!ReadTrans.LoadTemplateInfo())
                {
                    WriteLogApplication("   => Load Template Info => Unsuccessfully", false, false);
                    WriteLogApplication("   ==> Auto end, result => Unsuccessfully", false, true);
                    return false;
                }
                WriteLogApplication("   => Load Template Info => Successfully", false, false);

                WriteLogApplication("   => Data source: " + data[2], false, false);
                DirectoryFileUtilities df = new DirectoryFileUtilities();
                List<string> lsFile_Journal = new List<string>();
                if (File.Exists(data[2]))
                    lsFile_Journal.Add(data[2]);
                else if (Directory.Exists(data[2]))
                {
                    FileInfo[] files = df.GetAllFilePath(data[2], ReadTrans.ExtensionFile);
                    lsFile_Journal = files.Select(f => f.FullName).ToList();
                }
                if (lsFile_Journal.Count == 0)
                {
                    WriteLogApplication("   => Found: 0 files", false, false);
                    WriteLogApplication("   ==> Auto end, result => Unsuccessfully", false, true);
                    return false;

                }
                WriteLogApplication(string.Format("   => Found: {0} files", lsFile_Journal.Count), false, false);

                WriteLogApplication(string.Format("   => Read begin: {0:HH:mm:ss fff} ", DateTime.Now), false, false);
                var watch = System.Diagnostics.Stopwatch.StartNew(); watch.Start();
                ReadTrans = new ReadTransaction();
                if (!await ReadTrans.Reads(lsFile_Journal))
                {
                    WriteLogApplication("   ==> Auto end, result => Unsuccessfully", false, true); return false;
                    ;
                }
                watch.Stop();
                int count = ReadTrans.ListTransaction.Values.LastOrDefault().Keys.Count;
                WriteLogApplication(string.Format("   => Read time: {0} s\n   =>Transactions: {1}\n   =>Files: {2}", watch.ElapsedMilliseconds / 1000, count, lsFile_Journal.Count), false, false);
                Dictionary<int, string> TemplateChoosen = data[4].Replace("[", "").Replace("]", "").Split(';').ToDictionary(x => int.Parse(x.Split(',')[0]), x => x.Split(',')[1]);

                watch.Start();
                WriteLogApplication(string.Format("   => Export begin: {0:HH:mm:ss fff} ", DateTime.Now), false, false);
                WriteLogApplication("   => Export destination: " + data[3], false, false);
                WriteLogApplication("   => Export forms: " + TemplateChoosen.Values.ToString(), false, false);
                if (TemplateChoosen.Count == 0)
                {
                    WriteLogApplication("   ==> Auto end, result => Unsuccessfully", false, true); return false;
                }
                ReadTrans.Export(data[3], TemplateChoosen);
                watch.Stop();
                WriteLogApplication(string.Format("   => Export time:{0} s", watch.ElapsedMilliseconds / 1000), false, false);
                WriteLogApplication(string.Format("   ==> File: {0}, size {1} kb", ReadTrans.FileExport, new FileInfo(ReadTrans.FileExport).Length / 1024), false, false);
                WriteLogApplication("   ==> Auto end, result => Successfully", false, true);
                return false;

            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name); ;
                WriteLogApplication(ex.Message, false, true);
            }
            WriteLogApplication("   ==> Auto end, result => Unsuccessfully", false, true);
            return true;

        }


        public static void Send_Error(string MsgError, string ClassName, string MethodName)
        {
            try
            {
                if (WriteApplication)
                {
                    WriteLogApplication(string.Format("{0:HH:mm:ss fff} Class: {1}\nMethod: {2}\n{3}", DateTime.Now, ClassName, MethodName, MsgError), true, true);
                }
                if (AutoRunMode) return;
                UC_Info msg = new UC_Info();
                File.AppendAllText(@"D:\expert.log", MsgError);
                msg.TextCustom.ReadOnly = false;
                msg.TextCustom.Text = "Host name: " + Environment.MachineName;
                msg.TextCustom.AppendText(Environment.NewLine + "Class: " + ClassName);
                msg.TextCustom.AppendText(Environment.NewLine + "Method: " + MethodName);
                msg.TextCustom.AppendText(Environment.NewLine + "Date: " + String.Format("{0:yyyy/MM/dd-HH:mm:ss ffff}", DateTime.Now));
                //  msg.TextCustom.Select( );
                msg.TextCustom.Font = new System.Drawing.Font("Times New Roman", 13, FontStyle.Bold);
                msg.TextCustom.SelectionColor = Color.Green;

                Frm_TemplateDefault frm = new Frm_TemplateDefault(msg);
                frm.titleCustom.Text = "Error Message";
                frm.Height = 300;
                frm.Show();

                msg.TextCustom.AppendText(Environment.NewLine + "Error Message:");
                //  int indexline = msg.Messager.TextLength;
                //   msg.TextCustom.Select(indexline, (Environment.NewLine + "Error Message:").Length);
                msg.TextCustom.Font = new System.Drawing.Font("Times New Roman", 12, FontStyle.Italic);
                msg.TextCustom.SelectionColor = Color.Black;

                msg.TextCustom.AppendText(Environment.NewLine + MsgError);
                msg.TextCustom.Update();
                msg.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }

        }
        public static void WriteLogApplication(string msg, bool lineBegin, bool lineEnd)
        {
            try
            {
                if (!Directory.Exists(FolderSystemTrace)) Directory.CreateDirectory(FolderSystemTrace);
                string FileCcprot = string.Format("{0}\\{1:yyyyMMdd}.log", FolderSystemTrace, DateTime.Now);
                while (true)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(FileCcprot, true))
                        {
                            if (lineBegin) sw.WriteLine("----------------------------------------------");
                            sw.WriteLine(msg);
                            if (lineEnd) sw.WriteLine("----------------------------------------------\n");
                            sw.Flush();
                            sw.Close();
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + FolderSystemTrace, "WriteLogApplication", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public class TransactionType
    {
        public string Name;
        public string Identification;
        public string Successful;
        public string UnSuccessful;
    }

    public class ReadTransaction
    {
        SQLiteHelper sqlite;
        /// Transaction
        public string FormatTime = "HH:mm:ss";
        public string FormatDate = "yyyy-MM-dd";
        public string FormatDateTime = "MM - dd - yyyy HH:mm:ss";
        public string FormatDateTime_2 = "MM-dd-yyyy HH:mm:ss";

        public string TemplateTransactionID;
        public Dictionary<TransactionEvent.Events, string> transactionTemplate;
        public string[] ExtensionFile = { "*.txt", "*.log" };

        public Dictionary<string, Dictionary<DateTime, object>> ListTransaction;
        public DateTime StartDate = DateTime.MinValue;
        public DateTime EndDate = DateTime.MaxValue;
        public Dictionary<string, string> Template_EventTransaction;
        public Dictionary<string, string> Template_EventBeginInput;
        public Dictionary<string, string> Template_EventDevice;
        public Dictionary<string, string> Template_EventDevice_Select;
        public Dictionary<string, string> Template_EventRequest;
        public Dictionary<string, string> Template_EventReceive;
        public Dictionary<string, string> Template_SplitTransactions;
        public Dictionary<string, string> Template_EventCounterChanged;
        public Dictionary<string, string> Template_EventCashOutIn;
        public Dictionary<string, TransactionType> Template_TransType;
        public Dictionary<string, TransactionType> Template_TransType_Select;

        public string FileExport;

        //DAT : 6/12/2019
        public Dictionary<DateTime, Cycle> ListCycle = null;
        public Dictionary<DateTime, TransactionEvent> ListEvent = null;
        public Dictionary<DateTime, TransactionRequest> ListRequest = null;
        public ReadTransaction()
        {
            sqlite = new SQLiteHelper();
            LoadTemplateInfo();
        }
        public bool LoadTemplateInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(TemplateTransactionID)) TemplateTransactionID = LoadTemplateID();

                Template_EventDevice_Select = new Dictionary<string, string>();
                if (Template_EventTransaction == null) Template_EventTransaction = new Dictionary<string, string>();
                if (Template_EventBeginInput == null) Template_EventBeginInput = new Dictionary<string, string>();
                if (Template_EventDevice == null) Template_EventDevice = new Dictionary<string, string>();
                if (Template_EventRequest == null) Template_EventRequest = new Dictionary<string, string>();
                if (Template_EventReceive == null) Template_EventReceive = new Dictionary<string, string>();
                if (Template_EventCashOutIn == null) Template_EventCashOutIn = new Dictionary<string, string>();
                if (Template_SplitTransactions == null) Template_SplitTransactions = new Dictionary<string, string>();
                if (Template_EventCounterChanged == null) Template_EventCounterChanged = new Dictionary<string, string>();

                if (Template_TransType == null) Template_TransType = new Dictionary<string, TransactionType>(); else Template_TransType.Clear();

                DataTable cfg_data = sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "456", "Parent_ID", TemplateTransactionID);
                foreach (DataRow r in cfg_data.Rows)
                    Template_EventTransaction[r["Field"].ToString()] = r["Data"].ToString();

                cfg_data = sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "479", "Parent_ID", TemplateTransactionID);
                foreach (DataRow r in cfg_data.Rows)
                    Template_EventBeginInput[r["Field"].ToString()] = r["Data"].ToString();

                cfg_data = sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "457", "Parent_ID", TemplateTransactionID);
                foreach (DataRow r in cfg_data.Rows)
                {
                    Template_EventDevice[r["Field"].ToString()] = r["Data"].ToString();
                    Template_EventDevice_Select[r["Field"].ToString()] = r["Data"].ToString();
                }
                cfg_data = sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "471", "Parent_ID", TemplateTransactionID);
                foreach (DataRow r in cfg_data.Rows)
                    Template_EventRequest[r["Field"].ToString()] = r["Data"].ToString();

                cfg_data = sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "472", "Parent_ID", TemplateTransactionID);
                foreach (DataRow r in cfg_data.Rows)
                    Template_EventReceive[r["Field"].ToString()] = r["Data"].ToString();

                cfg_data = sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "458", "Parent_ID", TemplateTransactionID);
                foreach (DataRow r in cfg_data.Rows)
                    Template_SplitTransactions[r["Field"].ToString()] = r["Data"].ToString();

                cfg_data = sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "525", "Parent_ID", TemplateTransactionID);
                foreach (DataRow r in cfg_data.Rows)
                    Template_EventCashOutIn[r["Field"].ToString()] = r["Data"].ToString();

                cfg_data = sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "459", "Parent_ID", TemplateTransactionID);
                foreach (DataRow r in cfg_data.Rows)
                    Template_EventCounterChanged[r["Field"].ToString()] = r["Data"].ToString();


                Template_TransType = new Dictionary<string, TransactionType>();
                Template_TransType_Select = new Dictionary<string, TransactionType>();
                DataTable tb_transtype = sqlite.GetTableDataWithColumnName("Transactions", "TemplateID", TemplateTransactionID);
                foreach (DataRow r in tb_transtype.Rows)
                {
                    TransactionType type = new TransactionType();
                    type.Name = r["Name"].ToString();
                    type.Identification = r["IdentificationTxt"].ToString();
                    type.Successful = r["SuccessfulTxt"].ToString();
                    type.UnSuccessful = r["UnsuccessfulTxt"].ToString();
                    Template_TransType[type.Name] = type;
                    Template_TransType_Select[type.Name] = type;
                }
                return true;
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }
        public string LoadTemplateID()
        {
            DataTable cfg_vendor = InitParametar.sqlite.GetTableDataWith2ColumnName("CfgData", "Type_ID", "60", "Parent_ID", "54");
            foreach (DataRow R in cfg_vendor.Rows)
                if (R["Field"].ToString().EndsWith(@"(default)")) return R["ID"].ToString();
            return string.Empty;
        }
        public async Task<bool> Reads(List<string> files, TextProgressBar process = null)
        {

            try
            {
                DateTime dateBegin = DateTime.MinValue;
                DateTime dateEnd = DateTime.Now;
                DateTime currentDate = DateTime.MinValue;

                ListTransaction = new Dictionary<string, Dictionary<DateTime, object>>();

                if (process != null)
                {
                    process.CustomText = string.Format("Found: {0} files.", files.Count);
                    process.Step = (process.Maximum - process.Value) / files.Count;
                }

                ListCycle = new Dictionary<DateTime, Cycle>();
                foreach (string file in files)
                {
                    if (process != null)
                    {
                        process.CustomText = string.Format("Reading.. [{0}]", Path.GetFileName(file));
                        process.PerformStep();
                    }
                    ListRequest = new Dictionary<DateTime, TransactionRequest>();
                    ListEvent = new Dictionary<DateTime, TransactionEvent>();
                    string day = file.Substring(file.Length - 12, 8);
                    string Terminal = Path.GetFileName(file).Substring(0, 8);
                    string contenFile = File.ReadAllText(file);
                    DateTime.TryParseExact(day, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out currentDate);

                    contenFile = await SplitTransactionEJ(Terminal, contenFile);
                    contenFile = await FindEventDevice2Async(currentDate, Terminal, contenFile);
                    FindCounterChanged(ref contenFile, ref ListCycle);

                }
                //CHANGE 6/12
                var ListTransactionTemp = ListTransaction;
                for (int c = 0; c < ListTransactionTemp.Count; c++)
                {
                    var item = ListTransactionTemp.ToArray()[c];
                    var itemValue = item.Value.ToList(); ;
                    var cycles = ListCycle.Where(x => x.Value.TerminalID.Contains(item.Key)).ToList();
                    cycles.ForEach(x =>
                    {
                        ListTransaction.FirstOrDefault(x1 => x1.Key == item.Key).Value.Add(x.Key, x.Value);
                    });

                }
                return true;
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }

        public bool Export(string exportDestination, Dictionary<int, string> templateChoosen)
        {
            try
            {
                FileExport = exportDestination;
                if (Directory.Exists(exportDestination))
                    FileExport = exportDestination + string.Format("\\TransactionStatistical_{0:yyyyMMdd_HH-mm}.xlsx", DateTime.Now);
                if (ListTransaction != null)
                {

                    var cycle = new Dictionary<DateTime, Cycle>();
                    ListTransaction.Select(x => x.Value).ToList().ForEach(x =>
                    {
                        cycle = cycle.Concat(x.Where(i => i.Value is Cycle).ToDictionary(d => d.Key, d => (Cycle)d.Value))
                       .GroupBy(i => i.Key).ToDictionary(
                           group => group.Key,
                           group => group.First().Value).ToDictionary(d => d.Key, d => (Cycle)d.Value);
                    });
                    var transaction = new Dictionary<DateTime, Transaction>();
                    ListTransaction.Select(x => x.Value).ToList().ForEach(x =>
                    {
                        transaction = transaction.Concat(x.Where(i => i.Value is Transaction).ToDictionary(d => d.Key, d => (Transaction)d.Value))
                       .GroupBy(i => i.Key).ToDictionary(
                           group => group.Key,
                           group => group.First().Value).ToDictionary(d => d.Key, d => (Transaction)d.Value);
                    });
                    var transactionEvent = ListTransaction.ToDictionary(d => d.Key, d => d.Value.Where(x => x.Value is TransactionEvent).ToDictionary(k => k.Key, k => (TransactionEvent)k.Value));


                    Stream stream = null;
                    using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
                    {
                        TemplateHelper template = new TemplateHelper(InitParametar.SAuthor, InitParametar.STitle, InitParametar.SComment, excelPackage);
                        // Tạo buffer memory stream để hứng file excel
                        foreach (var item in templateChoosen)
                        {
                            switch (item.Key)
                            {
                                case (int)TemplateHelper.TEMPLATE.CanQuyTheoCouterTrenMay:
                                    template.CanQuyTheoCouterTrenMay(item.Value, OfficeOpenXml.Table.TableStyles.Custom, cycle);
                                    break;
                                case (int)TemplateHelper.TEMPLATE.BaoCaoGiaoDichTaiChinh:
                                    template.BaoCaoGiaoDichTaiChinh(item.Value, OfficeOpenXml.Table.TableStyles.Custom, transaction, cycle);
                                    break;
                                case (int)TemplateHelper.TEMPLATE.BaoCaoGiaoDichTaiChinhKhongThanhCong:
                                    template.BaoCaoGiaoDichTaiChinh(item.Value, OfficeOpenXml.Table.TableStyles.Custom, transaction, cycle);
                                    break;
                                case (int)TemplateHelper.TEMPLATE.BaoCaoGiaoDichTaiChinhBatThuong:
                                    template.BaoCaoGiaoDichTaiChinh(item.Value, OfficeOpenXml.Table.TableStyles.Custom, transaction, cycle);
                                    break;
                                case (int)TemplateHelper.TEMPLATE.BaoCaoHoatDongBatThuong:
                                    template.BaoCaoHoatDongBatThuong(item.Value, OfficeOpenXml.Table.TableStyles.Custom, transactionEvent);
                                    break;
                            }
                        }
                        stream = template.getStream();
                        var buffer = stream as MemoryStream;
                        File.WriteAllBytes(FileExport, buffer.ToArray());
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return false;
        }


        private async Task<string> SplitTransactionEJ(string TerminalID, string sString)
        {
            try
            {
                Dictionary<int, RegesValue> lst = new Dictionary<int, RegesValue>();
                foreach (string reg in Template_SplitTransactions.Values)
                {
                    if (Regexs.RunPatternRegular(sString, reg, out lst))
                    {
                        // Transaction trans;
                        List<Task> tasks = new List<Task>();
                        foreach (KeyValuePair<int, RegesValue> key in lst)
                        {
                            //await Task.Run(() => SplitTransactionEJ_Info(key.Value));
                            tasks.Add(SplitTransactionEJ_InfoAsync(key.Value));
                            sString = sString.Replace(key.Value.stringfind, string.Empty);
                        }
                        await Task.WhenAll(tasks);
                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return sString;
        }
        #region Dat
        private async Task<Transaction> FindEventTransaction(Transaction transaction, DateTime DateCurrent)
        {
            try
            {
                Dictionary<int, RegesValue> lst = new Dictionary<int, RegesValue>();
                foreach (KeyValuePair<string, string> tmp in Template_EventTransaction)
                {
                    if (Regexs.RunPatternRegular(transaction.TraceJournal_Remaining, tmp.Value, out lst))
                    {
                        foreach (RegesValue regx in lst.Values)
                        {
                            await Task.Run(() =>
                            {
                                TransactionEvent evt = new TransactionEvent();
                                evt.Name = tmp.Key;
                                evt.Status = Status.Types.Succeeded;
                                evt.TContent = regx.stringfind;
                                evt.Type = TransactionEvent.Events.Transaction;
                                if (regx.value.ContainsKey("Time") && !string.IsNullOrEmpty(regx.value["Time"]))
                                {
                                    DateTime.TryParseExact(String.Format("{0:yyyyMMdd}", DateCurrent) + regx.value["Time"], "yyyyMMdd" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out evt.DateBegin);
                                    DateCurrent = evt.DateBegin;
                                }
                                else
                                    evt.DateBegin = DateCurrent.AddMilliseconds(10);
                                if (transaction.ListEvent.ContainsKey(evt.DateBegin)) transaction.ListEvent[evt.DateBegin.AddMilliseconds(1)] = evt;
                                else transaction.ListEvent[evt.DateBegin] = evt;
                                transaction.TraceJournal_Remaining = transaction.TraceJournal_Remaining.Replace(regx.stringfind, string.Empty);
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return transaction;
        }
        private async Task<Transaction> FindEventDevice(Transaction transaction, DateTime DateCurrent)
        {
            try
            {
                Dictionary<int, RegesValue> lst = new Dictionary<int, RegesValue>();
                foreach (KeyValuePair<string, string> tmp in Template_EventDevice_Select)
                {
                    if (Regexs.RunPatternRegular(transaction.TraceJournal_Remaining, tmp.Value, out lst))
                    {
                        foreach (RegesValue regx in lst.Values)
                        {
                            await Task.Run(() =>
                            {
                                TransactionEvent evt = new TransactionEvent();
                                evt.Name = tmp.Key;
                                evt.Status = Status.Types.Succeeded;
                                evt.TContent = regx.stringfind;
                                if (regx.value.ContainsKey("Time") && !string.IsNullOrEmpty(regx.value["Time"]))
                                {
                                    DateTime.TryParseExact(string.Format("{0:yyyyMMdd}", DateCurrent) + regx.value["Time"], "yyyyMMdd" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out evt.DateBegin);
                                    DateCurrent = evt.DateBegin;
                                }
                                else
                                    evt.DateBegin = DateCurrent;
                                if (transaction.ListEvent.ContainsKey(evt.DateBegin)) transaction.ListEvent[evt.DateBegin.AddMilliseconds(1)] = evt;
                                else transaction.ListEvent[evt.DateBegin] = evt;
                                transaction.TraceJournal_Remaining = transaction.TraceJournal_Remaining.Replace(regx.stringfind, string.Empty);
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }

            return transaction;
        }
        private async Task<Transaction> FindEventBeginInput(Transaction transaction)
        {
            try
            {
                Dictionary<int, RegesValue> lst = new Dictionary<int, RegesValue>();
                TransactionEvent evt;
                DateTime DateCurrent = transaction.DateBegin;
                foreach (KeyValuePair<string, string> tmp in Template_EventBeginInput)
                {
                    if (Regexs.RunPatternRegular(transaction.TraceJournal_Remaining, tmp.Value, out lst))
                    {
                        foreach (RegesValue regx in lst.Values)
                        {
                            await Task.Run(() =>
                            {
                                evt = new TransactionEvent();
                                evt.Name = tmp.Key;
                                evt.Status = Status.Types.Succeeded;
                                evt.TContent = regx.stringfind;
                                if (regx.value.ContainsKey("Time") && !string.IsNullOrEmpty(regx.value["Time"]))
                                {
                                    DateTime.TryParseExact(String.Format("{0:yyyyMMdd}", DateCurrent) + regx.value["Time"], "yyyyMMdd" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out evt.DateBegin);
                                    DateCurrent = evt.DateBegin;
                                }
                                else
                                    evt.DateBegin = DateCurrent.AddMilliseconds(10);
                                if (regx.value["Data"].StartsWith("("))
                                    transaction.DataInput.Add(regx.value["Data"]);
                                else
                                {
                                    transaction.CardType = Transaction.CardTypes.CardNumber;
                                    transaction.CardNumber = regx.value["Data"];
                                }
                                if (transaction.ListEvent.ContainsKey(evt.DateBegin)) transaction.ListEvent[evt.DateBegin.AddMilliseconds(1)] = evt;
                                else transaction.ListEvent[evt.DateBegin] = evt;
                                transaction.TraceJournal_Remaining = transaction.TraceJournal_Remaining.Replace(regx.stringfind, string.Empty);
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return transaction;

        }
        private async Task<Transaction> FindEventReceive(DateTime DateCurrent, Transaction tran)
        {
            try
            {
                Dictionary<int, RegesValue> lst = new Dictionary<int, RegesValue>();
                foreach (KeyValuePair<string, string> tmp in Template_EventReceive)
                {
                    if (Regexs.RunPatternRegular(tran.TraceJournal_Remaining, tmp.Value, out lst))
                    {
                        foreach (RegesValue regx in lst.Values)
                        {
                            await Task.Run(() =>
                            {
                                TransactionEvent evt = new TransactionEvent();
                                evt.Name = tmp.Key;
                                evt.Status = Status.Types.Succeeded;
                                evt.TContent = regx.stringfind;
                                if (regx.value.ContainsKey("Time") && !string.IsNullOrEmpty(regx.value["Time"]))
                                {
                                    DateTime.TryParseExact(String.Format("{0:yyyyMMdd}", DateCurrent) + regx.value["Time"], "yyyyMMdd" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out evt.DateBegin);
                                    DateCurrent = evt.DateBegin;
                                }
                                else
                                    evt.DateBegin = DateCurrent.AddMilliseconds(10);
                                if (tran.ListEvent.ContainsKey(evt.DateBegin)) tran.ListEvent[evt.DateBegin.AddMilliseconds(1)] = evt;
                                else tran.ListEvent[evt.DateBegin] = evt;
                                tran.TraceJournal_Remaining = tran.TraceJournal_Remaining.Replace(regx.stringfind, string.Empty);
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return tran;
        }
        private async Task<Transaction> FindEventCashOutIn(DateTime DateCurrent, Transaction tran)
        {
            try
            {
                Dictionary<int, RegesValue> lst = new Dictionary<int, RegesValue>();

                foreach (KeyValuePair<string, string> tmp in Template_EventCashOutIn)
                {
                    if (Regexs.RunPatternRegular(tran.TraceJournal_Remaining, tmp.Value, out lst))
                    {
                        foreach (RegesValue regx in lst.Values)
                        {
                            await Task.Run(() =>
                            {
                                TransactionEvent evt = new TransactionEvent();
                                evt.Name = tmp.Key;
                                evt.Status = Status.Types.Succeeded;
                                evt.TContent = regx.stringfind;
                                if (regx.value.ContainsKey("Time") && !string.IsNullOrEmpty(regx.value["Time"]))
                                {
                                    DateTime.TryParseExact(string.Format("{0:yyyyMMdd}", DateCurrent) + regx.value["Time"], "yyyyMMdd" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out evt.DateBegin);
                                    DateCurrent = evt.DateBegin;
                                }
                                else
                                    evt.DateBegin = DateCurrent;
                                int node = 0;
                                if (regx.value.ContainsKey("TimeSeparation"))
                                {
                                    DateTime.TryParseExact(string.Format("{0:yyyyMMdd}", DateCurrent) + regx.value["TimeSeparation"], "yyyyMMdd" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out evt.DateBegin);
                                    evt.Type = TransactionEvent.Events.CashOut;
                                    if (regx.value.ContainsKey("Sep10k") && int.TryParse(regx.value["Sep10k"], out node)) tran.Value_10K -= node;
                                    if (regx.value.ContainsKey("Sep20k") && int.TryParse(regx.value["Sep20k"], out node)) tran.Value_20K -= node;
                                    if (regx.value.ContainsKey("Sep50k") && int.TryParse(regx.value["Sep50k"], out node)) tran.Value_50K -= node;
                                    if (regx.value.ContainsKey("Sep100k") && int.TryParse(regx.value["Sep100k"], out node)) tran.Value_100K -= node;
                                    if (int.TryParse(regx.value["Sep200k"], out node)) tran.Value_200K -= node;
                                    if (int.TryParse(regx.value["Sep500k"], out node)) tran.Value_500K -= node;
                                    if (int.TryParse(regx.value["Reject"], out node)) tran.Rejects += node;
                                }
                                if (regx.value.ContainsKey("TimeStored"))
                                {
                                    evt.Type = TransactionEvent.Events.CashIn;
                                    DateTime.TryParseExact(string.Format("{0:yyyyMMdd}", DateCurrent) + regx.value["TimeStored"], "yyyyMMdd" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out evt.DateBegin);
                                    if (regx.value.ContainsKey("Sto10k") && int.TryParse(regx.value["Sto10k"], out node)) tran.Value_10K += node;
                                    if (regx.value.ContainsKey("Sto20k") && int.TryParse(regx.value["Sto20k"], out node)) tran.Value_20K += node;
                                    if (regx.value.ContainsKey("Sto50k") && int.TryParse(regx.value["Sto50k"], out node)) tran.Value_50K += node;
                                    if (int.TryParse(regx.value["Sto100k"], out node)) tran.Value_100K += node;
                                    if (int.TryParse(regx.value["Sto200k"], out node)) tran.Value_200K += node;
                                    if (int.TryParse(regx.value["Sto500k"], out node)) tran.Value_500K += node;
                                    //if (int.TryParse(regx.value["StoReject"], out node)) tran.Node_Rejects += node;
                                }
                                if (tran.ListEvent.ContainsKey(evt.DateBegin)) tran.ListEvent[evt.DateBegin.AddMilliseconds(1)] = evt;
                                else tran.ListEvent[evt.DateBegin] = evt;
                                tran.TraceJournal_Remaining = tran.TraceJournal_Remaining.Replace(regx.stringfind, string.Empty);
                            });

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return tran;
        }
        private async Task<Transaction> FindEventRequest(DateTime DateCurrent, Transaction tran)
        {
            try
            {
                Dictionary<int, RegesValue> lst = new Dictionary<int, RegesValue>();


                foreach (KeyValuePair<string, string> tmp in Template_EventRequest)
                {
                    if (Regexs.RunPatternRegular(tran.TraceJournal_Remaining, tmp.Value, out lst))
                    {
                        foreach (RegesValue regx in lst.Values)
                        {
                            await Task.Run(() =>
                            {
                                TransactionEvent evt = new TransactionEvent();
                                evt.Name = tmp.Key;
                                evt.Status = Status.Types.Unknow;
                                evt.TContent = regx.stringfind;
                                evt.Type = TransactionEvent.Events.TransactionReqSend;
                                if (regx.value.ContainsKey("Data")) evt.Data = regx.value["Data"];
                                if (regx.value.ContainsKey("Time") && !string.IsNullOrEmpty(regx.value["Time"]))
                                {
                                    DateTime.TryParseExact(String.Format("{0:yyyyMMdd}", DateCurrent) + regx.value["Time"], "yyyyMMdd" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out evt.DateBegin);
                                    DateCurrent = evt.DateBegin;
                                }
                                else
                                    evt.DateBegin = DateCurrent.AddMinutes(10);
                                if (tran.ListEvent.ContainsKey(evt.DateBegin)) tran.ListEvent[evt.DateBegin.AddMilliseconds(1)] = evt;
                                else tran.ListEvent[evt.DateBegin] = evt;
                                tran.TraceJournal_Remaining = tran.TraceJournal_Remaining.Replace(regx.stringfind, string.Empty);
                            });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return tran;
        }
        private async Task SplitTransactionEJ_InfoAsync(RegesValue val)
        {
            Transaction trans = new Transaction();

            try
            {
                DateTime.TryParseExact(val.value["DateBegin"], "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out trans.DateBegin);
                DateTime.TryParseExact(string.Format("{0:MM-dd-yyyy}", trans.DateBegin) + val.value["TimeEnd"], "MM-dd-yyyy" + FormatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out trans.DateEnd);
                trans.DateEnd.AddDays(trans.DateBegin.Day);
                trans.DateEnd.AddMonths(trans.DateBegin.Month);
                trans.DateEnd.AddYears(trans.DateBegin.Year);

                trans.Terminal = val.value["TerminalID"];
                trans.MachineSequenceNo = val.value["MachineNo"];
                trans.TraceJournalFull = trans.TraceJournal_Remaining = val.stringfind;

                trans.TraceJournal_Remaining = trans.TraceJournal_Remaining.Replace(val.value["SStart"], null);
                trans.TraceJournal_Remaining = trans.TraceJournal_Remaining.Replace(val.value["SEnd"], null);

                trans = await Task.Run(() => FindEventBeginInput(trans));
                trans = await Task.Run(() => FindEventRequest(trans.DateBegin, trans));
                trans = await Task.Run(() => FindEventTransaction(trans, trans.DateBegin));

                trans = await Task.Run(() => FindEventReceive(trans.DateBegin, trans));
                trans = await Task.Run(() => FindEventDevice(trans, trans.DateBegin));
                trans = await Task.Run(() => FindEventCashOutIn(trans.DateBegin, trans));
                trans = await Task.Run(() => SplitRequest(trans));
                //  SplitRequest(ref trans);
                if (ListTransaction.ContainsKey(trans.Terminal))
                {
                    if (ListTransaction[trans.Terminal].ContainsKey(trans.DateBegin))
                        trans.DateBegin.AddMilliseconds(1);
                    ListTransaction[trans.Terminal][trans.DateBegin] = trans;
                }
                else
                    ListTransaction[trans.Terminal] = new Dictionary<DateTime, object>() { { trans.DateBegin, trans } };

            }
            catch (Exception ex)
            {
            }
            //return trans.TraceJournalFull;
        }
        #endregion



        private bool FindCounterChanged(ref string sString, ref Dictionary<DateTime, Cycle> Cycles)
        {
            try
            {
                Cycle cycleItem = null;
                Dictionary<int, RegesValueWithPatternOfGroup> lst = new Dictionary<int, RegesValueWithPatternOfGroup>();

                CycleEvent evt;
                foreach (KeyValuePair<string, string> reg in Template_EventCounterChanged)
                {
                    if (Regexs.RunPatternRegular(sString, reg.Value, out lst))
                    {
                        foreach (KeyValuePair<int, RegesValueWithPatternOfGroup> key in lst)
                        {
                            evt = new CycleEvent();
                            evt.Name = reg.Value;
                            evt.Log = key.Value.stringfind;

                            if (key.Value.value.ContainsKey("Settlement"))
                            {
                                cycleItem = new Cycle();
                                cycleItem.LogTxt = key.Value.stringfind;
                                cycleItem.IndexLog = key.Value.index;
                                var date = key.Value.value["StartDateTime"].FirstOrDefault().Value;
                                evt.TDate = date;
                                var settlementPeriodDateBegin = key.Value.value["Start"].FirstOrDefault().Value;
                                var settlementPeriodDateEnd = key.Value.value["End"].FirstOrDefault().Value;
                                var startSettlement = DateTime.ParseExact(date, FormatDateTime_2, CultureInfo.InvariantCulture);
                                var periodDateBegin = DateTime.ParseExact(settlementPeriodDateBegin, FormatDateTime_2, CultureInfo.InvariantCulture);
                                var periodDateEnd = DateTime.ParseExact(settlementPeriodDateEnd, FormatDateTime_2, CultureInfo.InvariantCulture);
                                cycleItem.SettlementPeriodDateBegin = periodDateBegin;
                                cycleItem.SettlementPeriodDateEnd = periodDateEnd;
                                cycleItem.DateBegin = startSettlement;
                                cycleItem.TerminalID = key.Value.value["TerminalNo"].FirstOrDefault().Value;
                                cycleItem.SerialNo = key.Value.value["SerialNo"].FirstOrDefault().Value;

                                //GET CASH COUNT OUT 
                                if (key.Value.value.ContainsKey("CashCount") && key.Value.value.ContainsKey("ListCassette"))
                                {
                                    var dateCashCountValue = key.Value.value["TimeCashCount"].FirstOrDefault().Value;
                                    var dateCashCount = DateTime.ParseExact(
                                        string.Format("{0} {1}",
                                        date.Split(' ')[0],
                                        dateCashCountValue),
                                        FormatDateTime_2,
                                        CultureInfo.InvariantCulture);
                                    var NameCassette = key.Value.value["NameCassette"].ToArray();
                                    var TypeCassette = key.Value.value["Type"].ToArray();
                                    var DenomiCassette = key.Value.value["Denomi"].ToArray();
                                    var InitialCassette = key.Value.value["InitialCassette"].ToArray();
                                    var CurrentCassette = key.Value.value["Current"].ToArray();
                                    var StatusCassette = key.Value.value["Status"].ToArray();
                                    for (int i = 0; i < NameCassette.Count(); i++)
                                    {
                                        Cassette cassette = new Cassette();
                                        cassette.Name = NameCassette[i].Value.Trim();
                                        cassette.Type = TypeCassette[i].Value.Trim();
                                        cassette.Denomi = DenomiCassette[i].Value.Trim();
                                        cassette.Initial = InitialCassette[i].Value.Trim();
                                        cassette.Current = CurrentCassette[i].Value.Trim();
                                        cassette.Status = StatusCassette[i].Value.Trim();
                                        cycleItem.Cashcount_Out.Add(NameCassette[i].Value, cassette);

                                    }

                                }
                                //GET DENOMINATION COUNT 
                                if (key.Value.value.ContainsKey("DenomiationCount")
                                    && key.Value.value.ContainsKey("ListDeno") && key.Value.value.ContainsKey("DenoListRetract"))
                                {
                                    var dateDenominationCountValue = key.Value.value["TimeCashCount"].FirstOrDefault().Value;
                                    var dateDenominationCount = DateTime.ParseExact(
                                        string.Format("{0} {1}",
                                        date.Split(' ')[0],
                                        dateDenominationCountValue),
                                        FormatDateTime_2,
                                        CultureInfo.InvariantCulture);
                                    var NameDenomi = key.Value.value["Name"].ToArray();
                                    var DispensedDenomi = key.Value.value["Dispensed"].ToArray();
                                    var DepositedDenomi = key.Value.value["Deposited"].ToArray();
                                    var RemainingDenomi = key.Value.value["Remaining"].ToArray();
                                    var InitialDenomi = key.Value.value["Initial"].ToArray();
                                    var NameDenoRetract = key.Value.value["NameDenoRetract"].ToArray();
                                    var CountDenoRetract = key.Value.value["CountDenoRetract"].ToArray();
                                    for (int i = 0; i < NameDenomi.Count(); i++)
                                    {
                                        Deno deno = new Deno();
                                        deno.Name = NameDenomi[i].Value;
                                        deno.Dispensed = DispensedDenomi[i].Value.Trim();
                                        deno.Deposited = DepositedDenomi[i].Value.Trim();
                                        deno.Initial = InitialDenomi[i].Value.Trim();
                                        deno.Remaining = RemainingDenomi[i].Value.Trim();
                                        cycleItem.DenominationCount.Add(NameDenomi[i].Value, deno);
                                    }
                                    for (int i = 0; i < NameDenoRetract.Count(); i++)
                                    {
                                        if (!cycleItem.DenominationCount.ContainsKey(NameDenoRetract[i].Value.Trim()))
                                        {
                                            Deno deno = new Deno();
                                            deno.Name = NameDenoRetract[i].Value.Trim();
                                            deno.Retracted = CountDenoRetract[i].Value.Trim();
                                            cycleItem.DenominationCount.Add(NameDenoRetract[i].Value.Trim(), deno);
                                        }
                                        else
                                        {
                                            cycleItem
                                                .DenominationCount
                                                .FirstOrDefault(x => x.Key == NameDenoRetract[i].Value.Trim())
                                                .Value
                                                .Retracted = CountDenoRetract[i].Value.Trim();

                                        }
                                    }

                                }
                                if (!Cycles.ContainsKey(startSettlement))
                                {
                                    var exItem = Cycles.Where(x => x.Value.SettlementPeriodDateBegin == cycleItem.SettlementPeriodDateBegin
                                    && x.Value.SettlementPeriodDateEnd == cycleItem.SettlementPeriodDateEnd).FirstOrDefault();
                                    if (exItem.Value != null)
                                    {
                                        Cycles.Remove(exItem.Key);
                                        Cycles.Add(startSettlement, cycleItem);
                                    }
                                    else
                                    {
                                        Cycles.Add(startSettlement, cycleItem);
                                    }
                                }
                            }
                            sString.Replace(key.Value.stringfind, null);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }

        private async Task<string> FindEventDevice2Async(DateTime DateCurrent, string Terminal, string sString)
        {
            Transaction transaction = new Transaction();

            try
            {
                transaction.TraceJournal_Remaining = sString;
                Dictionary<DateTime, TransactionEvent> t = new Dictionary<DateTime, TransactionEvent>();
                transaction = await Task.Run(() => FindEventDevice(transaction, DateCurrent));
                if (transaction.ListEvent.Count > 0)
                {

                    foreach (KeyValuePair<DateTime, TransactionEvent> vars in transaction.ListEvent)
                    {

                        if (ListTransaction[Terminal].ContainsKey(vars.Key)) ListTransaction[Terminal].Add(vars.Key.AddMilliseconds(1), vars.Value);
                        else ListTransaction[Terminal].Add(vars.Key, vars.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return transaction.TraceJournal_Remaining;
        }

        private async Task<Transaction> SplitRequest(Transaction transaction)
        {
            try
            {
                TransactionRequest req = new TransactionRequest();
                req.DateBegin = transaction.DateBegin;
                req.Status = Status.Types.UnSucceeded;
                foreach (TransactionEvent evt in transaction.ListEvent.Values)
                {
                    if (evt.Type.Equals(TransactionEvent.Events.TransactionReqSend))
                    {
                        req = new TransactionRequest();
                        req.DateBegin = evt.DateBegin;
                        req.Status = Status.Types.UnSucceeded;
                        if (CheckRequestName(evt.Data, ref req.Request)) transaction.ListRequest[req.DateBegin] = req;
                    }
                    else if (transaction.ListRequest.Count != 0 && (evt.Type.Equals(TransactionEvent.Events.Transaction) || evt.Type.Equals(TransactionEvent.Events.CashIn) || evt.Type.Equals(TransactionEvent.Events.CashOut)))
                    {
                        string name = transaction.ListRequest.LastOrDefault().Value.Request;
                        if (!Template_TransType_Select.ContainsKey(name)) continue;
                        if (Template_TransType_Select[name].Successful.Split(',').Contains(evt.Name))
                            transaction.ListRequest.LastOrDefault().Value.Status = Status.Types.Succeeded;
                        else if (Template_TransType_Select[name].UnSuccessful.Split(',').Contains(evt.Name))
                            transaction.ListRequest.LastOrDefault().Value.Status = Status.Types.UnSucceeded;
                    }
                }
            }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return transaction;
        }
        private bool CheckRequestName(string operationCode, ref string transactionType)
        {
            foreach (TransactionType type in Template_TransType_Select.Values)
                if (type.Identification.Split('|').Contains(operationCode)) { transactionType = type.Name; return true; }
            // transactionType = @"N/A: [" + operationCode + "]";
            return false;
        }
    }

    public class Status
    {
        public enum Types
        {
            Succeeded,
            UnSucceeded,
            Warning,
            Unknow,
            Error
        }
    }
    public class Transaction : CustomObjectType
    {

        public enum TransactionType
        {
            Alls,
            Withdrawal,
            Deposit,
            BalanceInquiry,
            MiniStatement,
            ChangePin
        }
        public enum CardTypes
        {
            CardNumber,
            CardLess
        }
        public int Value_10K;
        public int Value_20K;
        public int Value_50K;
        public int Value_100K;
        public int Value_200K;
        public int Value_500K;
        public int Rejects;
        public int Amount { get { return (Value_10K * 10000 + Value_20K * 20000 + Value_50K * 50000 + Value_100K * 100000 + Value_200K * 200000 + Value_500K * 500000); } }

        public string TraceJournalFull;
        public Dictionary<DateTime, TransactionEvent> ListEvent = new Dictionary<DateTime, TransactionEvent>();

        public int Result;


        [CategoryAttribute("1.Terminal"), DescriptionAttribute("Terminal ID")]
        public string Terminal { get; set; }
        string _status = "0000";
        [CategoryAttribute("1.Terminal"), DescriptionAttribute("Status")]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        CardTypes _cardtype = CardTypes.CardLess;
        [CategoryAttribute("2. Customer"), DescriptionAttribute("Data input")]
        public CardTypes CardType
        {
            get { return _cardtype; }
            set { _cardtype = value; }
        }
        List<string> _datainput = new List<string>();
        [CategoryAttribute("2. Customer"), DescriptionAttribute("Data input")]
        public List<string> DataInput
        {
            get { return _datainput; }
            set { _datainput = value; }
        }
        string _card = "";
        [CategoryAttribute("2. Customer"), DescriptionAttribute("Card number")]
        public string CardNumber
        {
            get { return _card; }
            set { _card = value; }
        }
        string _name = "";
        [CategoryAttribute("2. Customer"), DescriptionAttribute("Name of the customer")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string TraceJournal_Remaining;
        public string TraceDeviceTxt;
        public string TraceTransMsgTxt;
        public string TraceApplicationTrcTxt;

        [CategoryAttribute("3. Transaction"), DescriptionAttribute("Machine Sequence No")]
        public string MachineSequenceNo { get; set; }
        TransactionType _type = TransactionType.Withdrawal;
        [CategoryAttribute("3. Transaction"), DescriptionAttribute("Type the transaction")]
        public string Type
        {
            get { return string.Join("=>", ListRequest.Values); }
        }

        [CategoryAttribute("3. Transaction"), DescriptionAttribute("Date of the transaction")]
        public string TDate { get { return String.Format("{0:yyyy-MM-dd}", DateBegin); } }
        public DateTime DateBegin;
        public DateTime DateEnd;

        [CategoryAttribute("3. Transaction"), DescriptionAttribute("Start time of the transaction")]
        public string TimeStart { get { return String.Format("{0:HH:mm:ss}", DateBegin); } }

        [CategoryAttribute("3. Transaction"), DescriptionAttribute("End time of the transaction")]
        public string TimeEnd { get { return String.Format("{0:HH:mm:ss}", DateEnd); } }
        public Dictionary<DateTime, TransactionRequest> ListRequest = new Dictionary<DateTime, TransactionRequest>();


        [CategoryAttribute("6. Follow"), DescriptionAttribute("Follow of the transaction")]
        public string FullFollow
        {
            get { return string.Join("=>", ListEvent.Values); }
        }



        public Transaction()
        {

        }

        public override string ToString()
        {
            return String.Format("{0:HH:mm:ss }", DateBegin) + (CardType == Transaction.CardTypes.CardLess ? "Cardless: " + (DataInput.Count == 0 ? string.Empty : DataInput[0]) : "Card: " + CardNumber);
        }
    }

    public class TransactionEvent
    {
        public enum Events
        {
            TransactionStart,
            CardInserted,
            CardLess,
            Track1,
            Track2,
            Track3,
            PINEntered,
            AmountEnter,
            TransactionReqSend,
            TransactionResReceive,
            NotesInserted,
            ShutterOpen,
            NotesRemoved,
            AddMoreDeposit,
            TransactionEnd,
            Transaction,
            CashIn,
            CashOut
        }
        public Events Type;

        public string Data;
        public DateTime DateBegin;
        [CategoryAttribute("Event"), DescriptionAttribute("Status of the Event")]
        public Status.Types Status { get; set; }
        [CategoryAttribute("Event"), DescriptionAttribute("Name of the Event")]
        public string Name { get; set; }
        [CategoryAttribute("Event"), DescriptionAttribute("Date of the Event")]
        public string TDate { get { return String.Format("{0:yyyy-MM-dd}", DateBegin); } }

        [CategoryAttribute("Event"), DescriptionAttribute("Time of the Event")]
        public string TTime { get { return String.Format("{0:HH:mm:ss}", DateBegin); } }

        [CategoryAttribute("Event"), DescriptionAttribute("Content of the Event")]
        public string TContent { get; set; }
        public override string ToString()
        {
            return String.Format("{0:HH:mm:ss }", DateBegin) + Name + ": " + Status;
        }
    }
    public class TransactionRequest
    {
        public string Request;
        public DateTime DateBegin;
        public DateTime DateEnd;
        public Status.Types Status { get; set; }
        public List<Denomination> LstDenomination = new List<Denomination>();
        public int AmountDeposit
        {
            get
            {
                int a = 0;
                foreach (Denomination de in LstDenomination)
                { a += de.Amount; }
                return a;
            }
        }
        public int AmountRequest;
        public override string ToString()
        {
            return Request;
            // return String.Format("{0:HH:mm:ss }", DateBegin) + Request;
        }
    }



    public class RegesValue
    {
        public int index;
        public Dictionary<string, string> value = new Dictionary<string, string>();
        public string stringfind;
        public RegesValue()
        { }

    }
    public class RegesValueWithPatternOfGroup
    {
        public int index;
        public Dictionary<string, Dictionary<int, string>> value = new Dictionary<string, Dictionary<int, string>>();
        public string stringfind;
        public RegesValueWithPatternOfGroup()
        { }

    }
    public class Regexs
    {
        //public static Dictionary<int, RegesValue> RunPatternRegular(string sString, string sReg, bool ShowMessage)
        //{

        //    Dictionary<int, RegesValue> listResult = new Dictionary<int, RegesValue>();

        //    try
        //    {

        //        Regex myRegex = new Regex(sReg);
        //        MatchCollection m = myRegex.Matches(sString);
        //        if (m.Count != 0)
        //        {
        //            // m.Groups[""].Captures.

        //            List<string> listVar = new List<string>();
        //            bool var1 = false;
        //            bool var2 = false;
        //            string var = string.Empty;
        //            foreach (char c in sReg)
        //            {
        //                if (c == '?')
        //                {
        //                    var1 = true;
        //                }
        //                else if (c == '<') var2 = true;
        //                else if (c == '>') { var1 = var2 = false; listVar.Add(var); var = string.Empty; }
        //                else if (var1 && var2)
        //                {
        //                    var += c;
        //                }
        //            }
        //            int k = 0;
        //            string result = string.Empty;
        //            foreach (Match n in m)
        //            {
        //                RegesValue results = new RegesValue();
        //                results.stringfind = n.ToString();
        //                results.index = n.Index;

        //                k++;
        //                string value = string.Empty;
        //                foreach (string group in listVar)
        //                {

        //                    value += Environment.NewLine + group + " : " + n.Groups[group];
        //                    results.value[group] = n.Groups[group].ToString();
        //                }
        //                result = "Found map: " + k.ToString() + "/" + m.Count.ToString() + Environment.NewLine + "-----------Map string-----------" + Environment.NewLine + n.Value + Environment.NewLine + "-----------Group map-----------" + value + Environment.NewLine;
        //                listResult[n.Index] = results;
        //            }
        //            if (ShowMessage)
        //            {
        //                Message_Form frm = new Message_Form("Run Test Result", result);
        //              frm.Show();
        //            }

        //        }
        //        else
        //        {
        //            if (ShowMessage) MessageBox.Show("Not math :(", "Test fail", MessageBoxButtons.OK);
        //        }
        //    }
        //    catch (Exception ex)
        //    { InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name); }
        //    return listResult;
        //}
        //public static bool RunPatternRegular(string sString, string sReg, bool ShowMessage, out Dictionary<int, RegesValue> listResult)
        //{

        //    listResult = new Dictionary<int, RegesValue>();
        //    try
        //    {

        //        Regex myRegex = new Regex(sReg);
        //        MatchCollection m = myRegex.Matches(sString);
        //        if (m.Count != 0)
        //        {
        //            // m.Groups[""].Captures.

        //            List<string> listVar = new List<string>();
        //            bool var1 = false;
        //            bool var2 = false;
        //            string var = string.Empty;
        //            foreach (char c in sReg)
        //            {
        //                if (c == '?')
        //                {
        //                    var1 = true;
        //                }
        //                else if (c == '<') var2 = true;
        //                else if (c == '>') { var1 = var2 = false; listVar.Add(var); var = string.Empty; }
        //                else if (var1 && var2)
        //                {
        //                    var += c;
        //                }
        //            }
        //            int k = 0;
        //            string result = string.Empty;
        //            foreach (Match n in m)
        //            {
        //                RegesValue results = new RegesValue();
        //                results.stringfind = n.ToString();
        //                results.index = n.Index;

        //                k++;
        //                string value = string.Empty;
        //                foreach (string group in listVar)
        //                {

        //                    value += Environment.NewLine + group + " : " + n.Groups[group];
        //                    results.value[group] = n.Groups[group].ToString();
        //                }
        //                result = "Found map: " + k.ToString() + "/" + m.Count.ToString() + Environment.NewLine + "-----------Map string-----------" + Environment.NewLine + n.Value + Environment.NewLine + "-----------Group map-----------" + value + Environment.NewLine;
        //                listResult[n.Index] = results;
        //            }
        //            if (ShowMessage)
        //            {
        //                Message_Form frm = new Message_Form("Run Test Result", result);
        //                frm.Show();
        //            }
        //            return true;
        //        }
        //        else
        //        {
        //            if (ShowMessage) MessageBox.Show("Not math :(", "Test fail", MessageBoxButtons.OK);
        //            else return false;

        //        }
        //    }
        //    catch (Exception ex)
        //    { InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name); }
        //    return false;
        //}
        public static bool RunPatternRegular(string sString, string sReg, out Dictionary<int, RegesValue> listResult)
        {
            listResult = new Dictionary<int, RegesValue>();
            try
            {
                Regex myRegex = new Regex(sReg, RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(5));
                MatchCollection m = myRegex.Matches(sString);
                Match ma = myRegex.Match(sString);
                if (m.Count != 0)
                {
                    foreach (Match n in m)
                    {
                        RegesValue results = new RegesValue();
                        results.stringfind = n.ToString();
                        results.index = n.Index;
                        foreach (string groupName in myRegex.GetGroupNames())
                        {
                            if (groupName.Equals("0")) continue;
                            results.value[groupName] = n.Groups[groupName].ToString();
                        }
                        listResult[n.Index] = results;
                    }
                    return true;
                }
            }

            catch (TimeoutException)
            { }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }
        //Dat 5/12/2019
        public static bool RunPatternRegular(string sString, string sReg, out Dictionary<int, RegesValueWithPatternOfGroup> listResult)
        {

            listResult = new Dictionary<int, RegesValueWithPatternOfGroup>();
            try
            {
                Regex myRegex = new Regex(sReg, RegexOptions.ExplicitCapture, TimeSpan.FromSeconds(5));
                MatchCollection m = myRegex.Matches(sString);
                Match ma = myRegex.Match(sString);
                if (m.Count != 0)
                {
                    foreach (Match n in m)
                    {
                        RegesValueWithPatternOfGroup results = new RegesValueWithPatternOfGroup();
                        results.stringfind = n.ToString(); sString = sString.Replace(n.ToString(), string.Empty);
                        results.index = n.Index;
                        foreach (string groupName in myRegex.GetGroupNames())
                        {
                            if (groupName.Equals("0")) continue;
                            CaptureCollection captureCollection = n.Groups[groupName].Captures;
                            Dictionary<int, string> keyValues = new Dictionary<int, string>();
                            foreach (Capture capture in captureCollection)
                            {

                                keyValues.Add(capture.Index, capture.Value);
                            }
                            results.value[groupName] = keyValues;
                        }
                        listResult[n.Index] = results;
                    }
                    return true;
                }
            }

            catch (TimeoutException)
            { }
            catch (Exception ex)
            {
                InitParametar.Send_Error(ex.ToString(), MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }

    }
    public class UIHelper
    {
        static public void UIThread(Control control, MethodInvoker code)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.BeginInvoke(code);
                    return;
                }
                control.Invoke(code);
            }
            catch
            {
                //  MsgInfo.MessageBoxError("Core", "UIHelper", "UIThread", "Parameter1: " + control.ToString() + "\nParameter2: " + code.ToString() + "\n" + ex.Message.ToString());
            }
        }
        internal static void UIThread(ListViewItem it, MethodInvoker methodInvoker)
        {
            throw new NotImplementedException();
        }
    }
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    //public class Cycle
    //{
    //    public DateTime DateBegin;
    //    public DateTime DateEnd;
    //    public Dictionary<string, Cassette> ListCassette = new Dictionary<string, Cassette>();
    //    public string LogTxt;
    //    public string PathLog;
    //    public int IndexLog;
    //}
    //public class Cassette
    //{
    //    public string Name;
    //    public string Description;
    //    public string Denomination;
    //    public decimal Value;
    //    public int Counter;
    //    public DateTime DateChange;
    //    public Cassette()
    //    {

    //    }
    //}
    public class Denomination
    {

        public DateTime DateBegin;
        public DateTime DateEnd;
        public string Currency;
        public int Value;
        public int Remaining;
        public int Init { set { Remaining = value; } }
        public int Retract;
        public int Reject;
        public int Amount { get { return Value * Remaining; } }
    }

    public class CycleEvent
    {
        public enum Events
        {
            SettlementPeriod,
            CashCount,
            DenominationCount,
            CardCapture,
            ForgottenBanknote,
            Replenishment
        }

        public string Name;
        public string Log;

        [CategoryAttribute("Cycle"), DescriptionAttribute("Date of the cycle")]
        public string TDate;

    }
    public class Cycle
    {
        private DateTime dateBegin;
        private DateTime dateEnd;
        private DateTime settlementPeriodDateBegin;

        private DateTime settlementPeriodDateEnd;

        public Dictionary<string, CycleEvent> ListEvent = new Dictionary<string, CycleEvent>();
        public Dictionary<int, Cassette> Cashcount_In = null;


        private Dictionary<string, Cassette> cashcount_Out = new Dictionary<string, Cassette>();


        private Dictionary<string, Deno> denominationCount = new Dictionary<string, Deno>();
        public string LogTxt;
        public string PathLog;
        public int IndexLog;

        [CategoryAttribute("1. Info"), DescriptionAttribute("Terminal ID")]
        public string TerminalID;
        public string SerialNo;

        [CategoryAttribute("1. Info"), DescriptionAttribute("Time Start")]
        public DateTime DateBegin { get => dateBegin; set => dateBegin = value; }

        public DateTime DateEnd { get => dateEnd; set => dateEnd = value; }

        [CategoryAttribute("1. Info"), DescriptionAttribute("Start of settlement period")]
        public DateTime SettlementPeriodDateBegin { get => settlementPeriodDateBegin; set => settlementPeriodDateBegin = value; }

        [CategoryAttribute("1. Info"), DescriptionAttribute("End of settlement period")]
        public DateTime SettlementPeriodDateEnd { get => settlementPeriodDateEnd; set => settlementPeriodDateEnd = value; }

        [CategoryAttribute("2.Terminal Count"), DescriptionAttribute("Cash Count")]
        public Dictionary<string, Cassette> Cashcount_Out { get => cashcount_Out; set => cashcount_Out = value; }

        [CategoryAttribute("2.Terminal Count"), DescriptionAttribute("Denomination count")]
        public Dictionary<string, Deno> DenominationCount { get => denominationCount; set => denominationCount = value; }

        public Cycle()
        {
        }
        public override string ToString()
        {
            return "CYCLE START: " + settlementPeriodDateBegin.ToString();
        }
    }
    public class Deno
    {
        private string name = string.Empty;
        private string dispensed = "0";
        private string deposited = "0";
        private string remaining = "0";
        private string retracted = "0";
        private string initial = "0";
        private string currency = string.Empty;
        private string log = string.Empty;

        public string Name { get => name; set => name = value; }
        public string Dispensed { get => dispensed; set => dispensed = value; }
        public string Deposited { get => deposited; set => deposited = value; }
        public string Remaining { get => remaining; set => remaining = value; }
        public string Retracted { get => retracted; set => retracted = value; }
        public string Initial { get => initial; set => initial = value; }
        public string Currency { get => currency; set => currency = value; }
        public string Log { get => log; set => log = value; }
        public override string ToString()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append(this.Dispensed);
            //sb.Append(",");
            //sb.Append(this.Dispensed);
            //sb.Append(",");
            //sb.Append(this.Deposited);
            //sb.Append(",");
            //sb.Append(this.Remaining);
            //sb.Append(",");
            //sb.Append(this.Retracted);
            //return sb.ToString();
            return string.Format("Dispensed: {0}, Dispensed: {1}, Deposited: {2}, Remaining: {3}, Retracted: {4}, Initial: {5}",
                Dispensed, Dispensed, Deposited, Remaining, Retracted, Initial);
        }
    }
    public class CapturedCard
    {
        public DateTime DateTime;
        public int Count;
        public string CardNumber;
        public string RetractedTitle;
        public string Status;
    }
    public class ForgottenBanknote
    {
        public DateTime DateTime;
        public string No;
        public string CardNumber;
        public string Retract;
        public string Position;

    }
    public class Cassette
    {
        private string name;
        private string type;
        private string denomi;
        private string initial;
        private string current;
        private string status;
        private string log;

        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public string Denomi { get => denomi; set => denomi = value; }
        public string Initial { get => initial; set => initial = value; }
        public string Current { get => current; set => current = value; }
        public string Status { get => status; set => status = value; }
        public string Log { get => log; set => log = value; }

        public override string ToString()
        {
            return string.Format("Type: {0}, Denomi: {1}, Initial: {2}, Current: {3}, Status: {4}", Type, Denomi, Initial, Current, Status);
        }
        public Cassette()
        {

        }
    }
    public class EventData
    {
        private string sString;
        private Dictionary<DateTime, TransactionEvent> eventList;
        private Transaction transaction;

        public string SString { get => sString; set => sString = value; }
        public Dictionary<DateTime, TransactionEvent> EventList { get => eventList; set => eventList = value; }
        public Transaction Transaction { get => transaction; set => transaction = value; }

        public EventData(string sString, Dictionary<DateTime, TransactionEvent> eventList, Transaction transaction)
        {
            this.sString = sString;
            this.eventList = eventList;
            this.transaction = transaction;
        }
        public EventData(string sString, Dictionary<DateTime, TransactionEvent> eventList)
        {
            this.sString = sString;
            this.eventList = eventList;
        }
        public EventData()
        {
        }
    }
}
