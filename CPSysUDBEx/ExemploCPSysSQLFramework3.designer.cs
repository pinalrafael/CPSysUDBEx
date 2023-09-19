namespace CPSysUDBEx
{
    partial class ExemploCPSysSQLFramework3
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
            this.chbUseFun = new System.Windows.Forms.CheckBox();
            this.lblData = new System.Windows.Forms.Label();
            this.gvLista = new System.Windows.Forms.DataGridView();
            this.btnDeletar = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUpdateName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.txtAddNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNovo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvLista)).BeginInit();
            this.SuspendLayout();
            // 
            // chbUseFun
            // 
            this.chbUseFun.AutoSize = true;
            this.chbUseFun.Location = new System.Drawing.Point(17, 74);
            this.chbUseFun.Name = "chbUseFun";
            this.chbUseFun.Size = new System.Drawing.Size(87, 17);
            this.chbUseFun.TabIndex = 28;
            this.chbUseFun.Text = "Usar Função";
            this.chbUseFun.UseVisualStyleBackColor = true;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(454, 139);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(0, 13);
            this.lblData.TabIndex = 27;
            // 
            // gvLista
            // 
            this.gvLista.AllowUserToAddRows = false;
            this.gvLista.AllowUserToDeleteRows = false;
            this.gvLista.AllowUserToResizeColumns = false;
            this.gvLista.AllowUserToResizeRows = false;
            this.gvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLista.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvLista.Location = new System.Drawing.Point(17, 165);
            this.gvLista.MultiSelect = false;
            this.gvLista.Name = "gvLista";
            this.gvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvLista.Size = new System.Drawing.Size(771, 275);
            this.gvLista.TabIndex = 26;
            this.gvLista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLista_CellClick);
            // 
            // btnDeletar
            // 
            this.btnDeletar.Location = new System.Drawing.Point(607, 118);
            this.btnDeletar.Name = "btnDeletar";
            this.btnDeletar.Size = new System.Drawing.Size(75, 23);
            this.btnDeletar.TabIndex = 25;
            this.btnDeletar.Text = "Deletar";
            this.btnDeletar.UseVisualStyleBackColor = true;
            this.btnDeletar.Click += new System.EventHandler(this.btnDeletar_Click);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(494, 40);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 13);
            this.lblId.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(454, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "ID";
            // 
            // txtUpdateName
            // 
            this.txtUpdateName.Location = new System.Drawing.Point(497, 63);
            this.txtUpdateName.Name = "txtUpdateName";
            this.txtUpdateName.Size = new System.Drawing.Size(185, 20);
            this.txtUpdateName.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(454, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Nome";
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Location = new System.Drawing.Point(607, 89);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btnAtualizar.TabIndex = 20;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // txtAddNome
            // 
            this.txtAddNome.Location = new System.Drawing.Point(55, 44);
            this.txtAddNome.Name = "txtAddNome";
            this.txtAddNome.Size = new System.Drawing.Size(185, 20);
            this.txtAddNome.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nome";
            // 
            // btnNovo
            // 
            this.btnNovo.Location = new System.Drawing.Point(165, 70);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(75, 23);
            this.btnNovo.TabIndex = 17;
            this.btnNovo.Text = "Adicionar";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(452, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(336, 29);
            this.label2.TabIndex = 16;
            this.label2.Text = "REGISTRO SELECIONADO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 29);
            this.label1.TabIndex = 15;
            this.label1.Text = "NOVO REGISTRO";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(14, 128);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 29;
            // 
            // ExemploCPSysSQLFramework3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.chbUseFun);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.gvLista);
            this.Controls.Add(this.btnDeletar);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUpdateName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.txtAddNome);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ExemploCPSysSQLFramework3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExemploCPSysSQLFramework3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExemploCPSysSQLFramework3_FormClosing);
            this.Load += new System.EventHandler(this.ExemploCPSysSQLFramework3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvLista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbUseFun;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DataGridView gvLista;
        private System.Windows.Forms.Button btnDeletar;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUpdateName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.TextBox txtAddNome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMsg;
    }
}