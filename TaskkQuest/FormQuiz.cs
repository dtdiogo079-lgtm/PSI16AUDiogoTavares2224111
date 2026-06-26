using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskQuest
{
    public class FormQuiz : Form
    {
        public int Pontuacao { get; private set; }
        private int perguntaAtual = 0;
        private Button[] botoes;
        private Label lblPergunta;
        private Label lblNumero;
        private string[][] perguntas = new string[][]
        {
            new[] { "Qual é a capital de Portugal?", "Lisboa", "Porto", "Coimbra", "Faro", "Lisboa" },
            new[] { "Quanto é 5 + 3 × 2?", "11", "16", "13", "10", "11" },
            new[] { "Qual destes objetos não é inflamavel?", "Cadeira", "madeira", "vidro", "Livro", "vidro" },
            new[] { "Qual destas core absorve melhor energia?", "branco", "Vermelho", "preto", "Amarelo", "preto" },
            new[] { "Quem pintou a Mona Lisa?", "Van Gogh", "Picasso", "Da Vinci", "Monet", "Da Vinci" }
        };

        public FormQuiz()
        {
            Pontuacao = 0;
            this.Text = "📚 Quiz Rápido";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            ConfigurarQuiz();
        }

        private void ConfigurarQuiz()
        {
            lblNumero = new Label();
            lblNumero.Text = $"Pergunta 1 de {perguntas.Length}";
            lblNumero.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblNumero.Location = new System.Drawing.Point(20, 20);
            lblNumero.Size = new System.Drawing.Size(200, 25);
            this.Controls.Add(lblNumero);

            lblPergunta = new Label();
            lblPergunta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblPergunta.Location = new System.Drawing.Point(20, 60);
            lblPergunta.Size = new System.Drawing.Size(440, 60);
            this.Controls.Add(lblPergunta);

            botoes = new Button[4];
            for (int i = 0; i < 4; i++)
            {
                botoes[i] = new Button();
                botoes[i].Font = new System.Drawing.Font("Segoe UI", 10F);
                botoes[i].Location = new System.Drawing.Point(50, 140 + i * 50);
                botoes[i].Size = new System.Drawing.Size(380, 40);
                botoes[i].FlatStyle = FlatStyle.Flat;
                botoes[i].BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
                botoes[i].ForeColor = System.Drawing.Color.White;
                int respostaIndex = i;
                botoes[i].Click += (s, e) => VerificarResposta(respostaIndex);
                this.Controls.Add(botoes[i]);
            }

            AtualizarPergunta();
        }

        private void AtualizarPergunta()
        {
            if (perguntaAtual < perguntas.Length)
            {
                lblNumero.Text = $"Pergunta {perguntaAtual + 1} de {perguntas.Length}";
                lblPergunta.Text = perguntas[perguntaAtual][0];
                for (int i = 0; i < 4; i++)
                {
                    botoes[i].Text = perguntas[perguntaAtual][i + 1];
                }
            }
            else
            {
                MessageBox.Show($"Quiz terminado! Acertaste {Pontuacao} de {perguntas.Length} perguntas!", "Fim do Quiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void VerificarResposta(int index)
        {
            string respostaCorreta = perguntas[perguntaAtual][5];
            if (botoes[index].Text == respostaCorreta)
            {
                Pontuacao++;
                MessageBox.Show("✅ Correta!", "Boa!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"❌ Incorreta! A resposta certa era: {respostaCorreta}", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            perguntaAtual++;
            AtualizarPergunta();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormQuiz
            // 
            this.ClientSize = new System.Drawing.Size(308, 221);
            this.Name = "FormQuiz";
            this.ResumeLayout(false);

        }
    }
}