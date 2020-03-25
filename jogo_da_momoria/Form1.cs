using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace jogo_da_momoria
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Label primeiroClick = null;
        Label segundoClick = null;
        int guardatempo;
        String linha;

        public Form1()
        {
            InitializeComponent();
            carregaTXT();
            AtribuiIcones();

        }
        string arquivo = @"C:\\Users\\akyri\\source\\repos\\jogo_da_momoria\\Record.txt";

        private string carregaTXT()
            {
            

            using (StreamReader sr = new StreamReader(arquivo))
            {
                
                // Lê linha por linha até o final do arquivo
                while ((linha = sr.ReadLine()) != null)
                {

                    record1.Text = linha;


                }
            }

            return linha;
        }



    List<string> icones = new List<string>()
            {
                "!", "!", "N", "N", ",", ",", "k", "k",
                "b", "b", "v", "v", "w", "w", "z", "z"


            };

    private void AtribuiIcones()
    {
        foreach (Control control in tableLayoutPanel.Controls)
        {
            Label iconeLabel = control as Label;

            if (iconeLabel != null)
            {
                int randomNumero = random.Next(icones.Count);
                iconeLabel.Text = icones[randomNumero];
                icones.RemoveAt(randomNumero);


            }

        }
    }

    private void Form1_Click(object sender, EventArgs e)
    {

    }

    private void click_label(object sender, EventArgs e)
    {

    }


    private void label3_Click(object sender, EventArgs e)
    {

        if (timer1.Enabled == true)
            return;

        Label clickedLabel = sender as Label;

        if (clickedLabel != null)
        {

            if (clickedLabel.ForeColor == Color.Black)
                return;


            if (primeiroClick == null)
            {
                primeiroClick = clickedLabel;
                primeiroClick.ForeColor = Color.Black;
                return;
            }


            segundoClick = clickedLabel;
            segundoClick.ForeColor = Color.Black;



            ConfirmaVitoria();


            if (primeiroClick.Text == segundoClick.Text)
            {
                primeiroClick = null;
                segundoClick = null;
                return;
            }

            timer1.Start();


        }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        timer1.Stop();


        primeiroClick.ForeColor = primeiroClick.BackColor;
        segundoClick.ForeColor = segundoClick.BackColor;


        primeiroClick = null;
        segundoClick = null;
    }

    private void ConfirmaVitoria()
    {
        //
        foreach (Control control in tableLayoutPanel.Controls)
        {
            Label iconLabel = control as Label;

            if (iconLabel != null)
            {
                if (iconLabel.ForeColor == iconLabel.BackColor)
                    return;
            }
        }

        string ValorTempo = labeltempo.Text;
        string arquivo = @"C:\\Users\\akyri\\source\\repos\\jogo_da_momoria\\Record.txt";

        MessageBox.Show("Você venceu! Seu tempo Foi:  " + ValorTempo + " Segundos", "**************PARABÉNS!!**************");

        using (StreamWriter writer = new StreamWriter("C:\\Users\\akyri\\source\\repos\\jogo_da_momoria\\Record.txt", true))
        {
            writer.WriteLine(ValorTempo);
        }



    }


    private void timer2_Tick(object sender, EventArgs e)
    {
        guardatempo = (guardatempo + 1);
        DateTime date = new DateTime();
        date = date.AddSeconds(Convert.ToInt32(guardatempo));
        labeltempo.Text = date.ToLongTimeString();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

        

        timer2.Start();
    }
}
}
