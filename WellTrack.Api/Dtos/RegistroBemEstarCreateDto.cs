namespace WellTrack.Api.Dtos
{
    public class RegistroBemEstarCreateDto
    {
        public DateTime Data { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public int ColaboradorId { get; set; }
    }
}
