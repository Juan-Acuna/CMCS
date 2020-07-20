namespace CMCS
{
    partial class CMC
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEntrada = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkClases = new System.Windows.Forms.CheckBox();
            this.chkCM = new System.Windows.Forms.CheckBox();
            this.btnSalida = new System.Windows.Forms.Button();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbLenguaje = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.txtEntrada = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPCM = new System.Windows.Forms.Button();
            this.btnPClases = new System.Windows.Forms.Button();
            this.chkIClases = new System.Windows.Forms.CheckBox();
            this.lbMotor = new System.Windows.Forms.Label();
            this.txtSalida = new System.Windows.Forms.TextBox();
            this.cbMotor = new System.Windows.Forms.ComboBox();
            this.cbLenguaje = new System.Windows.Forms.ComboBox();
            this.ofdEntrada = new System.Windows.Forms.OpenFileDialog();
            this.fbdSalida = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEntrada
            // 
            this.btnEntrada.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrada.Location = new System.Drawing.Point(311, 56);
            this.btnEntrada.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEntrada.Name = "btnEntrada";
            this.btnEntrada.Size = new System.Drawing.Size(75, 27);
            this.btnEntrada.TabIndex = 0;
            this.btnEntrada.Text = "Examinar";
            this.btnEntrada.UseVisualStyleBackColor = true;
            this.btnEntrada.Click += new System.EventHandler(this.btnEntrada_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Entrada";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(417, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Salida";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Archivo de origen";
            // 
            // chkClases
            // 
            this.chkClases.AutoSize = true;
            this.chkClases.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClases.Location = new System.Drawing.Point(20, 113);
            this.chkClases.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkClases.Name = "chkClases";
            this.chkClases.Size = new System.Drawing.Size(111, 24);
            this.chkClases.TabIndex = 4;
            this.chkClases.Text = "Crear Clases";
            this.chkClases.UseVisualStyleBackColor = true;
            this.chkClases.CheckedChanged += new System.EventHandler(this.chkClases_CheckedChanged);
            // 
            // chkCM
            // 
            this.chkCM.AutoSize = true;
            this.chkCM.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCM.Location = new System.Drawing.Point(20, 204);
            this.chkCM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCM.Name = "chkCM";
            this.chkCM.Size = new System.Drawing.Size(213, 24);
            this.chkCM.TabIndex = 5;
            this.chkCM.Text = "Crear \"CommandManager\"";
            this.chkCM.UseVisualStyleBackColor = true;
            this.chkCM.CheckedChanged += new System.EventHandler(this.chkCM_CheckedChanged);
            // 
            // btnSalida
            // 
            this.btnSalida.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalida.Location = new System.Drawing.Point(314, 56);
            this.btnSalida.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(75, 27);
            this.btnSalida.TabIndex = 6;
            this.btnSalida.Text = "Examinar";
            this.btnSalida.UseVisualStyleBackColor = true;
            this.btnSalida.Click += new System.EventHandler(this.btnSalida_Click);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEjecutar.Location = new System.Drawing.Point(669, 394);
            this.btnEjecutar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(75, 31);
            this.btnEjecutar.TabIndex = 7;
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(750, 394);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 31);
            this.btnSalir.TabIndex = 8;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Carpeta de destino";
            // 
            // lbLenguaje
            // 
            this.lbLenguaje.AutoSize = true;
            this.lbLenguaje.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLenguaje.Location = new System.Drawing.Point(44, 152);
            this.lbLenguaje.Name = "lbLenguaje";
            this.lbLenguaje.Size = new System.Drawing.Size(71, 20);
            this.lbLenguaje.TabIndex = 10;
            this.lbLenguaje.Text = "Lenguaje";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.txtEntrada);
            this.panel1.Controls.Add(this.btnEntrada);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 58);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 369);
            this.panel1.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtConsole);
            this.panel3.Location = new System.Drawing.Point(18, 113);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(368, 236);
            this.panel3.TabIndex = 6;
            // 
            // txtConsole
            // 
            this.txtConsole.AcceptsReturn = true;
            this.txtConsole.BackColor = System.Drawing.Color.MidnightBlue;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.GreenYellow;
            this.txtConsole.Location = new System.Drawing.Point(0, 0);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(366, 234);
            this.txtConsole.TabIndex = 5;
            this.txtConsole.Visible = false;
            // 
            // txtEntrada
            // 
            this.txtEntrada.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrada.Location = new System.Drawing.Point(18, 59);
            this.txtEntrada.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.ReadOnly = true;
            this.txtEntrada.Size = new System.Drawing.Size(287, 24);
            this.txtEntrada.TabIndex = 4;
            this.txtEntrada.TabStop = false;
            this.txtEntrada.TextChanged += new System.EventHandler(this.txtEntrada_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnPCM);
            this.panel2.Controls.Add(this.btnPClases);
            this.panel2.Controls.Add(this.chkIClases);
            this.panel2.Controls.Add(this.lbMotor);
            this.panel2.Controls.Add(this.txtSalida);
            this.panel2.Controls.Add(this.cbMotor);
            this.panel2.Controls.Add(this.cbLenguaje);
            this.panel2.Controls.Add(this.chkClases);
            this.panel2.Controls.Add(this.lbLenguaje);
            this.panel2.Controls.Add(this.chkCM);
            this.panel2.Controls.Add(this.btnSalida);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(422, 58);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(403, 312);
            this.panel2.TabIndex = 12;
            // 
            // btnPCM
            // 
            this.btnPCM.Enabled = false;
            this.btnPCM.Location = new System.Drawing.Point(263, 248);
            this.btnPCM.Name = "btnPCM";
            this.btnPCM.Size = new System.Drawing.Size(115, 23);
            this.btnPCM.TabIndex = 18;
            this.btnPCM.Text = "Preferencias";
            this.btnPCM.UseVisualStyleBackColor = true;
            // 
            // btnPClases
            // 
            this.btnPClases.Enabled = false;
            this.btnPClases.Location = new System.Drawing.Point(263, 149);
            this.btnPClases.Name = "btnPClases";
            this.btnPClases.Size = new System.Drawing.Size(115, 23);
            this.btnPClases.TabIndex = 17;
            this.btnPClases.Text = "Preferencias";
            this.btnPClases.UseVisualStyleBackColor = true;
            // 
            // chkIClases
            // 
            this.chkIClases.AutoSize = true;
            this.chkIClases.Location = new System.Drawing.Point(263, 115);
            this.chkIClases.Name = "chkIClases";
            this.chkIClases.Size = new System.Drawing.Size(145, 24);
            this.chkIClases.TabIndex = 15;
            this.chkIClases.Text = "Codigo Identado";
            this.chkIClases.UseVisualStyleBackColor = true;
            // 
            // lbMotor
            // 
            this.lbMotor.AutoSize = true;
            this.lbMotor.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMotor.Location = new System.Drawing.Point(44, 251);
            this.lbMotor.Name = "lbMotor";
            this.lbMotor.Size = new System.Drawing.Size(102, 20);
            this.lbMotor.TabIndex = 14;
            this.lbMotor.Text = "Base de datos";
            // 
            // txtSalida
            // 
            this.txtSalida.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalida.Location = new System.Drawing.Point(20, 59);
            this.txtSalida.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.ReadOnly = true;
            this.txtSalida.Size = new System.Drawing.Size(288, 24);
            this.txtSalida.TabIndex = 13;
            this.txtSalida.TabStop = false;
            // 
            // cbMotor
            // 
            this.cbMotor.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMotor.FormattingEnabled = true;
            this.cbMotor.Items.AddRange(new object[] {
            "Oracle",
            "SQL Server",
            "MySQL",
            "SQLite"});
            this.cbMotor.Location = new System.Drawing.Point(130, 248);
            this.cbMotor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMotor.Name = "cbMotor";
            this.cbMotor.Size = new System.Drawing.Size(119, 27);
            this.cbMotor.TabIndex = 12;
            this.cbMotor.Text = "Seleccione motor";
            // 
            // cbLenguaje
            // 
            this.cbLenguaje.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLenguaje.FormattingEnabled = true;
            this.cbLenguaje.Items.AddRange(new object[] {
            "C#",
            "JAVA",
            "Python3"});
            this.cbLenguaje.Location = new System.Drawing.Point(130, 149);
            this.cbLenguaje.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbLenguaje.Name = "cbLenguaje";
            this.cbLenguaje.Size = new System.Drawing.Size(119, 27);
            this.cbLenguaje.TabIndex = 11;
            this.cbLenguaje.Text = "Seleccione lenguaje";
            // 
            // ofdEntrada
            // 
            this.ofdEntrada.Filter = "Archivos de comandos SQL (*.sql, *.txt)|*.sql;*.txt|Todos los archivos|*.*";
            // 
            // fbdSalida
            // 
            this.fbdSalida.Description = "Seleccione carpeta de destino";
            // 
            // CMC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 438);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEjecutar);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CMC";
            this.Text = "Command Manager Creator";
            this.Load += new System.EventHandler(this.CMC_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEntrada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkClases;
        private System.Windows.Forms.CheckBox chkCM;
        private System.Windows.Forms.Button btnSalida;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbLenguaje;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtEntrada;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbMotor;
        private System.Windows.Forms.TextBox txtSalida;
        private System.Windows.Forms.ComboBox cbMotor;
        private System.Windows.Forms.ComboBox cbLenguaje;
        private System.Windows.Forms.CheckBox chkIClases;
        private System.Windows.Forms.OpenFileDialog ofdEntrada;
        private System.Windows.Forms.FolderBrowserDialog fbdSalida;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btnPCM;
        private System.Windows.Forms.Button btnPClases;
        private System.Windows.Forms.Panel panel3;
    }
}

