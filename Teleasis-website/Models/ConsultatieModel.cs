namespace Teleasis_website.Models
{
    public class ConsultatieModel
    {
        public string? id_consultatie { get; set; }
        public string motiv_prezentare_consultatie { get; set; }
        public string data_consultatie { get; set; }
        public string simptome_consultatie { get; set; }
        public string diagnostic_consultatie { get; set; }
        public string trimitere_consultatie { get; set; }


        public string  retete_generate_consultatie{ get; set; }
        //public class Retete_generate_consultatie
        //{
        //    public string id_reteta { get; set; }
        //    public string substanta { get; set; }
        //    public string durata { get; set; }
        //    public string select_saptamana { get; set; }
        //    public string cantitate { get; set; }
        //    public string select_dimineata { get; set; }

        //}

       // public string? retete_de_afisat { get; set; }
        public string tratament_consultatie { get; set; }
        public string schema_consultatie { get; set; }
    
    }
}
