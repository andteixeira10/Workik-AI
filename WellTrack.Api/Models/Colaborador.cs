using System.Text.Json.Serialization;

namespace WellTrack.Api.Models
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;

        // Impede loop de referência
        [JsonIgnore]
        public List<RegistroBemEstar>? Registros { get; set; }
    }
}
