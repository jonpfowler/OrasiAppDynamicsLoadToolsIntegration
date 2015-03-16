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

extern void CallIncrementCounter(String^);
extern void CallResetCounter(String^);
extern void CallDeleteCounterCategory(String^);

extern "C"
{
	__declspec(dllexport) void IncrementCounter(string counterPath)
	{
		String^ clrCounterPath = gcnew String(counterPath.c_str());

		CallIncrementCounter(clrCounterPath);
	}

	__declspec(dllexport) void ResetCounter(string counterPath)
	{
		String^ clrCounterPath = gcnew String(counterPath.c_str());

		CallResetCounter(clrCounterPath);
	}

	__declspec(dllexport) void DeleteCounterCategory(string counterPath)
	{
		String^ clrCounterPath = gcnew String(counterPath.c_str());

		CallDeleteCounterCategory(clrCounterPath);
	}

}

void CallIncrementCounter(String^ counterPath){
	Counter::IncrementCounter(counterPath, 1); // call to the function written in C#
}

void CallResetCounter(String^ counterPath){
	Counter::ResetCounter(counterPath, 0); // call to the function written in C#
}

void CallDeleteCounterCategory(String^ counterPath){
	Counter::DeleteCounterCategory(counterPath); // call to the function written in C#
}
