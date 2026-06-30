using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TaskQuest
{
    public partial class FormTarefas : Form
    {
        private DatabaseHelper db;
        private int userID;

        public FormTarefas(int userId)
        {
            InitializeComponent();
            db = new DatabaseHelper();
            userID = userId;
            ConfigurarEventos();
            CarregarTarefas();
        }

        private void ConfigurarEventos()
        {
            btnAdicionar.Click += BtnAdicionar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnConcluir.Click += BtnConcluir_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnRefresh.Click += BtnRefresh_Click;
            cbFiltro.SelectedIndexChanged += CbFiltro_SelectedIndexChanged;
        }

        private void CarregarTarefas()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    string filtro = "";
                    switch (cbFiltro.SelectedIndex)
                    {
                        case 1: 
                            filtro = "AND Status = 0";
                            break;
                        case 2:
                            filtro = "AND Status = 1";
                            break;
                    }

                    string sql = $@"SELECT TarefaID as ID, 
                                          Titulo as Título, 
                                          Descricao as Descrição,
                                          CASE Dificuldade 
                                              WHEN 1 THEN 'Fácil (10XP)'
                                              WHEN 2 THEN 'Médio (25XP)'
                                              WHEN 3 THEN 'Difícil (50XP)'
                                          END as Dificuldade,
                                          CASE Status
                                              WHEN 0 THEN '📌 Pendente'
                                              WHEN 1 THEN '✅ Concluída'
                                          END as Status,
                                          FORMAT(DataLimite, 'dd/MM/yyyy') as 'Data Limite',
                                          XP_Ganho as 'XP',
                                          Moedas_Ganhas as 'Moedas'
                                   FROM Tarefas 
                                   WHERE UserID = @UserID {filtro}
                                   ORDER BY Status ASC, DataLimite ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    DataTable dt = new DataTable();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    dgvTarefas.DataSource = dt;

                  
                    foreach (DataGridViewRow row in dgvTarefas.Rows)
                    {
                        if (row.Cells["Status"].Value != null && row.Cells["Status"].Value.ToString().Contains("Concluída"))
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else if (row.Cells["Data Limite"].Value != null && !string.IsNullOrEmpty(row.Cells["Data Limite"].Value.ToString()))
                        {
                            DateTime dataLimite;
                            if (DateTime.TryParse(row.Cells["Data Limite"].Value.ToString(), out dataLimite))
                            {
                                if (dataLimite < DateTime.Now)
                                {
                                    row.DefaultCellStyle.BackColor = Color.LightPink;
                                }
                            }
                        }
                    }


                    dt.Load(reader);
                    dgvTarefas.DataSource = dt;
                    if (dgvTarefas.Columns.Count > 0)
                    {
                        dgvTarefas.Columns["ID"].Width = 50;
                        dgvTarefas.Columns["Título"].Width = 50;
                        dgvTarefas.Columns["Descrição"].Width = 50;
                        dgvTarefas.Columns["Dificuldade"].Width = 50;
                        dgvTarefas.Columns["Status"].Width = 50;
                        dgvTarefas.Columns["Data Limite"].Width = 50;
                        dgvTarefas.Columns["XP"].Width = 50;
                        dgvTarefas.Columns["Moedas"].Width = 50;


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            FormNovaTarefa form = new FormNovaTarefa(userID);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CarregarTarefas();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTarefas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma tarefa!");
                return;
            }

            int tarefaID = Convert.ToInt32(dgvTarefas.SelectedRows[0].Cells["ID"].Value);
            string titulo = dgvTarefas.SelectedRows[0].Cells["Título"].Value.ToString();

            FormEditarTarefa form = new FormEditarTarefa(userID, tarefaID, titulo);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CarregarTarefas();
            }
        }

        private void BtnConcluir_Click(object sender, EventArgs e)
        {
            if (dgvTarefas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma tarefa!");
                return;
            }

            int tarefaID = Convert.ToInt32(dgvTarefas.SelectedRows[0].Cells["ID"].Value);
            string status = dgvTarefas.SelectedRows[0].Cells["Status"].Value.ToString();

            if (status.Contains("Concluída"))
            {
                MessageBox.Show("Tarefa já concluída!");
                return;
            }

            DialogResult result = MessageBox.Show("Concluir esta tarefa?", "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ConcluirTarefa(tarefaID);
            }
        }

        private void ConcluirTarefa(int tarefaID)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();


                    string sqlDificuldade = "SELECT Dificuldade FROM Tarefas WHERE TarefaID = @ID";
                    SqlCommand cmdDificuldade = new SqlCommand(sqlDificuldade, conn);
                    cmdDificuldade.Parameters.AddWithValue("@ID", tarefaID);
                    int dificuldade = (int)cmdDificuldade.ExecuteScalar();

                    int xp = 0, moedas = 0;
                    if (dificuldade == 1) { xp = 10; moedas = 5; }
                    else if (dificuldade == 2) { xp = 25; moedas = 15; }
                    else { xp = 50; moedas = 30; }


                    string sqlUpdate = "UPDATE Tarefas SET Status = 1, DataConclusao = @Data, XP_Ganho = @XP, Moedas_Ganhas = @Moedas WHERE TarefaID = @ID";
                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                    cmdUpdate.Parameters.AddWithValue("@Data", DateTime.Now);
                    cmdUpdate.Parameters.AddWithValue("@XP", xp);
                    cmdUpdate.Parameters.AddWithValue("@Moedas", moedas);
                    cmdUpdate.Parameters.AddWithValue("@ID", tarefaID);
                    cmdUpdate.ExecuteNonQuery();

                   
                    string sqlSelectUser = "SELECT XP_Total, Nivel FROM Utilizadores WHERE UserID = @UserID";
                    SqlCommand cmdSelectUser = new SqlCommand(sqlSelectUser, conn);
                    cmdSelectUser.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = cmdSelectUser.ExecuteReader();
                    int xpAtual = 0;
                    int nivelAtual = 0;

                    if (reader.Read())
                    {
                        xpAtual = Convert.ToInt32(reader["XP_Total"]);
                        nivelAtual = Convert.ToInt32(reader["Nivel"]);
                    }
                    reader.Close();

               
                    int novoXP = xpAtual + xp;
                    int novoNivel = (novoXP / 100) + 1;
                    bool subiuNivel = (novoNivel > nivelAtual);

               
                    string sqlUpdateUser = "UPDATE Utilizadores SET XP_Total = @XP, Moeda_Total = Moeda_Total + @Moedas, Nivel = @Nivel WHERE UserID = @UserID";
                    SqlCommand cmdUpdateUser = new SqlCommand(sqlUpdateUser, conn);
                    cmdUpdateUser.Parameters.AddWithValue("@XP", novoXP);
                    cmdUpdateUser.Parameters.AddWithValue("@Moedas", moedas);
                    cmdUpdateUser.Parameters.AddWithValue("@Nivel", novoNivel);
                    cmdUpdateUser.Parameters.AddWithValue("@UserID", userID);
                    cmdUpdateUser.ExecuteNonQuery();

                    
                    string sqlProgresso = @"IF EXISTS (SELECT 1 FROM ProgressoDiario WHERE UserID = @UserID AND Data = @Data)
                                    UPDATE ProgressoDiario SET TarefasConcluidas = TarefasConcluidas + 1, XPGanhado = XPGanhado + @XP
                                    WHERE UserID = @UserID AND Data = @Data
                                    ELSE
                                    INSERT INTO ProgressoDiario (UserID, Data, TarefasConcluidas, XPGanhado) VALUES (@UserID, @Data, 1, @XP)";
                    SqlCommand cmdProgresso = new SqlCommand(sqlProgresso, conn);
                    cmdProgresso.Parameters.AddWithValue("@UserID", userID);
                    cmdProgresso.Parameters.AddWithValue("@Data", DateTime.Now.Date);
                    cmdProgresso.Parameters.AddWithValue("@XP", xp);
                    cmdProgresso.ExecuteNonQuery();

               
                    string mensagem = $"✅ Tarefa concluída!\nGanhaste {xp} XP e {moedas} moedas!";

                    if (subiuNivel)
                    {
                        mensagem += $"\n\n🎉 PARABÉNS! Subiste para o Nível {novoNivel}! 🎉";
                    }

                    MessageBox.Show(mensagem);

                    CarregarTarefas();

                   
                    if (Application.OpenForms["FormPrincipal"] is FormPrincipal principal)
                    {
                        principal.AtualizarDadosUtilizador();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTarefas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma tarefa!");
                return;
            }

            DialogResult result = MessageBox.Show("Eliminar tarefa?", "Confirmar", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int tarefaID = Convert.ToInt32(dgvTarefas.SelectedRows[0].Cells["ID"].Value);

                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM Tarefas WHERE TarefaID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", tarefaID);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Tarefa eliminada!");
                CarregarTarefas();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            CarregarTarefas();
        }

        private void CbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarTarefas();
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {

        }

        private void cbFiltro_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void FormTarefas_Load(object sender, EventArgs e)
        {


        }

        private void dgvTarefas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


    public partial class FormNovaTarefa : Form
    {
        private DatabaseHelper db;
        private int userID;
        private TextBox txtTitulo, txtDescricao;
        private ComboBox cbDificuldade;
        private DateTimePicker dtpDataLimite;

        public FormNovaTarefa(int userId)
        {
            userID = userId;
            db = new DatabaseHelper();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "➕ Nova Tarefa";
            this.Size = new System.Drawing.Size(450, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label lblTituloForm = new Label() { Text = "Nova Tarefa", Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(20, 20), Size = new Size(400, 40), TextAlign = ContentAlignment.MiddleCenter };
            Label lblTitulo = new Label() { Text = "Título:", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(30, 80), Size = new Size(80, 25) };
            txtTitulo = new TextBox() { Font = new Font("Segoe UI", 10), Location = new Point(30, 110), Size = new Size(380, 30) };
            Label lblDescricao = new Label() { Text = "Descrição:", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(30, 155), Size = new Size(80, 25) };
            txtDescricao = new TextBox() { Font = new Font("Segoe UI", 10), Location = new Point(30, 185), Size = new Size(380, 70), Multiline = true };
            Label lblDificuldade = new Label() { Text = "Dificuldade:", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(30, 270), Size = new Size(80, 25) };
            cbDificuldade = new ComboBox() { Font = new Font("Segoe UI", 10), Location = new Point(120, 268), Size = new Size(150, 31), DropDownStyle = ComboBoxStyle.DropDownList };
            cbDificuldade.Items.AddRange(new[] { "Fácil (10 XP)", "Médio (25 XP)", "Difícil (50 XP)" });
            cbDificuldade.SelectedIndex = 0;
            Label lblData = new Label() { Text = "Data Limite:", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(30, 310), Size = new Size(80, 25) };
            dtpDataLimite = new DateTimePicker() { Font = new Font("Segoe UI", 10), Location = new Point(120, 308), Size = new Size(150, 30), Format = DateTimePickerFormat.Short, MinDate = DateTime.Now };
            Button btnSalvar = new Button() { Text = "✅ Salvar", BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(100, 360), Size = new Size(120, 40) };
            btnSalvar.Click += BtnSalvar_Click;
            Button btnCancelar = new Button() { Text = "❌ Cancelar", BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(240, 360), Size = new Size(120, 40) };
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lblTituloForm);
            Controls.Add(lblTitulo);
            Controls.Add(txtTitulo);
            Controls.Add(lblDescricao);
            Controls.Add(txtDescricao);
            Controls.Add(lblDificuldade);
            Controls.Add(cbDificuldade);
            Controls.Add(lblData);
            Controls.Add(dtpDataLimite);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                MessageBox.Show("Insira um título!");
                return;
            }

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = "INSERT INTO Tarefas (UserID, Titulo, Descricao, Dificuldade, DataLimite) VALUES (@UserID, @Titulo, @Descricao, @Dificuldade, @Data)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                cmd.Parameters.AddWithValue("@Descricao", txtDescricao.Text);
                cmd.Parameters.AddWithValue("@Dificuldade", cbDificuldade.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@Data", dtpDataLimite.Value);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Tarefa adicionada!");
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }

  
    public partial class FormEditarTarefa : Form
    {
        private DatabaseHelper db;
        private int userID;
        private int tarefaID;
        private TextBox txtTitulo;
        private TextBox txtDescricao;

        public FormEditarTarefa(int userId, int tarefaId, string titulo)
        {
            userID = userId;
            tarefaID = tarefaId;
            db = new DatabaseHelper();
            InitializeComponent();
            txtTitulo.Text = titulo;
            CarregarDescricao();
        }

        private void InitializeComponent()
        {
            this.Text = "✏️ Editar Tarefa";
            this.Size = new System.Drawing.Size(450, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;


            Label lblTituloForm = new Label() { Text = "Editar Tarefa", Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(20, 20), Size = new Size(400, 40), TextAlign = ContentAlignment.MiddleCenter };
            Label lblTitulo = new Label() { Text = "Título:", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(30, 80), Size = new Size(80, 25) };
            txtTitulo = new TextBox() { Font = new Font("Segoe UI", 10), Location = new Point(30, 110), Size = new Size(380, 30) };
            Label lblDescricao = new Label() { Text = "Descrição:", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(30, 155), Size = new Size(80, 25) };
            txtDescricao = new TextBox() { Font = new Font("Segoe UI", 10), Location = new Point(30, 185), Size = new Size(380, 70), Multiline = true };
            Button btnSalvar = new Button() { Text = "💾 Salvar", BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(100, 280), Size = new Size(120, 40) };
            btnSalvar.Click += BtnSalvar_Click;
            Button btnCancelar = new Button() { Text = "❌ Cancelar", BackColor = Color.FromArgb(149, 165, 166), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(240, 280), Size = new Size(120, 40) };
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lblTituloForm);
            Controls.Add(lblTitulo);
            Controls.Add(txtTitulo);
            Controls.Add(lblDescricao);
            Controls.Add(txtDescricao);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
        }

        private void CarregarDescricao()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = "SELECT Descricao FROM Tarefas WHERE TarefaID = @ID AND UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", tarefaID);
                cmd.Parameters.AddWithValue("@UserID", userID);
                txtDescricao.Text = cmd.ExecuteScalar()?.ToString();
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                MessageBox.Show("Insira um título!");
                return;
            }

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE Tarefas SET Titulo = @Titulo, Descricao = @Descricao WHERE TarefaID = @ID AND UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                cmd.Parameters.AddWithValue("@Descricao", txtDescricao.Text);
                cmd.Parameters.AddWithValue("@ID", tarefaID);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Tarefa atualizada!");
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}