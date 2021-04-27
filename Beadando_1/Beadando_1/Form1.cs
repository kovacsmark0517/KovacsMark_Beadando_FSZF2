using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Beadando_1
{
    public partial class Form1 : Form
    {
        //Az egész csv file soronként egy tömbben
        string[] csvLines = File.ReadAllLines("lotto.csv");
        string[,] LottoTomb = new string[4000, 16];
        int lottoTombHeight;
        int lottoTombWidth;
        
        string[] Tipp = new string[5];
        int tippIndex = 0;
        
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lottoTombHeight = LottoTomb.GetLength(0);
            lottoTombWidth = LottoTomb.GetLength(1);
            this.dgv.ColumnCount = lottoTombWidth;

            for (int i = 0; i < csvLines.Length; i++)
            {
                string[] rawData = csvLines[i].Split(',');
                for (int j = 0; j < 16; j++)
                {
                    LottoTomb[i, j] = rawData[j];               
                }              
            }


            DataGridViewRow row1 = new DataGridViewRow();
            row1.CreateCells(this.dgv);


            row1.Cells[0].Value = "Év";
            row1.Cells[1].Value = "Hét";
            row1.Cells[2].Value = "Húzás dátuma";
            row1.Cells[3].Value = "5 találat db";
            row1.Cells[4].Value = "5 találat Ft";
            row1.Cells[5].Value = "4 találat db";
            row1.Cells[6].Value = "4 találat Ft";
            row1.Cells[7].Value = "3 találat db";
            row1.Cells[8].Value = "3 találat Ft";
            row1.Cells[9].Value = "2 találat db";
            row1.Cells[10].Value = "2 találat Ft";
            row1.Cells[11].Value = "1";
            row1.Cells[12].Value = "2";
            row1.Cells[13].Value = "3";
            row1.Cells[14].Value = "4";
            row1.Cells[15].Value = "5";
            row1.Frozen = true;

            this.dgv.Rows.Add(row1);


            for (int x = 0; x < lottoTombHeight; x++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dgv);
                for (int y = 0; y < lottoTombWidth; y++)
                {
                    row.Cells[y].Value = LottoTomb[x, y];
                    dgv.Columns[y].Visible = true;
                }
                this.dgv.Rows.Add(row);
            }
            clb.Items.Add("Év", CheckState.Checked);
            clb.Items.Add("Hét", CheckState.Checked);
            clb.Items.Add("Húzás dátuma", CheckState.Checked);
            clb.Items.Add("5 találat db", CheckState.Checked);
            clb.Items.Add("5 találat Ft", CheckState.Checked);
            clb.Items.Add("4 találat db", CheckState.Checked);
            clb.Items.Add("4 találat Ft", CheckState.Checked);
            clb.Items.Add("3 találat db", CheckState.Checked);
            clb.Items.Add("3 találat Ft", CheckState.Checked);
            clb.Items.Add("2 találat db", CheckState.Checked);
            clb.Items.Add("2 találat Ft", CheckState.Checked);
            clb.Items.Add("1", CheckState.Checked);
            clb.Items.Add("2", CheckState.Checked);
            clb.Items.Add("3", CheckState.Checked);
            clb.Items.Add("4", CheckState.Checked);
            clb.Items.Add("5", CheckState.Checked);
            


            label1.Text = "Legfrissebb adat: " + LottoTomb[0, 2];           
            label1.Refresh();

        }
        private void button1_Click(object sender, EventArgs e)//teljes kiírás
        {
            //label1.Text = clb.GetItemChecked(0).ToString();

            for(int i = 0; i < 16; i++)
            {
                if (clb.GetItemChecked(i) == true)
                {
                    dgv.Columns[i].Visible = true;
                }
                else
                {
                    dgv.Columns[i].Visible = false;
                }
            }
            dgv.Refresh();
        }
        //[0]Év [1]Hét [2]Húzás dátum [3]5 találat db [4]5 találat Ft [5]4 találat db [6]4 találat Ft [7]3 találat db [8]3 találat Ft [9]2 találat db [10]2 találat Ft [11]Sz1 [12]Sz2 [13]Sz3 [14]Sz4 [15]Sz5
        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbBe_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(tbBe.Text,out int n))
                {
                    if(n > 0 && n < 91)
                    {
                        if (Tipp.Contains(n.ToString()))
                        {
                            MessageBox.Show("Ezt a számot már írtad!");

                        }
                        else
                        {
                            
                            tbTippek.Text += n.ToString() + " ";
                            Tipp[tippIndex] = n.ToString();
                            tippIndex++;
                            tbBe.Text = "";
                            if (tippIndex == 5)
                            {
                                tbBe.Text =Tipp[0] + " " + Tipp[1] + " " + Tipp[2] + " " + Tipp[3] + " " + Tipp[4] + " ";
                                MessageBox.Show("Gratulálok mind az 5 számot beírtad!");
                                tbBe.ReadOnly = true;
                            }
                        }
                    }
                    else
                    {
                        tbBe.Text = "";
                        MessageBox.Show("1 és 90 közötti számot adj meg!");
                    }
                }
                else
                {
                    tbBe.Text = "";
                    MessageBox.Show("Nem számot adtál meg!");
                }
            }
        }
    }
        


    }

