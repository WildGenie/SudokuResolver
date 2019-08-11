using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
  public partial class Form1 : Form
  {
    private readonly List<konum> matrisResolve = new List<konum>();
    private int[,] matris;

    private int[,][] test;

    public Form1()
    {
      InitializeComponent();
    }

    public void yerlestir()
    {
      var veriler = "003020600900305001001806400008102900700000008006708200002609500800203009005010300".ToCharArray();
      matris = new int[9, 9];

      for (var i = 0; i < veriler.Length; i++)
      {
        var x = i / 9;
        var y = i % 9;
        Console.WriteLine($"{x},{y}");
        if (int.TryParse(veriler[i].ToString(), out var rakam))
        {
          matris[x, y] = rakam;
          var kareSatir = x / 3 % 3;
          var kareSutun = y / 3 % 3;
          var kareSira = kareSatir + kareSutun * 3;
          Console.WriteLine($"{x},{y} => {kareSatir}, {kareSutun}, {kareSira}");

          var label = new TextBox
          {
            Text = rakam == 0 ? string.Empty : rakam.ToString(),
            ReadOnly = rakam != 0,
            AutoSize = false,
            Size = new Size(200, 200),
            Font = new Font(FontFamily.GenericMonospace, 16),
            BackColor = kareSira % 2 == 0 ? Color.LightGreen : Color.DeepSkyBlue
          };
          matrisResolve.Add(new konum(x, y, rakam == 0, rakam));
          tableLayoutPanel1.Controls.Add(label, x, y);
        }
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      yerlestir();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
      timer1.Start();
    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
      var sonucBulundu = false;
      for (var i = 0; i < matris.Length; i++)
      {
        var x = i / 9;
        var y = i % 9;
        var kareSatir = x / 3 % 3;
        var kareSutun = y / 3 % 3;
        var kareSira = kareSatir + kareSutun * 3;
        var aktif = matrisResolve.Single(k => k.x == x && k.y == y);
        if (aktif.deger == 0)
        {
          //Enumerable.Range(0, matris.Length).Select(m => new konum(m.))
          var esler = matrisResolve.Where(k => k.deger != 0 && (k.x == x || k.y == y || k.KareSira == kareSira));
          foreach (var es in esler) aktif.olasi.Remove(es.deger);

          if (aktif.olasi.Count == 1)
          {
            sonucBulundu = true;
            aktif.deger = aktif.olasi[0];
            (tableLayoutPanel1.GetControlFromPosition(x, y) as TextBox).Text = aktif.deger.ToString();
          }
        }
      }

      if (sonucBulundu == false)
      {
        timer1.Stop();
        MessageBox.Show(matrisResolve.Any(m => m.deger == 0) ? "Sonuc Bulunamıyor!" : "Çözüldü!");
      }
    }

    private class konum
    {
      public readonly List<int> olasi;
      public readonly int x;
      public readonly int y;
      public int deger;

      public konum(int x, int y, bool bos, int deger)
      {
        this.x = x;
        this.y = y;
        this.deger = deger;
        olasi = bos ? Enumerable.Range(1, 9).ToList() : new List<int>();
      }

      public int KareSira => x / 3 % 3 + y / 3 % 3 * 3;
    }
  }
}