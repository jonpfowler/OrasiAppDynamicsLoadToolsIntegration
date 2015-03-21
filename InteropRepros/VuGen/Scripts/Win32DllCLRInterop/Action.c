Action()
{
	ExportStdNoParm();
	
	ExportStdStr1ParamNoCall("mystring1");
	
	ExportStdStr1Param("mystring1");
	
	ExportStdStr2Params("mystring1", "mystring2");
	
	return 0;
}
