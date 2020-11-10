/*******************************************************************
 * Autor: Juan Acuña.                                              *
 * Archivo: CMTools.cs.                                            *
 * Version: 1.0.                                                   *
 * Descripcion: Herramientas para convertir secuencias de creacion *
 * SQL a clases C#, Python y Java. Tambien permite crear una clase *
 * Command Manager.                                                *
 * Fecha: 30-08-2020.                                              *
 *******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CMTools
{
    class FileHandler
    {
        String DOCUMENT = "";
        bool CONSTRAINT = false;

        public List<List<String>> SQLDocHandler(String file, out int DocSize)
        {
            LoadFile(file);
            var contenido = SQLLineSpliter(DOCUMENT);
            contenido = FileCharacterFilter(contenido);
            Regex floatr = new Regex("[0-9],[0-9]");
            List<Match> matches = new List<Match>();
            Regex longr = new Regex("[8-9]");
            Regex longr2 = new Regex("[0-9]{2}");
            for(int i =0;i < contenido.Count();i++)
            {
                if (floatr.IsMatch(contenido[i]))
                {
                    contenido[i] = floatr.Replace(contenido[i], "0.0");
                }
                else if(longr.IsMatch(contenido[i]))
                {
                    contenido[i] = longr.Replace(contenido[i], "1.1");
                }
                else if (longr2.IsMatch(contenido[i]))
                {
                    contenido[i] = longr2.Replace(contenido[i], "1.1");
                }
            }
            var o = SQLTableFilter(contenido);
            List<String> filtro1 = (List<String>)o[0];
            List<String[]> pks = (List<String[]>)o[1];
            List<List<String>> DATA = SQLOrderData(filtro1);
            DATA = SQLDataTypes(DATA);
            if (CONSTRAINT)
            {
                DATA = SQLApplyPrimaryKey(DATA, pks);
            }
            DATA = Standarize(DATA);
            DocSize = DOCUMENT.Length;
            return DATA;
        }
        private void LoadFile(String file)
        {
            DOCUMENT = "";
            //DATA = new List<List<string>>();
            String linea;
            StreamReader sr = new StreamReader(file);
            while ((linea = sr.ReadLine()) != null)
            {
                linea = linea.Replace("\t", " ");
                if (!linea.Trim().StartsWith("--") && linea.Trim().Length > 0)
                {
                    DOCUMENT += linea;
                }
            }
            sr.Close();
        }
        private String[] SQLLineSpliter(String fileText)
        {
            String[] sep = { ";" };
            String[] contenido = fileText.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < contenido.Count(); i++)
            {
                contenido[i] = contenido[i].TrimEnd().TrimStart();
            }
            return contenido;
        }
        private String[] FileCharacterFilter(String[] contenido)
        {
            //filtro 0
            String st;
            for (int l = 0; l < contenido.Length; l++)
            {
                st = "";
                for (int i = 0; i < contenido[l].Length; i++)
                {
                    if (((int)contenido[l][i] >= 61 && (int)contenido[l][i] <= 90) || ((int)contenido[l][i] >= 48 && (int)contenido[l][i] <= 57) || ((int)contenido[l][i] >= 97 && (int)contenido[l][i] <= 122) || (int)contenido[l][i] == 95 || (int)contenido[l][i] == 63 || (int)contenido[l][i] == 44 || (int)contenido[l][i] == 40 || (int)contenido[l][i] == 41 || (int)contenido[l][i] == 32 || (int)contenido[l][i] == 45)
                    {
                        st += contenido[l][i];
                    }
                }
                contenido[l] = st;
            }
            return contenido;
        }
        private Object[] SQLTableFilter(String[] contenido)
        {
            List<String> filtro1 = new List<String>();
            List<String[]> pks = new List<String[]>();
            foreach (var tx in contenido)
            {
                if (tx.Length > 0 && tx.ToLower().StartsWith("create table"))
                {
                    filtro1.Add(tx);
                }
                else
                {
                    String[] p = new String[2];
                    if (tx.Length > 0 && tx.ToLower().StartsWith("alter table") && tx.ToLower().Contains("primary key"))
                    {
                        String[] s2;
                        String s1 = tx.Trim().Replace(" ","");
                        s1 = Regex.Replace(s1, "altertable", "", RegexOptions.IgnoreCase);
                        s1 = Regex.Replace(s1, "addconstraint", ";", RegexOptions.IgnoreCase);
                        s1 = Regex.Replace(s1, "primarykey", ";", RegexOptions.IgnoreCase);
                        s1 = s1.Replace("(", "");
                        s1 = s1.Replace(")", "");
                        /*
                         .Trim().Replace("altertable", "")
                            .Replace("addconstraint", ";")
                            .Replace("primarykey", ";")
                            .Replace("(", "")
                            .Replace(")", "");
                         */
                        s2 = s1.Split(';');
                        p[0] = s2[0].Trim();
                        p[1] = Capitalize(s2[2].Trim());
                        if (p[1].Contains(","))
                        {
                            p[1] += "*";
                        }
                        pks.Add(p);
                    }
                }
            }
            CONSTRAINT = pks.Count() > 0;
            Object[] o = new Object[2];
            o[0] = filtro1;
            o[1] = pks;
            return o;
        }
        private List<List<String>> SQLOrderData(List<String> filtro1)
        {
            List<List<String>> Data = new List<List<string>>();
            List<String[]> filtro2 = new List<string[]>();
            String[] sep2 = { "," };
            foreach (var tx in filtro1)
            {
                filtro2.Add(tx.Split(sep2, StringSplitOptions.RemoveEmptyEntries));
            }
            List<String> t;
            foreach (var tx in filtro2)
            {
                t = new List<string>();
                String s = tx[0];
                s = Regex.Replace(s, "create table ", "", RegexOptions.IgnoreCase);
                String[] tab = s.Split('(');
                t.Add(tab[0].Trim());
                for (int i = 0; i < tx.Count(); i++)
                {
                    String s2;
                    if (i == 0)
                    {
                        s2 = tx[i];
                        s2 = s2.ToLower();
                        String temp = "create table " + tab[0].Trim().ToLower();
                        s2 = s2.Replace(temp, "");
                        s2 = s2.Replace("(", "");
                        t.Add(s2.Trim());
                    }
                    else if (i == tx.Count() - 1)
                    {
                        s2 = tx[i];
                        s2 = s2.Replace(")", "");
                        t.Add(s2.Trim());
                    }
                    else
                    {
                        t.Add(tx[i].Trim());
                    }
                }
                Data.Add(t);
            }
            return Data;
        }
        private List<List<String>> SQLDataTypes(List<List<String>> Data)
        {
            //Regex rx = new Regex("0.0");
            foreach (var tabla in Data)
            {
                for (int i = 1; i < tabla.Count(); i++)
                {
                    String[] items;
                    String[] sep4 = { " " };
                    items = tabla[i].ToLower().Trim().Split(sep4, StringSplitOptions.RemoveEmptyEntries);
                    String typ = "";
                    
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
                    if ((items[1].Contains("number") || items[1].Contains("numeric")) && (items[1].Contains("0.0") || items[2].Contains("0.0")))
                    {
                        typ = "float";
                    }
                    else 
                    if ((items[1].Contains("number") || items[1].Contains("numeric") || items[1].Contains("int")) && (items[1].Contains("1.1") || items[2].Contains("1.1")))
                    {
                        typ = "long";
                    }
                    else
                    if (items[1].Contains("number") || items[1].Contains("numeric") || items[1].Contains("int"))
                    {
                        typ = "int";
                    }
                    else
                    {
                        typ = "unknown";
                    }
                    bool c = true;
                    tabla[i] = Capitalize(items[0]) + ";" + typ;
                    if (!CONSTRAINT)
                    {
                        if (items[2].Contains("primary"))
                        {
                            tabla[i] = Capitalize(tabla[i]) + ";pk";
                        }
                    }
                }
                for (int it = 2; it < tabla.Count; it++)
                {
                    if (tabla[it].Contains(";pk"))
                    {
                        int p_k = it;
                        String str = tabla[1];
                        tabla[1] = tabla[it];
                        tabla[it] = str;
                    }
                }
            }
            return Data;
        }
        private List<List<String>> SQLApplyPrimaryKey(List<List<String>> Data, List<String[]> pks)
        {
            for (int i = 0; i < Data.Count(); i++)
            {
                for (int j = 0; j < pks.Count(); j++)
                {
                    if (Data[i][0].Equals(pks[j][0]))
                    {
                        for (int m = 1; m < Data[i].Count(); m++)
                        {
                            bool b = pks[j][1].EndsWith("*");
                            if ((Data[i][m].Split(';')[0]).Equals(pks[j][1]) || b)
                            {
                                if (b)
                                {
                                    //(pks[j][1].Contains(Data[i][m].Split(';')[0])

                                    String[] doble = pks[j][1].Replace("*","").Split(',');
                                    foreach (var x in doble)
                                    {
                                        if ((Data[i][m].Split(';')[0]).Equals(Capitalize(x)))
                                        {
                                            Data[i][m] = Data[i][m] + ";pk";
                                        }
                                    }
                                }
                                else
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
                        }
                        j = pks.Count();
                    }
                }
            }
            return Data;
        }
        public static String Capitalize(String str)
        {
            String s = "";
            foreach (var l in str)
            {
                if (l >= 65 && l <= 90)
                {
                    s += ((char)(l + 32)).ToString();
                }
                else
                {
                    s += l.ToString();
                }
            }
            s = ((char)(s[0] - 32)).ToString() + s.Remove(0, 1);
            return s;
        }
        public List<List<String>> Standarize(List<List<String>> data)
        {
            for(int i = 0; i < data.Count(); i++)
            {
                for(int j = 0; j < data.Count(); j++)
                {
                    data[i][0] = data[i][0].Replace(data[j][0].ToLower(), data[j][0]);
                }
            }
            return data;
        }
    }
    public static class CMManager
    {
        private static String cont_CM = "";
        private static List<String[,]> DATA;
        private static String[] NEW;
        public static bool CreateCMFile(String ruta,DBMotor motor,Lenguaje lenguaje)
        {
            InstanciateArray();
            StreamReader sr = null;
            String extension = ".cs";
            String NS="";
            switch (lenguaje)
            {
                case Lenguaje.CSharp:
                    extension = ".cs";//se reemplazan las cosas
                    sr = new StreamReader(CMConfig.CM_CS);
                    NS = CMConfig.CSNAMESPACE;
                    NEW[0] = DATA[0][(int)motor,0];
                    NEW[1] = DATA[0][(int)motor, 1];
                    NEW[2] = DATA[0][(int)motor, 2];
                    NEW[3] = DATA[0][(int)motor, 3];
                    break;
                case Lenguaje.Java:
                    extension = ".java";//se reemplazan las cosas
                    sr = new StreamReader(CMConfig.CM_JAVA);
                    NS = CMConfig.JAVANAMESPACE;
                    NEW[0] = DATA[1][(int)motor, 0];
                    NEW[1] = DATA[1][(int)motor, 1];
                    NEW[2] = DATA[1][(int)motor, 2];
                    NEW[3] = DATA[1][(int)motor, 3];
                    break;
                case Lenguaje.Python3:
                    extension = ".py";//se reemplazan las cosas
                    sr = new StreamReader(CMConfig.CM_PY);
                    NEW[0] = DATA[2][(int)motor, 0];
                    NEW[1] = DATA[2][(int)motor, 1];
                    NEW[2] = DATA[2][(int)motor, 2];
                    NEW[3] = DATA[2][(int)motor, 3];
                    break;
            }

            cont_CM = sr.ReadToEnd();
            cont_CM = cont_CM.Replace("CommandManager", motor.ToString() + "CommandManager");
            cont_CM = cont_CM.Replace("CUSTOMNAMESPACE", NS);
            cont_CM = cont_CM.Replace("IMPORTS", NEW[0]);
            cont_CM = cont_CM.Replace("MOTORCOMMAND", NEW[1]);
            cont_CM = cont_CM.Replace("MOTORCON", NEW[2]);
            cont_CM = cont_CM.Replace("DATAREADER", NEW[3]);
            StreamWriter sw = new StreamWriter(ruta + "\\" + motor.ToString() + "CommandManager" + extension);
            sw.Write(cont_CM);
            sr.Close();
            sw.Close();
            return true;
        }
        private static void InstanciateArray()
        {
            NEW = new String[4];
            DATA = new List<String[,]>();
            //C#
            DATA.Add(new String[4, 4]);
            //C# - Oracle
            DATA[0][0, 0] = "using System;\nusing System.Collections.Generic;\nusing Oracle.ManagedDataAccess.Client;"
                + "\nusing System.Data;\nusing System.Reflection; ";
            DATA[0][0, 1] = "OracleCommand";
            DATA[0][0, 2] = "OracleConnection";
            DATA[0][0, 3] = "OracleDataReader";
            //C# - SQLServer
            DATA[0][1, 0] = "using System;\nusing System.Collections.Generic;\nusing System.Linq;"
                + "\nusing System.Data;\nusing System.Data.SqlClient;\nusing System.IO;\nusing System.Reflection; ";
            DATA[0][1, 1] = "SqlCommand";
            DATA[0][1, 2] = "SqlConnection";
            DATA[0][1, 3] = "SqlDataReader";
            //C# - MySQL
            DATA[0][2, 0] = "using System;\nusing System.Collections.Generic;\nusing System.Linq;"
                + "\nusing System.Data;\nusing System.Data.MySqlClient;\nusing System.IO;\nusing System.Reflection; ";
            DATA[0][2, 1] = "MySqlCommand";
            DATA[0][2, 2] = "MySqlConnection";
            DATA[0][2, 3] = "MySqlDataReader";
            //C# - SQLite
            DATA[0][3, 0] = "using System;\nusing System.Collections.Generic;\nusing System.Linq;"
                + "\nusing System.Text;\nusing System.Data.SQLite;\nusing System.IO;\nusing System.Reflection; ";
            DATA[0][3, 1] = "SQLiteCommand";
            DATA[0][3, 2] = "SQLiteConnection";
            DATA[0][3, 3] = "SQLiteDataReader";
            //Java
            DATA.Add(new String[4, 4]);
            //Java - Oracle
            DATA[1][0, 0] = "import java.sql.Connection;\nimport java.sql.DriverManager;"
                +"\nimport java.sql.SQLException;\njavax.swing.*;";
            DATA[1][0, 1] = "Statement";
            DATA[1][0, 2] = "Connection";
            DATA[1][0, 3] = "ResulSet";
            //Java - SQLServer
            DATA[1][1, 0] = "NO IMPLEMENTADO AUN";
            DATA[1][1, 1] = "NO IMPLEMENTADO AUN";
            DATA[1][1, 2] = "NO IMPLEMENTADO AUN";
            DATA[1][1, 3] = "NO IMPLEMENTADO AUN";
            //Java - MySQL
            DATA[1][2, 0] = "NO IMPLEMENTADO AUN";
            DATA[1][2, 1] = "NO IMPLEMENTADO AUN";
            DATA[1][2, 2] = "NO IMPLEMENTADO AUN";
            DATA[1][2, 3] = "NO IMPLEMENTADO AUN";
            //Java - SQLite
            DATA[1][3, 0] = "NO IMPLEMENTADO AUN";
            DATA[1][3, 1] = "NO IMPLEMENTADO AUN";
            DATA[1][3, 2] = "NO IMPLEMENTADO AUN";
            DATA[1][3, 3] = "NO IMPLEMENTADO AUN";
            //Python3
            DATA.Add(new String[4, 4]);
            //Python3 - Oracle
            DATA[2][0, 0] = "NO IMPLEMENTADO AUN";
            DATA[2][0, 1] = "NO IMPLEMENTADO AUN";
            DATA[2][0, 2] = "NO IMPLEMENTADO AUN";
            DATA[2][0, 3] = "NO IMPLEMENTADO AUN";
            //Python3 - SQLServer
            DATA[2][1, 0] = "NO IMPLEMENTADO AUN";
            DATA[2][1, 1] = "NO IMPLEMENTADO AUN";
            DATA[2][1, 2] = "NO IMPLEMENTADO AUN";
            DATA[2][1, 3] = "NO IMPLEMENTADO AUN";
            //Python3 - MySQL
            DATA[2][2, 0] = "NO IMPLEMENTADO AUN";
            DATA[2][2, 1] = "NO IMPLEMENTADO AUN";
            DATA[2][2, 2] = "NO IMPLEMENTADO AUN";
            DATA[2][2, 3] = "NO IMPLEMENTADO AUN";
            //Python3 - SQLite
            DATA[2][3, 0] = "NO IMPLEMENTADO AUN";
            DATA[2][3, 1] = "NO IMPLEMENTADO AUN";
            DATA[2][3, 2] = "NO IMPLEMENTADO AUN";
            DATA[2][3, 3] = "NO IMPLEMENTADO AUN";
        }
    }
    /// <summary>
    /// Clase estatica encargada de la configuracion global de las clases y CommandManager.
    /// </summary>
    public static class CMConfig
    {
        static String cs_pc = "pc.pcf";
        static String cm_cs = "cm_cs.pcf";
        static String cm_java = "cm_java.pcf";
        static String cm_py = "cm_py.pcf";
        static String customnamespace = "Model";
        static String csnamespace = "Conection";
        static String javanamespace = "conection";
        static bool models = false;
        /// <summary>
        /// Ubicación del archivo plantilla para las clases.
        /// </summary>
        public static String CS_PC { get { return cs_pc; } }
        /// <summary>
        /// Ubicación del archivo plantilla para CommandManager en C#.
        /// </summary>
        public static String CM_CS { get { return cm_cs; } }
        /// <summary>
        /// Ubicación del archivo plantilla para CommandManager en Java.
        /// </summary>
        public static String CM_JAVA { get { return cm_java; } }
        /// <summary>
        /// Ubicación del archivo plantilla para CommandManager en Python3.
        /// </summary>
        public static String CM_PY { get { return cm_py; } }
        /// <summary>
        /// Namespace o Package (dependiendo del lenguaje) para las clases. Por defecto su valor es 'Model'.
        /// </summary>
        public static String CUSTOMNAMESPACE { get { return customnamespace; } set { customnamespace = value; } }
        /// <summary>
        /// Namespace para CommandManager en lenguaje C#. Por defecto su valor es 'Conection'.
        /// </summary>
        public static String CSNAMESPACE { get { return csnamespace; } set { csnamespace = value; } }
        /// <summary>
        /// Package para CommandManager en lenguaje Java. Por defecto su valor es 'Conection'.
        /// </summary>
        public static String JAVANAMESPACE { get { return javanamespace; } set { javanamespace = value; } }
        /// <summary>
        /// Establece si las clases en Python 3 correspondel a 'models' para Django.
        /// </summary>
        public static bool MODELS { get { return models; } set { models = value; } }

        public static bool ValidateNamespace(String str)
        {
            str = str.ToUpper();
            switch (str)
            {
                case "CON":
                case "PRN":
                case "AUX":
                case "CLOCK$":
                case "NUL":
                case "COM1":
                case "COM2":
                case "COM3":
                case "COM4":
                case "COM5":
                case "COM6":
                case "COM7":
                case "COM8":
                case "COM9":
                case "LPT1":
                case "LPT2":
                case "LPT3":
                case "LPT4":
                case "LPT5":
                case "LPT6":
                case "LPT7":
                case "LPT8":
                case "LPT9":
                    return false;
                default:
                    return true;
            }
        }
    }
    public class ClassesFileManager
    {
        List<String> Bases = new List<string>();
        bool identado = true;
        String Escritura = "";
        String customNamespace = CMConfig.CUSTOMNAMESPACE;
        Lenguaje lenguaje;
        List<ClassFile> clases = new List<ClassFile>();

        public String CustomNamespace { get { return customNamespace; } set { customNamespace = value; } }
        public Lenguaje Lenguaje { get { return lenguaje; } set { lenguaje = value; } }
        public bool Identado { get { return identado; } set { identado = value; } }

        public ClassesFileManager()
        {
            customNamespace = CMConfig.CUSTOMNAMESPACE;
            Lenguaje lenguaje = Lenguaje.CSharp;
            StreamReader sr = new StreamReader(CMConfig.CS_PC);
            String str = sr.ReadToEnd();
            String[] s1 = str.Split('"');
            foreach (var item in s1)
            {
                Bases.Add(item);
            }
            sr.Close();
        }
        private void PrepareClases(List<List<String>> data)
        {
            ClassFile c;
            for (int l = 0; l < data.Count(); l++)
            {
                c = new ClassFile();
                c.ClassName = data[l][0];
                for (int i = 1; i < data[l].Count(); i++)
                {
                    String[] str = data[l][i].Split(';');
                    if (str.Count() > 2)
                    {
                        c.Ids.Add(str[0]);
                        c.IdTypes.Add(str[1]);
                    }
                    else
                    {
                        c.Atrs.Add(str[0]);
                        c.Types.Add(str[1]);
                    }
                }
                clases.Add(c);
            }
        }
        private void PrepareEscritura()
        {
            Escritura = "";
            List<String> l = new List<string>();
            switch (lenguaje)
            {
                case Lenguaje.CSharp:
                    l = MakeCSharp(Bases[0].Replace("C#=", ""));
                    break;
                case Lenguaje.Java:
                    l = MakeJava(Bases[1].Replace("JAVA=", ""));
                    break;
                case Lenguaje.Python3:
                    l = MakePython3(Bases[2].Replace("PYTHON3=", ""));
                    break;
                case Lenguaje.PHP:
                    l = MakePHP(Bases[3].Replace("PHP=", ""));
                    break;
            }
            foreach(var item in l)
            {
                Escritura += item + "*";
            }
        }
        public bool MakeClases(List<List<String>> data,String ruta)
        {
            PrepareClases(data);
            PrepareEscritura();
            String[] str = Escritura.Split('*');
            String extension = ".cs";
            switch (lenguaje)
            {
                case Lenguaje.CSharp:
                    extension = ".cs";
                    break;
                case Lenguaje.Java:
                    extension = ".java";
                    break;
                case Lenguaje.Python3:
                    extension = ".py";
                    break;
                case Lenguaje.PHP:
                    extension = ".php";
                    break;
            }
            try
            {
                Directory.CreateDirectory(ruta + "\\"+customNamespace.Split('.').Last() + "\\");
                if(lenguaje==Lenguaje.Python3 && CMConfig.MODELS)
                {
                    StreamWriter sw = new StreamWriter(ruta + "\\" + customNamespace.Split('.').Last() + "\\models.py", false);
                    sw.Write(Escritura.Replace("*","\n"));
                    sw.Flush();
                    sw.Close();
                }
                else if (lenguaje==Lenguaje.PHP)
                {
                    StreamWriter sw2 = new StreamWriter(ruta + "\\" + customNamespace.Split('.').Last() + "\\ProxyModelos.php", false);
                    String pr = "<?php";
                    for (int i = 0; i < clases.Count(); i++)
                    {
                        pr += "\ninclude_once '" + clases[i].ClassName + extension + "';";
                        StreamWriter sw = new StreamWriter(ruta + "\\" + customNamespace.Split('.').Last() + "\\" + clases[i].ClassName + extension, false);
                        if (identado)
                        {
                            sw.Write(str[i]);
                        }
                        else
                        {
                            sw.Write(str[i].Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("//PRIMARY KEY", "").Replace("#PRIMARY KEY", " ").Replace("  ", ""));
                        }
                        sw.Flush();
                        sw.Close();
                    }
                    pr += "\nuse " + customNamespace.Replace('.', '\\') + ";\n?>";
                    sw2.Write(pr);
                    sw2.Flush();
                    sw2.Close();
                }
                else
                {
                    for (int i = 0; i < clases.Count(); i++)
                    {
                        StreamWriter sw = new StreamWriter(ruta + "\\" + customNamespace.Split('.').Last() + "\\" + clases[i].ClassName + extension, false);
                        if (identado || lenguaje == Lenguaje.Python3)
                        {
                            sw.Write(str[i]);
                        }
                        else
                        {
                            sw.Write(str[i].Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("//PRIMARY KEY", "").Replace("#PRIMARY KEY", " ").Replace("  ", ""));
                        }
                        sw.Flush();
                        sw.Close();
                    }
                }
                Console.WriteLine(ruta);
                Process.Start("explorer", ruta+"\\" + customNamespace.Split('.').Last() + "\\");
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        private List<String> MakeCSharp(String plant)
        {
            List<String> lista = new List<string>();
            String[] partes = plant.Split('\'');
            for(int i = 0; i < clases.Count(); i++)
            {
                String str = "";
                str += partes[0].Replace("CUSTOMNAMESPACE", customNamespace);
                str += partes[1].Replace("CLASSNAME", clases[i].ClassName);
                String algo = "";
                for (int l = 0; l < clases[i].Ids.Count(); l++)
                {
                    algo = "";
                    switch (clases[i].IdTypes[l])
                    {
                        case "int":
                            algo = "int";
                            break;
                        case "double":
                            algo = "double";
                            break;
                        case "float":
                            algo = "float";
                            break;
                        case "long":
                            algo = "long";
                            break;
                        case "string":
                            algo = "String";
                            break;
                        case "datetime":
                            algo = "DateTime";
                            break;
                        case "char":
                            algo = "char";
                            break;
                        case "bool":
                            algo = "bool";
                            break;
                        case "blob?":
                            algo = "String";
                            break;
                        case "clob?":
                            algo = "String";
                            break;
                        case "unknown":
                            algo = "String";
                            break;
                    }
                    str += partes[2].Replace("DTYPE", algo).Replace("IDNAME", clases[i].Ids[l]);
                }
                for (int l = 0; l < clases[i].Atrs.Count(); l++)
                {
                    algo = "";
                    switch (clases[i].Types[l])
                    {
                        case "int":
                            algo = "int";
                            break;
                        case "double":
                            algo = "double";
                            break;
                        case "float":
                            algo = "float";
                            break;
                        case "long":
                            algo = "long";
                            break;
                        case "string":
                            algo = "String";
                            break;
                        case "datetime":
                            algo = "DateTime";
                            break;
                        case "char":
                            algo = "char";
                            break;
                        case "bool":
                            algo = "bool";
                            break;
                        case "blob?":
                            algo = "String";
                            break;
                        case "clob?":
                            algo = "String";
                            break;
                        case "unknown":
                            algo = "String";
                            break;
                    }
                    str += partes[3].Replace("DTYPE", algo).Replace("VARNAME", clases[i].Atrs[l]);
                }
                str += partes[4];
                lista.Add(str);
            }
            return lista;
        }
        private List<String> MakeJava(String plant)
        {
            List<String> lista = new List<string>();
            String[] partes = plant.Split('\'');
            for (int i = 0; i < clases.Count(); i++)
            {
                String str = "";
                str += partes[0].Replace("CUSTOMNAMESPACE", customNamespace.ToLower());
                str += partes[1].Replace("CLASSNAME", clases[i].ClassName);
                String algo = "";
                for (int l = 0; l < clases[i].Ids.Count(); l++)
                {
                    algo = "";
                    switch (clases[i].IdTypes[l])
                    {
                        case "int":
                            algo = "int";
                            break;
                        case "double":
                            algo = "double";
                            break;
                        case "float":
                            algo = "float";
                            break;
                        case "long":
                            algo = "long";
                            break;
                        case "string":
                            algo = "String";
                            break;
                        case "datetime":
                            algo = "DateTime";
                            break;
                        case "char":
                            algo = "char";
                            break;
                        case "bool":
                            algo = "boolean";
                            break;
                        case "blob?":
                            algo = "String";
                            break;
                        case "clob?":
                            algo = "String";
                            break;
                        case "unknown":
                            algo = "String";
                            break;
                    }
                    str += partes[2].Replace("DTYPE", algo).Replace("IDNAME", clases[i].Ids[l]);
                }
                for (int l = 0; l < clases[i].Atrs.Count(); l++)
                {
                    algo = "";
                    switch (clases[i].Types[l])
                    {
                        case "int":
                            algo = "int";
                            break;
                        case "double":
                            algo = "double";
                            break;
                        case "float":
                            algo = "float";
                            break;
                        case "long":
                            algo = "long";
                            break;
                        case "string":
                            algo = "String";
                            break;
                        case "datetime":
                            algo = "DateTime";
                            break;
                        case "char":
                            algo = "char";
                            break;
                        case "bool":
                            algo = "boolean";
                            break;
                        case "blob?":
                            algo = "String";
                            break;
                        case "clob?":
                            algo = "String";
                            break;
                        case "unknown":
                            algo = "String";
                            break;
                    }
                    str += partes[3].Replace("DTYPE", algo).Replace("VARNAME", clases[i].Atrs[l]);
                }
                str += partes[4];
                lista.Add(str);
            }
            return lista;
        }
        private List<String> MakePython3(String plant)
        {
            List<String> lista = new List<string>();
            String[] partes = plant.Split('\'');
            if (CMConfig.MODELS)
            {
                String str = "";
                str += partes[0].Replace("IMPORTS", "from django.db import models\n");
                for (int i = 0; i < clases.Count(); i++)
                {
                    str += partes[1].Replace("CLASSNAME():", clases[i].ClassName+"(models.Model):");
                    String algo = "";
                    for (int l = 0; l < clases[i].Ids.Count(); l++)
                    {
                        algo = "";
                        switch (clases[i].IdTypes[l])
                        {
                            case "int":
                            case "double":
                            case "float":
                                algo = "= models.FloatField()";
                                break;
                            case "char":
                            case "blob?":
                            case "unknown":
                            case "clob?":
                            case "string":
                                algo = "= models.TextField()";
                                break;
                            case "datetime":
                                algo = "= models.DateTimeField()";
                                break;
                            case "bool":
                                algo = "= models.BooleanField()";
                                break;
                        }
                        str += partes[2].Replace("DTYPE", algo).Replace("IDNAME", clases[i].Ids[l]);
                    }
                    for (int l = 0; l < clases[i].Atrs.Count(); l++)
                    {
                        algo = "";
                        switch (clases[i].Types[l])
                        {
                            case "int":
                            case "double":
                            case "float":
                                algo = "= models.FloatField()";
                                break;
                            case "char":
                            case "blob?":
                            case "unknown":
                            case "clob?":
                            case "string":
                                algo = "= models.TextField()";
                                break;
                            case "datetime":
                                algo = "= models.DateTimeField()";
                                break;
                            case "bool":
                                algo = "= models.BooleanField()";
                                break;
                        }
                        str += partes[3].Replace("DTYPE", algo).Replace("VARNAME", clases[i].Atrs[l]);
                    }
                    lista.Add(str);
                }
            }
            else
            {
                for (int i = 0; i < clases.Count(); i++)
                {
                    String str = "";
                    str += partes[0].Replace("IMPORTS\n", "");
                    str += partes[1].Replace("CLASSNAME", clases[i].ClassName).Replace("IMPORTS", "");
                    for (int l = 0; l < clases[i].Ids.Count(); l++)
                    {
                        str += partes[2].Replace("IDNAME", clases[i].Ids[l]).Replace(" DTYPE", ""); ;
                    }
                    for (int l = 0; l < clases[i].Atrs.Count(); l++)
                    {
                        str += partes[3].Replace("VARNAME", clases[i].Atrs[l]).Replace(" DTYPE", ""); ;
                    }
                    lista.Add(str);
                }
            }
            return lista;
        }
        private List<String> MakePHP(String plant)
        {
            List<String> lista = new List<string>();
            String[] partes = plant.Split('\'');
            for (int i = 0; i < clases.Count(); i++)
            {
                String str = "";
                str += partes[0].Replace("CUSTOMNAMESPACE", customNamespace);
                str += partes[1].Replace("CLASSNAME", clases[i].ClassName);
                for (int l = 0; l < clases[i].Ids.Count(); l++)
                {
                    str += partes[2].Replace("IDNAME", clases[i].Ids[l]); ;
                }
                for (int l = 0; l < clases[i].Atrs.Count(); l++)
                {
                    str += partes[3].Replace("VARNAME", clases[i].Atrs[l]); ;
                }
                str += partes[4];
                lista.Add(str);
            }
            return lista;
        }
        class ClassFile
        {
            public String ClassName { get; set; }
            public List<String> Ids;
            public List<String> IdTypes;
            public List<String> Atrs;
            public List<String> Types;
            public ClassFile()
            {
                Ids = new List<string>();
                IdTypes = new List<string>();
                Atrs = new List<string>();
                Types = new List<string>();
            }
        }
    }
    /// <summary>
    /// Enum que representa un motor de base de datos soportadas.
    /// </summary>
    public enum DBMotor
    {
        Oracle,
        SQLServer,
        MySQL,
        SQLite
    }
    /// <summary>
    /// Enum que representa un lenguaje de programación soportadas.
    /// </summary>
    public enum Lenguaje
    {
        CSharp,
        Java,
        Python3,
        PHP
    }
}
