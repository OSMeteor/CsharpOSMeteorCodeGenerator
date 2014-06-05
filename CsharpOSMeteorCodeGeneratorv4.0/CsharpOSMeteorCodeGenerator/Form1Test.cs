using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.CodeDom;
using System.Threading;

namespace CsharpOSMeteorCodeGenerator
{
    public partial class Form1Test : Form
    {
        public Form1Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DynamicCreateType1();
            DynamicCreateType();
            COdedongtai();
            codeDongtai2();
            //Test();
            ////动态创建的类类型  
            //Type classType = DynamicCreateType();
            ////调用有参数的构造函数  
            //Type[] ciParamsTypes = new Type[] { typeof(string) };
            //object[] ciParamsValues = new object[] { "Hello World" };
            //ConstructorInfo ci = classType.GetConstructor(ciParamsTypes);
            //object Vector = ci.Invoke(ciParamsValues);
            ////调用方法  
            //object[] methedParams = new object[] { };
            //MessageBox.Show(classType.InvokeMember("get_Field", BindingFlags.InvokeMethod, null, Vector, methedParams).ToString());
            ////Console.ReadKey();  
        }
        //动态创建的动态类型  
        public static Type DynamicCreateType()
        {
            //动态创建程序集  
            AssemblyName DemoName = new AssemblyName("OSMeteorAssemblyDB");
            AssemblyBuilder dynamicAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(DemoName, AssemblyBuilderAccess.RunAndSave);
            //动态创建模块  
            AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
            ModuleBuilder mb = dynamicAssembly.DefineDynamicModule(DemoName.Name, DemoName.Name + ".dll");
         
            //ModuleBuilder modBuild = dynamicAssembly.DefineDynamicModule("ModuleOne", "NestedEnum.dll");       
            //动态创建类MyClass  
            TypeBuilder Ptb = mb.DefineType("Eneity", TypeAttributes.Public);
            //TypeBuilder tb = mb.DefineType("table", TypeAttributes.Public);
            TypeBuilder tb = Ptb.DefineNestedType("Table1", TypeAttributes.NestedPublic | TypeAttributes.Sealed,  null);
          
            //动态创建字段  
            FieldBuilder fb = tb.DefineField("myField", typeof(System.String), FieldAttributes.Private);
            //动态创建构造函数  
            Type[] clorType = new Type[] { typeof(System.String) };
            ConstructorBuilder cb1 = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, clorType);
            //生成指令  
            ILGenerator ilg = cb1.GetILGenerator();//生成 Microsoft 中间语言 (MSIL) 指令  
            ilg.Emit(OpCodes.Ldarg_0);
            ilg.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
            ilg.Emit(OpCodes.Ldarg_0);
            ilg.Emit(OpCodes.Ldarg_1);
            ilg.Emit(OpCodes.Stfld, fb);
            ilg.Emit(OpCodes.Ret);
            //动态创建属性  
            PropertyBuilder pb = tb.DefineProperty("MyProperty", PropertyAttributes.HasDefault, typeof(string), null);
            //动态创建方法  
            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName;
            MethodBuilder myMethod = tb.DefineMethod("get_Field", getSetAttr, typeof(string), Type.EmptyTypes);
            //生成指令  
            ILGenerator numberGetIL = myMethod.GetILGenerator();
            numberGetIL.Emit(OpCodes.Ldarg_0);
            numberGetIL.Emit(OpCodes.Ldfld, fb);
            numberGetIL.Emit(OpCodes.Ret);
            //使用动态类创建类型  
            Type classType1 = Ptb.CreateType();
            Type classType = tb.CreateType();
            //保存动态创建的程序集 (程序集将保存在程序目录下调试时就在Debug下)  
            dynamicAssembly.Save(DemoName.Name + ".dll");  
            
            //创建类  
            return classType;
        }
        //动态创建的动态类型  
        public static Type DynamicCreateType1()
        {
            //动态创建程序集  
            AssemblyName DemoName = new AssemblyName("DynamicAssembly");
            AssemblyBuilder dynamicAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(DemoName, AssemblyBuilderAccess.RunAndSave);
            //动态创建模块  
            ModuleBuilder mb = dynamicAssembly.DefineDynamicModule(DemoName.Name, DemoName.Name + ".dll");
            //动态创建类MyClass  
            TypeBuilder tb = mb.DefineType("MyClass", TypeAttributes.Public);
            //动态创建字段  
            FieldBuilder fb = tb.DefineField("myField", typeof(System.String), FieldAttributes.Private);
            //动态创建构造函数  
            Type[] clorType = new Type[] { typeof(System.String) };
            ConstructorBuilder cb1 = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, clorType);
            //生成指令  
            ILGenerator ilg = cb1.GetILGenerator();//生成 Microsoft 中间语言 (MSIL) 指令  
            ilg.Emit(OpCodes.Ldarg_0);
            ilg.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
            ilg.Emit(OpCodes.Ldarg_0);
            ilg.Emit(OpCodes.Ldarg_1);
            ilg.Emit(OpCodes.Stfld, fb);
            ilg.Emit(OpCodes.Ret);
            //动态创建属性  
            PropertyBuilder pb = tb.DefineProperty("MyProperty", PropertyAttributes.HasDefault, typeof(string), null);
            //动态创建方法  
            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName;
            MethodBuilder myMethod = tb.DefineMethod("get_Field", getSetAttr, typeof(string), Type.EmptyTypes);
            //生成指令  
            ILGenerator numberGetIL = myMethod.GetILGenerator();
            numberGetIL.Emit(OpCodes.Ldarg_0);
            numberGetIL.Emit(OpCodes.Ldfld, fb);
            numberGetIL.Emit(OpCodes.Ret);
            //使用动态类创建类型  
            Type classType = tb.CreateType();
            //保存动态创建的程序集 (程序集将保存在程序目录下调试时就在Debug下)  
            dynamicAssembly.Save(DemoName.Name + ".dll");  
            //创建类  
            return classType;
        }
        public class CodeGenerator
        {
            AssemblyBuilder myAssemblyBuilder;
            public CodeGenerator()
            {
                // Get the current application domain for the current thread.
                AppDomain myCurrentDomain = AppDomain.CurrentDomain;
                AssemblyName myAssemblyName = new AssemblyName();
                myAssemblyName.Name = "TempAssembly";

                // Define a dynamic assembly in the current application domain.
                myAssemblyBuilder = myCurrentDomain.DefineDynamicAssembly
                               (myAssemblyName, AssemblyBuilderAccess.Run);

                // Define a dynamic module in this assembly.
                ModuleBuilder myModuleBuilder = myAssemblyBuilder.
                                                DefineDynamicModule("TempModule");

                // Define a runtime class with specified name and attributes.
                TypeBuilder myTypeBuilder = myModuleBuilder.DefineType
                                                 ("TempClass", TypeAttributes.Public);

                // Add 'Greeting' field to the class, with the specified attribute and type.
                FieldBuilder greetingField = myTypeBuilder.DefineField("Greeting",
                                                                      typeof(String), FieldAttributes.Public);
                Type[] myMethodArgs = { typeof(String) };

                // Add 'MyMethod' method to the class, with the specified attribute and signature.
                MethodBuilder myMethod = myTypeBuilder.DefineMethod("MyMethod",
                   MethodAttributes.Public, CallingConventions.Standard, null, myMethodArgs);

                ILGenerator methodIL = myMethod.GetILGenerator();
                methodIL.EmitWriteLine("In the method...");
                methodIL.Emit(OpCodes.Ldarg_0);
                methodIL.Emit(OpCodes.Ldarg_1);
                methodIL.Emit(OpCodes.Stfld, greetingField);
                methodIL.Emit(OpCodes.Ret);
                myTypeBuilder.CreateType();
            }
            public AssemblyBuilder MyAssembly
            {
                get
                {
                    return this.myAssemblyBuilder;
                }
            }
        }
        public void COdedongtai() {
            CodeGenerator myCodeGenerator = new CodeGenerator();
            // Get the assembly builder for 'myCodeGenerator' object.
            AssemblyBuilder myAssemblyBuilder = myCodeGenerator.MyAssembly;
            // Get the module builder for the above assembly builder object .
            ModuleBuilder myModuleBuilder = myAssemblyBuilder.
                                                                 GetDynamicModule("TempModule");
           MessageBox.Show("The fully qualified name and path to this "
                                     + "module is :" + myModuleBuilder.FullyQualifiedName);
            Type myType = myModuleBuilder.GetType("TempClass");
            MethodInfo myMethodInfo =
                                                      myType.GetMethod("MyMethod");
            // Get the token used to identify the method within this module.
            MethodToken myMethodToken =
                              myModuleBuilder.GetMethodToken(myMethodInfo);
            MessageBox.Show(String.Format("Token used to identify the method of 'myType'"
                          + " within the module is {0:x}", myMethodToken.Token));
            object[] args = { "Hello." };
            object myObject = Activator.CreateInstance(myType, null, null);
            myMethodInfo.Invoke(myObject, args);
        }
        public void codeDongtai2() {
            AssemblyName aName = new AssemblyName("DynamicAssemblyExample");            
            AssemblyBuilder ab =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    aName,
                    AssemblyBuilderAccess.RunAndSave);
        

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            ModuleBuilder mb =
                ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");
            //利用AssemblyBuilder创建ModuleBuilder
            ModuleBuilder newModule = ab.DefineDynamicModule("SayHello");
            //创建一个公共类MySayHello
            //TypeBuilder myType = newModule.DefineType("MySayHello", TypeAttributes.Public | TypeAttributes.Class);

           
            //TypeBuilder tb = mb.DefineType(
            //    "MyDynamicType",
            //     TypeAttributes.Public);

            TypeBuilder tb = mb.DefineType(
                "MyDynamicType",
                 TypeAttributes.Public);
            // Add a private field of type int (Int32).
            FieldBuilder fbNumber = tb.DefineField(
                "m_number",
                typeof(int),
                FieldAttributes.Private);

            // Define a constructor that takes an integer argument and 
            // stores it in the private field. 
            Type[] parameterTypes = { typeof(int) };
            ConstructorBuilder ctor1 = tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                parameterTypes);

            ILGenerator ctor1IL = ctor1.GetILGenerator();
            // For a constructor, argument zero is a reference to the new
            // instance. Push it on the stack before calling the base
            // class constructor. Specify the default constructor of the 
            // base class (System.Object) by passing an empty array of 
            // types (Type.EmptyTypes) to GetConstructor.
            ctor1IL.Emit(OpCodes.Ldarg_0);
            ctor1IL.Emit(OpCodes.Call,
                typeof(object).GetConstructor(Type.EmptyTypes));
            // Push the instance on the stack before pushing the argument
            // that is to be assigned to the private field m_number.
            ctor1IL.Emit(OpCodes.Ldarg_0);
            ctor1IL.Emit(OpCodes.Ldarg_1);
            ctor1IL.Emit(OpCodes.Stfld, fbNumber);
            ctor1IL.Emit(OpCodes.Ret);

            // Define a default constructor that supplies a default value
            // for the private field. For parameter types, pass the empty
            // array of types or pass null.
            ConstructorBuilder ctor0 = tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                Type.EmptyTypes);

            ILGenerator ctor0IL = ctor0.GetILGenerator();
            // For a constructor, argument zero is a reference to the new
            // instance. Push it on the stack before pushing the default
            // value on the stack, then call constructor ctor1.
            ctor0IL.Emit(OpCodes.Ldarg_0);
            ctor0IL.Emit(OpCodes.Ldc_I4_S, 42);
            ctor0IL.Emit(OpCodes.Call, ctor1);
            ctor0IL.Emit(OpCodes.Ret);

            // Define a property named Number that gets and sets the private 
            // field.
            //
            // The last argument of DefineProperty is null, because the
            // property has no parameters. (If you don't specify null, you must
            // specify an array of Type objects. For a parameterless property,
            // use the built-in array with no elements: Type.EmptyTypes)
            PropertyBuilder pbNumber = tb.DefineProperty(
                "Number",
                PropertyAttributes.HasDefault,
                typeof(int),
                null);

            // The property "set" and property "get" methods require a special
            // set of attributes.
            MethodAttributes getSetAttr = MethodAttributes.Public |
                MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            // Define the "get" accessor method for Number. The method returns
            // an integer and has no arguments. (Note that null could be 
            // used instead of Types.EmptyTypes)
            MethodBuilder mbNumberGetAccessor = tb.DefineMethod(
                "get_Number",
                getSetAttr,
                typeof(int),
                Type.EmptyTypes);

            ILGenerator numberGetIL = mbNumberGetAccessor.GetILGenerator();
            // For an instance property, argument zero is the instance. Load the 
            // instance, then load the private field and return, leaving the
            // field value on the stack.
            numberGetIL.Emit(OpCodes.Ldarg_0);
            numberGetIL.Emit(OpCodes.Ldfld, fbNumber);
            numberGetIL.Emit(OpCodes.Ret);

            // Define the "set" accessor method for Number, which has no return
            // type and takes one argument of type int (Int32).
            MethodBuilder mbNumberSetAccessor = tb.DefineMethod(
                "set_Number",
                getSetAttr,
                null,
                new Type[] { typeof(int) });

            ILGenerator numberSetIL = mbNumberSetAccessor.GetILGenerator();
            // Load the instance and then the numeric argument, then store the
            // argument in the field.
            numberSetIL.Emit(OpCodes.Ldarg_0);
            numberSetIL.Emit(OpCodes.Ldarg_1);
            numberSetIL.Emit(OpCodes.Stfld, fbNumber);
            numberSetIL.Emit(OpCodes.Ret);

            // Last, map the "get" and "set" accessor methods to the 
            // PropertyBuilder. The property is now complete. 
            pbNumber.SetGetMethod(mbNumberGetAccessor);
            pbNumber.SetSetMethod(mbNumberSetAccessor);

            // Define a method that accepts an integer argument and returns
            // the product of that integer and the private field m_number. This
            // time, the array of parameter types is created on the fly.
            MethodBuilder meth = tb.DefineMethod(
                "MyMethod",
                MethodAttributes.Public,
                typeof(int),
                new Type[] { typeof(int) });

            ILGenerator methIL = meth.GetILGenerator();
            // To retrieve the private instance field, load the instance it
            // belongs to (argument zero). After loading the field, load the 
            // argument one and then multiply. Return from the method with 
            // the return value (the product of the two numbers) on the 
            // execution stack.
            methIL.Emit(OpCodes.Ldarg_0);
            methIL.Emit(OpCodes.Ldfld, fbNumber);
            methIL.Emit(OpCodes.Ldarg_1);
            methIL.Emit(OpCodes.Mul);
            methIL.Emit(OpCodes.Ret);

            // Finish the type.
            Type t = tb.CreateType();

            // The following line saves the single-module assembly. This
            // requires AssemblyBuilderAccess to include Save. You can now
            // type "ildasm MyDynamicAsm.dll" at the command prompt, and 
            // examine the assembly. You can also write a program that has
            // a reference to the assembly, and use the MyDynamicType type.
            // 
            ab.Save(aName.Name + ".dll");

            // Because AssemblyBuilderAccess includes Run, the code can be
            // executed immediately. Start by getting reflection objects for
            // the method and the property.
            MethodInfo mi = t.GetMethod("MyMethod");
            PropertyInfo pi = t.GetProperty("Number");

            // Create an instance of MyDynamicType using the default 
            // constructor. 
            object o1 = Activator.CreateInstance(t);

            // Display the value of the property, then change it to 127 and 
            // display it again. Use null to indicate that the property
            // has no index.
            Console.WriteLine("o1.Number: {0}", pi.GetValue(o1, null));
            pi.SetValue(o1, 127, null);
            Console.WriteLine("o1.Number: {0}", pi.GetValue(o1, null));

            // Call MyMethod, passing 22, and display the return value, 22
            // times 127. Arguments must be passed as an array, even when
            // there is only one.
            object[] arguments = { 22 };
            Console.WriteLine("o1.MyMethod(22): {0}",
                mi.Invoke(o1, arguments));

            // Create an instance of MyDynamicType using the constructor
            // that specifies m_Number. The constructor is identified by
            // matching the types in the argument array. In this case, 
            // the argument array is created on the fly. Display the 
            // property value.
            object o2 = Activator.CreateInstance(t,
                new object[] { 5280 });
            Console.WriteLine("o2.Number: {0}", pi.GetValue(o2, null));
        
        }
        /// <summary>
        ///ReflectionOfDefineDynamicAssembly 的摘要说明
        /// </summary>
        /// 
        public interface ISayHello
        {
            int SayHello();
        }
        public class ReflectionOfDefineDynamicAssembly
        {
            private ISayHello sayHello = null;
            public ReflectionOfDefineDynamicAssembly()
            {
                //
                //TODO: 在此处添加构造函数逻辑
                //
            }

            private Assembly EmitAssembly(string sMsg)
            {
                //创建程序集名称
                AssemblyName assemblyName = new AssemblyName("SayHelloAssembly");
                //创建新的动态程序集
                AssemblyBuilder newAssembly = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                //利用AssemblyBuilder创建ModuleBuilder
                ModuleBuilder newModule = newAssembly.DefineDynamicModule("SayHello");
                //创建一个公共类MySayHello
                TypeBuilder myType = newModule.DefineType("MySayHello", TypeAttributes.Public | TypeAttributes.Class);
                //使得该类实现ISayHello接口
                myType.AddInterfaceImplementation(typeof(ISayHello));
                //方法的传入参数
                Type[] paramTypes = new Type[0];//不传入参数
                //方法返回的类型
                Type returnType = typeof(int);
                //定义接口中的方法
                MethodBuilder myMethod = myType.DefineMethod("SayHello", MethodAttributes.Public | MethodAttributes.Virtual, returnType, paramTypes);
                //获取ILGenerator
                ILGenerator generator = myMethod.GetILGenerator();
                //编写代码
                generator.EmitWriteLine("Hello " + sMsg);
                //入栈
                generator.Emit(OpCodes.Ldc_I4, 1);
                //返回栈顶元素
                generator.Emit(OpCodes.Ret);
                //获得接口的方法信息
                MethodInfo info = typeof(ISayHello).GetMethod("SayHello");
                //规定方法重载
                myType.DefineMethodOverride(myMethod, info);
                //创建类型
                myType.CreateType();
                return newAssembly;
            }
            //动态创建程序集
            //初始化接口变量
            public void GenerateCode(string sMsg)
            {
                Assembly theAssembly = EmitAssembly(sMsg);
                sayHello = (ISayHello)theAssembly.CreateInstance("MySayHello");
            }

            //设置接口变量 如果不为空则
            public int DoSayHello(string sMsg)
            {
                if (sayHello != null)
                {
                    GenerateCode(sMsg);
                }
                return sayHello.SayHello();
            }
        }
        public void codeDongtai3()
        {
            ReflectionOfDefineDynamicAssembly test = new ReflectionOfDefineDynamicAssembly();
            int i = test.DoSayHello("1987Raymond/果汁分你一半");
            Console.WriteLine(i);
        }
    }

}
