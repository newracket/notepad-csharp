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
        
        public static void PrintFile(string text, string fileName) {
            using (var printDialog = new PrintDialog()) {
                printDialog.AllowCurrentPage = true;
                printDialog.AllowPrintToFile = true;
                
                printDialog.Document = new PrintDocument();
                printDialog.Document.DocumentName = fileName;
                printDialog.Document.PrintPage += ((sender, e) => {
                    e.Graphics.DrawString(text, new Font("Arial", 14), Brushes.Black, 10, 10);
                });
                
                var result = printDialog.ShowDialog();
                if (result == DialogResult.OK) {
                    printDialog.Document.Print();
                }
                
                // TODO: Make work for multiple pages
            }
        }
    }
}