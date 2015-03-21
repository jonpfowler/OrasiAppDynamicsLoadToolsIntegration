// Win32ConsoleCLRInterop.cpp : Defines the entry point for the console application.
//

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
using namespace ClrTestApp;

extern void test1();
extern void StdStr1Param(string counterPath);
extern void StdStr2Params(string stdString1, string stdString2);
extern void ClrStr1Param(String^ clrString1);
extern void ClrStr2Params(String^ clrString1, String^ clrString2);


int _tmain(int argc, _TCHAR* argv[])
{
	test1();
	return 0;
}

void test1()
{
	StdStr1Param("string1");
	StdStr2Params("string1", "string2");
	return;
}

void StdStr1Param(string stdString1)
{
	cout << "StdStr1Param begin" << endl;
	cout << stdString1 << endl;

	String^ clrString1 = gcnew String(stdString1.c_str());
	ClrStr1Param(clrString1);
	cout << "StdStr1Param end" << endl;
	return;
}

void StdStr2Params(string stdString1, string stdString2)
{
	cout << "StdStr2Params begin" << endl;
	cout << stdString1 << endl;
	cout << stdString2 << endl;

	String^ clrString1 = gcnew String(stdString1.c_str());
	String^ clrString2 = gcnew String(stdString2.c_str());

	ClrStr2Params(clrString1, clrString1);
	cout << "StdStr2Params end" << endl;
	return;
}

void ClrStr1Param(String^ clrString1)
{
	Console::WriteLine("ClrStr1Param begin");
	Console::WriteLine(clrString1);

	Class1::ClrStr1Param(clrString1);

	Console::WriteLine("ClrStr1Param end");
}

void ClrStr2Params(String^ clrString1, String^ clrString2)
{
	Console::WriteLine("ClrStr2Params begin");
	Console::WriteLine("String1: {0}", clrString1);
	Console::WriteLine("String2: {0}", clrString2);

	Class1::ClrStr2Params(clrString1, clrString1);

	Console::WriteLine("ClrStr2Params end");
}