
namespace BDSqlPostGres.View
{
    partial class FrmBDConfig
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnBuscaPasta = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtPastaPadraoBkp = new System.Windows.Forms.TextBox();
            this.BtnTestar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtSenha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtBanco = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.BtnSalvar = new System.Windows.Forms.Button();
            this.TxtServidor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtPorta = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.TxtPorta);
            this.panel1.Controls.Add(this.BtnBuscaPasta);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtPastaPadraoBkp);
            this.panel1.Controls.Add(this.BtnTestar);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.TxtSenha);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TxtBanco);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TxtUsuario);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BtnCancelar);
            this.panel1.Controls.Add(this.BtnSalvar);
            this.panel1.Controls.Add(this.TxtServidor);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 422);
            this.panel1.TabIndex = 1;
            // 
            // BtnBuscaPasta
            // 
            this.BtnBuscaPasta.BackColor = System.Drawing.Color.Gainsboro;
            this.BtnBuscaPasta.Location = new System.Drawing.Point(289, 286);
            this.BtnBuscaPasta.Margin = new System.Windows.Forms.Padding(2);
            this.BtnBuscaPasta.Name = "BtnBuscaPasta";
            this.BtnBuscaPasta.Size = new System.Drawing.Size(57, 26);
            this.BtnBuscaPasta.TabIndex = 42;
            this.BtnBuscaPasta.Text = "Buscar";
            this.BtnBuscaPasta.UseVisualStyleBackColor = false;
            this.BtnBuscaPasta.Click += new System.EventHandler(this.BtnBuscaPasta_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(4, 237);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 16);
            this.label5.TabIndex = 41;
            this.label5.Text = "Local Padrão para Salvar Backup:";
            // 
            // TxtPastaPadraoBkp
            // 
            this.TxtPastaPadraoBkp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPastaPadraoBkp.Location = new System.Drawing.Point(5, 262);
            this.TxtPastaPadraoBkp.Margin = new System.Windows.Forms.Padding(2);
            this.TxtPastaPadraoBkp.Multiline = true;
            this.TxtPastaPadraoBkp.Name = "TxtPastaPadraoBkp";
            this.TxtPastaPadraoBkp.Size = new System.Drawing.Size(341, 20);
            this.TxtPastaPadraoBkp.TabIndex = 40;
            // 
            // BtnTestar
            // 
            this.BtnTestar.BackColor = System.Drawing.Color.Gainsboro;
            this.BtnTestar.Location = new System.Drawing.Point(55, 365);
            this.BtnTestar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnTestar.Name = "BtnTestar";
            this.BtnTestar.Size = new System.Drawing.Size(105, 46);
            this.BtnTestar.TabIndex = 39;
            this.BtnTestar.Text = "Teste Conexão";
            this.BtnTestar.UseVisualStyleBackColor = false;
            this.BtnTestar.Click += new System.EventHandler(this.BtnTestar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 167);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 16);
            this.label4.TabIndex = 38;
            this.label4.Text = "Senha";
            // 
            // TxtSenha
            // 
            this.TxtSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtSenha.Location = new System.Drawing.Point(5, 189);
            this.TxtSenha.Margin = new System.Windows.Forms.Padding(2);
            this.TxtSenha.Multiline = true;
            this.TxtSenha.Name = "TxtSenha";
            this.TxtSenha.PasswordChar = '*';
            this.TxtSenha.Size = new System.Drawing.Size(157, 20);
            this.TxtSenha.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "Banco de Dados";
            // 
            // TxtBanco
            // 
            this.TxtBanco.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtBanco.Location = new System.Drawing.Point(5, 79);
            this.TxtBanco.Margin = new System.Windows.Forms.Padding(2);
            this.TxtBanco.Multiline = true;
            this.TxtBanco.Name = "TxtBanco";
            this.TxtBanco.Size = new System.Drawing.Size(341, 20);
            this.TxtBanco.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 34;
            this.label2.Text = "Usuário";
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtUsuario.Location = new System.Drawing.Point(5, 135);
            this.TxtUsuario.Margin = new System.Windows.Forms.Padding(2);
            this.TxtUsuario.Multiline = true;
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(157, 20);
            this.TxtUsuario.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Servidor";
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.BackColor = System.Drawing.Color.Gainsboro;
            this.BtnCancelar.Location = new System.Drawing.Point(260, 365);
            this.BtnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(86, 46);
            this.BtnCancelar.TabIndex = 30;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = false;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnSalvar
            // 
            this.BtnSalvar.BackColor = System.Drawing.Color.Gainsboro;
            this.BtnSalvar.Location = new System.Drawing.Point(168, 365);
            this.BtnSalvar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSalvar.Name = "BtnSalvar";
            this.BtnSalvar.Size = new System.Drawing.Size(88, 46);
            this.BtnSalvar.TabIndex = 31;
            this.BtnSalvar.Text = "Salvar";
            this.BtnSalvar.UseVisualStyleBackColor = false;
            this.BtnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // TxtServidor
            // 
            this.TxtServidor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtServidor.Location = new System.Drawing.Point(5, 28);
            this.TxtServidor.Margin = new System.Windows.Forms.Padding(2);
            this.TxtServidor.Multiline = true;
            this.TxtServidor.Name = "TxtServidor";
            this.TxtServidor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtServidor.Size = new System.Drawing.Size(341, 20);
            this.TxtServidor.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(188, 111);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 44;
            this.label6.Text = "Porta";
            // 
            // TxtPorta
            // 
            this.TxtPorta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPorta.Location = new System.Drawing.Point(189, 135);
            this.TxtPorta.Margin = new System.Windows.Forms.Padding(2);
            this.TxtPorta.Multiline = true;
            this.TxtPorta.Name = "TxtPorta";
            this.TxtPorta.Size = new System.Drawing.Size(131, 20);
            this.TxtPorta.TabIndex = 43;
            // 
            // FrmBDConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(362, 438);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmBDConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuração do Banco de dados:";
            this.Load += new System.EventHandler(this.FrmBDConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnBuscaPasta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtPastaPadraoBkp;
        private System.Windows.Forms.Button BtnTestar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtSenha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtBanco;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.Button BtnSalvar;
        private System.Windows.Forms.TextBox TxtServidor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtPorta;
    }
}