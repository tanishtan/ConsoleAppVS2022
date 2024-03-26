using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleAppVS2022
{
    public class TestClass
    {
        private int _id;
        private string _name;
        public TestClass(int id, string name) {
            _id = id;
            _name = name;
        }
        public int Id { get { return _id; } set { _id = value; } }

        public void Show()
        {
            Console.WriteLine("Id: {0}, Name: {1}",_id, _name);
        }
    }
    internal class WorkingReflection
    {
        internal static void Test()
        {
           
            Assembly assembly = Assembly.GetExecutingAssembly();
            //Find the module - projectname.exe / projectname.dll
            Module mod = assembly.GetModule(nameof(ConsoleAppVS2022) + ".dll");
            Type t = mod.GetType("ConsoleAppVS2022.TestClass");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("-------- Type Information ---------")
            .AppendFormat("Name: {0}\n", t.Name)
            .AppendFormat("Fullname: {0}\n", t.FullName)
            .AppendFormat("Visibility: {0}\n", t.IsPublic ? "Public": "Not Public")
            .AppendFormat("Base Class: {0}\n", t.BaseType.FullName);
            Console.WriteLine (stringBuilder.ToString());

            //Field information
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo[] fields = t.GetFields(bindingAttr: bindingFlags);
            stringBuilder = new StringBuilder ();
            stringBuilder.AppendLine("\n--------Field Information---------");
            foreach(FieldInfo field in fields)
            {
                stringBuilder.AppendFormat("Field Name: {0}\n", field.Name)
                    .AppendFormat("Field Type: {0}\n", field.FieldType.FullName)
                    .AppendFormat("Visibility: {0}\n", field.IsPublic ? "Public" : "Not Public")
                    .AppendFormat("Declaring Type: {0}\n", field.DeclaringType.FullName);
                    //.AppendFormat("Initial value: {0}\n", field.GetRawConstantValue())
                    //.AppendLine();

                Console.WriteLine(stringBuilder.ToString());

                ConstructorInfo[] ctors = t.GetConstructors(bindingAttr: bindingFlags);
                stringBuilder = new StringBuilder ();
                stringBuilder.AppendLine("----------Constructors----------");
                foreach(ConstructorInfo ctor in ctors)
                {
                    stringBuilder.AppendFormat("Constructor Name: {0}\n", ctor.Name)
                    .AppendFormat("Visibility: {0}\n", ctor.IsPrivate ? "Private" : ctor.IsPublic ? "Public" : ctor.IsAssembly?"Internal":"Protected")
                    .AppendFormat("Has Arguments: {0}\n", ctor.GetParameters().Length>0?"Yes":"No");

                    foreach(ParameterInfo param in ctor.GetParameters())
                    {
                        stringBuilder.AppendFormat("Parameter Name: {0}, Type: {1}\n", param.Name, param.ParameterType.FullName);
                    }
                }
                stringBuilder = new StringBuilder ();
                //Method information
                MethodInfo[] methods = t.GetMethods(bindingAttr: bindingFlags);
                stringBuilder.AppendLine("----------Method Information----------");
                foreach (MethodInfo ctor in methods)
                {
                    stringBuilder.AppendFormat("Method Name: {0}\n", ctor.Name)
                        .AppendFormat("Declaring Type: {0}\n",ctor.DeclaringType.FullName)
                        .AppendFormat("Return Type: {0}\n", ctor.ReturnType.FullName)
                    .AppendFormat("Visibility: {0}\n", ctor.IsPrivate ? "Private" : ctor.IsPublic ? "Public" : ctor.IsAssembly ? "Internal" : "Protected")
                    .AppendFormat("Has Arguments: {0}\n", ctor.GetParameters().Length > 0 ? "Yes" : "No");

                    foreach (ParameterInfo param in ctor.GetParameters())
                    {
                        stringBuilder.AppendFormat("Parameter Name: {0}, Type: {1}\n", param.Name, param.ParameterType.FullName);
                    }
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine();

                // Dynamically instantiationg the type

                Console.WriteLine("Attempting to instantiate the type");

                //aActivator -> CreateInstance()
                object obj = Activator.CreateInstance(t,99,"Hello");
                Console.WriteLine("Object created");
                //to invoke the method
                t.InvokeMember(
                    name: "Show",
                    invokeAttr: BindingFlags.InvokeMethod| BindingFlags.Public|BindingFlags.Instance,
                    binder: null,
                    target: obj,
                    args: null
                    );
            }
        }
    }
}
