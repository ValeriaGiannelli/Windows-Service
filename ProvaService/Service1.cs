﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProvaService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); //usa system.Timers
        public Service1()
        {
            InitializeComponent();
        }


        //****************START SERVIZIO*************//
        protected override void OnStart(string[] args)
        {

            //richiama il metodo per scrivere nel file che il servizio è iniziato all'ora X
            WriteToFile($"Servizio iniziato alle {DateTime.Now}");

            //ogni volta che l'evento viene generato fa il metodo OnElapsedTime che stampa qualcosa
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 5000; //dopo quanto fa ripartire l'evento
            timer.Enabled = true;
        }

        //METODO SCRITTURA FILE (Scrive nel file il messaggio che viene passato)
        private void WriteToFile(string message)
        {
            //crea il percorso prendendo il dominio e la cd
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\Logs";

            //controlla che il percorso esista-> se non esiste lo crea.
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //specifico il percorso del file - esempio output: C:\MyApp\Logs\ServiceLog_14_01_2025.txt
            string filepath = path + @"\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            //se non esiste crea il file dove scrivere
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                }
            } else //se esiste aggiunge  il messaggio al file
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
        }

        //METODO AD OGNI CICLO (richiama funzione di scrittura nel file)
        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            WriteToFile("Il servizio è stato richiamato alle " + DateTime.Now);
        }


        //*****************STOP SERVIZIO******************//
        protected override void OnStop()
        {
            WriteToFile("il servizio si è fermato alle: " +  DateTime.Now);
        }
    }
}