using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TaskQuest
{
    public partial class FormMinijogos : Form
    {
        private DatabaseHelper db;
        private int userID;
        private int moedasAtuais;
        private Random random = new Random();
        private Label lblMoedas;

        public FormMinijogos(int userId)
        {
            db = new DatabaseHelper();
            userID = userId;
            this.Text = "🎮 Minijogos";
            this.Size = new System.Drawing.Size(750, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            CriarInterface();
            CarregarMoedas();
        }

        private void CriarInterface()
        {
            Label lblTitulo = new Label();
            lblTitulo.Text = "🎮 Minijogos";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            this.BackColor = Color.PaleGreen;
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Size = new Size(200, 41);
            this.Controls.Add(lblTitulo);

            lblMoedas = new Label();
            lblMoedas.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblMoedas.ForeColor = Color.Gold;
            lblMoedas.Location = new Point(550, 25);
            lblMoedas.Size = new Size(200, 32);
            lblMoedas.Text = "💰 Moedas: ...";
            this.Controls.Add(lblMoedas);

            Panel panelJogos = new Panel();
            panelJogos.BackColor = Color.Linen;
            panelJogos.Location = new Point(30, 80);
            panelJogos.Size = new Size(680, 400);
            this.Controls.Add(panelJogos);

            Button btnDados = new Button();
            btnDados.Text = "🎲\nDado da Sorte\n\nCusto: 5 moedas";
            btnDados.BackColor = Color.FromArgb(52, 152, 219);
            btnDados.ForeColor = Color.White;
            btnDados.FlatStyle = FlatStyle.Flat;
            btnDados.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDados.Location = new Point(40, 50);
            btnDados.Size = new Size(180, 150);
            btnDados.Click += BtnDados_Click;
            panelJogos.Controls.Add(btnDados);

            Button btnQuiz = new Button();
            btnQuiz.Text = "📚\nQuiz Rápido\n\nCusto: 10 moedas";
            btnQuiz.BackColor = Color.FromArgb(46, 204, 113);
            btnQuiz.ForeColor = Color.White;
            btnQuiz.FlatStyle = FlatStyle.Flat;
            btnQuiz.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnQuiz.Location = new Point(250, 50);
            btnQuiz.Size = new Size(180, 150);
            btnQuiz.Click += BtnQuiz_Click;
            panelJogos.Controls.Add(btnQuiz);

            Button btnMemoria = new Button();
            btnMemoria.Text = "🧠\nJogo da Memória\n\nGrátis!";
            btnMemoria.BackColor = Color.FromArgb(155, 89, 182);
            btnMemoria.ForeColor = Color.White;
            btnMemoria.FlatStyle = FlatStyle.Flat;
            btnMemoria.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnMemoria.Location = new Point(460, 50);
            btnMemoria.Size = new Size(180, 150);
            btnMemoria.Click += BtnMemoria_Click;
            panelJogos.Controls.Add(btnMemoria);
        }

        private void CarregarMoedas()
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Moeda_Total FROM Utilizadores WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        moedasAtuais = Convert.ToInt32(result);
                        lblMoedas.Text = $"💰 Moedas: {moedasAtuais}";
                    }
                    else
                    {
                        lblMoedas.Text = "💰 Moedas: 0";
                        moedasAtuais = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar moedas: " + ex.Message);
                lblMoedas.Text = "💰 Moedas: 0";
                moedasAtuais = 0;
            }
        }

        private void AtualizarMoedas(int delta)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE Utilizadores SET Moeda_Total = Moeda_Total + @Delta WHERE UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Delta", delta);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.ExecuteNonQuery();
                }
                CarregarMoedas();

               
                if (Application.OpenForms["FormPrincipal"] is FormPrincipal principal)
                {
                    principal.AtualizarDadosUtilizador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar moedas: " + ex.Message);
            }
        }

        private void GanharXP(int xp)
        {
            try
            {
                using (SqlConnection conn = db.GetConnection())
                {
                    conn.Open();

             
                    string sqlSelect = "SELECT XP_Total, Nivel FROM Utilizadores WHERE UserID = @UserID";
                    SqlCommand cmdSelect = new SqlCommand(sqlSelect, conn);
                    cmdSelect.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = cmdSelect.ExecuteReader();
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

              
                    string sqlUpdate = "UPDATE Utilizadores SET XP_Total = @XP, Nivel = @Nivel WHERE UserID = @UserID";
                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                    cmdUpdate.Parameters.AddWithValue("@XP", novoXP);
                    cmdUpdate.Parameters.AddWithValue("@Nivel", novoNivel);
                    cmdUpdate.Parameters.AddWithValue("@UserID", userID);
                    cmdUpdate.ExecuteNonQuery();

         
                    if (subiuNivel)
                    {
                        MessageBox.Show($"🎉 PARABÉNS! Subiste para o Nível {novoNivel}! 🎉",
                                        "Subida de Nível",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Asterisk);
                    }
                }

           
                if (Application.OpenForms["FormPrincipal"] is FormPrincipal principal)
                {
                    principal.AtualizarDadosUtilizador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao ganhar XP: " + ex.Message);
            }
        }

        private void BtnDados_Click(object sender, EventArgs e)
        {
            if (moedasAtuais < 5)
            {
                MessageBox.Show("Moedas insuficientes! Precisas de 5 moedas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int dado = random.Next(1, 7);
            int xp = dado * 10;
            AtualizarMoedas(-5);
            GanharXP(xp);
            MessageBox.Show($"🎲 Lançaste o dado e saiu {dado}!\n\nGanhaste {xp} XP!", "Dado da Sorte", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnQuiz_Click(object sender, EventArgs e)
        {
            if (moedasAtuais < 10)
            {
                MessageBox.Show("Moedas insuficientes! Precisas de 10 moedas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FormQuiz quiz = new FormQuiz();
            if (quiz.ShowDialog() == DialogResult.OK)
            {
                int xp = quiz.Pontuacao * 10;
                AtualizarMoedas(-10);
                GanharXP(xp);
                MessageBox.Show($"📚 Acertaste {quiz.Pontuacao} perguntas!\n\nGanhaste {xp} XP!", "Quiz Rápido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnMemoria_Click(object sender, EventArgs e)
        {
            FormMemoria memoria = new FormMemoria();
            if (memoria.ShowDialog() == DialogResult.OK)
            {
                int xp = memoria.Pontuacao + 1;
                GanharXP(xp);
                MessageBox.Show($"🧠 Parabéns!\n\nGanhaste {xp} XP!", "Jogo da Memória", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}