vuser_init()
{
	int retval = 0;
	retval = lr_load_dll("C:\\sources\\github\\OrasiAppDynamicsLoadToolsIntegration\\Release\\OrasiAppDynamicsLoadToolsExtension.dll");
    IncrementCounter("LoadRunner(VUsers)\\Count", 1);
	return 0;
}
