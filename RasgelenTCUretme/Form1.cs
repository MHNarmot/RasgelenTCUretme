using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RasgelenTCUretme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tcKimlikNo = OlusturTCKimlikNo();
            textBox1.Text = tcKimlikNo;
        }

        private string OlusturTCKimlikNo()
        {
            // Rastgele ilk 9 rakamı oluştur
            Random rnd = new Random();
            int[] kimlik = new int[9];
            for (int i = 0; i < 9; i++)
            {
                kimlik[i] = rnd.Next(0, 9);
            }

            // 10. rakamı hesapla
            int birinciKose = ((kimlik[0] + kimlik[2] + kimlik[4] + kimlik[6] + kimlik[8]) * 7) - (kimlik[1] + kimlik[3] + kimlik[5] + kimlik[7]);
            int onuncuRakam = birinciKose % 10;
            if (onuncuRakam < 0) onuncuRakam += 10;

            // 11. rakamı hesapla
            int toplam = kimlik.Sum() + onuncuRakam;
            int onBirinciRakam = toplam % 10;
            if (onBirinciRakam < 0) onBirinciRakam += 10;

            // T.C. kimlik numarasını oluştur
            string tcKimlikNo = string.Join("", kimlik) + onuncuRakam.ToString() + onBirinciRakam.ToString();

            // Doğruluğunu kontrol et
            if (!KontrolTCKimlikNo(tcKimlikNo))
            {
                // Doğru değilse tekrar oluştur
                tcKimlikNo = OlusturTCKimlikNo();
            }

            return tcKimlikNo;
        }

        private bool KontrolTCKimlikNo(string tcKimlikNo)
        {
            if (tcKimlikNo.Length != 11) return false;

            long tcNo;
            if (!Int64.TryParse(tcKimlikNo, out tcNo)) return false;

            long atcNo = tcNo / 100;
            long btcNo = tcNo / 100;

            int c1 = (int)(atcNo % 10); atcNo /= 10;
            int c2 = (int)(atcNo % 10); atcNo /= 10;
            int c3 = (int)(atcNo % 10); atcNo /= 10;
            int c4 = (int)(atcNo % 10); atcNo /= 10;
            int c5 = (int)(atcNo % 10); atcNo /= 10;
            int c6 = (int)(atcNo % 10); atcNo /= 10;
            int c7 = (int)(atcNo % 10); atcNo /= 10;
            int c8 = (int)(atcNo % 10); atcNo /= 10;
            int c9 = (int)(atcNo % 10); atcNo /= 10;

            int q1 = ((10 - ((((c1 + c3 + c5 + c7 + c9) * 3) + (c2 + c4 + c6 + c8)) % 10)) % 10);
            int q2 = ((10 - (((((c2 + c4 + c6 + c8) + q1) * 3) + (c1 + c3 + c5 + c7 + c9)) % 10)) % 10);

            return ((btcNo * 100) + (q1 * 10) + q2 == tcNo);
        }
    }
}
