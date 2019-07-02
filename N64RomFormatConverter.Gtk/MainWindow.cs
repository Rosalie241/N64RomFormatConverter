using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using N64RomConverter;
using System.Text.RegularExpressions;
namespace N64RomFormatConverter.Gtk
{
    class MainWindow : Window
    {

        //[UI] private Label _label1 = null;
        //[UI] private Button _button1 = null;
        [UI] private TextView RomInfoText = null;
        [UI] private TextView RomFileInputText = null;
        [UI] private Button SelectFileButton = null;
        [UI] private Button ConvertButton1 = null;
        [UI] private Button ConvertButton2 = null;
        [UI] private Button ConvertButton3 = null;

        private string FileName { get; set; }
        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            SelectFileButton.Clicked += SelectFileButton_Clicked;
            ConvertButton1.Clicked += ConvertButton1_Clicked;
            ConvertButton2.Clicked += ConvertButton2_Clicked;
            ConvertButton3.Clicked += ConvertButton3_Clicked;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void SelectFileButton_Clicked(object sender, EventArgs a)
        {
            FileChooserDialog fc =
                new FileChooserDialog("Select ROM",
                    this,
                    FileChooserAction.Open,
                    "Cancel", ResponseType.Cancel,
                    "Open", ResponseType.Accept);

            FileFilter fileFilter = new FileFilter();

            fileFilter.Name = "N64 rom";
            string[] allowedExtensions = { "n64", "z64", "v64" };
            foreach (string extension in allowedExtensions)
            {
                fileFilter.AddPattern("*." + extension.ToLower());
                fileFilter.AddPattern("*." + extension.ToUpper());
            }

            fc.AddFilter(fileFilter);

            if (fc.Run() != (int)ResponseType.Accept)
            {
                fc.Destroy();
                return;
            }

            FileName = fc.Filename;
            fc.Destroy();

            // since Gtk# lacks docs(thanks!)
            // I have no idea how to make the textview 'locked'
            // so if we use more characters than 48, our GUI will look ugly
            // so just..use a simple loop and trim the string
            if (FileName.Length <= 48)
                RomFileInputText.Buffer.Text = FileName;
            else
            {
                string trimmedFileName = null;

                for (int i = 0; i <= 48; i++)
                    trimmedFileName += FileName[i];

                RomFileInputText.Buffer.Text = trimmedFileName;
            }

            using (RomConverter romConverter = new RomConverter(FileName))
            {
                RomInfoText.Buffer.Text =   $"FORMAT      | {romConverter.GetFormat()}" + Environment.NewLine +
                                            $"HEADER       | {romConverter.RomHeader}" + Environment.NewLine +
                                            $"MD5 HASH | {romConverter.GetMd5Hash()}";
            }
        }
        private void ShowDialog(string text, MessageType type)
        {
            using (Dialog dialog = new MessageDialog(this,
                                  DialogFlags.DestroyWithParent,
                                  type,
                                  ButtonsType.Ok,
                                  text))
            {
                dialog.Run();
                dialog.Hide();
            }
        }
        private void ConvertButton_Click(Button button, RomConverter.RomFormat format)
        {
            using (RomConverter romConverter = new RomConverter(FileName))
            {
                // show error if we're trying to convert to the same format
                if (format.ToString().ToLower() ==
                    romConverter.GetFormat().ToString().ToLower())
                {
                    ShowDialog("Cannot convert to the same format!", MessageType.Error);
                    return;
                }

                FileChooserDialog fc =
                    new FileChooserDialog("Select ROM",
                    this,
                    FileChooserAction.Save,
                    "Cancel", ResponseType.Cancel,
                    "Save", ResponseType.Accept);

                FileFilter fileFilter = new FileFilter();

                fileFilter.Name = format.ToString();
                fileFilter.AddPattern("*." + format.ToString().ToUpper());
                fileFilter.AddPattern("*." + format.ToString().ToLower());

                fc.AddFilter(fileFilter);
                fc.CurrentName = System.IO.Path.GetFileName(
                            Regex.Replace(FileName,
                                romConverter.GetFormat().ToString(),
                                format.ToString().ToLower(),
                                RegexOptions.IgnoreCase));

                if (fc.Run() != (int)ResponseType.Accept)
                {
                    fc.Destroy();
                    return;
                }

                string targetFileName = fc.Filename;
                fc.Destroy();

                romConverter.Convert(targetFileName, format);

                ShowDialog("Converted successfully!", MessageType.Info);
            }
        }
        private void ConvertButton1_Clicked(object sender, EventArgs a)
        {
            ConvertButton_Click(ConvertButton1, RomConverter.RomFormat.N64);
        }
        private void ConvertButton2_Clicked(object sender, EventArgs a)
        {
            ConvertButton_Click(ConvertButton2, RomConverter.RomFormat.V64);
        }
        private void ConvertButton3_Clicked(object sender, EventArgs a)
        {
            ConvertButton_Click(ConvertButton3, RomConverter.RomFormat.Z64);
        }
    }
}
