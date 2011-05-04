using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace GCJSolver
{
	public partial class Form1 : Form
	{
		private string InputFile;
		private string OutputFile;

		public Form1()
		{
			InitializeComponent();

			statusStrip1.Text = "Ready";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				InputFile = lblInputFile.Text = openFileDialog1.FileName;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				OutputFile = lblOutputFile.Text = saveFileDialog1.FileName;
			}
		}

		private void solve_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(InputFile) 
				|| String.IsNullOrEmpty(OutputFile)
				|| String.IsNullOrEmpty(tbClassName.Text))
			{
				statusStrip1.Text = "Invalid inputs";
				return;
			}

			statusStrip1.Text = "Processing...";


		}
	}
}
