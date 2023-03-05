using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Notepad {
    public static class Utils {
        public static (string, string, string) OpenFile() {
            using (var openFileDialog = new OpenFileDialog()) {
                openFileDialog.DefaultExt = "txt";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Open File";

                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.ShowDialog();
                var openFilePath = openFileDialog.FileName;

                if (openFilePath == "") {
                    return ("", "", "");
                }

                var newText = File.ReadAllText(openFilePath);
                var fileName = Path.GetFileName(openFilePath);

                return (newText, openFilePath, fileName);
            }
        }

        public static void SaveFile(string text) {
            using (var saveFileDialog = new SaveFileDialog()) {
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Save File";

                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.ShowDialog();
                var saveFilePath = saveFileDialog.FileName;

                if (saveFilePath != "") {
                    File.WriteAllText(saveFilePath, text);
                }
            }
        }
    }
}