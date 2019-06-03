/*
 N64RomFormatConverter - https://gitlab.com/tim241/N64RomFormatConverter
 Copyright (C) 2019 Tim Wanders <tim241@mailbox.org>
 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.
 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.
 You should have received a copy of the GNU General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N64RomFormatConverter
{
    public partial class Form1 : Form
    {
        private string romFileName { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) { }


        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                // fileDialog settings
                fileDialog.RestoreDirectory = true;
                fileDialog.Filter = "N64 rom files|*.n64;*.z64;*.v64;*.N64;*.Z64;*.V64";

                // open dialog and get result
                DialogResult result = fileDialog.ShowDialog();
                if(result == DialogResult.OK)
                {
                    romFileName = fileDialog.FileName;
                    RomFileInputText.Text = romFileName;

                    using (RomConverter romConverter = new RomConverter(romFileName))
                    {

                        RomConverter.RomFormat format = romConverter.GetFormat();
                       
                        switch(format)
                        {
                            // invalid
                            case RomConverter.RomFormat.INVALID:
                                MessageBox.Show("Invalid ROM!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // disable all buttons
                                ConvertButton1.Enabled = false;
                                ConvertButton2.Enabled = false;
                                ConvertButton3.Enabled = false;
                                return;

                            case RomConverter.RomFormat.N64:
                                ConvertButton1.Enabled = false;
                                ConvertButton2.Enabled = true;
                                ConvertButton3.Enabled = true;
                                break;

                            case RomConverter.RomFormat.V64:
                                ConvertButton1.Enabled = true;
                                ConvertButton2.Enabled = false;
                                ConvertButton3.Enabled = true;
                                break;

                            case RomConverter.RomFormat.Z64:
                                ConvertButton1.Enabled = true;
                                ConvertButton2.Enabled = true;
                                ConvertButton3.Enabled = false;
                                break;
                        }

                        RomInfoText.Text = $"FORMAT     | {format.ToString()}" + Environment.NewLine +
                                           $"HEADER     | {romConverter.RomHeader}" + Environment.NewLine +
                                           $"MD5 HASH | {romConverter.GetMd5Hash()}";
                    }
                }
            }     
            
            
        }

        private void ConvertButton_Click(Button button, RomConverter.RomFormat format)
        {
            using (RomConverter romConverter = new RomConverter(romFileName))
            {
                using (SaveFileDialog fileDialog = new SaveFileDialog())
                {
                   
                    fileDialog.Filter = $"N64 rom(*.{format.ToString().ToLower()})|*.{format.ToString().ToLower()}";
                    fileDialog.FileName = Path.GetFileName(Regex.Replace(romFileName, romConverter.GetFormat().ToString(), format.ToString().ToLower(), RegexOptions.IgnoreCase));

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                            romConverter.Convert(fileDialog.FileName, format);
                            MessageBox.Show("Converted successfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void ConvertButton1_Click(object sender, EventArgs e)
        {
            ConvertButton_Click(ConvertButton1, RomConverter.RomFormat.N64);
        }

        private void ConvertButton2_Click(object sender, EventArgs e)
        {
            ConvertButton_Click(ConvertButton2, RomConverter.RomFormat.V64);
        }

        private void ConvertButton3_Click(object sender, EventArgs e)
        {
            ConvertButton_Click(ConvertButton3, RomConverter.RomFormat.Z64);
        }
    }
}
