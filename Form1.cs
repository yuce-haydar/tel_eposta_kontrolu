using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace tel_eposta_kontrolu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti =new SqlConnection(@"veri taabani yolu");
        string foto_yolu;
        void temizle()
        {
            txtAD.Text = "";
            txtID.Text = "";
            txtSYD.Text = "";
            txtMAIL.Text = "";
            msktxtTEL.Text = "";
            txtAD.Focus();
        }
        void listele() 
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Kisiler",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut=new SqlCommand("insert into Kisiler (AD,SOYAD,TELEFON,MAIL,FOTO) values ('"+txtAD.Text+ "','" + txtSYD.Text + "','" + msktxtTEL.Text  + "','" + txtMAIL.Text + "','" + foto_yolu + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kisi sisteme eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text=dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAD.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSYD.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktxtTEL.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtMAIL.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
            
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("gercekten silmek istediginize emin misiniz", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog==DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Kisiler where ID=" + txtID.Text, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("sectiginiz kisi rehberden silinmistir", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("sectiginiz kisi rehberden silinmemistir", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }


           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Kisiler set AD=@P1,SOYAD=@P2,TELEFON=@P3,MAIL=@P4,FOTO=@P6 where ID=@P5", baglanti);
            komut.Parameters.AddWithValue("@P1", txtAD.Text);
            komut.Parameters.AddWithValue("@P2",txtSYD.Text);
            komut.Parameters.AddWithValue("@P3", msktxtTEL.Text);
            komut.Parameters.AddWithValue("@P4", txtMAIL.Text);
            komut.Parameters.AddWithValue("@P5", txtID.Text);
            komut.Parameters.AddWithValue("@P6", foto_yolu);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("sectiginiz kisi rehberde guncenlenmistir ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            listele();
            temizle();


        }

        private void button5_Click(object sender, EventArgs e)
        {
          
            openFileDialog1.ShowDialog();
            foto_yolu = openFileDialog1.FileName;
            pictureBox1.ImageLocation = foto_yolu;
           
        }
    }
}
//Data Source=(local);Initial Catalog=filmarsiv;Integrated Security=True