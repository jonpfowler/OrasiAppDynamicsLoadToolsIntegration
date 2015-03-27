Action()
{
	double trans_time;
	/* <comments> */
	//Count, Rate/Sec, Average
	lr_start_transaction("MyTransactionName");
	trans_time=lr_get_transaction_duration("MyTransactionName");
	
	lr_log_message("trans_time: %d", trans_time);
	//lr_rendezvous("");

	IncrementCounter("LoadRunner(MyTransactionName)\\Rate/Sec", 1);	
	IncrementCounter("LoadRunner(MyTransactionName)\\Averagex", trans_time);	
	lr_end_transaction("MyTransactionName", LR_AUTO);

	return 0;
}
