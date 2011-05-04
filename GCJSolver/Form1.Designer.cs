namespace GCJSolver
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.tbClassName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.solve = new System.Windows.Forms.Button();
			this.lblInputFile = new System.Windows.Forms.Label();
			this.lblOutputFile = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 77);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Input file";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(12, 120);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Output file";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// tbClassName
			// 
			this.tbClassName.Location = new System.Drawing.Point(103, 12);
			this.tbClassName.Name = "tbClassName";
			this.tbClassName.Size = new System.Drawing.Size(250, 20);
			this.tbClassName.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Class name";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 244);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(472, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// solve
			// 
			this.solve.Location = new System.Drawing.Point(12, 164);
			this.solve.Name = "solve";
			this.solve.Size = new System.Drawing.Size(75, 23);
			this.solve.TabIndex = 5;
			this.solve.Text = "Solve";
			this.solve.UseVisualStyleBackColor = true;
			this.solve.Click += new System.EventHandler(this.solve_Click);
			// 
			// lblInputFile
			// 
			this.lblInputFile.AutoSize = true;
			this.lblInputFile.Location = new System.Drawing.Point(100, 82);
			this.lblInputFile.Name = "lblInputFile";
			this.lblInputFile.Size = new System.Drawing.Size(0, 13);
			this.lblInputFile.TabIndex = 6;
			// 
			// lblOutputFile
			// 
			this.lblOutputFile.AutoSize = true;
			this.lblOutputFile.Location = new System.Drawing.Point(100, 125);
			this.lblOutputFile.Name = "lblOutputFile";
			this.lblOutputFile.Size = new System.Drawing.Size(0, 13);
			this.lblOutputFile.TabIndex = 7;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(472, 266);
			this.Controls.Add(this.lblOutputFile);
			this.Controls.Add(this.lblInputFile);
			this.Controls.Add(this.solve);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbClassName);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "GCJ Solver";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox tbClassName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Button solve;
		private System.Windows.Forms.Label lblInputFile;
		private System.Windows.Forms.Label lblOutputFile;
	}
}

