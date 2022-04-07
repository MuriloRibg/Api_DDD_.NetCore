using System;
namespace Api.Domain.Security
{
    public class TokenConfigurations
    {
        public string Audience { get; set; } //Publico
        public string Issuer { get; set; }  //Emissor
        public Double Seconds { get; set; } //Segundos        
    }
}
