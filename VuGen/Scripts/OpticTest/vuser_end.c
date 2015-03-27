vuser_end()
{
    IncrementCounter("LoadRunner(VUsers)\\Count", -1);	
	return 0;
}
