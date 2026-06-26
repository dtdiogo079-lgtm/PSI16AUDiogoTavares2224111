using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskQuest
{
    public partial class FormMemoria : Form
    {
        public int Pontuacao { get; private set; }
        private Button[] botoes;
        private int[] valores;
        private bool[] revelado;
        private int primeiroIndex = -1;
        private int segundoIndex = -1;
        private int paresEncontrados = 0;
        private bool aguardar = false;
        private int tentativas = 0;
        private Label lblTentativas;
        private Label lblPares;
        private Timer timer;

        public FormMemoria()
        {
            Pontuacao = 0;
            this.Text = "🧠 Jogo da Memória";
            this.Size = new System.Drawing.Size(500, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.Beige;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            ConfigurarJogo();
        }

        private void ConfigurarJogo()
        {
            lblTentativas = new Label();
            lblTentativas.Text = "Tentativas: 0";
            lblTentativas.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTentativas.Location = new Point(20, 20);
            lblTentativas.Size = new Size(150, 30);
            this.Controls.Add(lblTentativas);

            lblPares = new Label();
            lblPares.Text = "Pares: 0/8";
            lblPares.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblPares.ForeColor = Color.Green;
            lblPares.Location = new Point(300, 20);
            lblPares.Size = new Size(150, 30);
            this.Controls.Add(lblPares);

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            IniciarJogo();
        }

        private void IniciarJogo()
        {
            paresEncontrados = 0;
            tentativas = 0;
            primeiroIndex = -1;
            segundoIndex = -1;

            lblTentativas.Text = $"Tentativas: {tentativas}";
            lblPares.Text = $"Pares: {paresEncontrados}/8";

            valores = new int[16];
            int[] numeros = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
            Random rand = new Random();
            for (int i = 0; i < valores.Length; i++)
            {
                int j = rand.Next(i, valores.Length);
                int temp = numeros[i];
                numeros[i] = numeros[j];
                numeros[j] = temp;
                valores[i] = numeros[i];
            }

            revelado = new bool[16];
            botoes = new Button[16];

            int x = 30, y = 70;
            for (int i = 0; i < 16; i++)
            {
                botoes[i] = new Button();
                botoes[i].Size = new System.Drawing.Size(90, 90);
                botoes[i].Location = new System.Drawing.Point(x, y);
                botoes[i].Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
                botoes[i].FlatStyle = FlatStyle.Flat;
                botoes[i].BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
                botoes[i].ForeColor = System.Drawing.Color.White;
                botoes[i].Text = "?";
                botoes[i].Tag = i;
                botoes[i].Click += Carta_Click;
                this.Controls.Add(botoes[i]);

                x += 100;
                if ((i + 1) % 4 == 0)
                {
                    x = 30;
                    y += 110;
                }
            }

            Button btnSair = new Button();
            btnSair.Text = "❌ Sair";
            btnSair.BackColor = System.Drawing.Color.Red;
            btnSair.ForeColor = System.Drawing.Color.White;
            btnSair.FlatStyle = FlatStyle.Flat;
            btnSair.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSair.Size = new System.Drawing.Size(100, 35);
            btnSair.Location = new System.Drawing.Point(180, 510);
            btnSair.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; Close(); };
            this.Controls.Add(btnSair);
        }

        private void Carta_Click(object sender, EventArgs e)
        {
            if (aguardar) return;

            Button btn = sender as Button;
            int index = (int)btn.Tag;

            if (revelado[index]) return;
            if (primeiroIndex == index) return;
            if (segundoIndex != -1) return;

            MostrarCarta(index);

            if (primeiroIndex == -1)
            {
                primeiroIndex = index;
            }
            else if (segundoIndex == -1 && primeiroIndex != index)
            {
                segundoIndex = index;
                tentativas++;
                lblTentativas.Text = $"Tentativas: {tentativas}";

                if (valores[primeiroIndex] == valores[segundoIndex])
                {
                    paresEncontrados++;
                    lblPares.Text = $"Pares: {paresEncontrados}/8";
                    revelado[primeiroIndex] = true;
                    revelado[segundoIndex] = true;
                    primeiroIndex = -1;
                    segundoIndex = -1;

                    if (paresEncontrados == 8)
                    {
                        Pontuacao = Math.Max(50 - tentativas, 10);
                        MessageBox.Show($"🎉 Parabéns!\n\nTentativas: {tentativas}\nPontuação: {Pontuacao}", "Vitória!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    aguardar = true;
                    timer.Start();
                }
            }
        }

        private void MostrarCarta(int index)
        {
            string[] emojis = { "", "🐶", "🐱", "🐭", "🐹", "🐰", "🦊", "🐻", "🐼" };
            botoes[index].Text = emojis[valores[index]];
            botoes[index].BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
        }

        private void EsconderCartas()
        {
            if (primeiroIndex != -1 && segundoIndex != -1)
            {
                botoes[primeiroIndex].Text = "?";
                botoes[primeiroIndex].BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
                botoes[segundoIndex].Text = "?";
                botoes[segundoIndex].BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
                primeiroIndex = -1;
                segundoIndex = -1;
            }
            aguardar = false;
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            EsconderCartas();
        }
    }
}