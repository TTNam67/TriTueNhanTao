﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BFS
{
    public partial class Form1 : Form
    {
        BreathFirstSearch _breathFS;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbBFD.Visible = false;
            lbBFDcotrongso.Visible = false;
            ptBKhongTrongSo.Visible = false;
            ptbCoTrongSO.Visible = false;
            txtDinh.Enabled = false;
            txtDinhKe.Enabled = false;
            txtSoDinh.Enabled = false;
            txtTrongSo.Enabled = false;
            btnADD.Enabled = false;
            grbChucNang.Visible = false;
        }

        private void btnBFDko_Click(object sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            lbBFD.Visible = true;
            lbBFDcotrongso.Visible = false;
            txtDinh.Enabled = true;
            txtDinhKe.Enabled = true;
            txtSoDinh.Enabled = true;
            txtTrongSo.Visible= false;
            lbTrongso.Visible = false;
            ptBKhongTrongSo.Visible = true;
            ptbCoTrongSO.Visible = false;
            btnADD.Enabled = true;
            grbChucNang.Visible = true;
        }

        private void btnBFDco_Click(object sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            lbBFD.Visible = false;
            lbBFDcotrongso.Visible = true;
            txtDinh.Enabled = true;
            txtDinhKe.Enabled = true;
            txtSoDinh.Enabled = true;
            txtTrongSo.Visible = true;
            txtTrongSo.Enabled = true;
            lbTrongso.Visible = true;
            ptBKhongTrongSo.Visible = false;
            ptbCoTrongSO.Visible = true;
            btnADD.Enabled = true;
            grbChucNang.Visible = true;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "TXT files (*.txt)|*.txt"; // Adjust filter for desired file type
                openFileDialog.ShowDialog();

                if (openFileDialog.FileName != "")
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        string fileContent = reader.ReadToEnd();
                        textBox1.Text = fileContent; // Display the content in a TextBox named "textBox1"

                        string[] strings = fileContent.Split(new string[] {"\n"}, StringSplitOptions.None);
                        _breathFS = new BreathFirstSearch(strings);
                        

                        // Do something with the file content based on your purpose:
                        // - Display in a TextBox: textBox1.Text = fileContent;
                        // - Process the text
                        // - Store in a database
                    }
                }
            }



        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ptbCoTrongSO_Click(object sender, EventArgs e)
        {

        }

        private void grbLoai_Enter(object sender, EventArgs e)
        {

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            _breathFS.Solve();


            string filePath = "../../../Exportfile.txt"; // Replace with desired path and filename
            

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(_breathFS.stringToExport); // Write the data to the file
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVe_Click(object sender, EventArgs e)
        {

        }
    }
}
