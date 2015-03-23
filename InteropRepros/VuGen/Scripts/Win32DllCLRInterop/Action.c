Action()
{
	char * charptr1 = "charptr";
	//string * strptr1 = "strptr";
	//char char1 = "char1";
	//string str1 = "str1";
	
	ExportStdNoParm();
	
	ExportStdInt1ParamNoCall(1);
	ExportStdInt2Param2NoCall(1, 2);

	ExportNewFunc(charptr1);
	ExportNewFunc2(charptr1, charptr1);
	
	
	ExportStdStr1ParamNoCall(charptr1);
	ExportStdStr1ParamNoCall("mystring1");
	
	ExportStdStr1Param("mystring1");
	
	ExportStdStr2Params("mystring1", "mystring2");
	
	return 0;
}
