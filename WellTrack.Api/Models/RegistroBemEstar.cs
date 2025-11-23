namespace WellTrack.Api.Models
{
public class RegistroBemEstar
{
public int Id { get; set; }
public DateTime Data { get; set; }
public int Nota { get; set; } // de 1 a 5
public string Comentario { get; set; } = string.Empty;
public int ColaboradorId { get; set; }
public Colaborador? Colaborador { get; set; }
}
}
