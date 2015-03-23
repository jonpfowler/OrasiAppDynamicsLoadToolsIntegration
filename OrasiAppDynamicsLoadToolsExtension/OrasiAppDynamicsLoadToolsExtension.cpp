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
using namespace OrasiPerformanceCounterUtility;

extern void CallIncrementCounter(string);
extern void CallResetCounter(string);
extern void CallDeleteCounterCategory(string);

extern "C"
{
	__declspec(dllexport) void IncrementCounter(const char * counterPath)
	{
		CallIncrementCounter(counterPath);
	}

	__declspec(dllexport) void ResetCounter(const char * counterPath)
	{
		CallResetCounter(counterPath);
	}

	__declspec(dllexport) void DeleteCounterCategory(const char * counterPath)
	{
		CallDeleteCounterCategory(counterPath);
	}
}

void CallIncrementCounter(string counterPath)
{
	String^ clrCounterPath = gcnew String(counterPath.c_str());

	Counter::IncrementCounter(clrCounterPath, 1); // call to the function written in C#
}

void CallResetCounter(string counterPath)
{
	String^ clrCounterPath = gcnew String(counterPath.c_str());

	Counter::ResetCounter(clrCounterPath, 0); // call to the function written in C#
}

void CallDeleteCounterCategory(string counterPath){
	String^ clrCounterPath = gcnew String(counterPath.c_str());

	Counter::DeleteCounterCategory(clrCounterPath); // call to the function written in C#
}
