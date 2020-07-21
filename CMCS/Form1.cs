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
using System.Threading;
using CMTools;

namespace CMCS
{
    public partial class CMC : Form
    {
        public CMC()
        {
            InitializeComponent();
        }

        #region Custom
        bool usado = false;
        int DocSize = 0;
        FileHandler fh = new FileHandler();
        List<List<String>> Data;
        String ln;

        private void DisableElements()
        {
            chkClases.Enabled = false;
            chkIClases.Enabled = false;
            lbLenguaje.Enabled = false;
            lbMotor.Enabled = false;
            cbLenguaje.Enabled = false;
            cbMotor.Enabled = false;
            btnEjecutar.Enabled = false;
        }
        private void EjecutarControler()
        {
            btnEjecutar.Enabled = ((chkClases.Checked || chkCM.Checked) && txtSalida.Text.Trim().Length > 0);
        }
        public void ConsolePrint(String str)
        {
            ln += str;
            txtConsole.Lines = ln.Split('\n');
            Thread.Sleep(10);
            txtConsole.Refresh();
            txtConsole.SelectionStart = txtConsole.TextLength;
            txtConsole.ScrollToCaret();
        }
        public void ConsolePrintl(String str)
        {
            ConsolePrint(str + "\n");
        }
        public void ConsolePrintl()
        {
            ConsolePrint("\n");
        }
        public void ConsoleDataPrint()
        {
            for (int l=0;l<Data.Count();l++)
            {
                ConsolePrintl("-------------------------------------------------");
                ConsolePrintl("TABLA: " + Data[l][0]);
                ConsolePrintl("-------------------------------------------------");
                ConsolePrintl("NOMBRE                           TIPO        PK\n");
                for (int i = 1; i < Data[l].Count(); i++)
                {
                    String[] str = Data[l][i].Split(';');
                    ConsolePrintl(str[0].PadRight(30, ' ') + "   " + str[1].PadRight(12, ' ') + (str.Count() > 2 ? "SI" : "NO"));
                }
                ConsolePrintl("-------------------------------------------------");
                if (l < Data.Count() - 1)
                {
                    ConsolePrintl("\n\n");
                }
            }
            usado = true;
        }

        #endregion
        private void CMC_Load(object sender, EventArgs e)
        {
            chkIClases.Checked = true;
            DisableElements();
            cbLenguaje.SelectedIndex = 0;
            cbMotor.SelectedIndex = 0;
        }

        private void txtEntrada_TextChanged(object sender, EventArgs e)
        {
            if(txtEntrada.Text.Trim().Length > 0)
            {
                txtConsole.Visible = true;
                Data = new List<List<string>>();
                if (usado)
                {
                    ConsolePrint("Limpiando data...");
                    Thread.Sleep(240);
                }
                ln = "";
                ConsolePrintl(">Leyendo archivo: "+txtEntrada.Text);
                Thread.Sleep(300);
                chkClases.Enabled = true;
                try
                {
                    Data = fh.SQLDocHandler(txtEntrada.Text, out DocSize);
                }
                catch(Exception ex)
                {
                    ConsolePrintl(ex.Message);
                }
            }
            for (int i = 0; i < Convert.ToInt32(DocSize * 0.0005f) + 3; i++)
            {
                Thread.Sleep(5);
                ConsolePrint(".");
            }
            Thread.Sleep(10);
            ConsolePrintl(".");
            Thread.Sleep(240);
            ConsolePrintl("Hecho!");
            Thread.Sleep(10);
            ConsolePrintl("Tablas encontradas:");
            ConsoleDataPrint();
            ConsolePrint(">");
            EjecutarControler();
        }

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            if(ofdEntrada.ShowDialog() == DialogResult.OK)
            {
                txtEntrada.Text = ofdEntrada.FileName;
            }
            EjecutarControler();
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            if (fbdSalida.ShowDialog() == DialogResult.OK)
            {
                txtSalida.Text = fbdSalida.SelectedPath;
            }
            EjecutarControler();
        }

        private void chkClases_CheckedChanged(object sender, EventArgs e)
        {
            bool b = chkClases.Checked;
            lbLenguaje.Enabled = b;
            cbLenguaje.Enabled = b;
            chkIClases.Enabled = b;
            btnPClases.Enabled = b;
            EjecutarControler();
        }

        private void chkCM_CheckedChanged(object sender, EventArgs e)
        {
            bool b = chkCM.Checked;
            lbMotor.Enabled = b;
            cbMotor.Enabled = b;
            btnPCM.Enabled = b;
            EjecutarControler();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (chkClases.Checked)
            {
                
                ClassesFileManager cfm = new ClassesFileManager();
                cfm.Lenguaje = (Lenguaje)cbLenguaje.SelectedIndex;
                cfm.Identado = chkIClases.Checked;
                bool b = cfm.MakeClases(Data, txtSalida.Text);
                Console.WriteLine(b?"Creado":"Fallo");
            }
            if (chkCM.Checked)
            {
                bool b = CMManager.CreateCMFile(txtSalida.Text, (DBMotor)cbMotor.SelectedIndex, (Lenguaje)cbLenguaje.SelectedIndex);
                Console.WriteLine(b ? "Creado" : "Fallo");
            }
        }
        private void Preferencias(object sender, EventArgs e)
        {
            FormConfig f = new FormConfig(this);
            f.Show();
            f.Focus();
        }
    }
}


/*
String[] sep = { ";" };
            String[] contenido = docOrigen.Split(sep,StringSplitOptions.RemoveEmptyEntries);
            for(int i=0;i<contenido.Count();i++)
            {
                contenido[i] = contenido[i].TrimEnd().TrimStart();
            }
            //filtro 0
            String st;
            for(int l =0;l<contenido.Length;l++)
            {
                st = "";
                for(int i = 0; i < contenido[l].Length; i++)
                {
                    if(((int)contenido[l][i] >= 48 && (int)contenido[l][i] <=57) || ((int)contenido[l][i] >= 97 && (int)contenido[l][i] <= 122) || (int)contenido[l][i] == 95 || (int)contenido[l][i] == 63 || (int)contenido[l][i] == 44 || (int)contenido[l][i] == 40 || (int)contenido[l][i] == 41 || (int)contenido[l][i] == 32 || (int)contenido[l][i] == 45)
                    {
                        st += contenido[l][i];
                    }
                }
                contenido[l] = st;
            }
            List<String> filtro1 = new List<String>();
            List<String[]> pks = new List<String[]>();
            foreach(var tx in contenido)
            {
                if (tx.Length > 0 && tx.StartsWith("create table"))
                {
                    filtro1.Add(tx);
                }
                else if(tx.Length > 0 && tx.StartsWith("alter table") && tx.Contains("primary key"))
                {
                    String[] p = new String[2];
                    String[] s2;
                    String s1 = tx.TrimStart().TrimEnd().Replace("alter table ", "")
                        .Replace(" add constraint ", ";").Replace(" primary key ", ";")
                        .Replace("(", "").Replace(")", "").TrimStart().TrimEnd();
                    s2 = s1.Split(';');
                    p[0] = s2[0].Trim();
                    p[1] = s2[2].Trim();
                    pks.Add(p);
                }
            }
            bool constraint = pks.Count() <= 0;
            List<String[]> filtro2 = new List<string[]>();
            String[] sep2 = { "," };
            foreach (var tx in filtro1)
            {
                filtro2.Add(tx.Split(sep2, StringSplitOptions.RemoveEmptyEntries));
            }
            List<String> t;
            foreach(var tx in filtro2)
            {
                t = new List<string>();
                String[] tab = tx[0].Replace("create table ", "").Split('(');
                t.Add(tab[0].Trim());
                for(int i =0;i<tx.Count();i++)
                {
                    
                    if (i == 0)
                    {
                        t.Add(tx[i].Replace("create table "+tab[0]+"(","").TrimStart().TrimEnd());
                    }else if (i == tx.Count() - 1)
                    {
                        t.Add(tx[i].TrimStart().TrimEnd().Replace(")", "").TrimStart().TrimEnd());
                    }
                    else
                    {
                        t.Add(tx[i].TrimStart().TrimEnd());
                    }
                }
                Data.Add(t);
            }
            foreach (var tabla in Data)
            {
                for (int i = 1; i < tabla.Count(); i++)
                {
                    String[] items;
                    // FALTA ELIMINAR COMENTARIOS DE ERROR
                    String[] sep4 = { " " };
                    items = tabla[i].TrimStart().TrimEnd().Split(sep4, StringSplitOptions.RemoveEmptyEntries);
                    String typ = "";
                    if (items[1].Contains("number") || items[1].Contains("numeric") || items[1].Contains("int"))
                    {
                        typ = "int";
                    }
                    else
                    if (items[1].Contains("varchar") || items[1].Contains("varchar2"))
                    {
                        typ = "string";
                    }
                    else
                    if (items[1].Contains("date") || items[1].Contains("datetime"))
                    {
                        typ = "datetime";
                    }
                    else
                    if (items[1].Contains("char"))
                    {
                        typ = "char";
                    }
                    else
                    if (items[1].Contains("blob"))
                    {
                        typ = "blob?";
                    }
                    else
                    if (items[1].Contains("clob"))
                    {
                        typ = "clob?";
                    }
                    else
                    {
                        typ = "string";
                    }
                    bool c = true;
                    if (constraint)
                    {
                        foreach (var it in items)
                        {
                            if (it.Contains("primary") && i > 1)
                            {
                                int p_k = i;
                                String str = tabla[1];
                                tabla[1] = items[0] + ";" + typ + ";pk";
                                tabla[i] = str;
                                c = false;
                            }
                        }
                    }
                    if (c)
                    {
                        tabla[i] = items[0] + ";" + typ;
                    }
                }
            }
            if (!constraint)
            {
                for(int i = 0; i < Data.Count(); i++)
                {
                    for (int j = 0; j < pks.Count(); j++)
                    {
                        if (Data[i][0].Equals(pks[j][0]))
                        {
                            for (int m = 1; m < Data[i].Count(); m++)
                            {
                                if ((Data[i][m].Split(';')[0]).Equals(pks[j][1]))
                                {
                                    String str = Data[i][1];
                                    Data[i][1] = Data[i][m] + ";pk";

                                    if (m != 1)
                                    {
                                        Data[i][m] = str;
                                    }
                                    m = Data[i].Count();
                                }
                            }
                            j = pks.Count();
                        }
                    }
                }
            }
            for(int i = 0; i < Convert.ToInt32(docOrigen.Length*0.0005f)+3; i++)
            {
                Thread.Sleep(5);
                ConsolePrint(".");
            }
            Thread.Sleep(10);
            ConsolePrintl(".");
            Thread.Sleep(240);
            ConsolePrintl("Hecho!");
            Thread.Sleep(10);
            ConsolePrintl("Tablas encontradas:");
            ConsoleDataPrint();
            ConsolePrint(">");
*/
