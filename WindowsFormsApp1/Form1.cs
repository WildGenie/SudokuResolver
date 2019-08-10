using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    public void yerlestir()
    {
      var veriler = "305420810487901506029056374850793041613208957074065280241309065508670192096512408".ToCharArray();
      int[,] matris = new int[9,9];

      for (int i = 0; i < veriler.Length; i++)
      {
        var x = i / 9;
        var y = i % 9;
        Console.WriteLine($"{x},{y}");
        if (int.TryParse(veriler[i].ToString(), out int rakam))
        {
          matris[x, y] = rakam;
          var kareSatir = (((x / 3) % 3));
          var kareSutun = (((y / 3) % 3));
          var kareSira = kareSatir + (kareSutun * 3);
          Console.WriteLine($"{x},{y} => {kareSatir},{kareSutun}, {kareSira}");

          TextBox label = new TextBox
          {
            Text = rakam == 0 ? string.Empty : rakam.ToString(),
            ReadOnly = rakam != 0,
            AutoSize = false,
            Size = new Size(200,200),
            Font = new Font(FontFamily.GenericMonospace, 16),
            BackColor = kareSira % 2 == 0 ? Color.AliceBlue : Color.Aquamarine
          };
          
          tableLayoutPanel1.Controls.Add(label, x, y);
        }
      }
      
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      yerlestir();
    }
  }
}
