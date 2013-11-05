using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DolWatcher
{
    public partial class SettingForm : Form
    {

        private static String _ConfigFileName { get { return "DolWatcher.conf"; } }

        private Lib.Tool _tool = null;
        private Lib.AppConfig _appConfig = null;

        public SettingForm()
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
                numericUpDown10c870.Value = _appConfig.Size10c870;
                numericUpDown80aaff.Value = _appConfig.Size80aaff;
                numericUpDown80ffff.Value = _appConfig.Size80ffff;
                numericUpDownd4d4d4.Value = _appConfig.Sized4d4d4;
                numericUpDownOther.Value = _appConfig.SizeOther;

                textBox10c870.Text = _appConfig.Color10c870;
                textBox80aaff.Text = _appConfig.Color80aaff;
                textBox80ffff.Text = _appConfig.Color80ffff;
                textBoxd4d4d4.Text = _appConfig.Colord4d4d4;
                textBoxOther.Text = _appConfig.ColorOther;

                checkBox10c870.Checked = _appConfig.Enable10c870;
                checkBox80aaff.Checked = _appConfig.Enable80aaff;
                checkBox80ffff.Checked = _appConfig.Enable80ffff;
                checkBoxd4d4d4.Checked = _appConfig.Enabled4d4d4;
                checkBoxOther.Checked = _appConfig.EnableOther;
                checkBoxDate.Checked = _appConfig.EnableDate;
            }
            catch(Exception)
            {
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                _appConfig.Size10c870 = numericUpDown10c870.Value;
                _appConfig.Size80aaff = numericUpDown80aaff.Value;
                _appConfig.Size80ffff = numericUpDown80ffff.Value;
                _appConfig.Sized4d4d4 = numericUpDownd4d4d4.Value;
                _appConfig.SizeOther = numericUpDownOther.Value;

                _appConfig.Color10c870 = textBox10c870.Text;
                _appConfig.Color80aaff = textBox80aaff.Text;
                _appConfig.Color80ffff = textBox80ffff.Text;
                _appConfig.Colord4d4d4 = textBoxd4d4d4.Text;
                _appConfig.ColorOther = textBoxOther.Text;

                _appConfig.Enable10c870 = checkBox10c870.Checked;
                _appConfig.Enable80aaff = checkBox80aaff.Checked;
                _appConfig.Enable80ffff = checkBox80ffff.Checked;
                _appConfig.Enabled4d4d4 = checkBoxd4d4d4.Checked;
                _appConfig.EnableOther = checkBoxOther.Checked;
                _appConfig.EnableDate = checkBoxDate.Checked;


                _tool.SaveConfig(_appConfig, typeof(Lib.AppConfig), _ConfigFileName);
            }
            catch(Exception)
            {

            }
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox10c870.Enabled = false;
                textBox80ffff.Enabled = false;
                textBoxd4d4d4.Enabled = false;
                textBoxOther.Enabled = false;

                numericUpDown10c870.Enabled = false;
                numericUpDown80ffff.Enabled = false;
                numericUpDownd4d4d4.Enabled = false;
                numericUpDownOther.Enabled = false;
            }
            else
            {
                textBox10c870.Enabled = true;
                textBox80ffff.Enabled = true;
                textBoxd4d4d4.Enabled = true;
                textBoxOther.Enabled = true;

                numericUpDown10c870.Enabled = true;
                numericUpDown80ffff.Enabled = true;
                numericUpDownd4d4d4.Enabled = true;
                numericUpDownOther.Enabled = true;
            }


        }

        private void textBox80aaff_TextChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox10c870.Text = textBox80aaff.Text;
                textBox80ffff.Text = textBox80aaff.Text;
                textBoxd4d4d4.Text = textBox80aaff.Text;
                textBoxOther.Text = textBox80aaff.Text;
            }
        }

        private void numericUpDown80aaff_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown10c870.Value = numericUpDown80aaff.Value;
                numericUpDown80ffff.Value = numericUpDown80aaff.Value;
                numericUpDownd4d4d4.Value = numericUpDown80aaff.Value;
                numericUpDownOther.Value = numericUpDown80aaff.Value;
            }
        }

    }
}
