using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;


namespace ITI.Archi_Vite.GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ChoseFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) 
            {
                string path = openFileDialog1.FileName;
                string path2 = @"C:\ArchiFile\GuillaumeFist3/";
                string fileName = openFileDialog1.SafeFileName;
                try
                {
                    System.IO.File.Copy(path, path2 + fileName, true);
 //                   File.Move(path, path2+fileName);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("The process failed: {0}", ex.ToString());
                }
            }
        }
    }
}
