using System;
using System.IO;
using System.Windows.Forms;

namespace Notepad {
    public sealed partial class Form1 : Form {
        private string _lastSavedText;

        private string _filePath;
        private string _fileName = "Unnamed";

        public Form1() {
            InitializeComponent();
            Text = _fileName;

            _lastSavedText = notepad.Text;
        }

        private void OnKeyPressed(object sender, EventArgs e) {
            if (notepad.Text != _lastSavedText) {
                Text = "*" + _fileName;
            }
            else {
                Text = _fileName;
            }
        }

        private void NewFileButton(object sender, EventArgs e) {
            if (!RequestSave(sender, e)) return;

            _lastSavedText = notepad.Text = "";
            _filePath = null;
            Text = _fileName = "Unnamed";
        }
        

        private bool RequestSave(object sender, EventArgs e) {
            if (notepad.Text == _lastSavedText) {
                return true;
            }

            var result = MessageBox.Show($"Do you want to save changes to {_fileName}?", "Notepad",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            switch (result) {
                case DialogResult.Yes:
                    SaveFileButton(sender, e);
                    break;
                case DialogResult.Cancel:
                    return false;
                case DialogResult.Abort:
                case DialogResult.Ignore:
                case DialogResult.No:
                case DialogResult.None:
                case DialogResult.OK:
                case DialogResult.Retry:
                default:
                    break;
            }

            return true;
        }

        private void OpenFileButton(object sender, EventArgs e) {
            var newFileValues = Utils.OpenFile();

            if (newFileValues.Item1 == "") {
                return;
            }

            _lastSavedText = notepad.Text = newFileValues.Item1;
            _filePath = newFileValues.Item2;
            Text = _fileName = newFileValues.Item3;

            Text = _fileName;
            _lastSavedText = notepad.Text;
        }


        private void SaveFileButton(object sender, EventArgs e) {
            if (_filePath != null) {
                File.WriteAllText(_filePath, notepad.Text);
            }
            else {
                Utils.SaveFile(notepad.Text);
            }

            Text = _fileName;
            _lastSavedText = notepad.Text;
        }

        private void SaveAsButton(object sender, EventArgs e) {
            Utils.SaveFile(notepad.Text);

            Text = _fileName;
            _lastSavedText = notepad.Text;
        }

        private void ExitButton(object sender, EventArgs e) {
            if (!RequestSave(sender, e)) return;
            
            Close();
        }
    }
}