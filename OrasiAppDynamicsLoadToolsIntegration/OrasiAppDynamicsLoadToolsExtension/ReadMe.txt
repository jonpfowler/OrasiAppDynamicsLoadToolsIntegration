===================================================================
Orasi AppDynamics LoadRunner Performance Extension Project Overview
===================================================================

Instructions
============

- Go to the LoadRunner machine running the scripts (VuGen for testing, a Load Generator for run time)
- Install the Orasi AppDynamics LoadRunner Performance Extension to the appropriate location
- Add the code to your LoadRunner Vugen scripts in the format of LoadRunner(MyTransactionName)
- Run your script once in Vugen
- In perfmon.msc, add the LoadRunner(MyTransactionName)\Rate/Sec and Count performance counters to verify counters are working
- Install the AppDynamics .Net Machine Agent (dotNetAgentSetup64.msi)
- Run the Agent Wizard to set the AppDynamics Controller name
- Modify %ProgramData%\AppDynamics\DotNetAgent\Config\config.xml to upload the appropriate counters to the AppDynamics Controller
- In services.msc, restart the AppDynamics.Agent.Coordinator service
- Run the scripts with the AppDynamics Agent running
- Repeat these instructions for each Load Generator
- View the results in AppDynamics Metric Browser
- Create an AppDynamics custom dashboard


Orasi Performance Utility Installation Instructions
===================================================

- Copy OrasiAppDynamicsLoadRunnerPerformanceExtension.dll to the LoadRunner\bin directory
  - Examples:
    - VuGen
      - xcopy OrasiAppDynamicsLoadRunnerPerformanceExtension.dll "C:\Program Files (x86)\HP\LoadRunner\bin\"
    - Load Generator
      - xcopy OrasiAppDynamicsLoadRunnerPerformanceExtension.dll "C:\Program Files (x86)\HP\Load Generator\bin\"
- Copy OrasiPerformanceCounterUtility.dll to the LoadRunner\bin\OrasiPerformanceCounterUtility directory
  - Examples:
    - VuGen
      - xcopy OrasiPerformanceCounterUtility.dll "C:\Program Files (x86)\HP\LoadRunner\bin\OrasiPerformanceCounterUtility\"
    - Load Generator
      - xcopy OrasiPerformanceCounterUtility.dll "C:\Program Files (x86)\HP\Load Generator\bin\OrasiPerformanceCounterUtility\"

Example LoadRunner VuGen code
=============================

vuser_init()
{
	int retval = 0;
	retval = lr_load_dll("OrasiAppDynamicsLoadRunnerPerformanceExtension.dll");  //Has to be in LoadRunner\bin root
	return 0;
}

Action()
{
	lr_start_transaction("MyTransactionName");

	web_service_call( "StepName=MyTransactionName",
	    ...
		LAST);	
	
	lr_end_transaction("MyTransactionName", LR_AUTO);
	
	//Pass in the Category and instance. Count and Rate/Sec is then recorded in Perfmon
	IncrementCounter("LoadRunner(MyTransactionName)");

	return 0;
}

AppDynamics Agent Config.xml
============================

notepad %ProgramData%\AppDynamics\DotNetAgent\Config\config.xml

  <machine-agent>
    <perf-counters>
      <perf-counter cat="LoadRunner" name="Rate/Sec" instance="MyTransactionName" />
      <perf-counter cat="LoadRunner" name="Count"    instance="MyTransactionName" />
    </perf-counters>
  </machine-agent>

AppDynamics Metric Browser
==========================

- Analyze
- Metric Browser
- Select a Metric:
  - Application Infrastructure Performance
  - Machine Agent
  - Individual Nodes
  - <My Node Name>
  - Custom Metrics
  - Performance Monitor
  - LoadRunner
  - Rate/Sec
  - <MyTransactionName>
- Double-click to add to graph

AppDynamics Custom Dashboard
============================

- Custom Dashboards
- Create Dashboard
- Add Metric Data Graph
- Add Data Series
- Select Application:
  - <Application Name>
- Select a Metric Category:
  - Custom (use any metrics)
- Select a Metric:
  - Application Infrastructure Performance
  - Machine Agent
  - Individual Nodes
  - <My Node Name>
  - Custom Metrics
  - Performance Monitor
  - LoadRunner
  - Rate/Sec
  - <MyTransactionName>

Dependencies
============
- Visual C++ Redistributable Packages for Visual Studio 2013
  - http://download.microsoft.com/download/2/E/6/2E61CFA4-993B-4DD4-91DA-3737CD5CD6E3/vcredist_x64.exe

Troubleshooting
===============
- Make sure you get it to work in a single execution of Vugen before deploying to Load Generators
- Double check the directories in the Installation Instructions
- You can use the Windows Dependency Walker (http://www.dependencywalker.com/) to check if any dependent DLL’s are not available.
  - It is common to see GPSVC.DLL, DBGHELP.DLL, and SHDOCVW.DLL listed as missing dependencies in Windows Dependency Walker
- If you are getting an error from the Controller only, make sure that you copy the DLL to the same directory of all the Load Generator machines.
- If you are loading the DLL from the script’s directory, make sure that the DLL is listed in the Controller’s Design View -> Details -> More -> Files.
- If you are still having problems, try to put your DLL into a path that is specified in the PATH environment variable of the machine.

Testing
=======
- Orasi AppDynamics LoadRunner Performance Extension has been verified to work on the following versions of HP LoadRunner
  - 11.50
  - 