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
    public class CMManager
    {
        private static String cont_CM = "";
        private static List<String[,]> DATA;
        private CMManager instance_ = new CMManager();
        private CMManager()
        {
            InstanciateArray();
        }
        public static bool CreateCMFile(String ruta,DBMotor motor,Lenguaje lenguaje,bool identado)
        {
            StreamReader sr = new StreamReader(CMConfig.CM_BP);
            cont_CM = sr.ReadToEnd();
            String extension = ".cs";
            switch (lenguaje)
            {
                case Lenguaje.CSharp:
                    extension = ".cs";//se reemplazan las cosas
                    cont_CM = cont_CM.Replace("IMPORTS", DATA[0][(int)motor, 0]);
                    cont_CM = cont_CM.Replace("MOTORCOMMAND", DATA[0][(int)motor, 1]);
                    cont_CM = cont_CM.Replace("MOTORCON", DATA[0][(int)motor, 2]);
                    cont_CM = cont_CM.Replace("DATAREADER", DATA[0][(int)motor, 3]);
                    break;
                case Lenguaje.Java:
                    extension = ".java";//se reemplazan las cosas
                    cont_CM = cont_CM.Replace("IMPORTS", DATA[1][(int)motor, 0]);
                    cont_CM = cont_CM.Replace("MOTORCOMMAND", DATA[1][(int)motor, 1]);
                    cont_CM = cont_CM.Replace("MOTORCON", DATA[1][(int)motor, 2]);
                    cont_CM = cont_CM.Replace("DATAREADER", DATA[1][(int)motor, 3]);
                    break;
                case Lenguaje.Python3:
                    extension = ".py";//se reemplazan las cosas
                    cont_CM = cont_CM.Replace("IMPORTS", DATA[2][(int)motor, 0]);
                    cont_CM = cont_CM.Replace("MOTORCOMMAND", DATA[2][(int)motor, 1]);
                    cont_CM = cont_CM.Replace("MOTORCON", DATA[2][(int)motor, 2]);
                    cont_CM = cont_CM.Replace("DATAREADER", DATA[2][(int)motor, 3]);
                    break;
            }
            StreamWriter sw = new StreamWriter(ruta+"\\"+motor.ToString()+"CommandManager"+extension);
            if (identado)
            {
                sw.Write(cont_CM);
            }
            else
            {
                sw.Write(cont_CM.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("  ", ""));
            }
            sr.Close();
            sw.Close();
            return true;
        }
        private void InstanciateArray()
        {
            DATA = new List<String[,]>();
            //C#
            DATA[0] = new String[4, 4];
            DATA[0][0, 0] = "using System;\nusing System.Collections.Generic;"
                + "\nusing System.Linq;\nusing System.Text;\nusing System.Threading.Tasks;"
                + "\nusing Oracle.ManagedDataAccess.Client;\nusing System.Data; ";
            DATA[0][0, 1] = "OracleCommand";
            DATA[0][0, 2] = "OracleConection";
            DATA[0][0, 3] = "OracleDataReader";

            DATA[0][1, 0] = "using System;\nusing System.Collections.Generic;"
                + "\nusing System.Data;\nusing System.Data.SqlClient;\nusing System.IO; ";
            DATA[0][1, 1] = "SqlCommand";
            DATA[0][1, 2] = "SqlConnection";
            DATA[0][1, 3] = "SqlDataReader";

            DATA[0][2, 0] = "using System;\nusing System.Collections.Generic;"
                + "\nusing System.Data;\nusing System.Data.MySqlClient;\nusing System.IO; ";
            DATA[0][2, 1] = "MySqlCommand";
            DATA[0][2, 2] = "MySqlConnection";
            DATA[0][2, 3] = "MySqlDataReader";

            DATA[0][3, 0] = "NO IMPLEMENTADO AUN";
            DATA[0][3, 1] = "NO IMPLEMENTADO AUN";
            DATA[0][3, 2] = "NO IMPLEMENTADO AUN";
            DATA[0][3, 3] = "NO IMPLEMENTADO AUN";
            //Java
            DATA[1] = new String[4, 4];
            DATA[1][0, 0] = "NO IMPLEMENTADO AUN";
            DATA[1][0, 1] = "NO IMPLEMENTADO AUN";
            DATA[1][0, 2] = "NO IMPLEMENTADO AUN";
            DATA[1][0, 3] = "NO IMPLEMENTADO AUN";

            DATA[1][1, 0] = "NO IMPLEMENTADO AUN";
            DATA[1][1, 1] = "NO IMPLEMENTADO AUN";
            DATA[1][1, 2] = "NO IMPLEMENTADO AUN";
            DATA[1][1, 3] = "NO IMPLEMENTADO AUN";

            DATA[1][2, 0] = "NO IMPLEMENTADO AUN";
            DATA[1][2, 1] = "NO IMPLEMENTADO AUN";
            DATA[1][2, 2] = "NO IMPLEMENTADO AUN";
            DATA[1][2, 3] = "NO IMPLEMENTADO AUN";

            DATA[1][3, 0] = "NO IMPLEMENTADO AUN";
            DATA[1][3, 1] = "NO IMPLEMENTADO AUN";
            DATA[1][3, 2] = "NO IMPLEMENTADO AUN";
            DATA[1][3, 3] = "NO IMPLEMENTADO AUN";
            //Python3
            DATA[2] = new String[4, 4];
            DATA[2][0, 0] = "NO IMPLEMENTADO AUN";
            DATA[2][0, 1] = "NO IMPLEMENTADO AUN";
            DATA[2][0, 2] = "NO IMPLEMENTADO AUN";
            DATA[2][0, 3] = "NO IMPLEMENTADO AUN";

            DATA[2][1, 0] = "NO IMPLEMENTADO AUN";
            DATA[2][1, 1] = "NO IMPLEMENTADO AUN";
            DATA[2][1, 2] = "NO IMPLEMENTADO AUN";
            DATA[2][1, 3] = "NO IMPLEMENTADO AUN";

            DATA[2][2, 0] = "NO IMPLEMENTADO AUN";
            DATA[2][2, 1] = "NO IMPLEMENTADO AUN";
            DATA[2][2, 2] = "NO IMPLEMENTADO AUN";
            DATA[2][2, 3] = "NO IMPLEMENTADO AUN";

            DATA[2][3, 0] = "NO IMPLEMENTADO AUN";
            DATA[2][3, 1] = "NO IMPLEMENTADO AUN";
            DATA[2][3, 2] = "NO IMPLEMENTADO AUN";
            DATA[2][3, 3] = "NO IMPLEMENTADO AUN";
        }
    }
    public static class CMConfig
    {
        static String cm_bp = "plantilla_cm.pcf";
        static String cs_bp = "plantilla_clases.pcf";

        public static String CM_BP { get { return cm_bp; } }
        public static String CS_BP { get { return cs_bp; } }
        public static String CUSTOMNAMESPACE { get; set; }
    }
    public class ClassesFileManager
    {
        List<String> Bases = new List<string>();
        bool identado = true;
        String Escritura = "";
        String customNamespace = "Model";
        Lenguaje lenguaje = Lenguaje.CSharp;
        List<ClassFile> clases = new List<ClassFile>();

        public String CustomNamespace { get { return customNamespace; } set { customNamespace = value; } }
        public Lenguaje Lenguaje { get; set; }
        public bool Identado { get { return identado; } set { identado = value; } }

        public ClassesFileManager()
        {
            StreamReader sr = new StreamReader(CMConfig.CS_BP);
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
                    l = MakeJava(Bases[2].Replace("JAVA=", ""));
                    break;
                case Lenguaje.Python3:
                    l = MakePython3(Bases[3].Replace("PYTHON3=", ""));
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
                for(int i = 0; i < clases.Count(); i++)
                {
                    StreamWriter sw = new StreamWriter(ruta + "\\" + customNamespace + "\\" + clases[i].ClassName + extension, false);
                    if (identado || lenguaje==Lenguaje.Python3)
                    {
                        sw.Write(str[i]);
                    }
                    else
                    {
                        sw.Write(str[i].Replace("\n","").Replace("\r","").Replace("\t","").Replace("//PRIMARY KEY", "").Replace("#PRIMARY KEY", " ").Replace("  ", ""));
                    }
                    sw.Flush();
                    sw.Close();
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
                //str += partes[0].Replace("CUSTOMNAMESPACE", customNamespace);
                str += partes[0].Replace("CLASSNAME", clases[i].ClassName);
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
                    str += partes[1].Replace("DTYPE", algo).Replace("IDNAME", clases[i].Id);
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
                    str += partes[2].Replace("DTYPE", algo).Replace("VARNAME", clases[i].Atrs[l]);
                }
                str += partes[3];
                lista.Add(str);
            }
            return lista;
        }
        private List<String> MakePython3(String plant)
        {
            List<String> lista = new List<string>();
            String[] partes = plant.Split('\'');
            for (int i = 0; i < clases.Count(); i++)
            {
                String str = "";
                str += partes[0].Replace("CLASSNAME", clases[i].ClassName);
                if (!clases[i].Id.Equals("N/A"))
                {
                    str += partes[1].Replace("IDNAME", clases[i].Id);
                }
                for (int l = 0; l < clases[i].Atrs.Count(); l++)
                {
                    str += partes[2].Replace("VARNAME", clases[i].Atrs[l]);
                }
                lista.Add(str);
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

    public enum DBMotor
    {
        Oracle,
        SQLServer,
        MySQL,
        SQLite
    }
    public enum Lenguaje
    {
        CSharp,
        Java,
        Python3
    }
}
