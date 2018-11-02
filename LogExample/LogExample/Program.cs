using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verisoft.Logging;

namespace LogExample
{

    public class Program
    {
        Logger logger = null;
        public string logName = string.Empty;

        static void Main (string[] args)
        {
            Program obj = new Program ();
            string xs = "";
            string msg = string.Empty;


            while (true)
            {
                Console.WriteLine ("SEÇİNİZ : \n1- LOG OLUŞTUR \n2- LOG SİL \n3-LOG YAZ");
                xs=Console.ReadLine ();
                try
                {

                    switch (xs)
                    {
                        case "1":
                            Console.WriteLine ("LOGNAME GİR");
                            obj.logName=Console.ReadLine ();
                            obj.CheckEventLog ();


                            break;
                        case "2":
                            Console.WriteLine ("LogName GİR");
                            obj.logName=Console.ReadLine ();
                            obj.Log_Delete (obj.logName);
                            break;
                        #region Log Yazma
                        case "3":
                            Console.WriteLine ("MESAJ GİR");
                            msg=Console.ReadLine ();
                            Console.WriteLine ("TİP GİR");
                            string cas = Console.ReadLine ();
                            Console.WriteLine ("LOGNAME GİR");
                            obj.logName=Console.ReadLine ();
                            switch (cas)
                            {
                                case "1":
                                    obj.Log (msg, EventLogEntryType.Error);
                                    break;
                                case "2":
                                    obj.Log (msg, EventLogEntryType.FailureAudit);

                                    break;
                                case "3":
                                    obj.Log (msg, EventLogEntryType.Information);

                                    break;
                                case "4":
                                    obj.Log (msg, EventLogEntryType.SuccessAudit);

                                    break;
                                case "5":
                                    obj.Log (msg, EventLogEntryType.Warning);
                                    break;
                            }

                            break;

                            #endregion

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine (ex.Message);
                    throw;
                }
            }



            //EventLog.Delete (ProgramName+" Log");

            // if (EventLog.Exists("LLLLL Log") )
            // {
            //     EventLog.Delete ("LLLLL Log");

            // }


        }




        private bool CheckEventLog ()
        {

            //TODO:   LOG OLUŞTURMA 
            try
            {
                if (!EventLog.Exists (logName))
                {
                    EventLog.CreateEventSource (logName, logName);
                    Log ("CREATE NEW LOG", EventLogEntryType.SuccessAudit);
                    return true;
                }
                else
                {
                    Log ("LOG HAS ALREADY EXIST", EventLogEntryType.SuccessAudit);

                    return true;
                }
            }
            catch (Exception e1)
            {
                Log ("Cannot check and create event log: "+e1.Message, EventLogEntryType.Error);
                return false;
            }
        }
        private void Log_Delete (string logname)
        {
            string msg = logname+"-->LOG DELETED !";
            try
            {
                EventLog.Delete (logname);

                using (StreamWriter w = File.AppendText (DateTime.Now.ToLongDateString ()+"-"+logName+".txt"))
                {

                    Logtxt (msg, w);
                }

            }
            catch (Exception)
            {
                Console.WriteLine ("FAIL");

            }



        }
        private void Log (string message, EventLogEntryType type)
        {
            //   EventLog.WriteEntry (logName, message, type);
            try
            {
                if (EventLog.Exists (logName))
                {
                    EventLog.WriteEntry (logName, message, type);

                    using (StreamWriter w = File.AppendText (DateTime.Now.ToLongDateString ()+"-"+logName+".txt"))
                    {

                        Logtxt (message, w);
                    }

                }
                else
                {
                    //CheckEventLog ();

                    Console.WriteLine ("LOG YOK");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine ("FAIL");
            }
          

            //using (StreamReader r = File.OpenText (DateTime.Now.ToLongDateString ()+"-"+ProgramName+".txt"))
            //{
            //    DumpLog (r);
            //}


            //using (EventLog eventLog = new EventLog ("Application"))
            //{
            //    eventLog.Source="Application";
            //    eventLog.WriteEntry ("Log message example", EventLogEntryType.Information, 101, 1);
            //}



        }
        public static void Logtxt (string logMessage, TextWriter w)
        {
            try
            {
                w.WriteLine ("{0} {1}", DateTime.Now.ToLongTimeString (),
              DateTime.Now.ToLongDateString ()+" --> "+logMessage);

                w.WriteLine ("-------------------------------");

            }
            catch (Exception)
            {

                Console.WriteLine ("FAIL");
            }
           
        }
        public void Verisoft_Log ()
        {
            Console.WriteLine ("asd");
            logger.Log ("asd yaz", EventLogEntryType.Error, "a");
        }



    }
}
