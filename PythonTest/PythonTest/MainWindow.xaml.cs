using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace PythonTest
{
	class Program
	{
		static void Main(string[] args)
		{
			// 加载外部 python 脚本文件.
			ScriptRuntime pyRumTime = Python.CreateRuntime();
			dynamic obj = pyRumTime.UseFile("hello.py");


			// ==================================================
			// 简单调用脚本文件中的方法.
			Console.WriteLine(obj.welcome("Test C# Call Python."));
			Console.WriteLine(obj.welcome("测试中文看看是否正常！"));



			// ==================================================
			// 测试自定义对象.
			TestDataObject testObj = new TestDataObject()
			{
				UserName = "张三",
				Age = 20,
				Desc = "",
			};
			Console.WriteLine("调用脚本前对象数据：{0}", testObj);
			obj.testAddAge(testObj);
			Console.WriteLine("调用 testAddAge 脚本后，对象数据={0}", testObj);

			obj.testAddAge2(testObj);
			Console.WriteLine("调用 testAddAge2 脚本后，对象数据={0}", testObj);




			// ==================================================
			// 测试 List.
			IronPython.Runtime.List testList = new IronPython.Runtime.List();
			testList.Add("List数据1");
			testList.Add("List数据2");
			testList.Add("List数据3");
			// 测试参数为 List.
			string result = obj.testList(testList);
			Console.WriteLine("调用 testList ， 返回结果：{0}", result);



			// ==================================================
			// 测试 Set.
			IronPython.Runtime.SetCollection testSet = new IronPython.Runtime.SetCollection();
			testSet.add("Set数据1");
			testSet.add("Set数据2");
			testSet.add("Set数据3");

			// 测试参数为 Set.
			result = obj.testSet(testSet);
			Console.WriteLine("调用 testSet ， 返回结果：{0}", result);



			// ==================================================
			// 测试 Dictionary.
			IronPython.Runtime.PythonDictionary testDictionary = new IronPython.Runtime.PythonDictionary();
			testDictionary["Key1"] = "Value1";
			testDictionary["Key2"] = "Value2";
			testDictionary["Key3"] = "Value3";
			// 测试参数为 Dictionary.
			result = obj.testDictionary(testDictionary);
			Console.WriteLine("调用 testDictionary ， 返回结果：{0}", result);

			Console.ReadLine();
		}
	}



	/// <summary>
	/// 测试对象.
	/// 
	/// 用于传递数据给 Python 脚本
	/// </summary>
	public class TestDataObject
	{
		/// <summary>
		/// 用户名.
		/// </summary>
		public string UserName { set; get; }

		/// <summary>
		/// 年龄.
		/// </summary>
		public int Age { set; get; }


		/// <summary>
		/// 描述信息.
		/// </summary>
		public string Desc { set; get; }


		public void AddAge(int age)
		{
			this.Age = this.Age + age;
			this.Desc = String.Format("{0}又大了{1}岁 in C#", this.UserName, age);
		}


		public override string ToString()
		{
			return String.Format("姓名：{0}; 年龄：{1}; 描述:{2}", this.UserName, this.Age, this.Desc);
		}

	}
}

