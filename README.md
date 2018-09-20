# Tafelmusik

A simple library for the purpose of developing simple command line tools that hybridizes already existing executables with new code.
Conditional/linear runners provide ways of conditionally executing specific paths of execution. For example if a tool needs an option to simply
install an executable that can be provided but if the application does not also provide a way of providing a clean install (uninstalling any unessecary files)
writing some extra code may be needed to provide this functionality. Using tafelmusik in this case allows the installation, cleanup and clean install to all
be wired as command line options. 

## Getting started
You will need .NET 4.7 in order to use this properly. Afterwards clone the environment, build and use the dll in your project or import the files directly into your project. The necessary files are:
* Applet
* IApp
* Lambda
* LinearRunner
* NaryConditionalRunner

## Running the tests

Note that if you modify these files there is a testing library included in this project. The test process is used to test the library's response to Win32 and System exceptions and should be included
if you write any new tests to maker sure they function properly. Using the tests is pretty straight forward if the library is cloned, simply build and run the tests using Visual Studio test manager.

## License: 
Copyright 2018 Ryxai

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
