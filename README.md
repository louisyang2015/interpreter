# Interpreter
This interpreter supports a subset of Python.

## Features
* string
* if, while, for
* functions
* lists and dictionaries

## [Video Intro (2 minutes)](https://youtu.be/672V6V_Q-bA)

## Running the Project
This project is using .Net Core 2.1+

Copy the test scripts and examples:
* check_output.py
* extension_examples.py
* script.py
* test_script.py

from "/test" to the build output directory

** Follow the [intro video (2 minutes)](https://youtu.be/672V6V_Q-bA) **

**dotnet Interpreter.dll test**

This is the command used in the intro video. It runs "test_script.py" through the interpreter.

**dotnet Interpreter.dll**

This is the default mode, which runs "script.py" through the interpreter, with a good deal of logging. The major intermediate stages of the interpreter are printed out: tokenization, intermediate code, and program statements. This is a good way to learn how the interpreter works.

**dotnet Interpreter.dll extension**

This runs the "extension_examples.py", which is an example of interpreted code interacting with compiled code. For more information, see:
* the documentation (/doc/interpreter.docx), section "Extension Examples" near the end
* the code inside Program.cs

## Understanding the Code

**Program.cs :: Program :: debug()**
* This is the default mode of the demo program. It prints out intermediate stages of the interpreter and is a good way to learn how the interpreter works.
* Try something easy in "script.py", like:
```
x = 2
x += 1 + 2 * 3
print(x) # 9
```

**The documentation at /doc/interpreter.docx**
* Design - this gives a high level view of how interpreter features work
* Implementation - provides more code level detail about the interpreter features
* Extension Examples - how to interact with compiled code
    
