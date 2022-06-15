
namespace DiffEquation
{
    partial class EquationWindow
    {
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.countLabel = new System.Windows.Forms.Label();
            this.countInput = new System.Windows.Forms.NumericUpDown();
            this.textBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize) (this.graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.countInput)).BeginInit();
            this.SuspendLayout();
            // 
            // graph
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.Name = "ChartArea1";
            this.graph.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.graph.Legends.Add(legend1);
            this.graph.Location = new System.Drawing.Point(12, 12);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(995, 600);
            this.graph.TabIndex = 0;
            this.graph.Text = "chart1";
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.countLabel.Location = new System.Drawing.Point(1046, 313);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(72, 46);
            this.countLabel.TabIndex = 4;
            this.countLabel.Text = "N=";
            // 
            // countInput
            // 
            this.countInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.countInput.Location = new System.Drawing.Point(1114, 311);
            this.countInput.Maximum = new decimal(new int[] {10000, 0, 0, 0});
            this.countInput.Minimum = new decimal(new int[] {2, 0, 0, 0});
            this.countInput.Name = "countInput";
            this.countInput.Size = new System.Drawing.Size(179, 53);
            this.countInput.TabIndex = 8;
            this.countInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.countInput.Value = new decimal(new int[] {2, 0, 0, 0});
            this.countInput.ValueChanged += new System.EventHandler(this.OnInputValueChanged);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(1013, 370);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(280, 260);
            this.textBox.TabIndex = 16;
            this.textBox.Text = "";
            // 
            // EquationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1305, 642);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.countInput);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.graph);
            this.Name = "EquationWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Elfimov_DiffEquation";
            ((System.ComponentModel.ISupportInitialize) (this.graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.countInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.RichTextBox textBox;

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart graph;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.NumericUpDown countInput;
    }
}

