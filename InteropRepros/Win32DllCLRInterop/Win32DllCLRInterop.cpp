#define WINVER 0x0501
#define _WIN32_WINNT 0x0501

#include "stdafx.h"
#include < stdio.h >
#include < stdlib.h >
#include < vcclr.h >
#include < string >
#include < iostream >

using namespace System;
using namespace std;
using namespace System::IO;

using namespace ClrTestApp;

extern void CallStdInt1ParamNoCall(int stdInt1);
extern void CallStdInt2ParamsNoCall(int stdInt1, int stdInt2);

extern void CallStdStr1Param(string stdString1);
extern void CallStdStr2Params(string stdString1, string stdString2);

extern void StdStr1Param(string stdString1);
extern void StdStr2Params(string stdString1, string stdString2);

extern void ClrStr1Param(string clrString1);
extern void ClrStr2Params(string clrString1, string clrString2);

void CallStdStr1ParamNoCall(string stdString1);
void StdStr1ParamNoCall(string stdString1);

void CallNewFunc(string clrString1);
void CallNewFunc2(string clrString1, string clrString2);

extern "C"
{
	__declspec(dllexport) void ExportStdNoParm(const char * a, const char * b)
	{
		//const char * a = "abcdefghijklmnopqrstuvwxyz";
		//const char * b = "123456789111111111122222222";
		//CallStdInt2ParamsNoCall(x, "");
		CallStdStr2Params(a, b);
	}

	__declspec(dllexport) void ExportStdInt1ParamNoCall(int stdInt1)
	{
		cout << "ExportStdInt1ParamNoCall begin" << stdInt1 << endl;
		CallStdInt1ParamNoCall(stdInt1);
	}

	__declspec(dllexport) void ExportStdInt2Param2NoCall(int stdInt1, int stdInt2)
	{
		cout << "ExportStdInt2Param2NoCall begin" << stdInt1 << endl;
		cout << "ExportStdInt2Param2NoCall begin" << stdInt2 << endl;
		CallStdInt2ParamsNoCall(stdInt1, stdInt2);
	}

	__declspec(dllexport) void ExportStdStr1ParamNoCall(const char * string1)
	{
		cout << "ExportStdStr1ParamNoCall begin" << string1 << endl;
		CallStdStr1ParamNoCall(string1);
	}

	__declspec(dllexport) void ExportStdStr1Param(const char * string1)
	{
		CallStdStr1Param(string1);
	}

	__declspec(dllexport) void ExportStdStr2Params(const char * string1, const char * string2)
	{
		cout << "ExportStdStr2Params begin" << endl;
		cout << "ExportStdStr2Params string1" << string1 << endl;
		cout << "ExportStdStr2Params string2" << string2 << endl;
		CallStdStr2Params(string1, string2);
	}

	__declspec(dllexport) void ExportNewFunc(const char * string1)
	{
		CallNewFunc(string1);
	}

	__declspec(dllexport) void ExportNewFunc2(const char * string1, const char * string2)
	{
		CallNewFunc2(string1, string2);
	}
}

void CallStdStr1ParamNoCall(string stdString1)
{
	StdStr1ParamNoCall(stdString1); // call to the function written in C#
}

void CallStdInt1ParamNoCall(int int1){
	//StdInt1Param(stdInt1); // call to the function written in C#
}

void CallStdInt2ParamsNoCall(int stdInt1, int stdInt2)
{

}

void CallStdStr1Param(string stdString1){
	StdStr1Param(stdString1); // call to the function written in C#
}

void CallStdStr2Params(string stdString1, string stdString2){
	StdStr2Params(stdString1, stdString2);
}

void StdStr1ParamNoCall(string stdString1)
{
	cout << "StdStr1Param begin" << endl;
	cout << stdString1 << endl;

	ClrStr1Param(stdString1);

	cout << "StdStr1Param end" << endl;
	return;
}

void StdStr1Param(string stdString1)
{
	cout << "StdStr1Param begin" << endl;
	cout << stdString1 << endl;

	String^ clrString1 = gcnew String(stdString1.c_str());

	ClrStr1Param(stdString1);
	cout << "StdStr1Param end" << endl;
	return;
}

void StdStr2Params(string stdString1, string stdString2)
{
	cout << "StdStr2Params begin" << endl;
	cout << stdString1 << endl;
	cout << stdString2 << endl;

	ClrStr2Params(stdString1, stdString2);
	cout << "StdStr2Params end" << endl;
	return;
}

void ClrStr1Param(string stdString1)
{
	Console::WriteLine("ClrStr1Param begin");

	String^ clrString1 = gcnew String(stdString1.c_str());

	File::WriteAllText("Win32DllCLRInterop.log", String::Format("StdStr1ParamNoCall, clrString1: {0}", clrString1));

	Class1::ClrStr1Param(clrString1);

	Console::WriteLine("ClrStr1Param end");
}

void ClrStr2Params(string stdString1, string stdString2)
{
	Console::WriteLine("ClrStr2Params begin");

	String^ clrString1 = gcnew String(stdString1.c_str());
	String^ clrString2 = gcnew String(stdString2.c_str());

	File::WriteAllText("Win32DllCLRInterop.log", String::Format("ClrStr2Params, clrString1: {0}, clrString2: {1}", clrString1, clrString2));

	Class1::ClrStr2Params(clrString1, clrString2);

	Console::WriteLine("ClrStr2Params end");
}

void CallNewFunc(string string1)
{
	String^ clrString1 = gcnew String(string1.c_str());

	File::WriteAllText("Win32DllCLRInterop.log", String::Format("CallNewFunc, clrString1: {0}", clrString1));
}

void CallNewFunc2(string string1, string string2)
{
	String^ clrString1 = gcnew String(string1.c_str());
	String^ clrString2 = gcnew String(string2.c_str());

	File::WriteAllText("Win32DllCLRInterop.log", String::Format("CallNewFunc2, clrString1: {0}, clrString2: {1}", clrString1, clrString2));
}
