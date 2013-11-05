using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DolWatcher
{
    public partial class Form1 : Form
    {
        private static String _ConfigFileName { get { return "DolWatcher.conf"; } }

        private Lib.Tool _tool = null;
        private Lib.AppConfig _appConfig = null;

        private Boolean _scrollStop = false;
        
        public Form1()
        {
            InitializeComponent();

            _tool = new Lib.Tool();
            try
            {
                _appConfig = (Lib.AppConfig)_tool.LoadConfig(typeof(Lib.AppConfig), _ConfigFileName);
            }
            catch (Exception)
            {
                _appConfig = new Lib.AppConfig();
                _appConfig.LoadDefault();
            }

            try
            {
                FileList();

                timer1.Start();
            }
            catch(Exception)
            { }
        }

        private void ApplicationExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private String FileList()
        {

            String[] files = System.IO.Directory.GetFiles(_appConfig.DolLogDir);

            List<DateTime> fileTimes = new List<DateTime>();

            foreach (String file in files)
            {
                if (_appConfig.NewestFileName == "")
                {
                    _appConfig.NewestFileName = file;
                    _appConfig.NewestFileTime = System.IO.File.GetLastWriteTime(file);
                    
                }
                else
                {
                    if (_appConfig.NewestFileTime < System.IO.File.GetLastWriteTime(file))
                    {
                        _appConfig.NewestFileName = file;
                        _appConfig.NewestFileTime = System.IO.File.GetLastWriteTime(file);
                    }
                }
            }

            //行を取得
            List<String> lines = new List<string>();

            using (FileStream fs = new FileStream(_appConfig.NewestFileName,
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (TextReader sr = new StreamReader(fs,
                    Encoding.GetEncoding("UTF-8")))
                {
                    String line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }

            _appConfig.LineCount = lines.Count;

            return _appConfig.NewestFileName;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //新しいファイルがあるかチェック
                String curFile = _appConfig.NewestFileName;
                DateTime curFileTime = _appConfig.NewestFileTime;
                int curFileCount = _appConfig.LineCount;

                String chkFile = FileList();
                DateTime chkFileTime = _appConfig.NewestFileTime;
                int chkFileCount = _appConfig.LineCount;

                if (curFile == chkFile && curFileCount < chkFileCount)
                {
                    //新しい

                    //行を取得
                    List<String> lines = new List<string>();

                    using (FileStream fs = new FileStream(curFile,
                        FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (TextReader sr = new StreamReader(fs,
                            Encoding.GetEncoding("UTF-8")))
                        {
                            String line = "";
                            while ((line = sr.ReadLine()) != null)
                            {
                                lines.Add(line);
                            }
                        }
                    }

                    List<String> newLine = new List<string>();

                    if (curFileCount >= 2)
                    {
                        newLine = lines.GetRange(curFileCount - 2, chkFileCount - curFileCount);
                    }
                    else
                    {
                        newLine = lines.GetRange(curFileCount, chkFileCount - curFileCount);
                    }

                    //String allLine = "";
                    Color rowColor = Color.Black;

                    foreach (String line in newLine)
                    {
                        String tempStr = line.Replace("</FONT><BR>", "");
                        String headStr = tempStr.Substring(0, 22);
                        String bodyStr = tempStr.Substring(22) + "\r\n";
                        bodyStr = bodyStr.Replace("&gt;", " : ");

                        bool curShow = true;
                        bool dateShow = true;
                        Font curFont = null;
                        Color curColor = Color.Black;


                        switch (headStr)
                        {
                            case "<FONT color=\"#80aaff\">":
                                curColor = ColorTranslator.FromHtml(_appConfig.Color80aaff);
                                curFont = new Font(richTextBox1.SelectionFont.FontFamily, (float)_appConfig.Size80aaff);
                                curShow = _appConfig.Enable80aaff;
                                dateShow = _appConfig.EnableDate;
                                break;
                            case "<FONT color=\"#80ffff\">":
                                curColor = ColorTranslator.FromHtml(_appConfig.Color80ffff);
                                curFont = new Font(richTextBox1.SelectionFont.FontFamily, (float)_appConfig.Size80ffff);
                                curShow = _appConfig.Enable80ffff;
                                dateShow = _appConfig.EnableDate;
                                break;
                            case "<FONT color=\"#d4d4d4\">":
                                curColor = ColorTranslator.FromHtml(_appConfig.Colord4d4d4);
                                curFont = new Font(richTextBox1.SelectionFont.FontFamily, (float)_appConfig.Sized4d4d4);
                                curShow = _appConfig.Enabled4d4d4;
                                dateShow = _appConfig.EnableDate;
                                break;
                            case "<FONT color=\"#10c870\">":
                                curColor = ColorTranslator.FromHtml(_appConfig.Color10c870);
                                curFont = new Font(richTextBox1.SelectionFont.FontFamily, (float)_appConfig.Size10c870);
                                curShow = _appConfig.Enable10c870;
                                dateShow = _appConfig.EnableDate;
                                break;
                            default:
                                curColor = ColorTranslator.FromHtml(_appConfig.ColorOther);
                                curFont = new Font(richTextBox1.SelectionFont.FontFamily, (float)_appConfig.SizeOther);
                                curShow = _appConfig.EnableOther;
                                dateShow = _appConfig.EnableDate;
                                break;
                        }
                        if (dateShow)
                        {
                            bodyStr = DateTime.Now.ToString("HH:mm:ss ") + bodyStr;
                        }

                        if (curShow)
                        {
                            /*
                            richTextBox1.Select(0, 0);
                            richTextBox1.SelectedText = bodyStr;
                            richTextBox1.Select(0, bodyStr.Length - 1);
                            richTextBox1.SelectionColor = curColor;
                            richTextBox1.SelectionFont = curFont;
                            richTextBox1.Select(0, 0);
                             */
                            richTextBox1.Select(richTextBox1.TextLength, 0);
                            richTextBox1.SelectedText = bodyStr;
                            richTextBox1.Select(richTextBox1.TextLength - bodyStr.Length, bodyStr.Length - 1);
                            richTextBox1.SelectionColor = curColor;
                            richTextBox1.SelectionFont = curFont;
                            richTextBox1.Select(richTextBox1.TextLength, 0);

                            if (_scrollStop == false)
                            {
                                richTextBox1.Focus();
                                richTextBox1.ScrollToCaret();
                            }
                        }

                    }


                }
            }
            catch(Exception)
            { }
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _tool.SaveConfig(_appConfig, typeof(Lib.AppConfig), _ConfigFileName);

            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();

            _appConfig = (Lib.AppConfig)_tool.LoadConfig(typeof(Lib.AppConfig), _ConfigFileName);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                _scrollStop = true;
            }
            else
            {
                _scrollStop = false;
            }
        }
    }
}
