Action()
{
	const char * charptr1 = "charptr1";
	const char * charptr2 = "charptr2";
	//string * strptr1 = "strptr";
	//char char1 = "char1";
	//string str1 = "str1";
	
	ExportStdNoParm(charptr1, charptr2);
	ExportStdNoParm("asdfaasdf asdfasdf asdfasfasdfasdfasdf", "!@341234 1241 2341234 12341 2341234`123412 341234");
	
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
