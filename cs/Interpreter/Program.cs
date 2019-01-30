using System;
using System.IO;
using System.Text;
using Interpreter;
using Interpreter.__;
using ValueType = Interpreter.__.ValueType;


namespace InterpreterDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.OutputEncoding = Encoding.UTF8;

      if (args.Length == 1)
      {
        if (args[0].Equals("debug"))
          debug();
        else if (args[0].Equals("test"))
          test();
        else if (args[0].Equals("extension"))
          extension_examples();
      }
      else
      {
        debug();
        // test();
        // extension_examples();
      }

      Console.Read();
    }

    static void debug()
    {
      var log_settings = new Script.LogSettings();
      log_settings.print_tokens = true;
      log_settings.print_intermediate_code = true;
      log_settings.print_statements = true;

      var script = Script.load_from_file("script.py", log_settings);
      
      var run_time = new RunTime(script);

      Console.WriteLine("\n\n");
      
      bool end = false;
      while (end == false)
      {
        // Console.WriteLine(run_time.ToString() + "\n");
        end = run_time.run(1);
      }
    }

    static void test()
    {
      var script = Script.load_from_file("test_script.py");

      using (var file = new StreamWriter(File.Open("output.txt", FileMode.Create)))
      {
        var run_time = new RunTime(script, file);
        run_time.run();
      }
    }

    static void extension_examples()
    {
      new ExtensionExample();
    }   

  }



  class ExtensionExample
  {
    public ExtensionExample()
    {
      var log_settings = new Script.LogSettings();
      log_settings.print_tokens = true;
      log_settings.print_intermediate_code = true;
      log_settings.print_statements = true;

      var script = Script.load_from_file("extension_examples.py", log_settings);
      
      var run_time = new RunTime(script);

      run_time.add_function("user_add", user_add);
      run_time.add_function("UserSum", UserSum);

      // ---------------------------------------------
      // Variable Initialization      
      run_time.symbol_table.store("a", new DynamicValue(1));
      run_time.symbol_table.store("b", new DynamicValue(2));
      
      Console.WriteLine("\n\n");
      run_time.run();
    }

    /// <summary>
    /// User defined function example - add two numbers
    /// </summary>
    Value user_add(FunctionArguments arguments, SymbolTable symbol_table)
    {
      arguments.check_num_args(2);
      var val1 = arguments.get_double_argument(0, null, null, symbol_table);
      var val2 = arguments.get_double_argument(1, null, null, symbol_table);

      return new DynamicValue(val1 + val2);
    }


    /// <summary>
    /// Object based programming example - Python side constructor.
    /// </summary>
    Value UserSum(FunctionArguments arguments, SymbolTable symbol_table)
    {
      arguments.check_num_args(1);
      var start = arguments.get_double_argument(0, null, null, symbol_table);
      
      return new UserSum(start);
    }
  }



  class UserSum : Value
  {
    double sum = 0;


    public UserSum(double start)
    {
      sum = start;
    }
    
    public Value call(string function_name, FunctionArguments arguments, SymbolTable symbol_table)
    {
      if (function_name.Equals("add"))
      {
        arguments.check_num_args(1);
        var val1 = arguments.get_double_argument(0, null, null, symbol_table);
        sum += val1;

        return NoneValue.NONE;
      }
      else if (function_name.Equals("get"))
      {
        arguments.check_num_args(0);
        return new DynamicValue(sum);
      }

      return null;
    }


    
    public ValueType Type { get { return ValueType.UserDefined; } }
    
    public bool Equals(Value other)
    {
      // This is needed to be able to use the class in a list.
      return false;
    }

    public Value operate(OperatorType op_type, Value val2)
    {
      // This is needed to be able to use the class in a list.
      return null;
    }

    public override int GetHashCode()
    {
      // This is needed to be able to use the class as a dictionary key.
      return base.GetHashCode();
    }
  }

}
