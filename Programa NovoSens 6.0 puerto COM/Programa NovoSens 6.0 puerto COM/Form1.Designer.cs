namespace Programa_NovoSens_6._0_puerto_COM
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.BotBuscarPuerto = new System.Windows.Forms.Button();
            this.ComboPuertos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SpPuertos = new System.IO.Ports.SerialPort(this.components);
            this.BotAbrirPuerto = new System.Windows.Forms.Button();
            this.comboBaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DatosRecibidos = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BotBuscarPuerto
            // 
            this.BotBuscarPuerto.Location = new System.Drawing.Point(513, 94);
            this.BotBuscarPuerto.Name = "BotBuscarPuerto";
            this.BotBuscarPuerto.Size = new System.Drawing.Size(75, 23);
            this.BotBuscarPuerto.TabIndex = 0;
            this.BotBuscarPuerto.Text = "BUSCAR";
            this.BotBuscarPuerto.UseVisualStyleBackColor = true;
            this.BotBuscarPuerto.Click += new System.EventHandler(this.BotBuscarPuerto_Click);
            // 
            // ComboPuertos
            // 
            this.ComboPuertos.FormattingEnabled = true;
            this.ComboPuertos.Location = new System.Drawing.Point(187, 95);
            this.ComboPuertos.Name = "ComboPuertos";
            this.ComboPuertos.Size = new System.Drawing.Size(299, 21);
            this.ComboPuertos.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "PUERTO COM";
            // 
            // SpPuertos
            // 
            this.SpPuertos.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Datorecibido);
            // 
            // BotAbrirPuerto
            // 
            this.BotAbrirPuerto.Location = new System.Drawing.Point(643, 95);
            this.BotAbrirPuerto.Name = "BotAbrirPuerto";
            this.BotAbrirPuerto.Size = new System.Drawing.Size(75, 23);
            this.BotAbrirPuerto.TabIndex = 4;
            this.BotAbrirPuerto.Text = "ABRIR";
            this.BotAbrirPuerto.UseVisualStyleBackColor = true;
            this.BotAbrirPuerto.Click += new System.EventHandler(this.BotAbrirPuerto_Click);
            // 
            // comboBaudRate
            // 
            this.comboBaudRate.FormattingEnabled = true;
            this.comboBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "115200"});
            this.comboBaudRate.Location = new System.Drawing.Point(187, 175);
            this.comboBaudRate.Name = "comboBaudRate";
            this.comboBaudRate.Size = new System.Drawing.Size(121, 21);
            this.comboBaudRate.TabIndex = 5;
            this.comboBaudRate.Text = "115200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "BAUD RATE";
            // 
            // DatosRecibidos
            // 
            this.DatosRecibidos.FormattingEnabled = true;
            this.DatosRecibidos.Location = new System.Drawing.Point(187, 280);
            this.DatosRecibidos.Name = "DatosRecibidos";
            this.DatosRecibidos.Size = new System.Drawing.Size(401, 134);
            this.DatosRecibidos.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "DATOS RECIBIDOS";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DatosRecibidos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBaudRate);
            this.Controls.Add(this.BotAbrirPuerto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComboPuertos);
            this.Controls.Add(this.BotBuscarPuerto);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BotBuscarPuerto;
        private System.Windows.Forms.ComboBox ComboPuertos;
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort SpPuertos;
        private System.Windows.Forms.Button BotAbrirPuerto;
        private System.Windows.Forms.ComboBox comboBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox DatosRecibidos;
        private System.Windows.Forms.Label label3;
    }
}

