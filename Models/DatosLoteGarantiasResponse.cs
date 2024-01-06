using Newtonsoft.Json;
using System.Globalization;

namespace ProyectGarantia.Models
{
    public class DatosLoteGarantiasResponse
    {
        public int LoteId { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Prestamo { get; set; }

        public string Agencia { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Fecha_otorgamiento { get; set; }
        public string Asesor { get; set; }
        public List<CGarantia> Cgarantia { get; set; }
    }

    public class CGarantia
    {
        public string Prestamo { get; set; }
        public string Cod_aval { get; set; }
        public string Nombre_aval { get; set; }
        public decimal Monto_garantia { get; set; }
        public int TpAval { get; set; }
        public string Descrip_aval { get; set; }
    }
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                string dateStr = (string)reader.Value;
                if (DateTime.TryParseExact(dateStr, "d/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
            }
            throw new JsonReaderException("Could not convert string to DateTime");
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("d/M/yyyy HH:mm:ss"));
        }
    }
}
