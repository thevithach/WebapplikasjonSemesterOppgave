﻿using System;
namespace WebapplikasjonSemesterOppgave.Models
{
	public class HomeEntity
	{
        //Parametre for HomeController for å definere SVG Ikon, Text og AspAction for Home/Index
        public string SvgPath { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}

