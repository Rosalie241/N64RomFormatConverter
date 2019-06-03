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
        private void changeConvertButtonsText(RomConverter.RomFormat button1Format, RomConverter.RomFormat button2Format)
        {
            if(!ConvertButton1.Enabled || !ConvertButton2.Enabled)
            {
                ConvertButton1.Enabled = true;
                ConvertButton2.Enabled = true;
            }

            ConvertButton1.Text = $"Convert to {button1Format.ToString()}";
            ConvertButton2.Text = $"Convert to {button2Format.ToString()}";
        }

        private void resetConvertButtons()
        {
            // reset text
            ConvertButton1.Text = "Convert to ...";
            ConvertButton2.Text = "Convert to ...";

            // disable them
            ConvertButton1.Enabled = false;
            ConvertButton2.Enabled = false;
        }
        private string romFileName { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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
#if DEBUG
                                MessageBox.Show($"Invalid ROM header: {romConverter.RomHeader}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                                MessageBox.Show("Invalid ROM!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                                resetConvertButtons();
                                return;

                            case RomConverter.RomFormat.N64:
                                changeConvertButtonsText(RomConverter.RomFormat.V64, RomConverter.RomFormat.Z64);
                                break;

                            case RomConverter.RomFormat.V64:
                                changeConvertButtonsText(RomConverter.RomFormat.N64, RomConverter.RomFormat.Z64);
                                break;

                            case RomConverter.RomFormat.Z64:
                                changeConvertButtonsText(RomConverter.RomFormat.N64, RomConverter.RomFormat.V64);
                                break;
                        }


                        RomInfoText.Text = $"FORMAT: {format.ToString()}" + Environment.NewLine +
                                           $"HEADER: {romConverter.RomHeader}";



                    }
                }
            }     
            
            
        }

        private void ConvertButtonClick(Button button)
        {
            using (RomConverter romConverter = new RomConverter(romFileName))
            {
                using (SaveFileDialog fileDialog = new SaveFileDialog())
                {
                    RomConverter.RomFormat format = RomConverter.RomFormat.INVALID;

                    if (button.Text.Contains("N64"))
                    {
                        format = RomConverter.RomFormat.N64;
                        fileDialog.Filter = "N64 rom(*.n64)|*.n64";
                    }
                    else if (button.Text.Contains("V64"))
                    {
                        format = RomConverter.RomFormat.V64;
                        fileDialog.Filter = "N64 rom(*.v64)|*.v64";
                    }
                    else if (button.Text.Contains("Z64"))
                    {
                        format = RomConverter.RomFormat.Z64;
                        fileDialog.Filter = "N64 rom(*.z64)|*.z64";
                    }

                    fileDialog.FileName = Path.GetFileName(Regex.Replace(romFileName, romConverter.GetFormat().ToString(), format.ToString().ToLower(), RegexOptions.IgnoreCase));

                    DialogResult result = fileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        romConverter.Convert(fileDialog.FileName, format);
                    }
                }
            }
        }
        private void ConvertButton1_Click(object sender, EventArgs e)
        {
            ConvertButtonClick(ConvertButton1);
        }

        private void ConvertButton2_Click(object sender, EventArgs e)
        {
            ConvertButtonClick(ConvertButton2);
        }
    }
}
