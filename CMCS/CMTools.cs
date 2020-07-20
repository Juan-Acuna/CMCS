using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace CMTools
{
    /*class FileHandler
    {
        public static List<T>  
    }*/
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
            DATA[0][0, 0] = "using System;\nusing System.Collections.Generic;"
                + "\nusing System.Linq;\nusing System.Text;\nusing System.Threading.Tasks;"
                + "\nusing Oracle.ManagedDataAccess.Client;\nusing System.Data; ";
            DATA[0][0, 1] = "OracleCommand";
            DATA[0][0, 2] = "OracleConection";
            DATA[0][0, 3] = "OracleDataReader";
            //C# - SQLServer
            DATA[0][1, 0] = "using System;\nusing System.Collections.Generic;"
                + "\nusing System.Data;\nusing System.Data.SqlClient;\nusing System.IO; ";
            DATA[0][1, 1] = "SqlCommand";
            DATA[0][1, 2] = "SqlConnection";
            DATA[0][1, 3] = "SqlDataReader";
            //C# - MySQL
            DATA[0][2, 0] = "using System;\nusing System.Collections.Generic;"
                + "\nusing System.Data;\nusing System.Data.MySqlClient;\nusing System.IO; ";
            DATA[0][2, 1] = "MySqlCommand";
            DATA[0][2, 2] = "MySqlConnection";
            DATA[0][2, 3] = "MySqlDataReader";
            //C# - SQLite
            DATA[0][3, 0] = "NO IMPLEMENTADO AUN";
            DATA[0][3, 1] = "NO IMPLEMENTADO AUN";
            DATA[0][3, 2] = "NO IMPLEMENTADO AUN";
            DATA[0][3, 3] = "NO IMPLEMENTADO AUN";
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
        static String javanamespace = "Conection";
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
                        c.Id = str[0];
                        c.IdType = str[1];
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
            }
            try
            {
                Directory.CreateDirectory(ruta + "\\"+customNamespace+"\\");
                if(lenguaje==Lenguaje.Python3 && CMConfig.MODELS)
                {
                    StreamWriter sw = new StreamWriter(ruta + "\\" + customNamespace + "\\models.py", false);
                    sw.Write(Escritura.Replace("*","\n"));
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    for (int i = 0; i < clases.Count(); i++)
                    {
                        StreamWriter sw = new StreamWriter(ruta + "\\" + customNamespace + "\\" + clases[i].ClassName + extension, false);
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
                Process.Start("explorer", ruta+"\\" + customNamespace + "\\");
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
                if (!clases[i].Id.Equals("N/A") && !clases[i].IdType.Equals("N/A"))
                {
                    switch (clases[i].IdType)
                    {
                        case "int":
                            algo = "int";
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
                    }
                    str += partes[2].Replace("DTYPE", algo).Replace("IDNAME", clases[i].Id);
                }
                for(int l = 0; l < clases[i].Atrs.Count(); l++)
                {
                    algo = "";
                    switch (clases[i].Types[l])
                    {
                        case "int":
                            algo = "int";
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
                str += partes[0].Replace("CUSTOMNAMESPACE", customNamespace);
                str += partes[1].Replace("CLASSNAME", clases[i].ClassName);
                String algo = "";
                if (!clases[i].Id.Equals("N/A") && !clases[i].IdType.Equals("N/A"))
                {
                    switch (clases[i].IdType)
                    {
                        case "int":
                            algo = "int";
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
                    }
                    str += partes[2].Replace("DTYPE", algo).Replace("IDNAME", clases[i].Id);
                }
                for (int l = 0; l < clases[i].Atrs.Count(); l++)
                {
                    algo = "";
                    switch (clases[i].Types[l])
                    {
                        case "int":
                            algo = "int";
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
                    if (!clases[i].Id.Equals("N/A") && !clases[i].IdType.Equals("N/A"))
                    {
                        switch (clases[i].IdType)
                        {
                            case "int":
                                algo = "= models.FloatField()";
                                break;
                            case "char":
                            case "blob?":
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
                        str += partes[2].Replace("DTYPE", algo).Replace("IDNAME", clases[i].Id);
                    }
                    for (int l = 0; l < clases[i].Atrs.Count(); l++)
                    {
                        algo = "";
                        switch (clases[i].Types[l])
                        {
                            case "int":
                                algo = "= models.FloatField()";
                                break;
                            case "char":
                            case "blob?":
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
                    if (!clases[i].Id.Equals("N/A"))
                    {
                        str += partes[2].Replace("IDNAME", clases[i].Id).Replace(" DTYPE", "");
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
        class ClassFile
        {
            public String ClassName { get; set; }
            public String Id = "N/A";
            public String IdType = "N/A";
            public List<String> Atrs;
            public List<String> Types;
            public ClassFile()
            {
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
        Python3
    }
}
